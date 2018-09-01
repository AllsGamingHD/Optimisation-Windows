using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;

namespace Optimisation_Windows_Capet.Base
{
    public partial class DDUCommand : DevExpress.XtraEditors.XtraUserControl
    {
        public DDUCommand()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var process = new Process // Ouvre le processus DDU avec ces arguments
            {
                StartInfo =
                    {
                        FileName = @"C:\Temp Display Drivers Uninstaller\Display Driver Uninstaller.exe",
                        Arguments = textBox1.Text
                    }
            };
            process.Start(); ;
            this.Visible = false; // La ControlUser "DDUCommand" va devenir invisible
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e) // Je vous explique juste sur cette checkEdit2 c'est pareilles pour les autres sauf que c'est pas " -createsystemrestorepoint " mais des autres arguments
        {
            if (checkEdit2.Checked == true) // Si la checkEdit 2 est cocher il fait l'action
            {
                if (textBox1.Text.Contains("-createsystemrestorepoint")) // Si la textBox1 contient " -createsystemrestorepoint " elle fait le code ci-dessous
                {
                    // pas de code car pas besoin
                }
                else
                {
                    textBox1.Text = " -createsystemrestorepoint " + textBox1.Text; // Ajoute de " -createsystemrestorepoint " + le texte qui était déjà dans la textBox avant
                }
            }
            else // Si non faire cette action
            {
                if (textBox1.Text.Contains(" -createsystemrestorepoint ")) // Si la textBox1 contient " -createsystemrestorepoint" elle fait le code ci-dessous
                {
                    textBox1.Text = textBox1.Text.Replace(" -createsystemrestorepoint ", ""); // Remplacement de " -createsystemrestorepoint" par ""
                }
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked == true)
            {
                if (textBox1.Text.Contains("-nosafemode"))
                {

                }
                else
                {
                    textBox1.Text = " -nosafemode " + textBox1.Text;
                }
            }
            else
            {
                if (textBox1.Text.Contains(" -nosafemode "))
                {
                    textBox1.Text = textBox1.Text.Replace(" -nosafemode ", "");
                }
            }
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit4.Checked == true)
            {
                if (textBox1.Text.Contains("-restart"))
                {

                }
                else
                {
                    textBox1.Text = " -restart " + textBox1.Text;
                }
            }
            else
            {
                if (textBox1.Text.Contains(" -restart "))
                {
                    textBox1.Text = textBox1.Text.Replace(" -restart ", "");
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                if (textBox1.Text.Contains("-silent"))
                {

                }
                else
                {
                    textBox1.Text = " -silent " + textBox1.Text;
                }
            }
            else
            {
                if (textBox1.Text.Contains( "-silent "))
                {
                    textBox1.Text = textBox1.Text.Replace(" -silent ", "");
                }
            }
        }

        private void DDUCommand_Load(object sender, EventArgs e)
        {
                checkEdit1.Text = "Désinstallation silencieuse";
                checkEdit2.Text = "Créer un point de restauration";
                checkEdit3.Text = "Pas de mode sans échec";
                checkEdit4.Text = "Redémarrer le PC après le clean";
        }
    }
}
