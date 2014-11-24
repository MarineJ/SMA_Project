using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mogre;

namespace SMA_Project_V1
{
    public partial class SimulationSetUp : Form
    {
        public SimulationSetUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

            this.Close();
        }

    }
}
