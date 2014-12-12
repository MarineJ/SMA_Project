using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Manager : IComportement
    {

        public Manager()
        {

        }

        public Manager(Agent agent) 
        {
            agent.MAngryness = Tools.MANAGER_ANGRYNESS_INITIAL;
            agent.MFatigue = Tools.MANAGER_FATIGUE_INITIAL;
            agent.MLeaderShip = Tools.MANAGER_LEADERSHIP_INITIAL;
            agent.MMotivation = Tools.MANAGER_MOTIVATION_INITIAL;
            agent.MSimpathy = Tools.MANAGER_SYMPATHY_INITIAL;
        }

        // le comportement de l'agent lors de la simulation
        public  bool Comportement(FrameEvent evt, Random rand, Agent agent, Agent other)
        {
            // visibilité du cube
            //cube.Visible = bcube;
            agent.negociate(agent, other);
            if(agent.MWalkList.Count != 2)
            {
                marcheAleatoire(rand, agent);
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


        // le manager marche aléatoirement
        public void marcheAleatoire(Random rand, Agent agent)
        {
            agent.MWalkList.Clear();

            double angle = rand.NextDouble() * System.Math.PI;
            Vector3 tmp = new Vector3((float)(1500 * System.Math.Cos(angle)), agent.Node.Position.y, (float)(1500 * System.Math.Sin(angle)));
            agent.MWalkList.AddLast(tmp);
        }

        public  void evolve(Agent agent)
        {
        }

        public void regress(Agent agent)
        {
            agent.MComportement = new Builder(agent);
        }


        public void negociateWithManager(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(200);
            if (num < other.MLeaderShip && other.MLeaderShip > negociator.MLeaderShip)
            {
                regress(negociator);
            }
            else if(num < (other.MLeaderShip + negociator.MLeaderShip) && other.MLeaderShip < negociator.MLeaderShip)
            {
                regress(other);
            }
        }

        public void negociateWithIdler(Agent negociator,Agent other)
        {
            // l'évolution dépend de Angryness et du leadership du manager
            // il a une chance d'évoluer en Builder, regresser en Drag ou de rester Idler
            // si il devient builder --> le manager gagne l en leadership
            // si il devient si il regresse en Drag --> le manager perd l en leadership
            // si il reste Idler --> Angryness ++
            Random rand = new Random();
            int num = rand.Next(200);
            if (num < other.MAngryness)
            {
                other.MComportement.regress(other);
                Tools.updateValue(negociator.MLeaderShip, Tools.LEADERSHIP_DOWN);
            }
            else if (num < other.MAngryness + negociator.MLeaderShip)
            {
                other.MComportement.evolve(other);
                Tools.updateValue(negociator.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else
            {
                Tools.updateValue(other.MAngryness, Tools.ANGRYNESS_UP);
            }

        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {

            // l'évolution dépend du leadership du manager
            // il a une chance d'évoluer en feignant, il ne peut pas regresser mais la présence du manager
            // a une chance d'amplifier sa tendance à être motivé ou énervé
            // plus le Drag est énervé plus il y a de chance qu'au contacte du manager son énervement augmente
            // plus le Drag est motivé plus il y a de chance qu'au contacte du manager sa motivation augmente
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < negociator.MLeaderShip)
            {
                other.MComportement.evolve(other);
            }
            else if (num < negociator.MLeaderShip + other.MAngryness)
            {
                Tools.updateValue(other.MAngryness, Tools.ANGRYNESS_UP);
            }
            else if (num < negociator.MLeaderShip + other.MAngryness + other.MMotivation)
            {
                Tools.updateValue(other.MMotivation, Tools.MOTIVATION_UP);
            }
        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {
            // l'évolution dépend du leadership  du manager et de la motivation et fatigue builder
            // le builder a une chance d'évoluer en manager, de regresser en feignant ou de rester builder
            // si il devient manager --> le manager gagne l en leadership
            // si il devient feignant --> le manager perd l en leadership
            // si il reste builder --> Motivation ++
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < negociator.MLeaderShip)
            {
                Tools.updateValue(other.MMotivation, Tools.MOTIVATION_UP);
            }
            else if (num < negociator.MLeaderShip + other.MMotivation)
            {
                other.MComportement.evolve(other);
                Tools.updateValue(negociator.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else if(num< negociator.MLeaderShip + other.MMotivation + other.MFatigue)
            {
                other.MComportement.regress(other);
                Tools.updateValue(negociator.MLeaderShip, Tools.LEADERSHIP_DOWN);
            }


        }


    }
}
