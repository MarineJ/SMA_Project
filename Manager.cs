﻿using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class ComportementManager : IComportement
    {

        public ComportementManager() { }



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
            Vector3 tmp = new Vector3((float)(1500 * System.Math.Cos(angle)), node.Position.y, (float)(1500 * System.Math.Sin(angle)));
            agent.MWalkList.AddLast(tmp);
        }

    }
}
