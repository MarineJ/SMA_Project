using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Manager : Agent
    {

        public Manager(string mesh, SceneManager SceneManager, string nom, LinkedList<Vector3> walklist, float walkspeed, int index) :
            base(mesh, SceneManager, nom, walklist, walkspeed, index) { }



        // le comportement de l'agent lors de la simulation
        public override bool Comportement(FrameEvent evt, Random rand)
        {
            // visibilité du cube
            //cube.Visible = bcube;

            if (mWalkList.Count != 2)
            {
                marcheAleatoire(rand);
            }

            // vitesse de l'agent
            float move = mWalkSpeed * (evt.timeSinceLastFrame);
            // distance à parcourir
            mDistance -= move;

            //distance en ligne droite
            if (mDistance <= 0.0f)
            {   // si on est arrivé
                if (!TurnNextLocation())
                {
                    // on attend
                    mAnimationState = ent.GetAnimationState("Idle");
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
        public void marcheAleatoire(Random rand)
        {
            mWalkList.Clear();

            double angle = rand.NextDouble() * System.Math.PI;
            Vector3 tmp = new Vector3((float)(1500 * System.Math.Cos(angle)), node.Position.y, (float)(1500 * System.Math.Sin(angle)));
            mWalkList.AddLast(tmp);
        }

    }
}
