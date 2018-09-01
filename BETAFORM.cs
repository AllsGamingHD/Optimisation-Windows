using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net.Sockets;
using System.Net;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Reflection;
using System.Threading;
using System.Globalization;

namespace Optimisation_Windows_Capet.Base.UserControl
{
    public partial class BETAFORM : DevExpress.XtraEditors.XtraForm
    {
        UdpClient server = new UdpClient();
        WebClient client = new WebClient();
        string URL = "http://lossantoscityfr.esy.es/";

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public BETAFORM()
        {
            InitializeComponent();
        }

        private void BETAFORM_Load(object sender, EventArgs e)
        {
            label1.Text = "Version actuel : " + string.Format(Optimisation_Windows_Capet.Properties.Settings.Default.CurrentVersion, Assembly.GetEntryAssembly().GetName().Version);
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Metropolis Dark");





            string runas = Convert.ToString(IsAdministrator());
            if (runas == "True")
            {

            }
            else
            {
                XtraMessageBox.Show("Veuillez redémarrer l'application en tant qu'administrateur ! \n\nSi vous n'autorisée par l'accès l'application ne pourras pas fonctionnée comme elle le devrait ! \n\nRégler le raccourci pour qu'il s'éxécute tous le temps en administrateur \n\nClique droit sur l'application -> Onglet 'Compatibilité' -> Coché ' Exécuter en tant qu'administrateur '\n\nOu si vous le faites sur un raccourci : Clique droit sur le raccourci -> Onglet 'Raccourci' -> Avancé -> Coché ' Exécuter en tant qu'administrateur '", "Succès!", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                Application.Exit();
            }
            if (runas == "True")
            {
                label2.Text = "Admin : Activée";
            }
            else
            {
                label2.Text = "Admin : Désactivée";
            }


            if (Optimisation_Windows_Capet.Properties.Settings.Default.CheckBeta == "Unchecked")
            {
                checkEdit1.Checked = false;
            }
            else
            {
                checkEdit1.Checked = true;
                textEdit1.Text = Optimisation_Windows_Capet.Properties.Settings.Default.PassBeta;
                textEdit2.Text = Optimisation_Windows_Capet.Properties.Settings.Default.UserBeta;
                simpleButton1.PerformClick();
            }
            textEdit3.Text = getUniqueID("C");
        }

        private string getUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = getVolumeSerial(drive);
            string cpuID = getCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        private string getVolumeSerial(string drive)
        {
            ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            return volumeSerial;
        }

        private string getCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            progressPanel2.Visible = true;
            if (checkEdit1.Checked == true)
            {
                Optimisation_Windows_Capet.Properties.Settings.Default.CheckBeta = checkEdit1.CheckState.ToString();
                Optimisation_Windows_Capet.Properties.Settings.Default.PassBeta = textEdit1.Text;
                Optimisation_Windows_Capet.Properties.Settings.Default.UserBeta = textEdit2.Text;
                Optimisation_Windows_Capet.Properties.Settings.Default.Save();
            }
            else
            {
                Optimisation_Windows_Capet.Properties.Settings.Default.CheckBeta = "Unchecked";
                Optimisation_Windows_Capet.Properties.Settings.Default.Save();

            }
            await Task.Delay(500);
            try
            {
                string thisguid = getUniqueID("C");
                server.Connect("localhost", 80);
                string loginusername = textEdit1.Text;
                string loginpassword = textEdit2.Text;
                if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x05"))
                {
                   progressPanel2.Visible = false;

                   XtraMessageBox.Show("Vous êtes connectée !\nBonne continuation", "Succès!", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                   Optimisation_Windows_Capet.Form1 F1 = new Optimisation_Windows_Capet.Form1();
                   F1.Show();
                   this.Hide();
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x01"))
                {
                   progressPanel2.Visible = false;


                   XtraMessageBox.Show("HWID ne correspond pas :(", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x02"))
                {
                   progressPanel2.Visible = false;

                   XtraMessageBox.Show("Veuillez remplir toutes les cases.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x03"))
                {
                   progressPanel2.Visible = false;

                   XtraMessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (client.DownloadString(URL + "api.php?type=login&username=" + textEdit1.Text + "&password=" + textEdit2.Text + "&hwid=" + thisguid).Contains("0x04"))
                {
                   progressPanel2.Visible = false;

                   XtraMessageBox.Show("Vous êtes banni.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (WebSocketException)
            {
               progressPanel2.Visible = false;
               XtraMessageBox.Show("Serveur pas disponible", "Erreur Serveur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                textEdit2.Properties.UseSystemPasswordChar = false;
            }
            else
            {
                textEdit2.Properties.UseSystemPasswordChar = true;

            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked == true)
            {
                textEdit3.Properties.UseSystemPasswordChar = false;
            }
            else
            {
                textEdit3.Properties.UseSystemPasswordChar = true;
            }
        }

        private void BETAFORM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                simpleButton1.PerformClick();
            }
        }
    }
}