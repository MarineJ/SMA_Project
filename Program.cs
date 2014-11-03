using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;

namespace SMA_Project_V1
{
        #region class Program
        static class Program
        {
            [STAThread]
            static void Main()
            {
                try
                {
                    BuildingSimulation win = new BuildingSimulation();
                    win.Go();
                }
                catch (System.Runtime.InteropServices.SEHException)
                {
                    if (OgreException.IsThrown)
                        MessageBox.Show(OgreException.LastException.FullDescription, "An Ogre exception has occurred!");
                    else
                        throw;
                }
            }
        }
        #endregion
 
}
