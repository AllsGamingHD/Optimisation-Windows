namespace Optimisation_Windows_Capet
{
    partial class Download_Driver
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lblSize = new DevExpress.XtraEditors.LabelControl();
            this.lblSpeed = new DevExpress.XtraEditors.LabelControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Location = new System.Drawing.Point(7, 51);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Properties.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F);
            this.progressBarControl1.Size = new System.Drawing.Size(494, 20);
            this.progressBarControl1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(153, 135);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(205, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Stop";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lblSize
            // 
            this.lblSize.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F);
            this.lblSize.Appearance.Options.UseFont = true;
            this.lblSize.Location = new System.Drawing.Point(253, 77);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(3, 23);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = " ";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F);
            this.lblSpeed.Appearance.Options.UseFont = true;
            this.lblSpeed.Location = new System.Drawing.Point(253, 106);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(3, 23);
            this.lblSpeed.TabIndex = 2;
            this.lblSpeed.Text = " ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 14.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(139, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(230, 23);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = " Téléchargement du pilotes en cours :";
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            // 
            // Download_Driver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.progressBarControl1);
            this.LookAndFeel.SkinName = "Metropolis Dark";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Download_Driver";
            this.Size = new System.Drawing.Size(508, 180);
            this.Load += new System.EventHandler(this.Download_Driver_Load);
            this.VisibleChanged += new System.EventHandler(this.Download_Driver_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl lblSize;
        private DevExpress.XtraEditors.LabelControl lblSpeed;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Timer timer2;
    }
}
