using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class BuildingSimulation : WindowMaker
    {
        Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        Vector3 mDestination = Vector3.ZERO;
        float _TimeSpeed = 1;
        // The destination the object is moving towards
        LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        float mWalkSpeed = 200.0f;  // The speed at which the object is moving
        List<Agent> robotList;
        List<Agent> TMProbotList;
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
            //mCameraMan = new CameraMan(mCamera);
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
            1500, 1500, 20, 20, true, 1, 5, 5, Vector3.UNIT_Z);

            Entity groundEnt = SceneManager.CreateEntity("GroundEntity", "ground");
            SceneManager.RootSceneNode.CreateChildSceneNode().AttachObject(groundEnt);

            groundEnt.SetMaterialName("Examples/Rockwall");
            groundEnt.CastShadows = false;

            // Create knot objects so we can see movement
            Entity ent = SceneManager.CreateEntity("Knot1", "knot.mesh");
            SceneNode node = SceneManager.RootSceneNode.CreateChildSceneNode("Knot1Node",
                new Vector3(0.0f, -10.0f, 25.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);
            //
            ent = SceneManager.CreateEntity("Knot2", "knot.mesh");
            node = SceneManager.RootSceneNode.CreateChildSceneNode("Knot2Node",
                new Vector3(550.0f, -10.0f, 50.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);
            //
            ent = SceneManager.CreateEntity("Knot3", "knot.mesh");
            node = SceneManager.RootSceneNode.CreateChildSceneNode("Knot3Node",
                new Vector3(-100.0f, -10.0f, -200.0f));
            node.AttachObject(ent);
            node.Scale(0.1f, 0.1f, 0.1f);

            // SKY

            SceneManager.SetSkyDome(true, "Examples/CloudySky", 5, 8);

            // Fog (brouillard) 
            ColourValue fadeColour = new ColourValue(0.9f, 0.9f, 0.9f);

            //mWindow.GetViewport(0).BackgroundColour = fadeColour;

            SceneManager.SetFog(FogMode.FOG_EXP, fadeColour, 0.0005f);


            //Lights
            SceneManager.AmbientLight = ColourValue.White;

            /*Light pointLight = SceneManager.CreateLight("pointLight");
            pointLight.Type = Light.LightTypes.LT_POINT;
            pointLight.Position = new Vector3(0, 150, 250);

            pointLight.DiffuseColour = ColourValue.Red;
            pointLight.SpecularColour = ColourValue.Red;

            Light directionalLight = SceneManager.CreateLight("directionalLight");
            directionalLight.Type = Light.LightTypes.LT_DIRECTIONAL;
            directionalLight.DiffuseColour = new ColourValue(.25f, .25f, 0);
            directionalLight.SpecularColour = new ColourValue(.25f, .25f, 0);
            directionalLight.Direction = new Vector3(0, -1, 1);

            Light spotLight = SceneManager.CreateLight("spotLight");
            spotLight.Type = Light.LightTypes.LT_SPOTLIGHT;
            spotLight.DiffuseColour = ColourValue.Blue;
            spotLight.SpecularColour = ColourValue.Blue;

            spotLight.Direction = new Vector3(-1, -1, 0);
            spotLight.Position = new Vector3(300, 300, 0);*/
        }

        protected override void CreateViewport()
        {
            this.Viewport = this.RenderWindow.AddViewport(this.Camera);
            this.Viewport.BackgroundColour = ColourValue.Black;
            this.Camera.AspectRatio = (float)this.Viewport.ActualWidth / this.Viewport.ActualHeight;
        }

        protected override void CreateAgents()
        {
            // Create the walking list
            mWalkList = new LinkedList<Vector3>();
            mWalkList.AddLast(new Vector3(550.0f, 0.0f, 50.0f));
            mWalkList.AddLast(new Vector3(-100.0f, 0.0f, -200.0f));
            mWalkList.AddLast(new Vector3(0.0f, 0.0f, 25.0f));

            robotList = new List<Agent>();
            for (int i = 0; i < _AgentsNumber/3; i = i+4)
            {

                Agent builder = new Agent("robot.mesh",SceneManager, "Robot" + i.ToString(), mWalkList, mWalkSpeed, i, new Builder());
                robotList.Add(builder);
                Agent manager = new Agent("ninja.mesh", SceneManager, "Robot" + (i + 1).ToString(), mWalkList, mWalkSpeed, i + 1, new Manager());
                robotList.Add(manager);
                Agent drag = new Agent("robot.mesh", SceneManager, "Robot" + (i + 2).ToString(), mWalkList, mWalkSpeed, i + 2, new Drag());
                robotList.Add(drag);
                Agent idler = new Agent("robot.mesh", SceneManager, "Robot" + (i + 3).ToString(), mWalkList, mWalkSpeed, i + 3,new Idler());
                robotList.Add(idler);

            }

            //Console.WriteLine("passer");

            TMProbotList = new List<Agent>(robotList);
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

            new DefaultInputHandler(this);
            this.Root.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);


        }

        bool FrameStarted(FrameEvent evt)
        {

            TMProbotList = new List<Agent>(robotList);

            do
            {
                int tmp = rand.Next(0, TMProbotList.Count);
                TMProbotList[tmp].animation("Walk");
                TMProbotList[tmp].MComportement.Comportement(evt, rand, TMProbotList[tmp]);
                TMProbotList.Remove(TMProbotList[tmp]);
            } while (TMProbotList.Count > 0);

            return true;
        }
    }
}
