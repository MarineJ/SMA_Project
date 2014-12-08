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
                BuildingSimulation win = new BuildingSimulation(AgentsTrackBar.Value, (float)TimeTrackBar.Value / (float)100);
                this.Close();
                win.Go();
              
            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                if (OgreException.IsThrown)
                {
                    MessageBox.Show(OgreException.LastException.FullDescription, "An Ogre exception has occurred!");  
                }
                else
                    throw;
            }
          
        }

        private void AgentsTrackBar_Scroll(object sender, EventArgs e)
        {
            CurrentAgentsNumber.Text = AgentsTrackBar.Value.ToString();

        }

        private void SimulationLast_Scroll(object sender, EventArgs e)
        {
            CurrentLastSimulation.Text = SimulationLast.Value.ToString();
        }

        private void TimeTrackBar_Scroll(object sender, EventArgs e)
        {
            TimeValue.Text = TimeTrackBar.Value.ToString();
        }

    }
}
