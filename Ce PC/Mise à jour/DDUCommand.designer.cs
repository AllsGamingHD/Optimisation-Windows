namespace Optimisation_Windows_Capet.Base
{
    partial class DDUCommand
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
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit3 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit4 = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(17, 15);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Désintallation Silencieuse";
            this.checkEdit1.Size = new System.Drawing.Size(173, 19);
            this.checkEdit1.TabIndex = 1;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(17, 40);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "Créer un point de restauration";
            this.checkEdit2.Size = new System.Drawing.Size(173, 19);
            this.checkEdit2.TabIndex = 1;
            this.checkEdit2.CheckedChanged += new System.EventHandler(this.checkEdit2_CheckedChanged);
            // 
            // checkEdit3
            // 
            this.checkEdit3.Location = new System.Drawing.Point(17, 65);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "Pas de mode sans échec";
            this.checkEdit3.Size = new System.Drawing.Size(173, 19);
            this.checkEdit3.TabIndex = 1;
            this.checkEdit3.CheckedChanged += new System.EventHandler(this.checkEdit3_CheckedChanged);
            // 
            // checkEdit4
            // 
            this.checkEdit4.Location = new System.Drawing.Point(17, 90);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "Rédémarrer le PC après le clean";
            this.checkEdit4.Size = new System.Drawing.Size(173, 19);
            this.checkEdit4.TabIndex = 1;
            this.checkEdit4.CheckedChanged += new System.EventHandler(this.checkEdit4_CheckedChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(13, 118);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(173, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Exécuter";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 154);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 130);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "-cleannvidia";
            this.textBox1.Visible = false;
            // 
            // DDUCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.checkEdit4);
            this.Controls.Add(this.checkEdit3);
            this.Controls.Add(this.checkEdit2);
            this.Controls.Add(this.checkEdit1);
            this.LookAndFeel.SkinName = "Metropolis Dark";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "DDUCommand";
            this.Size = new System.Drawing.Size(201, 155);
            this.Load += new System.EventHandler(this.DDUCommand_Load);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.CheckEdit checkEdit3;
        private DevExpress.XtraEditors.CheckEdit checkEdit4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
    }
}
