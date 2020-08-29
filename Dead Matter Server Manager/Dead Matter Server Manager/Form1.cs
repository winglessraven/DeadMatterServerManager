using Dead_Matter_Server_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Dead_Matter_Server_Manager
{
    public partial class Form1 : Form
    {
        private static List<Settings> settings = new List<Settings>();
        private static string configFilePath;
        private List<String> scripts = new List<String>();
        delegate void SetTextOnControl(Control controlToChange, string message, Color foreColour, bool enabled);
        delegate void SetProgressBar(ProgressBar progressBar, double maximum, double current);
        delegate string ReadControls(Control control);
        delegate void SetReadOnlyControl(Control control, bool enabled);
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private bool serverStarted;
        private bool firstTimeServerStarted;
        public Form1()
        {
            InitializeComponent();
            configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DMSM.cfg";
            CheckAppData();
            AddConfigRows();
            MonitorServer(maxServerMemory.Text);
        }

        private void CheckAppData()
        {
            if (File.Exists(configFilePath))
            {
                string[] cfg = File.ReadAllLines(configFilePath);

                foreach (string s in cfg)
                {
                    if (s.StartsWith("SteamCMDPath"))
                    {
                        String[] temp = s.Split('=');
                        steamCMDPath.Text = temp[1];
                    }

                    if (s.StartsWith("ServerPath"))
                    {
                        String[] temp = s.Split('=');
                        serverFolderPath.Text = temp[1];
                    }

                    if (s.StartsWith("SteamID"))
                    {
                        String[] temp = s.Split('=');
                        steamID.Text = temp[1];
                    }

                    if (s.StartsWith("UpdateServer"))
                    {
                        String[] temp = s.Split('=');
                        checkUpdateOnStart.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("MaxMemory"))
                    {
                        String[] temp = s.Split('=');
                        maxServerMemory.Text = temp[1];
                    }
                }
            }
            else
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "DeadMatterServerManager");
                Directory.CreateDirectory(specificFolder);
                File.Create(configFilePath).Close();
            }
        }


        private void AddConfigRows()
        {
            settings.Add(new Settings { Variable = "ServerName", Value = "My Server", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Server name. Has a soft limit of 255 characters due to Steam server limitations." });
            settings.Add(new Settings { Variable = "MaxPlayers", Value = "36", Script = "[/Script/Engine.GameSession]", Tooltip = "Maximum player count for the server." });
            settings.Add(new Settings { Variable = "Password", Value = "", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Server password. Has a soft limit of 255 characters due to Steam server limitations." });
            settings.Add(new Settings { Variable = "MOTD", Value = "Welcome to the server.", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Server's MOTD, displayed in character creation." });
            settings.Add(new Settings { Variable = "MaxPlayerClaims", Value = "3", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Maximum claims per group or player." });
            settings.Add(new Settings { Variable = "MaxZombieCount", Value = "2048", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "The absolute hard-cap for zombie NPCs. If this many zombies are on the server, no more will be allowed to spawn." });
            settings.Add(new Settings { Variable = "MaxAnimalCount", Value = "100", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "The absolute hard-cap for animal NPCs. If this many animals are on the server, no more will be allowed to spawn." });
            settings.Add(new Settings { Variable = "MaxBanditCount", Value = "256", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "The absolute hard-cap for non-safezone human NPCs. If this many human NPCs are on the server, no more will be allowed to spawn." });
            settings.Add(new Settings { Variable = "PVP", Value = "true", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Toggles whether or not PVP is enabled. If this is false, no damage can be inflicted by one player on another." });
            settings.Add(new Settings { Variable = "FallDamageMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Change how much damage falling does here. 1.0 is full damage, 0 is no damage at all." });
            settings.Add(new Settings { Variable = "AnimalSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.FlockSpawner]", Tooltip = "How many more animals to spawn than usual." });
            settings.Add(new Settings { Variable = "ZombieSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.GlobalAISpawner]", Tooltip = "How many more zombies to spawn than usual." });
            settings.Add(new Settings { Variable = "Timescale", Value = "5.5", Script = "[/Script/DeadMatter.Agenda]", Tooltip = "The timescale, relative to real time. The default value of 5.5 indicates that one real-life second is 5.5 seconds ingame." });
            settings.Add(new Settings { Variable = "AttackMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How strongly the zombies do damage. Set to zero to disable." });
            settings.Add(new Settings { Variable = "DefenseMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How much the zombies soak up hits. Set to zero to make them made of paper." });
            settings.Add(new Settings { Variable = "SteamQueryIP", Value = "0.0.0.0", Script = "[/Script/DeadMatter.ServerInfoProxy]", Tooltip = "Change the Steam query host." });
            settings.Add(new Settings { Variable = "SteamQueryPort", Value = "27016", Script = "[/Script/DeadMatter.ServerInfoProxy]", Tooltip = "Change the Steam query port." });
            settings.Add(new Settings { Variable = "WhitelistActive", Value = "false", Script = "[/Script/DeadMatter.SurvivalBaseGamemode]", Tooltip = "If the server whitelist is enabled." });

            foreach (Settings s in settings)
            {
                configSettings.Rows.Add(s.Variable, s.Value, s.Script, s.Tooltip);
            }

            configSettings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void steamCMDBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "SteamCMD Folder";
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                steamCMDPath.Text = folderBrowserDialog.SelectedPath;
            }

            SaveData();

        }

        private void serverFolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Dead Matter Server Folder";
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                serverFolderPath.Text = folderBrowserDialog.SelectedPath;
            }

            SaveData();
        }

        private void updateSteamCMD_Click(object sender, EventArgs e)
        {
            if (steamCMDPath.Text == "")
            {
                MessageBox.Show("SteamCMD path not selected", "No Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WebClient downloader = new WebClient();
            downloader.DownloadFile("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip", String.Join("\\", steamCMDPath.Text, "steamcmd.zip"));
            using (ZipArchive zipFile = ZipFile.OpenRead(String.Join("\\", steamCMDPath.Text, "steamcmd.zip")))
            {
                foreach (ZipArchiveEntry zipArchiveEntry in zipFile.Entries)
                {
                    zipArchiveEntry.ExtractToFile(steamCMDPath.Text + "\\" + zipArchiveEntry.Name, true);
                }
            }

            MessageBox.Show("SteamCMD Updated", "SteamCMD Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void updateServer_Click(object sender, EventArgs e)
        {
            if (steamCMDPath.Text.Equals(""))
            {
                MessageBox.Show("Enter SteamCMD path first!", "No SteamCMD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (steamID.Text.Equals(""))
            {
                MessageBox.Show("Enter SteamID first!", "No Steam ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (steamPassword.Text.Equals(""))
            {
                MessageBox.Show("Enter Steam password first!", "No Steam Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Process steamCMD = new Process();
            steamCMD.StartInfo.FileName = steamCMDPath.Text + "\\steamcmd.exe";
            steamCMD.StartInfo.Arguments = "+login " + steamID.Text + " " + steamPassword.Text + " +force_install_dir " + serverFolderPath.Text + " +app_update 1110990 +quit";
            steamCMD.Start();
            steamCMD.WaitForExit();
        }

        private void getConfig_Click(object sender, EventArgs e)
        {
            if (!File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini"))
            {
                MessageBox.Show("Game.ini not found, try running the server once to initialize the config files", "Game.ini not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] configGame = File.ReadAllLines(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini");
            string[] configEngine = File.ReadAllLines(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini");

            whitelistDGV.Rows.Clear();
            adminDGV.Rows.Clear();
            superAdminDGV.Rows.Clear();

            foreach (string configLine in configGame)
            {
                string[] configVariable = configLine.Split('=');
                foreach (Settings s in settings)
                {
                    if (configVariable[0] == s.Variable)
                    {
                        foreach (DataGridViewRow dataGridViewRow in configSettings.Rows)
                        {
                            if (dataGridViewRow.Cells[0].Value.Equals(s.Variable))
                            {
                                dataGridViewRow.Cells[1].Value = configVariable[1];
                            }
                        }
                    }
                }

                if (configVariable[0] == "Admins")
                {
                    adminDGV.Rows.Add(configVariable[1]);
                }
                if (configVariable[0] == "SuperAdmins")
                {
                    superAdminDGV.Rows.Add(configVariable[1]);
                }
                if (configVariable[0] == "Whitelist")
                {
                    whitelistDGV.Rows.Add(configVariable[1]);
                }
            }
        }

        private void saveConfig_Click(object sender, EventArgs e)
        {
            List<WriteConfig> writeConfigs = new List<WriteConfig>();

            foreach (DataGridViewRow dataGridViewRow in configSettings.Rows)
            {
                if (writeConfigs.Exists(p => p.Script == dataGridViewRow.Cells[2].Value))
                {
                    writeConfigs.Find(p => p.Script == dataGridViewRow.Cells[2].Value).Values += Environment.NewLine + dataGridViewRow.Cells[0].Value.ToString() + "=" + dataGridViewRow.Cells[1].Value.ToString();
                }
                else
                {
                    writeConfigs.Add(new WriteConfig { Script = dataGridViewRow.Cells[2].Value.ToString(), Values = dataGridViewRow.Cells[0].Value + "=" + dataGridViewRow.Cells[1].Value });
                }
                scripts.Add(dataGridViewRow.Cells[2].Value.ToString());
            }

            string gameIni = "";

            foreach (WriteConfig writeConfig in writeConfigs)
            {
                gameIni += Environment.NewLine + writeConfig.Script + Environment.NewLine + writeConfig.Values;
            }

            //write whitelist players
            gameIni += Environment.NewLine + "[/Script/DeadMatter.DMGameSession]";
            foreach(DataGridViewRow row in whitelistDGV.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    gameIni += Environment.NewLine + "Whitelist=" + row.Cells[0].Value;
                }
            }

            //write admin players
            gameIni += Environment.NewLine + "[/Script/DeadMatter.DMGameSession]";
            foreach (DataGridViewRow row in adminDGV.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    gameIni += Environment.NewLine + "Admins=" + row.Cells[0].Value;
                }
            }

            //write superadmin players
            gameIni += Environment.NewLine + "[/Script/DeadMatter.DMGameSession]";
            foreach (DataGridViewRow row in superAdminDGV.Rows)
            {
                if(row.Cells[0].Value != null)
                {
                    gameIni += Environment.NewLine + "SuperAdmins=" + row.Cells[0].Value;
                }
                
            }

            File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini", gameIni);
            MessageBox.Show("Config file saved", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveData()
        {
            File.WriteAllText(configFilePath, "SteamCMDPath=" + steamCMDPath.Text + Environment.NewLine + "ServerPath=" + serverFolderPath.Text + Environment.NewLine + "SteamID=" + steamID.Text + Environment.NewLine + "UpdateServer=" + checkUpdateOnStart.Checked + Environment.NewLine + "MaxMemory=" + maxServerMemory.Text);
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            firstTimeServerStarted = true;
            serverStarted = true;

        }

        private async void MonitorServer(string maxMemory)
        {
            SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());

            await Task.Run(() =>
            {
                long memory;
                Process[] dmServer;
                dmServer = Process.GetProcessesByName("deadmatterServer-Win64-Shipping");
                if (dmServer.Length != 0)
                {
                    memory = dmServer[0].VirtualMemorySize64;
                    string memoryGB = SizeSuffix(memory, 2);
                    SetText(memoryUsed, memoryGB, Color.Black, true);
                    SetText(serverStatus, "SERVER RUNNING", Color.Green, true);
                    serverStatus.ForeColor = Color.Green;
                    SetText(startServer, "Start Server", Color.Black, false);
                    SetText(stopServer, "Stop Server", Color.Black, true);
                    SetReadOnly(maxServerMemory, false);

                    string maxMem = ReadControl(maxServerMemory);
                    if (maxMem == "")
                    {
                        maxMem = "20";
                    }
                    SetProgress(memoryUsedProgressBar, Convert.ToDouble(maxMem), Convert.ToDouble(memory / 1024 / 1024 / 1024));

                    if (Convert.ToDouble(memory) / 1024 / 1024 / 1024 > Convert.ToDouble(maxMem))
                    {
                        dmServer[0].CloseMainWindow();
                    }
                }
                else
                {
                    SetText(memoryUsed, "0 GB", Color.Black, true);
                    SetText(serverStatus, "SERVER OFFLINE", Color.Red, true);
                    SetText(startServer, "Start Server", Color.Black, true);
                    SetText(stopServer, "Stop Server", Color.Black, false);
                    SetProgress(memoryUsedProgressBar, 100, 0);
                    SetReadOnly(maxServerMemory, true);
                    if (serverStarted && firstTimeServerStarted)
                    {
                        if (checkUpdateOnStart.Checked)
                        {
                            updateServer_Click(this, null);
                            string serverExe = serverFolderPath.Text + @"\deadmatterServer.exe";
                            Process.Start(serverExe, "-USEALLAVAILABLECORES -log");
                        }
                        else
                        {
                            string serverExe = serverFolderPath.Text + @"\deadmatterServer.exe";
                            Process.Start(serverExe, "-USEALLAVAILABLECORES -log");
                        }
                    }
                }
            });

            Thread.Sleep(30);

            MonitorServer(maxServerMemory.Text);
        }

        public string ReadControl(Control control)
        {
            if (control.InvokeRequired)
            {
                ReadControls DDD = new ReadControls(ReadControl);
                control.Invoke(DDD, control);
                return control.Text;
            }
            else
            {
                return control.Text;
            }

        }

        public void SetReadOnly(Control control, bool enabled)
        {
            if (control.InvokeRequired)
            {
                SetReadOnlyControl DDD = new SetReadOnlyControl(SetReadOnly);
                control.Invoke(DDD, control, enabled);
            }
            else
            {
                control.Enabled = enabled;
            }
        }

        public void SetText(Control controlToChange, string message, Color foreColour, bool enabled)
        {
            if (controlToChange.InvokeRequired)
            {
                SetTextOnControl DDD = new SetTextOnControl(SetText);
                controlToChange.Invoke(DDD, controlToChange, message, foreColour, enabled);
            }
            else
            {
                controlToChange.Text = message;
                controlToChange.ForeColor = foreColour;
                controlToChange.Enabled = enabled;
            }
        }

        public void SetProgress(ProgressBar progressBar, double maximum, double current)
        {
            if (progressBar.InvokeRequired)
            {
                SetProgressBar DDD = new SetProgressBar(SetProgress);
                progressBar.Invoke(DDD, progressBar, maximum, current);
            }
            else
            {
                int currentVal = Convert.ToInt32(current);
                int maxVal = Convert.ToInt32(maximum);

                if (currentVal > maxVal)
                {
                    progressBar.Maximum = maxVal;
                    progressBar.Value = maxVal;
                }
                else
                {
                    progressBar.Maximum = maxVal;
                    progressBar.Value = currentVal;
                }
            }

        }
        static string SizeSuffix(Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }

        public class Settings
        {
            public string Variable { get; set; }
            public string Value { get; set; }
            public string Script { get; set; }
            public string Tooltip { get; set; }
        }

        public class WriteConfig
        {
            public string Script { get; set; }
            public string Values { get; set; }
        }

        private void steamID_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void steamPassword_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void maxServerMemory_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void stopServer_Click(object sender, EventArgs e)
        {
            Process[] dmServer;
            dmServer = Process.GetProcessesByName("deadmatterServer-Win64-Shipping");
            if (dmServer.Length != 0)
            {
                dmServer[0].CloseMainWindow();
            }
            serverStarted = false;
            firstTimeServerStarted = false;
        }

        private void checkUpdateOnStart_CheckedChanged(object sender, EventArgs e)
        {
            SaveData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://paypal.me/winglessraven");
        }
    }
}
