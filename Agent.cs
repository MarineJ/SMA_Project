using System;
using System.Collections.Generic;
using System.Text;
using Mogre;


namespace SMA_Project_V1
{
    class Agent
    {
        //SceneManager SceneManager;
        private AnimationState mAnimationState = null; //The AnimationState the moving object
        private float mDistance = 0.0f;
        private Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        private Vector3 mDestination = Vector3.ZERO;
        private LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        private float mWalkSpeed = 50.0f;  // The speed at which the object is moving
        private bool mWalking = false;
        private String name;
        private int mIndexInList = 0;
        private Entity ent;
        private bool bcube = false;
        private Entity cube;
        private SceneNode nodecube;
        private SceneManager mSceneManager;
        private SceneNode node;
        private IComportement mComportement;
        private int TimeBeforeNextNegociation = 30;
 

        // Agent state
        private int mLeaderShip;
        private int mFatigue;
        private int mMotivation;
        private int mSimpathy;
        private int mAngryness;
        private int mFavoriteColor;



       

        public Agent(string mesh, SceneManager SceneManager, string nom, LinkedList<Vector3> walklist, float walkspeed, int index, IComportement comportement)
        {

            //SceneManager = this.SceneManager;
            // Create the Robot entity
            name = nom;
            mComportement = comportement;
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

        public void negociateColor(Agent agent) 
        {
            agent.mFavoriteColor = this.mFavoriteColor;
        }

        public void negociate(Agent agent, Agent other)
        {
            if (other.MComportement.GetType() == typeof(Builder))
            {
                agent.MComportement.negociateWithBuilder(agent, other);
            }
            else if (other.MComportement.GetType() == typeof(Manager))
            {
                agent.MComportement.negociateWithManager(agent, other);
            }
            else if (other.MComportement.GetType() == typeof(Idler))
            {
                agent.MComportement.negociateWithIdler(agent, other);
            }
            else
            {
                agent.MComportement.negociateWithDrag(agent, other);
            }
        }
    



        #region properties

        internal IComportement MComportement
        {
            get { return mComportement; }
            set { mComportement = value; }
        } 

        public int MFavoriteColor
        {
            get { return mFavoriteColor; }
            set { mFavoriteColor = value; }
        }
        

        public int MAngryness
        {
            get { return mAngryness; }
            set { mAngryness = value; }
        }

        public int MSimpathy
        {
            get { return mSimpathy; }
            set { mSimpathy = value; }
        }

        public int MMotivation
        {
            get { return mMotivation; }
            set { mMotivation = value; }
        }

        public int MFatigue
        {
            get { return mFatigue; }
            set { mFatigue = value; }
        }

        public int MLeaderShip
        {
            get { return mLeaderShip; }
            set { mLeaderShip = value; }
        }

        public AnimationState MAnimationState
        {
            get { return mAnimationState; }
            set { mAnimationState = value; }
        }
        //The distance the object has left to travel

        public float MDistance
        {
            get { return mDistance; }
            set { mDistance = value; }
        }

        public Vector3 MDirection
        {
            get { return mDirection; }
            set { mDirection = value; }
        }
        // The destination the object is moving towards

        public LinkedList<Vector3> MWalkList
        {
            get { return mWalkList; }
            set { mWalkList = value; }
        }

        public float MWalkSpeed
        {
            get { return mWalkSpeed; }
            set { mWalkSpeed = value; }
        }

        public Entity Ent
        {
            get { return ent; }
            set { ent = value; }
        }


        public SceneNode Node
        {
            get { return node; }
            set { node = value; }
        }

        public Entity Cube
        {
            get { return cube; }
            set { cube = value; }
        }

        public SceneManager MSceneManager
        {
            get { return mSceneManager; }
            set { mSceneManager = value; }
        }
        #endregion


    }
}
