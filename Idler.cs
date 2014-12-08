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
        public override bool Comportement(FrameEvent evt, Random rand, Agent agent)
        {
            // visibilité du cube
            //cube.Visible = bcube;

            if(agent.MWalkList.Count != 2)
            {
                marcheAleatoire(rand);
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
                node.Translate(mDirection * move);
            }
            //Passe à la frame d'animation suivante
            mAnimationState.AddTime(evt.timeSinceLastFrame * mWalkSpeed / 20);

            return true;

        }



        // le manager marche aléatoirement
        public void marcheAleatoire(Random rand, Agent agent, SceneManager sc)
        {
            agent.MWalkList.Clear();

            double angle = rand.NextDouble() * System.Math.PI;
            Vector3 tmp = new Vector3((float)(1500 * System.Math.Cos(angle)), sc.node.Position.y, (float)(1500 * System.Math.Sin(angle)));
            agent.MWalkList.AddLast(tmp);
        }

      

    }
}
