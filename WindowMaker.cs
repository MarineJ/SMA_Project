using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using System.Drawing;
using System.Windows.Forms;

namespace SMA_Project_V1
{
    class WindowMaker : Form
    {

        // Fields
   // private IContainer components;
    private Camera mCamera;
    private Root mRoot;
    private SceneManager mSceneMgr;
    private Viewport mViewport;
    private RenderWindow mWindow;

    // Events
    public event SceneEventHandler SceneCreating;

    // Methods
    public WindowMaker()
    {
        this.InitializeComponent();
     //   base.Icon = Resources.OgreHead;
    }

    protected virtual void CreateCamera()
    {
        this.mCamera = this.mSceneMgr.CreateCamera("MainCamera");
        this.mCamera.NearClipDistance = 1f;
        this.mCamera.Position = new Vector3(0f, 0f, 300f);
        this.mCamera.LookAt(Vector3.ZERO);
    }

    protected virtual void CreateInputHandler()
    {
    }

    protected virtual void CreateRenderWindow(IntPtr handle)
    {
        this.mRoot.Initialise(false, "Main Ogre Window");
        NameValuePairList miscParams = new NameValuePairList();
        if (handle != IntPtr.Zero)
        {
            miscParams["externalWindowHandle"] = handle.ToString();
            this.mWindow = this.mRoot.CreateRenderWindow("Autumn main RenderWindow", 800, 600, false, miscParams);
        }
        else
        {
            this.mWindow = this.mRoot.CreateRenderWindow("Autumn main RenderWindow", 800, 600, false);
        }
    }

    protected virtual void CreateSceneManager()
    {
        this.mSceneMgr = this.mRoot.CreateSceneManager(SceneType.ST_GENERIC, "Main SceneManager");
    }

    protected virtual void CreateViewport()
    {
        this.mViewport = this.mWindow.AddViewport(this.mCamera);
        this.mViewport.BackgroundColour = new ColourValue(0f, 0f, 0f, 1f);
    }

    protected override void Dispose(bool disposing)
    {
    /*    if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);*/
    }

    public void Go()
    {
        if (this.mRoot == null)
        {
            this.InitializeOgre();
        }
        base.Show();
        bool flag = true;
        while (flag && (this.mRoot != null))
        {
            flag = this.mRoot.RenderOneFrame();
            Application.DoEvents();
        }
    }

    private void InitializeComponent()
    {
        base.SuspendLayout();
        base.AutoScaleDimensions = new SizeF(6f, 13f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x318, 0x23d);
        base.FormBorderStyle = FormBorderStyle.Fixed3D;
        base.MaximizeBox = false;
        base.Name = "OgreWindow";
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Mogre Render Window";
        base.ResumeLayout(false);
    }

    public void InitializeOgre()
    {
        if (this.mRoot != null)
        {
            throw new Exception("Ogre is already initialized!");
        }
//        Splash splash = new Splash();
//        splash.Show();
        try
        {
//            splash.Increment("Creating the root object...");
            this.mRoot = new Root();
 //           splash.Increment("Loading resources...");
            this.InitResources();
 //           splash.Increment("Setting up DirectX...");
            this.SetupDirectX();
  //          splash.Increment("Creating the window...");
            this.CreateRenderWindow(base.Handle);
  //          splash.Increment("Initializing resources...");
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
   //         splash.Increment("Creating Ogre objects...");
            this.CreateSceneManager();
            this.CreateCamera();
            this.CreateViewport();
     //       splash.Increment("Creating input handler...");
            this.CreateInputHandler();
      //      splash.Increment("Creating scene...");
            base.Disposed += new EventHandler(this.OgreWindow_Disposed);
            this.OnSceneCreating();
        }
        finally
        {
        //    splash.Close();
        //    splash.Dispose();
        }
    }

    protected virtual void InitResources()
    {
        ConfigFile file = new ConfigFile();
        file.Load("resources.cfg", "\t:=", true);
        ConfigFile.SectionIterator sectionIterator = file.GetSectionIterator();
        while (sectionIterator.MoveNext())
        {
            string currentKey = sectionIterator.CurrentKey;
            foreach (KeyValuePair<string, string> pair in sectionIterator.Current)
            {
                string key = pair.Key;
                string name = pair.Value;
                ResourceGroupManager.Singleton.AddResourceLocation(name, key, currentKey);
            }
        }
    }

    private void OgreWindow_Disposed(object sender, EventArgs e)
    {
        this.mRoot.Dispose();
        this.mRoot = null;
        this.mWindow = null;
        this.mCamera = null;
        this.mViewport = null;
        this.mSceneMgr = null;
    }

    protected virtual void OnSceneCreating()
    {
        if (this.SceneCreating != null)
        {
          //  this.SceneCreating(this);
        }
    }

    private void SetupDirectX()
    {
        RenderSystem renderSystemByName = this.mRoot.GetRenderSystemByName("Direct3D9 Rendering Subsystem");
        this.mRoot.RenderSystem = renderSystemByName;
        renderSystemByName.SetConfigOption("Full Screen", "No");
        renderSystemByName.SetConfigOption("Video Mode", "800 x 600 @ 32-bit colour");
    }

    // Properties
    public Camera Camera
    {
        get
        {
            return this.mCamera;
        }
        protected set
        {
            this.mCamera = value;
        }
    }

    public RenderWindow RenderWindow
    {
        get
        {
            return this.mWindow;
        }
        protected set
        {
            this.mWindow = value;
        }
    }

    public Root Root
    {
        get
        {
            return this.mRoot;
        }
    }

    public SceneManager SceneManager
    {
        get
        {
            return this.mSceneMgr;
        }
        protected set
        {
            this.mSceneMgr = value;
        }
    }

    public Viewport Viewport
    {
        get
        {
            return this.mViewport;
        }
        protected set
        {
            this.mViewport = value;
        }
    }

    // Nested Types
    public delegate void SceneEventHandler(WindowMaker win);
    }
}
