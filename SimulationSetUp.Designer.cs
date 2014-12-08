namespace SMA_Project_V1
{
    partial class SimulationSetUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AgentsTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SimulationLast = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.CurrentAgentsNumber = new System.Windows.Forms.Label();
            this.CurrentLastSimulation = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.Label();
            this.TimeTrackBar = new System.Windows.Forms.TrackBar();
            this.TimeValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AgentsTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationLast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // AgentsTrackBar
            // 
            this.AgentsTrackBar.Location = new System.Drawing.Point(49, 92);
            this.AgentsTrackBar.Maximum = 100;
            this.AgentsTrackBar.Name = "AgentsTrackBar";
            this.AgentsTrackBar.Size = new System.Drawing.Size(330, 45);
            this.AgentsTrackBar.TabIndex = 0;
            this.AgentsTrackBar.TickFrequency = 10;
            this.AgentsTrackBar.Value = 30;
            this.AgentsTrackBar.Scroll += new System.EventHandler(this.AgentsTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(61, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of agents : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(61, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Simulation Last (min):";
            // 
            // SimulationLast
            // 
            this.SimulationLast.Location = new System.Drawing.Point(43, 348);
            this.SimulationLast.Maximum = 60;
            this.SimulationLast.Name = "SimulationLast";
            this.SimulationLast.Size = new System.Drawing.Size(330, 45);
            this.SimulationLast.TabIndex = 3;
            this.SimulationLast.Value = 30;
            this.SimulationLast.Scroll += new System.EventHandler(this.SimulationLast_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(179, 429);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CurrentAgentsNumber
            // 
            this.CurrentAgentsNumber.AutoSize = true;
            this.CurrentAgentsNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentAgentsNumber.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CurrentAgentsNumber.Location = new System.Drawing.Point(234, 47);
            this.CurrentAgentsNumber.Name = "CurrentAgentsNumber";
            this.CurrentAgentsNumber.Size = new System.Drawing.Size(30, 24);
            this.CurrentAgentsNumber.TabIndex = 5;
            this.CurrentAgentsNumber.Text = "30";
            // 
            // CurrentLastSimulation
            // 
            this.CurrentLastSimulation.AutoSize = true;
            this.CurrentLastSimulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentLastSimulation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CurrentLastSimulation.Location = new System.Drawing.Point(255, 302);
            this.CurrentLastSimulation.Name = "CurrentLastSimulation";
            this.CurrentLastSimulation.Size = new System.Drawing.Size(30, 24);
            this.CurrentLastSimulation.TabIndex = 6;
            this.CurrentLastSimulation.Text = "30";
            // 
            // Time
            // 
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Time.Location = new System.Drawing.Point(61, 165);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(158, 24);
            this.Time.TabIndex = 7;
            this.Time.Text = "Time speed (%) : ";
            // 
            // TimeTrackBar
            // 
            this.TimeTrackBar.Location = new System.Drawing.Point(49, 226);
            this.TimeTrackBar.Maximum = 300;
            this.TimeTrackBar.Name = "TimeTrackBar";
            this.TimeTrackBar.Size = new System.Drawing.Size(330, 45);
            this.TimeTrackBar.TabIndex = 8;
            this.TimeTrackBar.TickFrequency = 10;
            this.TimeTrackBar.Value = 100;
            this.TimeTrackBar.Scroll += new System.EventHandler(this.TimeTrackBar_Scroll);
            // 
            // TimeValue
            // 
            this.TimeValue.AutoSize = true;
            this.TimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeValue.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TimeValue.Location = new System.Drawing.Point(225, 165);
            this.TimeValue.Name = "TimeValue";
            this.TimeValue.Size = new System.Drawing.Size(40, 24);
            this.TimeValue.TabIndex = 9;
            this.TimeValue.Text = "100";
            // 
            // SimulationSetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(436, 502);
            this.Controls.Add(this.TimeValue);
            this.Controls.Add(this.TimeTrackBar);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.CurrentLastSimulation);
            this.Controls.Add(this.CurrentAgentsNumber);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SimulationLast);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AgentsTrackBar);
            this.Name = "SimulationSetUp";
            this.Text = "SimulationSetUp";
            ((System.ComponentModel.ISupportInitialize)(this.AgentsTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationLast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar AgentsTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar SimulationLast;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label CurrentAgentsNumber;
        private System.Windows.Forms.Label CurrentLastSimulation;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.TrackBar TimeTrackBar;
        private System.Windows.Forms.Label TimeValue;
    }
}