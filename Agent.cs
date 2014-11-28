using System;
using System.Collections.Generic;
using System.Text;
using Mogre;


namespace SMA_Project_V1
{
    abstract class Agent
    {
        //SceneManager SceneManager;
        protected AnimationState mAnimationState = null; //The AnimationState the moving object
        protected float mDistance = 0.0f;              //The distance the object has left to travel
        protected Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        protected Vector3 mDestination = Vector3.ZERO; // The destination the object is moving towards
        protected LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        protected float mWalkSpeed = 50.0f;  // The speed at which the object is moving
        protected bool mWalking = false;
        protected String name;
        protected int mIndexInList = 0;
        protected Entity ent;
        protected SceneNode node;
        protected bool bcube = false;
        protected Entity cube;
        protected SceneNode nodecube;

        // Agent state
        protected int mLeaderShip;
        protected int mInitiative;
        protected int mFavoriteColor;
        protected int mFriends;
        protected float mTauxParticipation;
        protected string mRole; // {batisseur, manageur, feigneant, troll}




        public Agent(string mesh, SceneManager SceneManager, string nom, LinkedList<Vector3> walklist, float walkspeed, int index)
        {

            //SceneManager = this.SceneManager;
            // Create the Robot entity
            name = nom;
            mIndexInList = index;
            // la forme du robot
            ent = SceneManager.CreateEntity(nom, mesh);
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
        public  void animation(string typeAnimation)
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
        
        public bool TurnNextLocation()
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

        public void changeYourColor(Agent agent) 
        {
            agent.mFavoriteColor = this.mFavoriteColor;
        }

        public virtual void evolve()
        {
            
        }

        public virtual bool Comportement(FrameEvent evt, Random rand) { return (true); }
         
        public virtual void initiateNegociation(List<Agent> agentsInRange)
        {

        }

    }
}
