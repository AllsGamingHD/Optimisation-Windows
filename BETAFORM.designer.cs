namespace Optimisation_Windows_Capet.Base.UserControl
{
    partial class BETAFORM
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
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, null, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BETAFORM));
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.progressPanel2 = new DevExpress.XtraWaitForm.ProgressPanel();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit3 = new DevExpress.XtraEditors.CheckEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new System.Drawing.Point(12, 41);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 13F);
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEdit1.Size = new System.Drawing.Size(206, 28);
            this.textEdit1.TabIndex = 0;
            this.textEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(12, 163);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(206, 28);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Connexion";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            this.simpleButton1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 14F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(59, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(113, 23);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Nom d\'utilisateur :";
            this.labelControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // textEdit2
            // 
            this.textEdit2.EditValue = "";
            this.textEdit2.Location = new System.Drawing.Point(12, 104);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 13F);
            this.textEdit2.Properties.Appearance.Options.UseFont = true;
            this.textEdit2.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEdit2.Properties.UseSystemPasswordChar = true;
            this.textEdit2.Size = new System.Drawing.Size(206, 28);
            this.textEdit2.TabIndex = 2;
            this.textEdit2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Bahnschrift Condensed", 14F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(72, 75);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(87, 23);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Mot de passe :";
            this.labelControl2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // textEdit3
            // 
            this.textEdit3.EditValue = "";
            this.textEdit3.Location = new System.Drawing.Point(12, 222);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.Appearance.Options.UseTextOptions = true;
            this.textEdit3.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEdit3.Properties.ReadOnly = true;
            this.textEdit3.Properties.UseSystemPasswordChar = true;
            this.textEdit3.Size = new System.Drawing.Size(206, 20);
            this.textEdit3.TabIndex = 6;
            this.textEdit3.ToolTip = "Veuillez me donner ce que contient cette textBox pour que vous puissiez accéder à" +
    " l\'application BETA";
            this.textEdit3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.textEdit3.ToolTipTitle = "HWID Unique par utilisateur";
            this.textEdit3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // progressPanel1
            // 
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel1.AppearanceCaption.Options.UseFont = true;
            this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel1.AppearanceDescription.Options.UseFont = true;
            this.progressPanel1.BarAnimationElementThickness = 2;
            this.progressPanel1.Caption = "Veuillez patientez";
            this.progressPanel1.Description = "Chargement...";
            this.progressPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressPanel1.ImageHorzOffset = 20;
            this.progressPanel1.Location = new System.Drawing.Point(0, 17);
            this.progressPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.Size = new System.Drawing.Size(246, 39);
            this.progressPanel1.TabIndex = 0;
            this.progressPanel1.Text = "progressPanel1";
            // 
            // progressPanel2
            // 
            this.progressPanel2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel2.Appearance.Options.UseBackColor = true;
            this.progressPanel2.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.progressPanel2.AppearanceCaption.Options.UseFont = true;
            this.progressPanel2.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.progressPanel2.AppearanceDescription.Options.UseFont = true;
            this.progressPanel2.BarAnimationElementThickness = 2;
            this.progressPanel2.Caption = "Veuillez patientez";
            this.progressPanel2.Description = "Chargement...";
            this.progressPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressPanel2.ImageHorzOffset = 20;
            this.progressPanel2.Location = new System.Drawing.Point(0, 0);
            this.progressPanel2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.progressPanel2.Name = "progressPanel2";
            this.progressPanel2.Size = new System.Drawing.Size(230, 271);
            this.progressPanel2.TabIndex = 4;
            this.progressPanel2.Text = "progressPanel2";
            this.progressPanel2.Visible = false;
            this.progressPanel2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(33, 138);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Sauvegardez les identifiants ?";
            this.checkEdit1.Size = new System.Drawing.Size(164, 19);
            this.checkEdit1.TabIndex = 3;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            this.checkEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(165, 80);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "Voir ?";
            this.checkEdit2.Size = new System.Drawing.Size(53, 19);
            this.checkEdit2.TabIndex = 1;
            this.checkEdit2.CheckedChanged += new System.EventHandler(this.checkEdit2_CheckedChanged);
            this.checkEdit2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // checkEdit3
            // 
            this.checkEdit3.Location = new System.Drawing.Point(71, 197);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "Voir le HWID ?";
            this.checkEdit3.Size = new System.Drawing.Size(89, 19);
            this.checkEdit3.TabIndex = 5;
            this.checkEdit3.CheckedChanged += new System.EventHandler(this.checkEdit3_CheckedChanged);
            this.checkEdit3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Version actuel : 1.0.0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Admin :";
            // 
            // BETAFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 271);
            this.Controls.Add(this.progressPanel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkEdit3);
            this.Controls.Add(this.checkEdit2);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.textEdit1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Metropolis Dark";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BETAFORM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log BETA";
            this.Load += new System.EventHandler(this.BETAFORM_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BETAFORM_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel1;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel2;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.CheckEdit checkEdit3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}