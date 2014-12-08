using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Manager : IComportement
    {

        public Manager() { }

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
            agent.MComportement = new Builder();
        }

        public void remaneWhatYouAre(Agent agent)
        {

        }

        public void negociateWithManager(Agent agent, Agent other)
        {

        }

        public void negociateWithIdler(Agent negociator,Agent other)
        {

        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {

        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {

        }


    }
}
