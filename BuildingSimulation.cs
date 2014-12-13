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
        List<Agent> robotList;
        List<Agent> TMProbotList;
        List<SceneNode> cubeList;
        List<SceneNode> gridList;

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
           /* mWalkList.AddLast(new Vector3(550.0f, 0.0f, 50.0f));
            mWalkList.AddLast(new Vector3(-100.0f, 0.0f, -200.0f));
            mWalkList.AddLast(new Vector3(0.0f, 0.0f, 25.0f));*/

            robotList = new List<Agent>();
            for (int i = 0; i < _AgentsNumber/3; i = i+4)
            {
                

                Agent builder = new Agent("robot.mesh",SceneManager, "builder" + i.ToString(), mWalkList, mWalkSpeed, i, new Builder());
                builder.initiateBuilderValues();
                robotList.Add(builder);
                Agent manager = new Agent("ninja.mesh", SceneManager, "manager" + (i + 1).ToString(), mWalkList, mWalkSpeed, i + 1, new Manager());
                manager.initiateManagerValues();
                robotList.Add(manager);
                Agent drag = new Agent("robot.mesh", SceneManager, "drag" + (i + 2).ToString(), mWalkList, mWalkSpeed, i + 2, new Drag());
                drag.initiateDragValues();
                robotList.Add(drag);
                Agent idler = new Agent("jaiqua.mesh", SceneManager, "idler" + (i + 3).ToString(), mWalkList, mWalkSpeed, i + 3,new Idler());
                idler.initiateIdlerValues();
                robotList.Add(idler);

            }

            //Console.WriteLine("passer");

            TMProbotList = new List<Agent>(robotList);
        }

        protected override void CreateCubes()
        {
            cubeList = new List<SceneNode>();
            for (int i = 0; i < 100; i++)
            {
                ColourValue col = new ColourValue(1, 0, 0);
                Entity entcube = SceneManager.CreateEntity("cube" + i, "cube.mesh");
                int _couleur = rand.Next(1, 6);
                MaterialPtr mat = MaterialManager.Singleton.Create("CubeMat" + i, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
                TextureUnitState tuisTexture = mat.GetTechnique(0).GetPass(0).CreateTextureUnitState(Tools.color(_couleur));

                entcube.SetMaterialName("CubeMat" + i);

                double angle = rand.NextDouble() * 2 * System.Math.PI;
                double module = rand.Next(400, 1500);


                SceneNode ncube = SceneManager.RootSceneNode.CreateChildSceneNode("nCube" + i, new Vector3((float)(module * System.Math.Cos(angle)), 0.0f, (float)(module * System.Math.Sin(angle))));

                ncube.Scale(0.5f, 0.01f, 0.5f);
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
            int rayon = 15;
            TMProbotList = new List<Agent>(robotList);
            int [,] collision = new int[robotList.Count,robotList.Count];

            for (int i = 0 ; i < TMProbotList.Count;i++)
            {
                //Collision avec les autres agents
                for (int j = i+1 ; j < TMProbotList.Count;j++)
                {
                    /*if (TMProbotList[i].Equals(TMProbotList[j]))
                    {
                        break;
                    }*/

                    if (System.Math.Abs(TMProbotList[i].node.Position.x - TMProbotList[j].node.Position.x) <= rayon &&
                        System.Math.Abs(TMProbotList[i].node.Position.z - TMProbotList[j].node.Position.z) <= rayon)
                    {
                        collision[i, j] = 1;
                        collision[j, i] = 1;
                    }
                }

                // Collision avec une dalles colorée
                for (int j = 0; j < cubeList.Count; j++)
                {
                    if (System.Math.Abs(cubeList[j].Position.x - TMProbotList[i].node.Position.x) <= rayon &&
                        System.Math.Abs(cubeList[j].Position.z - TMProbotList[i].node.Position.z) <= rayon &&
                        !TMProbotList[i].bcube)
                    {
                        TMProbotList[i].nodecube.AttachObject(cubeList[j].DetachObject((ushort)0));
                        TMProbotList[i].bcube = true;

                        cubeList[j].Parent.RemoveChild(cubeList[j]);
                        cubeList.Remove(cubeList[j]);
                        j--;
                    }
                }

                // marche pas encore 

                // Collision avec Grille
                for (int j = 0; j < gridList.Count; j++)
                {
                    if (System.Math.Abs(gridList[j].Position.x - TMProbotList[i].node.Position.x) <= rayon &&
                            System.Math.Abs(gridList[j].Position.z - TMProbotList[i].node.Position.z) <= rayon)
                    {
                        if (TMProbotList[i].bcube == true &&
                            gridList[j].NumAttachedObjects() == 1 &&
                            System.Math.Abs(gridList[j].Position.x - TMProbotList[i].node.Position.x) <= rayon &&
                            System.Math.Abs(gridList[j].Position.z - TMProbotList[i].node.Position.z) <= rayon)
                        {
                            if (TMProbotList[i].nodecube.NumAttachedObjects() > (ushort)0)
                            {
                                gridList[j].DetachAllObjects();
                                int test = TMProbotList[i].nodecube.NumAttachedObjects();
                                gridList[j].AttachObject(TMProbotList[i].nodecube.DetachObject((ushort)0));
                                //gridList[j].Scale(Tools.CUBE_SCALE);
                                gridList.Remove(gridList[j]);
                                j--;
                                break;
                            }
                        }
                    }
                    
                }



                
            }





            do
            {

                int tmp = rand.Next(0, TMProbotList.Count-1);    
                List<Agent> TMProbotList2 = new List<Agent>();
                for (int i = 0; i < robotList.Count; i++) 
                {
                    if (collision[tmp, i] == 1)
                    { 
                        TMProbotList2.Add(robotList[i]);
                    }
                }

                    TMProbotList[tmp].animation("Walk");

                    if (TMProbotList2.Count != 0)
                    {
                        do
                        {
                            int tmp2 = rand.Next(0, TMProbotList2.Count);
                            if (System.Math.Abs(TMProbotList[tmp].Node.Position.x - TMProbotList2[tmp2].Node.Position.x) <= rayon &&
                                System.Math.Abs(TMProbotList[tmp].Node.Position.z - TMProbotList2[tmp2].Node.Position.z) <= rayon &&
                                tmp2 != tmp)
                            {
                                TMProbotList[tmp].negociate(TMProbotList[tmp], TMProbotList2[tmp2]);
                            }
                            TMProbotList2.Remove(TMProbotList2[tmp2]);

                        } while (TMProbotList2.Count > 0);
                    }
            TMProbotList[tmp].MComportement.Comportement(evt, rand, TMProbotList[tmp]);
            TMProbotList.Remove(TMProbotList[tmp]);
            } while (TMProbotList.Count > 0);

            return true;
        }

        
    }
}
