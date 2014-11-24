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
        SceneNode node ;


        public Robot(SceneManager SceneManager, string nom, LinkedList<Vector3> walklist, float walkspeed) 
        {

            //SceneManager = this.SceneManager;
            // Create the Robot entity
            name = nom;
            
            ent = SceneManager.CreateEntity(nom, "robot.mesh");

            // Create the Robot's SceneNode
            node = SceneManager.RootSceneNode.CreateChildSceneNode(nom+"Node",new Vector3(0.0f, 0.0f, 0.25f));
            node.AttachObject(ent);

            //ent = SceneManager.GetEntity(nom);
            //node = SceneManager.GetSceneNode(nom+"Node");


            mWalkList = walklist;
            mWalkSpeed = walkspeed;
        }


        public void animetoi(SceneManager SceneManager) 
        {
            mAnimationState = SceneManager.GetEntity(name).GetAnimationState("Walk");
            mAnimationState.Loop = true;
            mAnimationState.Enabled = true;
        }

        protected bool nextLocation()
        {
            if (mWalkList.Count == 0)
                return false;
            return true;
        }

        public bool Comportement(FrameEvent evt) 
        {
            float move = mWalkSpeed * (evt.timeSinceLastFrame);
            mDistance -= move;

            //Knot arrival check
            if (mDistance <= 0.0f)
            {
                if (!TurnNextLocation())
                {
                    mAnimationState = ent.GetAnimationState("Idle");
                    return true;
                }
            }
            else
            {
                //movement code goes here
                node.Translate(mDirection * move);
            }

            //Update the Animation State.
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
