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
            ((System.ComponentModel.ISupportInitialize)(this.AgentsTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationLast)).BeginInit();
            this.SuspendLayout();
            // 
            // AgentsTrackBar
            // 
            this.AgentsTrackBar.Location = new System.Drawing.Point(49, 90);
            this.AgentsTrackBar.Maximum = 100;
            this.AgentsTrackBar.Name = "AgentsTrackBar";
            this.AgentsTrackBar.Size = new System.Drawing.Size(330, 45);
            this.AgentsTrackBar.TabIndex = 0;
            this.AgentsTrackBar.TickFrequency = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of agents";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(140, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Durée de la simulation";
            // 
            // SimulationLast
            // 
            this.SimulationLast.Location = new System.Drawing.Point(49, 229);
            this.SimulationLast.Maximum = 60;
            this.SimulationLast.Name = "SimulationLast";
            this.SimulationLast.Size = new System.Drawing.Size(330, 45);
            this.SimulationLast.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 300);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Lancer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SimulationSetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 369);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SimulationLast);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AgentsTrackBar);
            this.Name = "SimulationSetUp";
            this.Text = "SimulationSetUp";
            ((System.ComponentModel.ISupportInitialize)(this.AgentsTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SimulationLast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar AgentsTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar SimulationLast;
        private System.Windows.Forms.Button button1;
    }
}