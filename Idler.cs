using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Idler : IComportement
    {


        
        public Idler() { }


        // le comportement de l'agent lors de la simulation
        public  bool Comportement(FrameEvent evt, Random rand, Agent agent)
        {
            // visibilité du cube
            //cube.Visible = bcube;

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

        public void evolve(Agent agent)
        {
        }

        public void regress(Agent agent)
        {
        }


        public void negociateWithManager(Agent negociator, Agent other)
        {
            // l'évolution dépend de Angryness et du leadership du manager
            // il a une chance d'évoluer en Builder, regresser en Drag ou de rester Idler
            // si il devient builder --> le manager gagne l en leadership
            // si il devient si il regresse en Drag --> le manager perd l en leadership
            // si il reste Idler --> Angryness ++
            Random rand = new Random();
            int num = rand.Next(200);
            if (num < negociator.MAngryness)
            {
                regress(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_DOWN);
            }
            else if (num < negociator.MAngryness + other.MLeaderShip)
            {
                evolve(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else
            {
                Tools.updateValue(negociator.MAngryness, Tools.ANGRYNESS_UP);
            }
        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {
        }

        public void negociateWithIdler(Agent negociator, Agent other)
        {
        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {
        }



        // le manager marche aléatoirement
        public void marcheAleatoire(Random rand, Agent agent)
        {
            agent.MWalkList.Clear();

            double angle = rand.NextDouble() * System.Math.PI;
            Vector3 tmp = new Vector3((float)(1500 * System.Math.Cos(angle)), agent.Node.Position.y, (float)(1500 * System.Math.Sin(angle)));
            agent.MWalkList.AddLast(tmp);
        }

      

    }
}
