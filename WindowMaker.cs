using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using System.Drawing;
using System.Windows.Forms;

namespace SMA_Project_V1
{
    public class WindowMaker : Form
    {

   // Fields
   // private IContainer components;
    private Camera mCamera;
    private Root mRoot;
    private SceneManager mSceneMgr;
    private Viewport mViewport;
    private RenderWindow mWindow;
    private Overlay mOverlay;


    // Events
    public event SceneEventHandler SceneCreating;

    // Methods
    public WindowMaker()
    {
        this.InitializeComponent();
        this.Size = new Size(900, 700);
        Disposed += new EventHandler(OgreForm_Disposed);
        Resize += new EventHandler(OgreForm_Resize);
    }
    protected virtual void CreateCamera(){}

    protected virtual void CreateInputHandler() { }

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

    protected virtual void ChooseSceneManager(){}

    protected virtual void CreateSceneManager(){}

    protected virtual void CreateViewport(){}

    protected virtual void CreateAgents(){}

    protected virtual void CreateCubes() { }

    protected virtual void CreateOverlay(){}
        
    protected override void Dispose(bool disposing)
    {
    /*    if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);*/
    }
    void OgreForm_Resize(object sender, EventArgs e)
    {
        mWindow.WindowMovedOrResized();
    }

    void OgreForm_Disposed(object sender, EventArgs e)
    {
        mRoot.Dispose();
        mRoot = null;
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
        try
        {

            this.mRoot = new Root();
            this.InitResources();
            this.SetupDirectX();
            this.CreateRenderWindow(base.Handle);
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
            this.ChooseSceneManager();
            this.CreateSceneManager();
            this.CreateCamera();
            this.CreateViewport();
            this.CreateInputHandler();
            this.CreateOverlay();
            this.CreateAgents();
            this.CreateCubes();
            base.Disposed += new EventHandler(this.OgreWindow_Disposed);
            this.OnSceneCreating();
        }
        finally
        {

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
    public Overlay Overlay
    {
        get
        {
            return this.mOverlay;
        }
        protected set
        {
            this.mOverlay = value;
        }
    }

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
