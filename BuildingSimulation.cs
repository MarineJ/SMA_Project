using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class  BuildingSimulation : WindowMaker
    {
        Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        Vector3 mDestination = Vector3.ZERO;
        float _TimeSpeed = 1;
        // The destination the object is moving towards
        LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        float mWalkSpeed = 150.0f;  // The speed at which the object is moving
        internal List<Agent> agentList;
        internal List<Agent> TMPagentList;
        public static  List<SceneNode> cubeList;
        internal List<SceneNode> gridList;

        int _AgentsNumber = 0;
        Random rand = new Random();

        
        
        
        public BuildingSimulation(int agentNumb, float timeSpeed) : base() 
        {
            _AgentsNumber = agentNumb;
            _TimeSpeed = timeSpeed;
            mWalkSpeed = mWalkSpeed * _TimeSpeed;
        }

        protected  override void CreateCamera()
        {
            Camera = SceneManager.CreateCamera("MainCamera");
            Camera.Position = new Vector3(0, 100, 500);
            Camera.LookAt(Vector3.ZERO);
            Camera.NearClipDistance = 5;
            mCameraMan = new CameraMan(mCamera);
        }

        protected override void CreateRenderWindow(IntPtr handle)
        {
            this.Root.Initialise(false, "Main Ogre Window");
            NameValuePairList miscParams = new NameValuePairList();
            if (handle != IntPtr.Zero)
            {
                miscParams["externalWindowHandle"] = handle.ToString();
                this.RenderWindow = this.Root.CreateRenderWindow("Autumn main RenderWindow", 800, 600, false, miscParams);
            }
            else
            {
                this.RenderWindow = this.Root.CreateRenderWindow("Autumn main RenderWindow", 800, 600, false);
            }
        }

        protected override void ChooseSceneManager()
        {
            this.SceneManager = this.Root.CreateSceneManager(SceneType.ST_EXTERIOR_CLOSE);
        }

        protected override void CreateSceneManager()
        {
            //mSceneMgr.SetWorldGeometry("terrain.cfg");
            this.SceneManager.AmbientLight = ColourValue.Black;
            SceneManager.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;

            Plane plane = new Plane(Vector3.UNIT_Y, 0);
            MeshManager.Singleton.CreatePlane("ground",
            ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane,
            15000, 15000, 20, 20, true, 1, 5, 5, Vector3.UNIT_Z);

            Entity groundEnt = SceneManager.CreateEntity("GroundEntity", "ground");
            SceneManager.RootSceneNode.CreateChildSceneNode().AttachObject(groundEnt);

            groundEnt.SetMaterialName("Examples/Rockwall");
            groundEnt.CastShadows = false;

      

            // SKY

            SceneManager.SetSkyDome(true, "Examples/CloudySky", 5, 8);

            // Fog (brouillard) 
            ColourValue fadeColour = new ColourValue(0.9f, 0.9f, 0.9f);

            //mWindow.GetViewport(0).BackgroundColour = fadeColour;

            SceneManager.SetFog(FogMode.FOG_EXP, fadeColour, 0.0005f);


            //Lights
            SceneManager.AmbientLight = ColourValue.White;

        }

        protected override void CreateViewport()
        {
            this.Viewport = this.RenderWindow.AddViewport(this.Camera);
            this.Viewport.BackgroundColour = ColourValue.Black;
            this.Camera.AspectRatio = (float)this.Viewport.ActualWidth / this.Viewport.ActualHeight;
        }

        protected override void CreateGrid()
        {
            gridList = new List<SceneNode>();

            int largeur = 200;
            int longueur = 200;

            int cote = 100;


            for (int i = -largeur; i < largeur; i += cote)
            {
                for (int j = -longueur; j < longueur; j += cote)
                {
                    Entity entgrid = SceneManager.CreateEntity("grid" + i.ToString() + j.ToString(), "cube.mesh");

                    SceneNode temp = SceneManager.RootSceneNode.CreateChildSceneNode(new Vector3( i, 0.0f, j));
                    temp.AttachObject(entgrid);
                    temp.Scale(Tools.CUBE_SCALE);
                    gridList.Add(temp);
                }
            }
        }

        protected override void CreateAgents()
        {
            // Create the walking list
            mWalkList = new LinkedList<Vector3>();
    
            agentList = new List<Agent>();
           
            for (int i = 0; i < _AgentsNumber; i=i+4)
            {
                bool condition = true;
                int rayon = 60;
                Agent builder = new Agent(Tools.BUILDER_MESH, SceneManager, "builder" + i.ToString(), mWalkList, mWalkSpeed, i, new Builder());
                Agent manager = new Agent(Tools.MANAGER_MESH, SceneManager, "manager" + (i).ToString(), mWalkList, mWalkSpeed, i + 1, new Manager());
                Agent drag = new Agent(Tools.DRAG_MESH, SceneManager, "drag" + (i).ToString(), mWalkList, mWalkSpeed, i + 2, new Drag());
                Agent idler = new Agent(Tools.IDLER_MESH, SceneManager, "idler" + (i).ToString(), mWalkList, mWalkSpeed, i + 3, new Idler());

                builder.initiateBuilderValues();
                manager.initiateManagerValues();
                drag.initiateDragValues();
                idler.initiateIdlerValues();

                //builder
                do
                {
                    condition = true;
                    builder.InitiatePosition(rand);
                    foreach (Agent age in agentList)
                    {
                        if (System.Math.Abs(age.Node.Position.x - builder.Node.Position.x) <= rayon &&
                            System.Math.Abs(age.Node.Position.y - builder.Node.Position.y) <= rayon)
                        {
                            condition = false; ;
                        }
                    }
                } while (condition == false);
                agentList.Add(builder);

                //manager
                do
                {
                    condition = true;
                    manager.InitiatePosition(rand);
                    foreach (Agent age in agentList)
                    {
                        if (System.Math.Abs(age.Node.Position.x - manager.Node.Position.x) <= rayon &&
                            System.Math.Abs(age.Node.Position.y - manager.Node.Position.y) <= rayon)
                        {
                            condition = false; ;
                        }
                    }
                } while (condition == false);
                agentList.Add(manager);

                //drag
                do
                {
                    condition = true;
                    drag.InitiatePosition(rand);
                    foreach (Agent age in agentList)
                    {
                        if (System.Math.Abs(age.Node.Position.x - drag.Node.Position.x) <= rayon &&
                            System.Math.Abs(age.Node.Position.y - drag.Node.Position.y) <= rayon)
                        {
                            condition = false; ;
                        }
                    }
                } while (condition == false);
                agentList.Add(drag);

                //idler
                do
                {
                    condition = true;
                    idler.InitiatePosition(rand);
                    foreach (Agent age in agentList)
                    {
                        if (System.Math.Abs(age.Node.Position.x - idler.Node.Position.x) <= rayon &&
                            System.Math.Abs(age.Node.Position.y - idler.Node.Position.y) <= rayon)
                        {
                            condition = false; ;
                        }
                    }
                } while (condition == false);
                agentList.Add(idler);



            } 


            //Console.WriteLine("passer");

            TMPagentList = new List<Agent>(agentList);
        }

        protected override void CreateCubes()
        {
            cubeList = new List<SceneNode>();
            for (int i = 0; i < 4*_AgentsNumber; i++)
            {
                ColourValue col = new ColourValue(1, 0, 0);
                Entity entcube = SceneManager.CreateEntity("cube" + i, "cube.mesh");
                int _couleur = rand.Next(1, 6);
                MaterialPtr mat = MaterialManager.Singleton.Create("CubeMat" + i, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
                TextureUnitState tuisTexture = mat.GetTechnique(0).GetPass(0).CreateTextureUnitState(Tools.color(_couleur));

                entcube.SetMaterialName("CubeMat" + i);

                double angle = rand.NextDouble() * 2 * System.Math.PI;
                double module = rand.Next(Tools.RAYON_SCENE_INT, Tools.RAYON_SCENE_EXT);


                SceneNode ncube = SceneManager.RootSceneNode.CreateChildSceneNode("nCube" + i, new Vector3((float)(module * System.Math.Cos(angle)), 0.0f, (float)(module * System.Math.Sin(angle))));

                ncube.Scale(Tools.CUBE_SCALE);
                ncube.AttachObject(entcube);

                cubeList.Add(ncube);

            }

        }
        
        protected override void CreateOverlay()
        {
            this.Overlay = OverlayManager.Singleton.Create("TestOverlay");
            // Create a panel.
            var panel = (PanelOverlayElement)OverlayManager.Singleton.CreateOverlayElement("Panel", "PanelElement");
            // Set panel properties.
            panel.MaterialName = "Core/StatsBlockCenter";
            panel.MetricsMode = GuiMetricsMode.GMM_PIXELS;
            panel.Top = 0;
            panel.Left = 0;
            panel.Width = 250;
            panel.Height = 150;

            // Add the panel to the overlay.
            Overlay.Add2D(panel);

            // Make the overlay visible.
            Overlay.Show();
        }

        protected override void CreateInputHandler()
        {

            //new DefaultInputHandler(this);

            base.CreateFrameListeners();

            this.Root.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);


        }

        bool FrameStarted(FrameEvent evt)
        {
            
            TMPagentList = new List<Agent>(agentList);
            int [,] collision = new int[agentList.Count,agentList.Count];

            for (int i = 0 ; i < TMPagentList.Count;i++)
            {
                //Collision avec les autres agents
                for (int j = i+1 ; j < TMPagentList.Count;j++)
                {
                    /*if (TMPagentList[i].Equals(TMPagentList[j]))
                    {
                        break;
                    }*/

                    if (System.Math.Abs(TMPagentList[i].Node.Position.x - TMPagentList[j].Node.Position.x) <= Tools.RAYON_COLLISION &&
                        System.Math.Abs(TMPagentList[i].Node.Position.z - TMPagentList[j].Node.Position.z) <= Tools.RAYON_COLLISION)
                    {
                        collision[i, j] = 1;
                        collision[j, i] = 1;
                    }
                }

                

                // si l'agent est un constructeur
                if (TMPagentList[i].MComportement.ToString() == typeof(Builder).ToString()) 
                {
                    // Collision avec une dalles colorée
                    for (int j = 0; j < cubeList.Count; j++)
                    {
                        if (System.Math.Abs(cubeList[j].Position.x - TMPagentList[i].Node.Position.x) <= Tools.RAYON_COLLISION &&
                            System.Math.Abs(cubeList[j].Position.z - TMPagentList[i].Node.Position.z) <= Tools.RAYON_COLLISION &&
                            !TMPagentList[i].Bcube /*&& TMPagentList[i].GetType() == typeof( Builder)*/)
                        {
                            TMPagentList[i].Nodecube.AttachObject(cubeList[j].DetachObject((ushort)0));
                            TMPagentList[i].Bcube = true;

                            cubeList[j].Parent.RemoveChild(cubeList[j]);
                            cubeList.Remove(cubeList[j]);
                            j--;
                        }
                    }


                    

                    // Collision avec Grille
                    for (int j = 0; j < gridList.Count; j++)
                    {
                        if (System.Math.Abs(gridList[j].Position.x - TMPagentList[i].Node.Position.x) <= Tools.RAYON_COLLISION &&
                                System.Math.Abs(gridList[j].Position.z - TMPagentList[i].Node.Position.z) <= Tools.RAYON_COLLISION)
                        {
                            if (TMPagentList[i].Bcube == true &&
                                gridList[j].NumAttachedObjects() == 1 &&
                                System.Math.Abs(gridList[j].Position.x - TMPagentList[i].Node.Position.x) <= Tools.RAYON_COLLISION &&
                                System.Math.Abs(gridList[j].Position.z - TMPagentList[i].Node.Position.z) <= Tools.RAYON_COLLISION)
                            {
                                if (TMPagentList[i].Nodecube.NumAttachedObjects() > (ushort)0)
                                {
                                    gridList[j].DetachAllObjects();
                                    int test = TMPagentList[i].Nodecube.NumAttachedObjects();
                                    gridList[j].AttachObject(TMPagentList[i].Nodecube.DetachObject((ushort)0));
                                    //gridList[j].Scale(Tools.CUBE_SCALE);
                                    gridList.Remove(gridList[j]);
                                    j--;
                                    break;
                                }
                            }
                        }

                    }
                }



                
            }




            //debut des interactions aléatoires
            do
            {

                int tmp = rand.Next(0, TMPagentList.Count-1); // chosie aleatoirement un index de la liste
                
                // Liste les agents aux alentoures
                List<Agent> TMPagentList2 = new List<Agent>();
                for (int i = 0; i < agentList.Count; i++) 
                {
                    if (collision[tmp, i] == 1)
                    { 
                        TMPagentList2.Add(agentList[i]);
                    }
                }

                    TMPagentList[tmp].animation("Walk");

                // si un agent est dans le coins
                if (TMPagentList2.Count != 0)
                {
                    // Interaction aléatoire
                    do
                    {
                        int tmp2 = rand.Next(0, TMPagentList2.Count);
                        /*if (System.Math.Abs(TMPagentList[tmp].Node.Position.x - TMPagentList2[tmp2].Node.Position.x) <= rayon &&
                            System.Math.Abs(TMPagentList[tmp].Node.Position.z - TMPagentList2[tmp2].Node.Position.z) <= rayon &&
                            tmp2 != tmp)
                        {*/
                        TMPagentList[tmp].updateNegociation();
                        TMPagentList[tmp].negociate(TMPagentList[tmp], TMPagentList2[tmp2]);
                        //}
                        TMPagentList2.Remove(TMPagentList2[tmp2]);

                    } while (TMPagentList2.Count > 0);
                }

                TMPagentList[tmp].MComportement.Comportement(evt, rand, TMPagentList[tmp]);
                TMPagentList.Remove(TMPagentList[tmp]);
            } while (TMPagentList.Count > 0);

            return true;
        }


        
    }
}
