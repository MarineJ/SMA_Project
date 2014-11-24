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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SimulationSetUp());

            }
        }
        #endregion
 
}
