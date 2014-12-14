using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Drag : IComportement
    {
        public Drag()
        {
        }

        public Drag(Agent agent) 
        {
            agent.MAngryness = Tools.DRAG_ANGRYNESS_INITIAL;
            agent.MFatigue = Tools.DRAG_FATIGUE_INITIAL;
            agent.MLeaderShip = Tools.DRAG_LEADERSHIP_INITIAL;
            agent.MMotivation = Tools.DRAG_MOTIVATION_INITIAL;
            agent.MSimpathy = Tools.DRAG_SYMPATHY_INITIAL;
        }

        public bool Comportement(FrameEvent evt, Random rand, Agent agent)
        {
            // visibilité du cube
            //cube.Visible = bcube;;
            if (agent.MWalkList.Count != 2)
            {
                agent.marcheAleatoire(rand, agent);
            }

            // vitesse de l'agent
            float move = agent.MWalkSpeed * (evt.timeSinceLastFrame);
            // distance à parcourir
            agent.MDistance -= move;

            //distance en ligne droite
            if (agent.MDistance <= 0.0f)
            {   // si on est arrivé
                if (!agent.TurnNextLocation())
                {
                    // on attend
                    agent.MAnimationState = agent.Ent.GetAnimationState("Idle");
                    return true;
                }
            }
            else
            {
                //l'agent bouge
                agent.Node.Translate(agent.MDirection * move);
            }
            //Passe à la frame d'animation suivante
            agent.MAnimationState.AddTime(evt.timeSinceLastFrame * agent.MWalkSpeed / 20);

            return true;

        }

        public void evolve(Agent agent)
        {
            agent.MComportement = new Idler(agent);
            agent.Node.DetachAllObjects();
            agent.Ent = agent.MSceneManager.CreateEntity(Tools.IDLER_MESH);
            agent.Node.AttachObject(agent.Ent);
        }

        public void regress(Agent agent)
        {
        }


        public void negociateWithManager(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < other.MLeaderShip)
            {
                evolve(negociator);
            }
            else if (num < other.MLeaderShip + negociator.MAngryness)
            {
                Tools.updateValue(negociator.MAngryness, Tools.ANGRYNESS_UP);
            }
            else if (num < other.MLeaderShip + negociator.MAngryness + negociator.MMotivation)
            {
                Tools.updateValue(negociator.MMotivation, Tools.MOTIVATION_UP);
            }
        }

        public void negociateWithIdler(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(200);
            if (num < negociator.MMotivation)
            {
                other.MComportement.regress(other);
            }
            else if (num < negociator.MMotivation + other.MSimpathy)
            {
                evolve(negociator);
            }
            else
            {
                Tools.updateValue(other.MFatigue, Tools.FATIGUE_UP);
            }

        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < negociator.MSimpathy + other.MSimpathy)
            {
               evolve(negociator);
               other.MComportement.evolve(other);
            }
            else if (num < negociator.MSimpathy + other.MSimpathy + other.MAngryness)
            {
                Tools.updateValue(negociator.MAngryness, Tools.ANGRYNESS_UP);
            }
        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < other.MSimpathy)
            {
                evolve(negociator);
            }
            else if (num < other.MSimpathy + other.MAngryness + other.MFatigue)
            {
                other.MComportement.regress(other);
            }
            else
            {
                Tools.updateValue(negociator.MMotivation, Tools.MOTIVATION_UP);
            }
        }

    }
}
