using AutoUpdaterDotNET;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace Optimisation_Windows_Capet
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        IntPtr appWin1;
        IntPtr appWin2;

        public Form1()
        {
            InitializeComponent();
        }
        public void DisplayFolder(string folderPath)
        {
            listBoxControl2.Items.Clear();
            string[] files = System.IO.Directory.GetFiles(folderPath);

            for (int x = 0; x < files.Length; x++)
            {
                listBoxControl2.Items.Add(files[x]);
            }
        }
        public void DisplayFolder2(string folderPath)
        {
            listBoxControl3.Items.Clear();
            string[] files = System.IO.Directory.GetFiles(folderPath);

            for (int x = 0; x < files.Length; x++)
            {
                listBoxControl3.Items.Add(files[x]);
            }
        }
        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Metropolis Dark");
            UserLookAndFeel.Default.SetSkinStyle("Metropolis Dark");
            labelControl20.Text = "Version actuel : " + string.Format(Properties.Settings.Default.CurrentVersion, Assembly.GetEntryAssembly().GetName().Version);

            progressPanel1.Description = "Création du point de restauration !";

            await Task.Delay(1500);
            if (Properties.Settings.Default.FirstLunchBetamsg == "PasOuvert")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Bienvenue sur l'application ! \n\nTestez la de fond en comble et dîtes moi si vous rencontrer des bugs\n\nSi vous rencontrer un bug merci de m'envoyer un message sur discord \nAccompagner d'une screenshot si possible ou encore une vidéos sur YouTube\n\nOu directement en envoyant un report d'erreur depuis l'application.", @"/!\ Avertissement /!\", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Properties.Settings.Default.FirstLunchBetamsg = "Ouvert";
                Properties.Settings.Default.Save();
            }

            try
            {
                Properties.Settings.Default.REGSystemRespons = Convert.ToString(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", "Erreur"));
                Properties.Settings.Default.REGNDU = Convert.ToString(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Ndu", "Start", "Erreur"));
                Properties.Settings.Default.REGTimeBroker = Convert.ToString(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\TimeBrokerSvc", "Start", "Erreur"));
                Properties.Settings.Default.REGPrefetech = Convert.ToString(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnablePrefetcher", "Erreur"));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DialogResult dialogResult = XtraMessageBox.Show("Voulez-vous créer un point de restauration ?\n\nJe vous recommande dans créer même si vous trouvez sa inutile !\n\nEn cas de problèmes ou de bug sur votre PC vous aurez juste à le restaurer", "Créer un point de restauration ?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            progressPanel1.Visible = true;
            xtraTabControl1.Enabled = false;
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    progressPanel1.Show();
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\SystemRestore", "SystemRestorePointCreationFrequency", 0);
                    Optimisation_Windows_Capet.Base.UserControl.RestorePoint.CreateRestorePoint("");
                    await Task.Delay(1500);
                    xtraTabControl1.Enabled = true;
                    AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr");
                    AutoUpdater.AppTitle = "Une mise à jour est disponible !";
                    AutoUpdater.Start("http://lossantoscityfr.esy.es/OptimisationWindowsMAJ/AutoUpdater.NET.xml");
                    StartAllOnApp();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                try
                {
                    progressPanel1.Hide();
                    xtraTabControl1.Enabled = true;
                    AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr");
                    AutoUpdater.AppTitle = "Une mise à jour est disponible !";
                    AutoUpdater.Start("http://lossantoscityfr.esy.es/OptimisationWindowsMAJ/AutoUpdater.NET.xml");
                    StartAllOnApp();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void StartAllOnApp()
        {
            xtraTabControl1.Enabled = false;
            progressPanel1.Show();

            await Task.Delay(100);

            progressPanel1.Description = "Démarrage de tous les élements de l'application !";

            await Task.Delay(100);

            if (File.Exists(Application.StartupPath.ToString() + @"\DX.xml"))
            {
                ControlFile.Start();
            }
            else
            {
                try
                {

                    textBox1.Text = @"dxdiag.exe /whql:off /dontskip /x " + Application.StartupPath.ToString() + @"\DX.xml" + " | Out-Null";
                    textBox2.Text = @"[xml]$dxDiagLog = Get-Content " + Application.StartupPath.ToString() + @"\DX.xml";
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = @"powershell.exe";
                    startInfo.Arguments = textBox1.Text + Environment.NewLine + textBox2.Text;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();

                    string errors = process.StandardError.ReadToEnd();
                    ControlFile.Start();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            await Task.Delay(100);

            Process[] MyProcess = Process.GetProcesses();
            for (int i = 0; i < MyProcess.Length; i++)
                dataGridView1.Rows.Add(string.Format(MyProcess[i].ProcessName), MyProcess[i].Id, FormatBytes(MyProcess[i].PagedMemorySize, 1, true));

            await Task.Delay(100);

            ServiceController[] localServices = ServiceController.GetServices();
            foreach (ServiceController service in localServices)
            {
                listBoxControl4.Items.Add(service.DisplayName);
            }

            await Task.Delay(100);

            int nbFichiers = Directory.GetFiles(@"C:\Windows\Prefetch", "*.*", SearchOption.AllDirectories).Length - 1;
            int nbFichiers1 = Directory.GetFiles(@"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Local\Temp", "*.*", SearchOption.AllDirectories).Length - 1;
            DisplayFolder(@"C:\Windows\Prefetch");
            labelControl24.Text = string.Format("Poids total de tous les fichiers : {0}" + "  (" + nbFichiers + ")", FormatBytes(DirSize(new DirectoryInfo(@"C:\Windows\Prefetch")), 1, true));

            DisplayFolder2(@"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Local\Temp");
            labelControl30.Text = string.Format("Poids total de tous les fichiers : {0}" + "  (" + nbFichiers1 + ")", FormatBytes(DirSize(new DirectoryInfo(@"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Local\Temp")), 1, true));

            await Task.Delay(100);

            ManagementScope ManScope = new ManagementScope(@"\\localhost\root\default");
            ManagementPath ManPath = new ManagementPath("SystemRestore");
            ObjectGetOptions ManOptions = new ObjectGetOptions();
            ManagementClass ManClass = new ManagementClass(ManScope, ManPath, ManOptions);
            foreach (ManagementObject mo in ManClass.GetInstances())
            {
                string time = mo["CreationTime"].ToString();
                time = time.Remove(14);
                string year = "";
                string day = "";
                string month = "";
                string Heure = "";
                string minutes = "";
                string seconde = "";

                year = time.Remove(4);
                month = time.Remove(6);
                month = month.Substring(4);
                day = time.Remove(8);
                day = day.Substring(6);
                Heure = time.Remove(10);
                Heure = Heure.Substring(8);
                int heuretot = Convert.ToInt32(Heure);
                heuretot = heuretot + 2;
                minutes = time.Remove(12);
                minutes = minutes.Substring(10);
                seconde = time;
                seconde = time.Substring(12);

                time = day + "/" + month + "/" + year + " " + heuretot + ":" + minutes + ":" + seconde;
                //DateTime myDate = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss,fff",
                //                                     System.Globalization.CultureInfo.InvariantCulture);
                //string realtime = myDate.ToString("yyyy-MM-dd HH:mm:ss,fff", CultureInfo.CurrentCulture);
                //20180821180316
                //vssadmin list shadows > C:\test.txt
                dataGridView2.Rows.Add(time, mo["Description"].ToString(), mo["SequenceNumber"]);
            }
            progressPanel1.Hide();
            await Task.Delay(100);
            xtraTabControl1.Enabled = true;

        }
        #region InfosComposants
        protected XMLExplorateur xe = new XMLExplorateur();
        public class XMLExplorateur
        {
            protected XPathDocument docNav;
            protected XPathNavigator nav;
            protected XPathNodeIterator xit;
            protected bool initpath = true;

            public XMLExplorateur() { }

            public XMLExplorateur(String path)
            {
                try
                {
                    docNav = new XPathDocument(path);
                    nav = docNav.CreateNavigator();
                }
                catch
                {
                    docNav = null;
                    nav = null;
                }
            }

            public bool Init(String path)
            {
                try
                {
                    docNav = new XPathDocument(path);
                    nav = docNav.CreateNavigator();
                }
                catch
                {
                    docNav = null;
                    nav = null;
                    return false;
                }
                return true;
            }

            public String ValueOf(String Item)
            {
                if (nav == null) return "Erreur Navigateur null";
                String tmp = "descendant::" + Item;
                try
                {
                    xit = nav.Select(tmp);
                    if (xit.MoveNext()) tmp = xit.Current.Value;
                    else tmp = "null";
                }
                catch
                {
                    tmp = "null";
                }
                return tmp;
            }
        }
        static readonly string[] SizeSuffixes = { "bytes", "Ko", "Mo", "Go", "To", "PB", "EB", "ZB", "YB" };
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

        static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        private void ControlFile_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath.ToString() + @"\DX.xml"))
            {
                ControlFile.Stop();
                xe.Init(Application.StartupPath.ToString() + @"\DX.xml");
                string time = xe.ValueOf("DxDiag/SystemInformation/Time");

                string year = "";
                string day = "";
                string month = "";
                string Heure = "";

                year = time.Remove(9);
                year = year.Substring(5);
                month = time.Remove(1);
                day = time.Remove(4);
                day = day.Substring(2);
                Heure = time.Substring(11);
                if (day.Contains("/"))
                {
                    time = day + month + "/" + year + " à " + Heure;
                }
                else
                {
                    time = day + "/" + month + "/" + year + " à " + Heure;
                }
                labelControl1.Text = "Heure du diagnostics : " + time;
                labelControl1.Left = (this.xtraTabPage3.Width - labelControl1.Size.Width) / 2;

                labelControl2.Text = "Nom de la machine : " + xe.ValueOf("DxDiag/SystemInformation/MachineName");
                labelControl2.Left = (this.xtraTabPage3.Width - labelControl2.Size.Width) / 2;

                labelControl3.Text = ("Systèmes d'exploitation : " + xe.ValueOf("DxDiag/SystemInformation/OperatingSystem"));
                labelControl3.Left = (this.xtraTabPage3.Width - labelControl3.Size.Width) / 2;

                labelControl4.Text = ("Carte mère : " + xe.ValueOf("DxDiag/SystemInformation/SystemManufacturer") + " - " + xe.ValueOf("DxDiag/SystemInformation/SystemModel") + " - V : " + xe.ValueOf("DxDiag/SystemInformation/BIOS"));
                labelControl4.Left = (this.xtraTabPage3.Width - labelControl4.Size.Width) / 2;

                labelControl5.Text = ("Processeur : " + xe.ValueOf("DxDiag/SystemInformation/Processor"));
                labelControl5.Left = (this.xtraTabPage3.Width - labelControl5.Size.Width) / 2;

                ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject obj in myProcessorObject.Get())
                {
                    labelControl6.Text = ("Nombre de coeurs : " + obj["NumberOfCores"] + "  |  " + "Nombre de coeurs activé : " + obj["NumberOfEnabledCore"] + "  |  " + "Nombre de coeurs logiques : " + obj["NumberOfLogicalProcessors"]);
                    labelControl6.Left = (this.xtraTabPage3.Width - labelControl6.Size.Width) / 2;
                }
                labelControl7.Text = "Mémoire RAM : " + xe.ValueOf("DxDiag/SystemInformation/Memory");
                labelControl7.Left = (this.xtraTabPage3.Width - labelControl7.Size.Width) / 2;

                ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
                foreach (ManagementObject obj in myVideoObject.Get())
                {
                    string drvresult = "" + obj["DriverVersion"].ToString().Replace(".", string.Empty).Substring(5);
                    drvresult = drvresult.Substring(0, 3) + "." + drvresult.Substring(3);

                    labelControl8.Text = ("Carte Graphique : " + xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/CardName") + " - " + xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/DedicatedMemory") + " - " + "Version du drivers installée : " + drvresult);
                    labelControl8.Left = (this.xtraTabPage3.Width - labelControl8.Size.Width) / 2;
                }
                labelControl9.Text = "Résolution : " + xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/CurrentMode");
                labelControl9.Left = (this.xtraTabPage3.Width - labelControl9.Size.Width) / 2;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("============================================");
            lBCPUGPU.Items.Add("=================<INFOS>====================");
            lBCPUGPU.Items.Add("===============<PRINCIPAL>==================");
            lBCPUGPU.Items.Add("============================================");
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Nom de la machine==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/MachineName"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Système d'exploitation==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/OperatingSystem"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Langue==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/Language"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Carte mère==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/SystemManufacturer") + " - " + xe.ValueOf("DxDiag/SystemInformation/SystemModel") + " - V : " + xe.ValueOf("DxDiag/SystemInformation/BIOS"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Processeur==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/Processor"));

            ManagementObjectSearcher myProcessorObject1 = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject1.Get())
            {
                lBCPUGPU.Items.Add("Nombre de coeurs : " + obj["NumberOfCores"] + "  |  " + "Nombre de coeurs activé : " + obj["NumberOfEnabledCore"] + "  |  " + "Nombre de coeurs logiques : " + obj["NumberOfLogicalProcessors"]);
            }
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==RAM==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/Memory"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Carte Graphique==");

            ManagementObjectSearcher myVideoObject1 = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject1.Get())
            {
                string drvresult = "" + obj["DriverVersion"].ToString().Replace(".", string.Empty).Substring(5);
                drvresult = drvresult.Substring(0, 3) + "." + drvresult.Substring(3);
                lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/CardName") + " - " + xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/DedicatedMemory") + " - " + "Version du drivers installée : " + drvresult);

            }
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Résolution principal==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/CurrentMode"));
            lBCPUGPU.Items.Add("");
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            foreach (ManagementObject obj2 in myOperativeSystemObject.Get())
            {
                string lastboot = Convert.ToString(obj2["LastBootUpTime"]);
                string timeinstalldate = Convert.ToString(obj2["InstallDate"]);
                timeinstalldate = timeinstalldate.Remove(14);
                string yearinstalldate = "";
                string dayinstalldate = "";
                string monthinstalldate = "";
                string Heureinstalldate = "";
                string minutesinstalldate = "";
                string secondeinstalldate = "";

                yearinstalldate = timeinstalldate.Remove(4);
                monthinstalldate = timeinstalldate.Remove(6);
                monthinstalldate = monthinstalldate.Substring(4);
                dayinstalldate = timeinstalldate.Remove(8);
                dayinstalldate = dayinstalldate.Substring(6);
                Heureinstalldate = timeinstalldate.Remove(10);
                Heureinstalldate = Heureinstalldate.Substring(8);
                int heuretotinstalldate = Convert.ToInt32(Heureinstalldate);
                minutesinstalldate = timeinstalldate.Remove(12);
                minutesinstalldate = minutesinstalldate.Substring(10);
                secondeinstalldate = timeinstalldate;
                secondeinstalldate = timeinstalldate.Substring(12);

                timeinstalldate = dayinstalldate + "/" + monthinstalldate + "/" + yearinstalldate + " " + heuretotinstalldate + ":" + minutesinstalldate + ":" + secondeinstalldate;

                lastboot = lastboot.Remove(14);
                string yearlastboot = "";
                string daylastboot = "";
                string monthlastboot = "";
                string Heurelastboot = "";
                string minuteslastboot = "";
                string secondelastboot = "";

                yearlastboot = lastboot.Remove(4);
                monthlastboot = lastboot.Remove(6);
                monthlastboot = monthlastboot.Substring(4);
                daylastboot = lastboot.Remove(8);
                daylastboot = daylastboot.Substring(6);
                Heurelastboot = lastboot.Remove(10);
                Heurelastboot = Heurelastboot.Substring(8);
                int Heuretotlastboot = Convert.ToInt32(Heurelastboot);
                minuteslastboot = lastboot.Remove(12);
                minuteslastboot = minuteslastboot.Substring(10);
                secondelastboot = lastboot;
                secondelastboot = lastboot.Substring(12);

                lastboot = dayinstalldate + "/" + monthlastboot + "/" + yearlastboot + " " + Heuretotlastboot + ":" + minuteslastboot + ":" + secondelastboot;





                lBCPUGPU.Items.Add("==Clé d'enregistrement==");
                lBCPUGPU.Items.Add(obj2["SerialNumber"]);
                lBCPUGPU.Items.Add("");
                lBCPUGPU.Items.Add("==Windows installé le==");
                lBCPUGPU.Items.Add(timeinstalldate);
                lBCPUGPU.Items.Add("");
                lBCPUGPU.Items.Add("==Date du dernier démarrage du PC==");
                lBCPUGPU.Items.Add(lastboot);
                lBCPUGPU.Items.Add("");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("============================================");
            lBCPUGPU.Items.Add("=================<INFOS>====================");
            lBCPUGPU.Items.Add("===============<PRINCIPAL>==================");
            lBCPUGPU.Items.Add("============================================");
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Nom de la machine==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/MachineName"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Système d'exploitation==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/OperatingSystem"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Langue==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/Language"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Carte mère==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/SystemManufacturer") + " - " + xe.ValueOf("DxDiag/SystemInformation/SystemModel") + " - V : " + xe.ValueOf("DxDiag/SystemInformation/BIOS"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Processeur==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/Processor"));

            ManagementObjectSearcher myProcessorObject1 = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject1.Get())
            {
                lBCPUGPU.Items.Add("Nombre de coeurs : " + obj["NumberOfCores"] + "  |  " + "Nombre de coeurs activé : " + obj["NumberOfEnabledCore"] + "  |  " + "Nombre de coeurs logiques : " + obj["NumberOfLogicalProcessors"]);
            }
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==RAM==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/SystemInformation/Memory"));
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Carte Graphique==");

            ManagementObjectSearcher myVideoObject1 = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject1.Get())
            {
                string drvresult = "" + obj["DriverVersion"].ToString().Replace(".", string.Empty).Substring(5);
                drvresult = drvresult.Substring(0, 3) + "." + drvresult.Substring(3);
                lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/CardName") + " - " + xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/DedicatedMemory") + " - " + "Version du drivers installée : " + drvresult);

            }
            lBCPUGPU.Items.Add("");
            lBCPUGPU.Items.Add("==Résolution principal==");
            lBCPUGPU.Items.Add(xe.ValueOf("DxDiag/DisplayDevices/DisplayDevice/CurrentMode"));
            lBCPUGPU.Items.Add("");
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            foreach (ManagementObject obj2 in myOperativeSystemObject.Get())
            {
                string lastboot = Convert.ToString(obj2["LastBootUpTime"]);
                string timeinstalldate = Convert.ToString(obj2["InstallDate"]);
                timeinstalldate = timeinstalldate.Remove(14);
                string yearinstalldate = "";
                string dayinstalldate = "";
                string monthinstalldate = "";
                string Heureinstalldate = "";
                string minutesinstalldate = "";
                string secondeinstalldate = "";

                yearinstalldate = timeinstalldate.Remove(4);
                monthinstalldate = timeinstalldate.Remove(6);
                monthinstalldate = monthinstalldate.Substring(4);
                dayinstalldate = timeinstalldate.Remove(8);
                dayinstalldate = dayinstalldate.Substring(6);
                Heureinstalldate = timeinstalldate.Remove(10);
                Heureinstalldate = Heureinstalldate.Substring(8);
                int heuretotinstalldate = Convert.ToInt32(Heureinstalldate);
                minutesinstalldate = timeinstalldate.Remove(12);
                minutesinstalldate = minutesinstalldate.Substring(10);
                secondeinstalldate = timeinstalldate;
                secondeinstalldate = timeinstalldate.Substring(12);

                timeinstalldate = dayinstalldate + "/" + monthinstalldate + "/" + yearinstalldate + " " + heuretotinstalldate + ":" + minutesinstalldate + ":" + secondeinstalldate;

                lastboot = lastboot.Remove(14);
                string yearlastboot = "";
                string daylastboot = "";
                string monthlastboot = "";
                string Heurelastboot = "";
                string minuteslastboot = "";
                string secondelastboot = "";

                yearlastboot = lastboot.Remove(4);
                monthlastboot = lastboot.Remove(6);
                monthlastboot = monthlastboot.Substring(4);
                daylastboot = lastboot.Remove(8);
                daylastboot = daylastboot.Substring(6);
                Heurelastboot = lastboot.Remove(10);
                Heurelastboot = Heurelastboot.Substring(8);
                int Heuretotlastboot = Convert.ToInt32(Heurelastboot);
                minuteslastboot = lastboot.Remove(12);
                minuteslastboot = minuteslastboot.Substring(10);
                secondelastboot = lastboot;
                secondelastboot = lastboot.Substring(12);

                lastboot = dayinstalldate + "/" + monthlastboot + "/" + yearlastboot + " " + Heuretotlastboot + ":" + minuteslastboot + ":" + secondelastboot;





                lBCPUGPU.Items.Add("==Clé d'enregistrement==");
                lBCPUGPU.Items.Add(obj2["SerialNumber"]);
                lBCPUGPU.Items.Add("");
                lBCPUGPU.Items.Add("==Windows installé le==");
                lBCPUGPU.Items.Add(timeinstalldate);
                lBCPUGPU.Items.Add("");
                lBCPUGPU.Items.Add("==Date du dernier démarrage du PC==");
                lBCPUGPU.Items.Add(lastboot);
                lBCPUGPU.Items.Add("");
            }

            listBoxControl1.Items.Add("");
            listBoxControl1.Items.Add("============================================");
            listBoxControl1.Items.Add("=================<INFOS>====================");
            listBoxControl1.Items.Add("=================<RESEAU>===================");
            listBoxControl1.Items.Add("============================================");
            listBoxControl1.Items.Add("");
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics1 = NetworkInterface.GetAllNetworkInterfaces();
            if (nics1 == null || nics1.Length < 1)
            {
                listBoxControl1.Items.Add("  Pas de cartes réseau détecté.");
                return;
            }
            listBoxControl1.Items.Add(string.Format("  Nombre de carte réseau .................... : {0}", nics1.Length));
            foreach (NetworkInterface adapter in nics1)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                listBoxControl1.Items.Add("");
                listBoxControl1.Items.Add(adapter.Description);
                listBoxControl1.Items.Add(String.Empty.PadLeft(adapter.Description.Length, '='));
                listBoxControl1.Items.Add(string.Format("  Type d'interface .......................... : {0}", adapter.NetworkInterfaceType));
                listBoxControl1.Items.Add(string.Format("  Adresse physique ........................ : {0}", adapter.GetPhysicalAddress().ToString()));
                listBoxControl1.Items.Add(string.Format("  Status ...................... : {0}", adapter.OperationalStatus));
                string versions = "";

                // Create a display string for the supported IP versions.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }
                listBoxControl1.Items.Add(string.Format("  version IP  .............................. : {0}", versions));
                //ShowIPAddresses(properties);

                // The following information is not useful for loopback adapters.
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }
                listBoxControl1.Items.Add(string.Format("  Suffixe DNS .............................. : {0}", properties.DnsSuffix));
                string label = "";
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPv4InterfaceProperties ipv4 = properties.GetIPv4Properties();
                    listBoxControl1.Items.Add(string.Format("  MTU...................................... : {0}", ipv4.Mtu));
                    if (ipv4.UsesWins)
                    {

                        IPAddressCollection winsServers = properties.WinsServersAddresses;
                        if (winsServers.Count > 0)
                        {
                            label = "  WINS Servers ............................ :";
                            //ShowIPAddresses(label, winsServers);
                        }
                    }
                }
                listBoxControl1.Items.Add(string.Format("  DNS activé ............................. : {0}", properties.IsDnsEnabled));
                listBoxControl1.Items.Add(string.Format("  DNS dynamique automatique .............. : {0}", properties.IsDynamicDnsEnabled));
                listBoxControl1.Items.Add(string.Format("  Réception uniquement ............................ : {0}", adapter.IsReceiveOnly));
                listBoxControl1.Items.Add(string.Format("  Multicast ............................... : {0}", adapter.SupportsMulticast));
                listBoxControl1.Items.Add(String.Empty.PadLeft(adapter.Description.Length, '='));
            }
            listBoxControl1.Items.Add("");
            listBoxControl1.Items.Add("============================================");
            listBoxControl1.Items.Add("=================<INFOS>====================");
            listBoxControl1.Items.Add("=================<DISQUE>===================");
            listBoxControl1.Items.Add("============================================");
            listBoxControl1.Items.Add("");
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                listBoxControl1.Items.Add(string.Format("Disque dur : " + "\"{0}\"", d.Name.ToString().Remove(2)));
                if (d.IsReady == true)
                {
                    listBoxControl1.Items.Add("========================");
                    listBoxControl1.Items.Add(string.Format("Espace disponible :  {0, 15}", SizeSuffix(d.TotalFreeSpace)));
                    listBoxControl1.Items.Add("========================");
                    listBoxControl1.Items.Add(string.Format("Espace totale :  {0, 15} ", SizeSuffix(d.TotalSize)));
                    listBoxControl1.Items.Add("========================");
                    listBoxControl1.Items.Add("");
                    listBoxControl1.Items.Add("");
                }
            }
            listBoxControl1.Items.Add("");
            listBoxControl1.Items.Add("============================================");
            listBoxControl1.Items.Add("=================<INFOS>====================");
            listBoxControl1.Items.Add("=================<SONS>=====================");
            listBoxControl1.Items.Add("============================================");
            listBoxControl1.Items.Add("");
            ManagementObjectSearcher myAudioObject = new ManagementObjectSearcher("select * from Win32_SoundDevice");
            foreach (ManagementObject obj in myAudioObject.Get())
            {
                listBoxControl1.Items.Add(String.Empty.PadLeft(obj["ProductName"].ToString().Length, '='));
                listBoxControl1.Items.Add("Nom  -  " + obj["Name"]);
                listBoxControl1.Items.Add(String.Empty.PadLeft(obj["ProductName"].ToString().Length, '='));
            }
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {

            try
            {
                progressPanel1.Show();
                progressPanel1.Description = "Mise à jour des composants en cours !";
                await Task.Delay(500);
                textBox1.Text = @"dxdiag.exe /whql:off /dontskip /x " + Application.StartupPath.ToString() + @"\DX.xml" + " | Out-Null";
                textBox2.Text = @"[xml]$dxDiagLog = Get-Content " + Application.StartupPath.ToString() + @"\DX.xml";
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"powershell.exe";
                startInfo.Arguments = textBox1.Text + Environment.NewLine + textBox2.Text;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();

                string errors = process.StandardError.ReadToEnd();
                ControlFile.Start();
                progressPanel1.Hide();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Mise à jour NVIDIA
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            xtraTabPage3.Enabled = false;
            simpleButton7.Enabled = false;
            try
            {
                Directory.CreateDirectory(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\");
                File.WriteAllBytes(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\Nvidia Driver Checker.exe", Properties.Resources.TinyNvidiaUpdateChecker);
                File.WriteAllBytes(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\HtmlAgilityPack.dll", Properties.Resources.HtmlAgilityPack);
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = Application.StartupPath.ToString() + @"\NvidiaDriverChecker\Nvidia Driver Checker.exe",
                        Arguments = "--quiet"
                    }
                };
                process.Start();
                TinyUpCheck.Start();
                GPUinfos.Start();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            download_Driver1.Visible = true;
            simpleButton7.Enabled = false;
            simpleButton6.Enabled = false;
            DDUDriverCheckafter.Start();
        }
        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }

        private async void simpleButton5_Click(object sender, EventArgs e)
        {
            string exedir = @"C:\Temp Display Drivers Uninstaller";

            if (Directory.Exists(@"C:\Temp Display Drivers Uninstaller"))
            {
                try
                {
                    clearFolder(exedir);
                    await Task.Delay(500);
                    Directory.CreateDirectory(@"C:\Temp Display Drivers Uninstaller\");

                    File.WriteAllBytes(@"C:\Temp Display Drivers Uninstaller\DDU.zip", Properties.Resources.DDU);

                    string zipPath = @"C:\Temp Display Drivers Uninstaller\DDU.zip";
                    string extractPath = @"C:\Temp Display Drivers Uninstaller\";

                    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
                    File.WriteAllText(@"C:\Temp Display Drivers Uninstaller\settings\Settings.xml", Properties.Resources.SettingsFR);
                    File.Delete(zipPath);
                    await Task.Delay(3000);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(@"C:\Temp Display Drivers Uninstaller\");

                    File.WriteAllBytes(@"C:\Temp Display Drivers Uninstaller\DDU.zip", Properties.Resources.DDU);

                    string zipPath = @"C:\Temp Display Drivers Uninstaller\DDU.zip";
                    string extractPath = @"C:\Temp Display Drivers Uninstaller\";

                    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);

                    File.WriteAllText(@"C:\Temp Display Drivers Uninstaller\settings\Settings.xml", Properties.Resources.SettingsFR);
                    File.Delete(zipPath);

                    await Task.Delay(3000);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void simpleButton4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(folderBrowserDialog1.SelectedPath.ToString() + @"\INSPECTOR.zip", Properties.Resources.NVIDIAINSPECTOR);

                    string zipPath = folderBrowserDialog1.SelectedPath.ToString() + @"\INSPECTOR.zip";
                    string extractPath = folderBrowserDialog1.SelectedPath.ToString();

                    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);

                    await Task.Delay(500);
                    File.Delete(folderBrowserDialog1.SelectedPath.ToString() + @"\INSPECTOR.zip");
                    await Task.Delay(250);
                    Process.Start(folderBrowserDialog1.SelectedPath.ToString() + @"\nvidiaProfileInspector.exe");
                    labelControl11.Visible = true;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void TinyUpCheck_Tick(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\DriversInfos.ini"))
            {
                labelControl15.Visible = true;
                labelControl14.Visible = true;
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader srr = new StreamReader(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\DriversInfos.ini"))
                    {
                        // Read the stream to a string, and write the string to the console.
                        String line = srr.ReadToEnd();
                        if (line == "uptodate")
                        {
                            TinyUpCheck.Stop();
                            XtraMessageBox.Show("Votre pilotes est à jour !", "Informations GPU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            xtraTabPage3.Enabled = true;
                            simpleButton6.Visible = true;
                            simpleButton4.Visible = true;
                            labelControl10.Visible = true;
                            ManagementObjectSearcher mos2 = new ManagementObjectSearcher("select * from Win32_VideoController");
                            foreach (ManagementObject managementObject in mos2.Get())
                            {
                                if (managementObject["MinRefreshRate"] != null)
                                {
                                    labelControl10.Text = ("La fréquence max de votre écran est : " + managementObject["MaxRefreshRate"].ToString() + "Hz");
                                    labelControl10.Left = (this.xtraTabPage3.Width - labelControl10.Size.Width) / 2;
                                }
                            }
                        }
                        else
                        {
                            if (line == "notuptodate")
                            {
                                TinyUpCheck.Stop();
                                DialogResult dialogResult = XtraMessageBox.Show("Votre pilotes n'est pas a jour voulez vous le mettre à jour ?", "Informations GPU", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    xtraTabPage3.Enabled = true;
                                    download_Driver1.Visible = true;
                                    DDUDriverCheckafter.Start();

                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    xtraTabPage3.Enabled = true;
                                    labelControl11.Visible = true;
                                    simpleButton6.Visible = true;
                                    simpleButton4.Visible = true;
                                    labelControl10.Visible = true;
                                    ManagementObjectSearcher mos2 = new ManagementObjectSearcher("select * from Win32_VideoController");
                                    foreach (ManagementObject managementObject in mos2.Get())
                                    {
                                        if (managementObject["MinRefreshRate"] != null)
                                        {
                                            labelControl10.Text = ("La fréquence max de votre écran est : " + managementObject["MaxRefreshRate"].ToString() + "Hz");
                                            labelControl10.Left = (this.xtraTabPage3.Width - labelControl10.Size.Width) / 2;

                                        }
                                    }
                                }
                            }
                            else if (line == "nogpu")
                            {
                                xtraTabPage3.Enabled = true;
                                TinyUpCheck.Stop();
                                XtraMessageBox.Show("Nous trouvons pas votre carte graphique ou aucun pilotes n'est installée \nVeuillez le télécharger manuellement !", "Informations GPU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException)
                    {
                        TinyUpCheck.Stop();
                        XtraMessageBox.Show("Données irrécupérables", "Informations GPU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xtraTabPage3.Enabled = true;

                    }
                    else
                    {
                        TinyUpCheck.Stop();
                        XtraMessageBox.Show("Erreur lors de l'obtention du fichier de données" + ex, "Informations GPU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xtraTabPage3.Enabled = true;

                    }
                }
            }
        }

        private void GPUinfos_Tick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\DriversInfos.ini"))
                {
                    using (StreamReader srr = new StreamReader(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\DriversInfos.ini"))
                    {
                        // Read the stream to a string, and write the string to the console.
                        String line = srr.ReadToEnd();
                        if (line == "nogpu")
                        {
                            GPUinfos.Stop();
                            TinyUpCheck.Stop();
                            XtraMessageBox.Show("Nous trouvons pas votre carte graphique ou aucun pilotes n'est installée \nVeuillez le télécharger manuellement !", "Informations GPU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            xtraTabPage3.Enabled = true;
                            simpleButton6.Visible = true;
                            simpleButton4.Visible = true;
                            labelControl14.Text = "Pas de GPU";
                            labelControl10.Visible = true;
                            ManagementObjectSearcher mos2 = new ManagementObjectSearcher("select * from Win32_VideoController");
                            foreach (ManagementObject managementObject in mos2.Get())
                            {
                                if (managementObject["MinRefreshRate"] != null)
                                {
                                    labelControl10.Text = ("La fréquence max de votre écran est : " + managementObject["MaxRefreshRate"].ToString() + "Hz");
                                    labelControl10.Left = (this.xtraTabPage3.Width - labelControl10.Size.Width) / 2;
                                }
                            }
                        }
                        else
                        {
                            GPUinfos.Stop();
                            using (StreamReader srr1 = new StreamReader(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\GPUInfos.ini"))
                            {
                                String line2 = srr1.ReadToEnd();
                                labelControl14.Text = line2;
                                labelControl15.Left = (this.xtraTabPage3.Width - labelControl15.Size.Width) / 2;
                                labelControl14.Left = (this.xtraTabPage3.Width - labelControl14.Size.Width) / 2;
                                labelControl10.Left = (this.xtraTabPage3.Width - labelControl10.Size.Width) / 2;
                            }
                        }
                    }
                }
                else if (File.Exists(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\GPUInfos.ini"))
                {
                    GPUinfos.Stop();
                    using (StreamReader srr1 = new StreamReader(Application.StartupPath.ToString() + @"\NvidiaDriverChecker\GPUInfos.ini"))
                    {
                        String line2 = srr1.ReadToEnd();
                        labelControl14.Text = line2;
                        labelControl14.Left = (this.xtraTabPage3.Width - labelControl14.Size.Width) / 2;
                        labelControl15.Left = (this.xtraTabPage3.Width - labelControl15.Size.Width) / 2;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DDUDriverCheckafter_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DownDDULaunch == "1")
            {
                groupBox2.Visible = true;
                DDUDriverCheckafter.Stop();
                simpleButton3.PerformClick();
                labelControl5.Visible = true;
                simpleButton3.Visible = true;
            }
            else
            {
                groupBox2.Visible = true;
            }
        }
        #endregion

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("bcdedit /set {current} quietboot Yes");
                cmd.StandardInput.Flush();
                cmd.StandardInput.WriteLine("bcdedit /timeout 10");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                DevExpress.XtraEditors.XtraMessageBox.Show("Le logiciel vient d'activé “Ne pas démarrer la GUI” & à mis le délai à 10 secondes", "Informations !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblOptiDemarrages.ForeColor = Color.Green;
                lblOptiDemarrages.Text = "Optimiser le démarrage de Windows - OK";
                lblOptiDemarrages.Left = (this.xtraTabPage19.Width - lblOptiDemarrages.Size.Width) / 2;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void xtraTabControl6_Click(object sender, EventArgs e)
        {
            ProgrammeUpdateStatus.Start();
        }

        private void ProgrammeUpdateStatus_Tick(object sender, EventArgs e)
        {
            try
            {
                Process[] MyProcess = Process.GetProcesses();
                for (int i = 0; i < MyProcess.Length; i++)
                    dataGridView1.Rows[i].Cells[0].Value = (string.Format(MyProcess[i].ProcessName));
                for (int i = 0; i < MyProcess.Length; i++)
                    dataGridView1.Rows[i].Cells[1].Value = MyProcess[i].Id;
                for (int i = 0; i < MyProcess.Length; i++)
                    dataGridView1.Rows[i].Cells[2].Value = (string.Format(FormatBytes(MyProcess[i].PagedMemorySize, 1, true)));
            }
            catch
            {

            }
        }

        private void listBoxControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceController[] localServices = ServiceController.GetServices();
            ServiceController service = localServices[listBoxControl4.SelectedIndex];

            labelControl27.Text = "Nom d'affichage du service : " + service.DisplayName;
            labelControl27.Left = (this.xtraTabPage3.Width - labelControl27.Size.Width) / 2;
            labelControl26.Text = "Nom du services : " + service.ServiceName;
            labelControl26.Left = (this.xtraTabPage3.Width - labelControl26.Size.Width) / 2;
            labelControl29.Text = "Status : " + service.StartType;
            labelControl29.Left = (this.xtraTabPage3.Width - labelControl29.Size.Width) / 2;
            if (service.Status.ToString() == "Running")
            {
                labelControl25.Text = "Status du service : Démarrer";
            }
            else if (service.Status.ToString() == "Stopped")
            {
                labelControl25.Text = "Status du service : Pas démarrer";
            }
            labelControl25.Left = (this.xtraTabPage3.Width - labelControl25.Size.Width) / 2;

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ProgrammeUpdateStatus.Start();
        }

        private void listBoxControl2_SelectedIndexChanged(object sender, EventArgs e)
        {


            try
            {
                labelControl21.Text = "Fichier sélectioner : " + listBoxControl2.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                {
                    labelControl21.Text = "Fichier sélectioner : N/A";
                }
                else
                {
                    XtraMessageBox.Show("Erreur lors de récupération des fichiers");
                }
            }
        }

        private void listBoxControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                labelControl23.Text = "Fichier sélectioner : " + listBoxControl3.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException)
                {
                    labelControl23.Text = "Fichier sélectioner : N/A";
                }
                else
                {
                    XtraMessageBox.Show("Erreur lors de récupération des fichiers");
                }
            }
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(listBoxControl2.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(listBoxControl3.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void simpleButton15_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(Application.StartupPath.ToString() + @"\SHUTUP10\");
                File.WriteAllBytes(Application.StartupPath.ToString() + @"\SHUTUP10\SHUTUP10.exe", Properties.Resources.OOSU10);
                Process.Start(Application.StartupPath.ToString() + @"\SHUTUP10\SHUTUP10.exe");
                lblDWT.ForeColor = Color.Green;
                SHUTUPCheck.Start();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void SHUTUPCheck_Tick(object sender, EventArgs e)
        {
            string exedir = Application.StartupPath.ToString() + @"\SHUTUP10\";

            Process[] pname = Process.GetProcessesByName("SHUTUP10");
            if (pname.Length == 0)
            {
                SHUTUPCheck.Stop();

                await Task.Delay(5000);
                //Répertoire de l'exécutable
                foreach (string filePath in Directory.GetFiles(exedir))
                {
                    if (filePath != exedir)
                    {
                        File.Delete(filePath);
                    }
                }
                try
                {
                    Directory.Delete(Application.StartupPath.ToString() + @"\SHUTUP10\");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                lblDWT.ForeColor = Color.Red;
            }
        }

        private void automaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + labelControl26.Text, true);
            //key.SetValue("Start", 2);
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(@"sc config " + labelControl26.Text + " start=auto");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        private void xtraTabPage3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton36_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo1 = new ProcessStartInfo();
            startInfo1.Verb = "runas";
            startInfo1.FileName = @"powershell.exe";
            startInfo1.Arguments = "powercfg.exe /l";
            startInfo1.RedirectStandardOutput = true;
            startInfo1.RedirectStandardError = true;
            startInfo1.UseShellExecute = false;
            startInfo1.CreateNoWindow = true;
            Process process1 = new Process();
            process1.StartInfo = startInfo1;
            process1.Start();

            string output1 = process1.StandardOutput.ReadToEnd();

            string errors1 = process1.StandardError.ReadToEnd();
            if (output1.Contains("Performances optimales"))
            {
                XtraMessageBox.Show("Profil déjà existant", "Existe déjà", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Attention ! \n\nSa débloque pour le moment juste le profil !\n\nVous devez l'activer manuellement dans le panneau de configuration ( Paramêtres D'alimentation )", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Ouvre le processus avec les arguments pour débloquer le mode Ultimate performances.Start();
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.Verb = "runas";
                    startInfo.FileName = @"powershell.exe";
                    startInfo.Arguments = "powercfg.exe -duplicatescheme e9a42b02-d5df-448d-aa00-03f14749eb61";
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();

                    string errors = process.StandardError.ReadToEnd();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            try
            {
                //Ouvre le processus avec les arguments pour set les performances sur élevées
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "powercfg.exe",
                        Arguments = "/setactive 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c"
                    }
                };
                process.Start();
                lblPerfEleve.Text = "Changer le profil d'alimentations - OK";
                lblPerfEleve.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "powercfg.exe",
                        Arguments = "/SETACVALUEINDEX SCHEME_CURRENT 0012ee47-9041-4b5d-9b77-535fba8b1442 6738e2c4-e8a5-4a42-b16a-e040e769756e 0"
                    }
                };
                process.Start();
                lblEmpecherLeDisque.Text = "Empêcher le disque dur de s'arrêter au bout de 20 minutes - OK";
                lblEmpecherLeDisque.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            try
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "powercfg.exe",
                        Arguments = "/SETACVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 0"
                    }
                };
                process.Start();
                lblSupensionUSB.Text = "Désactiver la suspension séléctive USB - OK";
                lblSupensionUSB.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("Voulez vous voir le réglages conseillers ou vous voulez régler par vous mêmes ?", "Question ?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //GIFRegEffetsVisu PPR = new GIFRegEffetsVisu();
                //PPR.Show();
                try
                {
                    var process = new Process
                    {
                        StartInfo =
                            {
                                FileName = "SystemPropertiesPerformance.exe",
                                Arguments = "/7 /startup"
                            }
                    };
                    process.Start();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SysProp.Start();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                try
                {
                    var process = new Process
                    {
                        StartInfo =
                            {
                                FileName = "SystemPropertiesPerformance.exe",
                                Arguments = "/7 /startup"
                            }
                    };
                    process.Start();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            SysProp.Start();
        }

        private void simpleButton31_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            labelControl19.Text = @"Augmenter les images par secondes 
( Non conseillée pour les record de vidéos et streamers ) - OK";
            labelControl19.ForeColor = Color.Green;
        }

        private void simpleButton32_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Ndu", "Start", 4);
                labelControl18.Text = @"Désactiver NDU - OK";
                labelControl18.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void xtraTabPage17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton33_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\TimeBrokerSvc", "Start", 4);
                labelControl17.Text = @"Désactiver RuntinBroker - OK";
                labelControl17.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton34_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnablePrefetcher", 0);
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(@"net.exe stop superfetch");
                cmd.StandardInput.Flush();
                cmd.StandardInput.WriteLine(@"sc config sysmain start=disabled");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                labelControl16.Text = @"Désactiver Prefetech && Superfetch - OK";
                labelControl16.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "runas";
                startInfo.FileName = @"powershell.exe";
                startInfo.Arguments = "Set-MpPreference -DisableRealtimeMonitoring $true";
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();

                string errors = process.StandardError.ReadToEnd();
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(@"net.exe stop mpssvc");
                cmd.StandardInput.Flush();
                cmd.StandardInput.WriteLine(@"sc config mpssvc start=disabled");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinDefend", "Start", 4);

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", 1);
                lblDisWindowsDef.Text = "Désactiver windows defender - OK";
                lblDisWindowsDef.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            try
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine("reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search\" /v \"AllowCortana\" /t REG_DWORD /d 0 /f \"");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", 0);

                lblDisCortana.Text = "Désactiver cortana - OK";
                lblDisCortana.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton20_Click(object sender, EventArgs e)
        {
            //check x64 et Uninstall Onedrive
            if (Environment.Is64BitOperatingSystem)
            {
                var process = new Process
                {
                    StartInfo =
                            {
                                FileName = @"C:\Windows\SysWOW64\OneDriveSetup.exe",
                                Arguments = "/uninstall"
                            }
                };
                process.Start();
            }
            else
            {
                var process = new Process
                {
                    StartInfo =
                            {
                                FileName = @"C:\Windows\System32\OneDriveSetup.exe",
                                Arguments = "/uninstall"
                            }
                };
                process.Start();
            }
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            Process.Start("rstrui.exe");

        }

        private async void simpleButton23_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("Êtes vous sur de vouloir supprimée tous les points de restauration ?\n\nVous ne pourrais plus les récupérer après cette action !", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "runas";
                startInfo.FileName = @"powershell.exe";
                startInfo.Arguments = "vssadmin delete shadows /For=C: /all /quiet";
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();

                string errors = process.StandardError.ReadToEnd();
                /*var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "powershell.exe",
                        Arguments = "vssadmin delete shadows /For=C: /all /quiet"
                    }
                };
                process.Start();*/
                await Task.Delay(1500);
                dataGridView2.Rows.Clear();
                ManagementScope ManScope = new ManagementScope(@"\\localhost\root\default");
                ManagementPath ManPath = new ManagementPath("SystemRestore");
                ObjectGetOptions ManOptions = new ObjectGetOptions();
                ManagementClass ManClass = new ManagementClass(ManScope, ManPath, ManOptions);

                foreach (ManagementObject mo in ManClass.GetInstances())
                {
                    string time = mo["CreationTime"].ToString();
                    time = time.Remove(14);
                    string year = "";
                    string day = "";
                    string month = "";
                    string Heure = "";
                    string minutes = "";
                    string seconde = "";

                    year = time.Remove(4);
                    month = time.Remove(6);
                    month = month.Substring(4);
                    day = time.Remove(8);
                    day = day.Substring(6);
                    Heure = time.Remove(10);
                    Heure = Heure.Substring(8);
                    int heuretot = Convert.ToInt32(Heure);
                    heuretot = heuretot + 2;
                    minutes = time.Remove(12);
                    minutes = minutes.Substring(10);
                    seconde = time;
                    seconde = time.Substring(12);

                    time = day + "/" + month + "/" + year + " " + heuretot + ":" + minutes + ":" + seconde;
                    //DateTime myDate = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss,fff",
                    //                                     System.Globalization.CultureInfo.InvariantCulture);
                    //string realtime = myDate.ToString("yyyy-MM-dd HH:mm:ss,fff", CultureInfo.CurrentCulture);
                    //20180821180316
                    //vssadmin list shadows > C:\test.txt
                    dataGridView2.Rows.Add(time, mo["Description"].ToString(), mo["SequenceNumber"]);
                }
            }
            else
            {

            }
        }

        private void simpleButton21_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            ManagementScope ManScope = new ManagementScope(@"\\localhost\root\default");
            ManagementPath ManPath = new ManagementPath("SystemRestore");
            ObjectGetOptions ManOptions = new ObjectGetOptions();
            ManagementClass ManClass = new ManagementClass(ManScope, ManPath, ManOptions);

            foreach (ManagementObject mo in ManClass.GetInstances())
            {
                string time = mo["CreationTime"].ToString();
                time = time.Remove(14);
                string year = "";
                string day = "";
                string month = "";
                string Heure = "";
                string minutes = "";
                string seconde = "";

                year = time.Remove(4);
                month = time.Remove(6);
                month = month.Substring(4);
                day = time.Remove(8);
                day = day.Substring(6);
                Heure = time.Remove(10);
                Heure = Heure.Substring(8);
                int heuretot = Convert.ToInt32(Heure);
                heuretot = heuretot + 2;
                minutes = time.Remove(12);
                minutes = minutes.Substring(10);
                seconde = time;
                seconde = time.Substring(12);

                time = day + "/" + month + "/" + year + " " + heuretot + ":" + minutes + ":" + seconde;
                //DateTime myDate = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss,fff",
                //                                     System.Globalization.CultureInfo.InvariantCulture);
                //string realtime = myDate.ToString("yyyy-MM-dd HH:mm:ss,fff", CultureInfo.CurrentCulture);
                //20180821180316
                //vssadmin list shadows > C:\test.txt
                dataGridView2.Rows.Add(time, mo["Description"].ToString(), mo["SequenceNumber"]);
            }
        }


        private void simpleButton24_Click(object sender, EventArgs e)
        {

        }

        private async void simpleButton25_Click(object sender, EventArgs e)
        {
            try
            {
                progressPanel1.Show();
                progressPanel1.Description = "Création du point de restauration !";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\SystemRestore", "SystemRestorePointCreationFrequency", 0);
                Optimisation_Windows_Capet.Base.UserControl.RestorePoint.CreateRestorePoint("");
                await Task.Delay(1500);
                progressPanel1.Hide();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton24_Click_1(object sender, EventArgs e)
        {
            if (checkVeille.Checked == true)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.Verb = "runas";
                    startInfo.FileName = @"powershell.exe";
                    startInfo.Arguments = "bcdedit /set bootmenupolicy legacy";
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();

                    string errors = process.StandardError.ReadToEnd();
                    /*var process = new Process
                    {
                        StartInfo =
                        {
                            FileName = "powershell.exe",
                            Arguments = "bcdedit /set bootmenupolicy legacy"
                        }
                    };
                    process.Start();*/
                    checkVeille.Text = "Désactiver le mode mise en veille prolongé - OK";
                    checkVeille.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                checkVeille.Text = "Désactiver le mode mise en veille prolongé - Deactivated";
                checkVeille.ForeColor = Color.Red;
            }

            if (checkF8.Checked == true)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.Verb = "runas";
                    startInfo.FileName = @"powershell.exe";
                    startInfo.Arguments = "powercfg -h off";
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    Process process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();

                    string errors = process.StandardError.ReadToEnd();
                    /*var process = new Process
                    {
                        StartInfo =
                        {
                            FileName = "powershell.exe",
                            Arguments = "powercfg -h off"
                        }
                    };
                    process.Start();*/
                    checkF8.Text = "activé la fonction F8 au démarrage - OK";
                    checkF8.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                checkF8.Text = "Ré-activé la fonction F8 au démarrage - Deactivated";
                checkF8.ForeColor = Color.Red;
            }
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Windows\Prefetch";

            try
            {
                var dir = new DirectoryInfo(folderPath);
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                dir.Delete(true);

                XtraMessageBox.Show(folderPath + " les fichiers PREFETCH ont été supprimées.", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblSuppresionsDesPrefetch.Text = "Suppression des fichiers PREFETCH ( Clear )";
                lblSuppresionsDesPrefetch.ForeColor = Color.Green;

                if (Directory.Exists(folderPath))
                {

                }
                else
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            catch (Exception ex)
            {
                if (ex is IOException)
                {
                    lblSuppresionsDesPrefetch.Text = "Suppresion des fichiers PREFETCH ( Effacer mais encore quelques fichiers )";
                    lblSuppresionsDesPrefetch.ForeColor = Color.Orange;
                    XtraMessageBox.Show("Cette erreur peut être normal ! \n\nPourquoi ?\n\nCar les fichiers peuvent être en train d'être utiliser et malgrès le sytèmes que j'utilise quelques fichiers restent encore mais tous ceux qui a pu être supprimez la était ! \n\nEt que même vous manuellement ne pourriez pas les supprimez ! \n\n\n\nVoici la source du problèmes ! : \n\n" + ex.Message + "\n\n" + ex.GetType(), "Informations sur la suppresion de fichiers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show(folderPath + " les fichiers PREFETCH ont été supprimées.");
                    lblSuppresionsDesPrefetch.Text = "Suppression des fichiers PREFETCH - OK";
                    lblSuppresionsDesPrefetch.ForeColor = Color.Green;

                }
            }
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            // Créer et défini le string folderPath
            string folderPath = @"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Local\Temp";

            // Si possible faire ce code
            try
            {
                // défini la var "dir" en créant un directoryInfo du dossier défini plus haut "folderPath"
                var dir = new DirectoryInfo(folderPath);
                // Défini les attributs pour le dossier et les fichiers
                dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                // Supprime le dossier si possible et les fichiers obligatoirements
                dir.Delete(true);
                // Si langue FR faire le code ci-dessous
                DevExpress.XtraEditors.XtraMessageBox.Show(folderPath + " les fichiers temporaires ont était supprimées !");
                lblSuppresionTemporaires.Text = "Suppresion des fichiers temporaires ( Clear )";
                // Le label change de couleur pour le vert
                lblSuppresionTemporaires.ForeColor = Color.Green;
                // Si le dossier existe faire le code ci-dessous
                if (Directory.Exists(folderPath))
                {
                    // Pas besoin de code donc normal qu'il n'y est rien :D
                }
                else
                {
                    // Créer un dossier à l'emplacement défini au départ ( folderPath )
                    Directory.CreateDirectory(folderPath);
                }
            }
            // Si il y à un problèmes faire une exeception
            catch (Exception ex)
            {
                // Si l'exeception est du à un problème à la suppresion du fichier faire ce code ci-dessous
                if (ex is IOException)
                {
                    lblSuppresionTemporaires.Text = "Suppresion des fichiers temporaires ( Clear but still some files )";
                    lblSuppresionTemporaires.ForeColor = Color.Orange;
                    DevExpress.XtraEditors.XtraMessageBox.Show("Cette erreur peut être normal ! \n\nPourquoi ?\n\nCar les fichiers peuvent être en train d'être utiliser et malgrès le sytèmes que j'utilise quelques fichiers restent encore mais tous ceux qui a pu être supprimez la était ! \n\nEt que même vous manuellement ne pourriez pas les supprimez ! \n\n\n\nVoici la source du problèmes ! : " + folderPath + "\n\n" + ex.Message + "\n\n" + ex.GetType(), "Informations sur la suppression de fichiers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Si non faire celui-ci
                {
                    lblSuppresionTemporaires.Text = "Suppression de fichiers temporaires - OK";
                    // Le label change de couleur pour le vert
                    lblSuppresionTemporaires.ForeColor = Color.Green;
                }
            }
        }

        private async void Restauration_Tick(object sender, EventArgs e)
        {

        }
        private void AutoUpdater_ApplicationExitEvent()
        {
            Text = @"Closing application...";
            Thread.Sleep(5000);
            Application.Exit();
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string exedir = Application.StartupPath.ToString() + @"\SHUTUP10\";

                Process[] pname = Process.GetProcessesByName("SHUTUP10");
                if (pname.Length == 0)
                {
                    await Task.Delay(1000);
                    //Répertoire de l'exécutable
                    foreach (string filePath in Directory.GetFiles(exedir))
                    {
                        if (filePath != exedir)
                        {
                            File.Delete(filePath);
                        }
                    }
                    try
                    {
                        Directory.Delete(Application.StartupPath.ToString() + @"\SHUTUP10\");
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Une erreur est survenue ! \n\nVoici le details de l'erreur : \n\n" + ex, "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    lblDWT.ForeColor = Color.Red;
                }
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();

                cmd.StandardInput.WriteLine(@"RMDIR /S /Q " + "\u0022" + Application.StartupPath.ToString() + @"\NvidiaDriverChecker" + "\u0022");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
                if (File.Exists(Application.StartupPath.ToString() + @"\HtmlAgilityPack.dll"))
                {
                    File.Delete(Application.StartupPath.ToString() + @"\HtmlAgilityPack.dll");
                }
                // Ferme l'application complétement
            }
            catch (Exception ex)
            {
            }
            Application.Exit();
        }

        private void simpleButton27_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show("Cochez bien “MASQUEZ TOUS LES SERVICES MICROSOFT” \nSINON VOUS RISQUEZ DE FAIRE DES BÊTISES ! ", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            var process = new Process
            {
                StartInfo =
                {
                    FileName = Path.Combine(Environment.SystemDirectory, "msconfig.exe"),
                    Arguments = "/3"
                }
            };
            process.Start();
        }

        private void simpleButton26_Click(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "Taskmgr.exe",
                    Arguments = "/7 /startup"
                }
            };
            process.Start();
        }

        private void simpleButton40_Click(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "powercfg.exe",
                    Arguments = "/setactive 381b4222-f694-41f0-9685-ff5bb260df2e"
                }
            };
            process.Start();
            XtraMessageBox.Show("Mode d'alimentations activé sur 'Normal'", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton41_Click(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "powercfg.exe",
                    Arguments = "/SETACVALUEINDEX SCHEME_CURRENT 0012ee47-9041-4b5d-9b77-535fba8b1442 6738e2c4-e8a5-4a42-b16a-e040e769756e 1200"
                }
            };
            process.Start();
            XtraMessageBox.Show("L'arrêt des disques à était ré-activé au bout de 20 minutes", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton42_Click(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "powercfg.exe",
                    Arguments = "/SETACVALUEINDEX SCHEME_CURRENT 2a737441-1930-4402-8d77-b2bebba308a3 48e6b7a6-50f5-4782-a5d4-53bb8f07e226 1"
                }
            };
            process.Start();
            XtraMessageBox.Show("La suspension séléctive USB à était ré-activé", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton38_Click(object sender, EventArgs e)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("bcdedit /timeout 30");
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine("bcdedit /set {current} quietboot No");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            DevExpress.XtraEditors.XtraMessageBox.Show("Le logiciel vient de désactivé “Ne pas démarrer la GUI” & à mis le délai à 30 secondes comme par défaut !", "Informations !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton28_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 10);
            XtraMessageBox.Show("La valeur à était remis par défaut", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton29_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Ndu", "Start", Properties.Settings.Default.REGNDU);
            XtraMessageBox.Show("NDU à était ré-activé", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton35_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\TimeBrokerSvc", "Start", Properties.Settings.Default.REGTimeBroker);
            XtraMessageBox.Show("RuntimeBroker à était ré-activé", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton30_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnablePrefetcher", Properties.Settings.Default.REGPrefetech);
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(@"sc config sysmain start=auto");
            cmd.StandardInput.Flush();
            cmd.StandardInput.WriteLine(@"net.exe start superfetch");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            XtraMessageBox.Show("Prefectch && Superfetch ont était ré-activé", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton37_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Application.StartupPath.ToString() + @"\SHUTUP10\");
            File.WriteAllBytes(Application.StartupPath.ToString() + @"\SHUTUP10\SHUTUP10.exe", Properties.Resources.OOSU10);
            Process.Start(Application.StartupPath.ToString() + @"\SHUTUP10\SHUTUP10.exe");
        }

        private void simpleButton39_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.Verb = "runas";
            startInfo.FileName = @"powershell.exe";
            startInfo.Arguments = "bcdedit /set bootmenupolicy standard";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            string errors = process.StandardError.ReadToEnd();

            ProcessStartInfo startInfo1 = new ProcessStartInfo();
            startInfo1.Verb = "runas";
            startInfo1.FileName = @"powershell.exe";
            startInfo1.Arguments = "powercfg -h on";
            startInfo1.RedirectStandardOutput = true;
            startInfo1.RedirectStandardError = true;
            startInfo1.UseShellExecute = false;
            startInfo1.CreateNoWindow = true;
            Process process1 = new Process();
            process1.StartInfo = startInfo1;
            process1.Start();

            string output1 = process.StandardOutput.ReadToEnd();

            string errors1 = process.StandardError.ReadToEnd();
            XtraMessageBox.Show("L’option de mise en veille prolongée à était activé + F8 à était désactivé", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton43_Click(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "Taskmgr.exe",
                    Arguments = "/7 /startup"
                }
            };
        }

        private void simpleButton44_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show("COCHEZ BIEN “MASQUEZ TOUS LES SERVICES MICROSOFT” SINON VOUS RISQUEZ DE FAIRE DES BÊTISES ! ", "Attention !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var process = new Process
            {
                StartInfo =
                {
                    FileName = Path.Combine(Environment.SystemDirectory, "msconfig.exe"),
                    Arguments = "/3"
                }
            };
            process.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R && (e.Control))
            {
                if (xtraTabPage14.PageVisible == true)
                {
                    xtraTabPage14.PageVisible = false;
                }
                else
                {
                    xtraTabPage14.PageVisible = true;
                    //Colorlbl.Start();
                    //Colorlbl2.Start();
                }
            }
        }
    }
}