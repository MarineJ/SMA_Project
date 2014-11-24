using System;
using System.Collections.Generic;

using System.Text;

using Mogre;


namespace SMA_Project_V1
{
    class Robot
    {
        //SceneManager SceneManager;
        AnimationState mAnimationState = null; //The AnimationState the moving object
        float mDistance = 0.0f;              //The distance the object has left to travel
        Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        Vector3 mDestination = Vector3.ZERO; // The destination the object is moving towards
        LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        float mWalkSpeed = 50.0f;  // The speed at which the object is moving
        bool mWalking = false;
        String name;

        Entity ent;
        SceneNode node;
        bool bcube = false;
        Entity cube;
        SceneNode nodecube;


        public Robot(SceneManager SceneManager, string nom, LinkedList<Vector3> walklist, float walkspeed)
        {

            //SceneManager = this.SceneManager;
            // Create the Robot entity
            name = nom;
            // la forme du robot
            ent = SceneManager.CreateEntity(nom, "robot.mesh");
            // la forme du cube
            cube = SceneManager.CreateEntity("cube" + nom, "cube.mesh");



            //  Robot SceneNode
            node = SceneManager.RootSceneNode.CreateChildSceneNode(nom + "Node", new Vector3(0.0f, 0.0f, 0.25f));
            // le noeud enfant du robot, celui du cube
            nodecube = node.CreateChildSceneNode(nom + "NodeCube", new Vector3(0.0f, 120.0f, 0.0f));

            // taille du cube
            nodecube.Scale(0.5f, 0.5f, 0.5f);
            // on attache les noeuds à leur modèle
            node.AttachObject(ent);
            nodecube.AttachObject(cube);

            //ent = SceneManager.GetEntity(nom);
            //node = SceneManager.GetSceneNode(nom+"Node");


            mWalkList = walklist;
            mWalkSpeed = walkspeed;
        }

        // attive une animation en boucle
        public void animation(string typeAnimation)
        {
            //Start the walk animation
            mAnimationState = ent.GetAnimationState(typeAnimation);
            mAnimationState.Loop = true;
            mAnimationState.Enabled = true;
        }
        //passe au suivant
        protected bool nextLocation()
        {
            if (mWalkList.Count == 0)
                return false;
            return true;
        }
        // le comportement de l'agent lors de la simulation
        public bool Comportement(FrameEvent evt)
        {
            // visibilité du cube
            //cube.Visible = bcube;

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

        bool TurnNextLocation()
        {

            if (nextLocation())
            {
                //Start the walk animation
                mAnimationState = ent.GetAnimationState("Walk");
                mAnimationState.Loop = true;
                mAnimationState.Enabled = true;

                LinkedListNode<Vector3> tmp;  //temporary listNode
                mDestination = mWalkList.First.Value; //get the next destination.
                tmp = mWalkList.First; //save the node that held it
                mWalkList.RemoveFirst(); //remove that node from the front of the list
                mWalkList.AddLast(tmp);  //add it to the back of the list.

                //update the direction and the distance
                mDirection = mDestination - node.Position;
                mDistance = mDirection.Normalise();

                Vector3 src = node.Orientation * Vector3.UNIT_X;


                if ((1.0f + src.DotProduct(mDirection)) < 0.0001f)
                {
                    node.Yaw(new Angle(180.0f));
                }
                else
                {
                    Quaternion quat = src.GetRotationTo(mDirection);
                    node.Rotate(quat);
                }

                return true;

            }

            return false;
        }



    }
}
