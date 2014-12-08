using System;
using System.Collections.Generic;
using System.Text;
using Mogre;


namespace SMA_Project_V1
{
    abstract class Agent
    {
        //SceneManager SceneManager;
        private AnimationState mAnimationState = null; //The AnimationState the moving object

        public AnimationState MAnimationState
        {
            get { return mAnimationState; }
            set { mAnimationState = value; }
        }
        private float mDistance = 0.0f;              //The distance the object has left to travel

        public float MDistance
        {
            get { return mDistance; }
            set { mDistance = value; }
        }
        private Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving

        public Vector3 MDirection
        {
            get { return mDirection; }
            set { mDirection = value; }
        }
        private Vector3 mDestination = Vector3.ZERO; // The destination the object is moving towards
        private LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints

        public LinkedList<Vector3> MWalkList
        {
            get { return mWalkList; }
            set { mWalkList = value; }
        }
        private float mWalkSpeed = 50.0f;  // The speed at which the object is moving

        public float MWalkSpeed
        {
            get { return mWalkSpeed; }
            set { mWalkSpeed = value; }
        }
        private bool mWalking = false;
        private String name;
        private int mIndexInList = 0;
        private Entity ent;

        public Entity Ent
        {
            get { return ent; }
            set { ent = value; }
        }
        private SceneNode node;

        public SceneNode Node
        {
            get { return node; }
            set { node = value; }
        }
        private bool bcube = false;
        private Entity cube;

        public Entity Cube
        {
            get { return cube; }
            set { cube = value; }
        }
        private SceneNode nodecube;
        private SceneManager mSceneManager;

        public SceneManager MSceneManager
        {
            get { return mSceneManager; }
            set { mSceneManager = value; }
        }


        // Agent state
        private int mLeaderShip;
        private int mInitiative;
        private int mFavoriteColor;
        private int mFriends;
        private float mTauxParticipation;
        private string mRole; // {batisseur, manageur, feigneant, troll}

        public Agent(string mesh, SceneManager SceneManager, string nom, LinkedList<Vector3> walklist, float walkspeed, int index)
        {

            //SceneManager = this.SceneManager;
            // Create the Robot entity
            name = nom;
            mIndexInList = index;
            mSceneManager = SceneManager;
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
