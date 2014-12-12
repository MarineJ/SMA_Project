using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Builder : IComportement
    {

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


        public Builder()
        {

        }


        public Builder(Agent agent) 
        {
            agent.MAngryness = Tools.BUILDER_ANGRYNESS_INITIAL;
            agent.MFatigue = Tools.BUILDER_FATIGUE_INITIAL;
            agent.MLeaderShip = Tools.BUILDER_LEADERSHIP_INITIAL;
            agent.MMotivation = Tools.BUILDER_MOTIVATION_INITIAL;
            agent.MSimpathy = Tools.BUILDER_SYMPATHY_INITIAL;
        }

        public void evolve(Agent agent)
        {
            agent.MComportement = new Manager(agent);
        }

        public void regress(Agent agent)
        {
            agent.MComportement = new Idler(agent);
        }

        //
        public void negociateWithManager(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);

            if (num < other.MLeaderShip)
            {
                Tools.updateValue(negociator.MMotivation, Tools.MOTIVATION_UP);
            }
            else if (num < other.MLeaderShip + negociator.MMotivation)
            {
                evolve(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else if(num < other.MLeaderShip + negociator.MMotivation + negociator.MFatigue)
            {
                regress(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_DOWN);
            }
        }

        //
        public void negociateWithIdler(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < other.MMotivation + negociator.MSimpathy)
            {
                other.MComportement.evolve(other);
            }
            else if (num < other.MMotivation + negociator.MSimpathy + other.MAngryness)
            {
                regress(negociator);
            }
        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < negociator.MSimpathy)
            {
                other.MComportement.evolve(other);
            }
            else if (num < negociator.MSimpathy + negociator.MAngryness + negociator.MFatigue)
            {
                regress(negociator);
            }
            else
            {
                Tools.updateValue(other.MMotivation, Tools.MOTIVATION_UP);
            }
        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < other.MMotivation + other.MSimpathy)
            {
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else if (num < other.MMotivation + other.MSimpathy + other.MFatigue)
            {
                regress(negociator);
            }
            else
            {
                Tools.updateValue(negociator.MSimpathy, Tools.SIMPATHY_UP);
            }
        }
    }
}
