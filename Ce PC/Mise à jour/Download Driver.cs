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
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;

namespace Optimisation_Windows_Capet
{
    public partial class Download_Driver : DevExpress.XtraEditors.XtraUserControl
    {
        WebClient webClient;
        Stopwatch sw = new Stopwatch();
        public string urlAddress = "";
        public string location = Application.StartupPath + @"\NvidiaLastDrivers.exe";

        public Download_Driver()
        {
            InitializeComponent();
        }

        public void DownloadFile()
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                // Assigner l'url du drivers à URL
                Uri URL = new Uri(urlAddress);

                // Démarrer le stopwatch pour le calcul de la vitesse de téléchargement
                sw.Start();

                try
                {
                    // Démarrer le téléchargement
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Permet de "convertir" les Ko en Mo ou en Go ( ex : à partir de 1024 Ko sa passera à 1 Mo et à partir de 1024 Mo sa passera à 1 Go ) Nous ont à pas l'utilité du Go ici car le drivers fait moins de 1Go ^^
        private string FormatBytes(long bytes, int decimalPlaces, bool showByteType) 
        {
            double num = bytes;
            string format = "{0";
            string str2 = "B";
            if ((num > 1024.0) && (num < 1048576.0))
            {
                num /= 1024.0;
                str2 = "Ko";
            }
            else if ((num > 1048576.0) && (num < 1073741824.0))
            {
                num /= 1048576.0;
                str2 = "Mo";
            }
            else
            {
                num /= 1073741824.0;
                str2 = "Go";
            }
            if (decimalPlaces > 0)
            {
                format = format + ":0.";
            }
            for (int i = 0; i < decimalPlaces; i++)
            {
                format = format + "0";
            }
            format = format + "}";
            if (showByteType)
            {
                format = format + str2;
            }
            return string.Format(format, num);
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Calcul de la vitesse de téléchargement et assignée au lblSpeed
            long speedd = Convert.ToInt32((e.BytesReceived  / sw.Elapsed.TotalSeconds));
            lblSpeed.Text = string.Format("{0} /s", FormatBytes(speedd, 1, true));

            // Mise à jour du % de la progressBar au même % que le téléchargement actuel
            progressBarControl1.EditValue = e.ProgressPercentage;

            // Mets à jour le label avec la taille télécharger sur la taille total avec le pourcentage téléchargé.
            lblSize.Text = string.Format("{0} MO's / {1} MO's " + e.ProgressPercentage.ToString() + "%",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.0"));
            
            // centre les label
            lblSize.Left = (this.ClientSize.Width - lblSize.Size.Width) / 2; 
            lblSpeed.Left = (this.ClientSize.Width - lblSpeed.Size.Width) / 2;
            labelControl2.Left = (this.ClientSize.Width - labelControl2.Size.Width) / 2; 


        }

        private async void Completed(object sender, AsyncCompletedEventArgs e)
        {
            // Remet à zéro la stopwatch
            sw.Reset();

            // MessageBox si le téléchargement et arreter
            if (e.Cancelled == true)
            {
                sw.Stop();
                sw.Reset();
                progressBarControl1.EditValue = 0;
                XtraMessageBox.Show("Téléchargement arreté après votre demande !\nLe fichier à était supprimée avec succèes", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else // Si le téléchargement n'est pas arreter mais qu'il est termninée de télécharger
            {
                // Attente de 2.5 Secondes avec l'éxécution de la suite du code
                await Task.Delay(2500);
                // Ouvre une MessageBox avec la question et "Oui" / "Non"
                DialogResult dialogResult = XtraMessageBox.Show("Téléchargement du pilotes terminée !\nVoulez-vous installer le pilotes directement (Oui) ou desinstaller l'ancien pilote proprement et reinstaller le nouveau pour faire une installation propre (No)", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information); // ouvre une message box
                                                                                                                                                                                                                                                                                                                                   // Si oui faire le code ci-dessous
                if (dialogResult == DialogResult.Yes)
                {
                    // Ouverture du pilotes en mode installation silencieuse
                    var process = new Process
                    {
                        StartInfo =
                            {
                                FileName = Application.StartupPath.ToString() + @"\NvidiaLastDrivers.exe",
                                Arguments = "/s"
                            }
                    };
                    process.Start();
                    // L'usercontrol Download Driver passe de visible à invisible.
                    this.Visible = false;
                    // Ouverture d'un CMD sans fenêtre avec la command pour supprimer le dossier NvidiaDriverChecker
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();

                    cmd.StandardInput.WriteLine(@"RMDIR /S /Q " + "\u0022" + Application.StartupPath.ToString() + @"\NvidiaDriverChecker\" + "\u0022\n");
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.Close();
                    cmd.WaitForExit();
                    Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                }
                // Si non faire le code ci-dessous
                else if (dialogResult == DialogResult.No)
                {
                    //Définir la propriétés DownDDULaunch sur 1 pour la récupérer dans la Form1
                    Optimisation_Windows_Capet.Properties.Settings.Default.DownDDULaunch = "1";
                    // L'usercontrol Download Driver passe de visible à invisible.
                    this.Visible = false;
                    // Ouverture d'un CMD sans fenêtre avec la command pour supprimer le dossier NvidiaDriverChecker
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();

                    cmd.StandardInput.WriteLine(@"RMDIR /S /Q " + "\u0022" + Application.StartupPath.ToString() + @"\NvidiaDriverChecker\" + "\u0022\n");
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.Close();
                    cmd.WaitForExit();
                    Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                }
            }
        }

        private void Download_Driver_Load(object sender, EventArgs e)
        {
            // Démarrage du timer1
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Si le fichier existe dans l'emplacement défini
            if (File.Exists(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\DownloadLink.ini")) 
            {
                // Arret du timer 1 car il à détecter le fichier comme existant
                timer1.Stop();
                // Si possible faire ce code
                try
                {   // Ouverture du fichier texte à l'emplacement defini
                    using (StreamReader srr = new StreamReader(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\DownloadLink.ini")) 
                    {
                        // lecture du fichier et tranformation en string
                        String line = srr.ReadToEnd();
                        // Le string urlAdress devient le texte qui était dans le fichier texte
                        urlAddress = line;
                        // Appel de la fonction pour lancer le téléchargement
                        DownloadFile();
                        // Changement du texte du labelControl2 et devient visible
                        labelControl2.Text = "Téléchargement du pilotes en cours...";
                        labelControl2.Visible = true;
                        // Les label deviennent visibles
                        lblSize.Visible = true;
                        lblSpeed.Visible = true;
                    }
                }
                // Si une erreur ( execption ) est détecté
                catch (Exception ex) 
                {
                    // Si le fichier n'est pas trouvée alors éxécuter le code
                    if (ex is FileNotFoundException) 
                    {
                        // Arret du timer 1
                        timer1.Stop();
                        // Ouverture d'une MessageBox pour informées que le fichier n'est pas Récupérables
                        XtraMessageBox.Show("Données irrécupérables", "Informations GPU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // si non faire celui-ci
                    else 
                    {
                        // Arret du timer 1
                        timer1.Stop();
                        // Ouverture d'une MessageBox pour informés que une erreur est survenu pendant l'obtention du fichier avec les détails de l'erreur
                        XtraMessageBox.Show("Erreur lors de l'obtention du fichier de données" + ex, "Download GPU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        private async void simpleButton1_Click(object sender, EventArgs e) 
        {
            if (simpleButton1.Text == "Stop") // Si bouton "Stop" Cliquez faire le code ci-dessous
            {
                webClient.CancelAsync();
                labelControl2.Text = "Le téléchargement à était arréter";
                lblSize.Visible = false;
                lblSpeed.Visible = false;
                simpleButton1.Text = "Relancer le téléchargement";
                await Task.Delay(3000);
                try
                {
                    File.Delete(location);
                }
                catch (Exception ex)
                {
                        XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else // Si le bouton n'a pas "Stop" comme texte assigné faire le code ci-dessous
            {
                DownloadFile();

                    labelControl2.Text = "Téléchargement du pilotes en cours...";
                    labelControl2.Left = (this.ClientSize.Width - labelControl2.Size.Width) / 2;
                labelControl2.Visible = true;
                lblSize.Visible = true;
                lblSpeed.Visible = true;
                simpleButton1.Text = "Stop";

            }

        }

        private void Download_Driver_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
