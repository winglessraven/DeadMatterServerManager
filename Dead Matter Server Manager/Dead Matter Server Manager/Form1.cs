using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

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
        private DateTime serverStartTime;
        private IPAddress serverIP;
        private A2S_INFO serverInfo;
        private int steamQueryPort;
        private bool killSent;

        public Form1()
        {
            InitializeComponent();
            VersionCheckOnStart();
            configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DMSM.cfg";
            AddConfigRows();
            CheckAppData();

            //check if server is already running
            Process[] dmServerShipping = Process.GetProcessesByName("deadmatterServer-Win64-Shipping");
            if (dmServerShipping.Length != 0)
            {
                serverStarted = true;
                firstTimeServerStarted = true;
            }
            MonitorServer(maxServerMemory.Text);
            if (autoStartServer.Checked)
            {
                startServer_Click(this, null);
            }
        }

        private void VersionCheckOnStart()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
            }
            WebClient webClient = new WebClient();

            string releaseVersion;
            try
            {
                releaseVersion = webClient.DownloadString("https://www.winglessraven.com/DMSM.html");
            }
            catch
            {
                //server not found
                releaseVersion = this.ProductVersion.ToString();
            }
            Version version = new Version(releaseVersion);
            //MessageBox.Show(version.ToString() + Environment.NewLine + this.ProductVersion);
            if (version.CompareTo(new Version(this.ProductVersion)) > 0)
            {
                DialogResult result = MessageBox.Show("Update available, download now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Process.Start("https://github.com/winglessraven/DeadMatterServerManager/releases/latest");
                    Environment.Exit(0);
                }
            }
        }

        private void VersionCheck()
        {
            WebClient webClient = new WebClient();
            string releaseVersion;
            try
            {
                releaseVersion = webClient.DownloadString("https://www.winglessraven.com/DMSM.html");
            }
            catch
            {
                releaseVersion = this.ProductVersion.ToString();
            }

            Version version = new Version(releaseVersion);
            //MessageBox.Show(version.ToString() + Environment.NewLine + this.ProductVersion);
            if (version.CompareTo(new Version(this.ProductVersion)) > 0)
            {
                updateSoftware.Text = "Version " + releaseVersion + " available - click to update now";
            }
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
                        getConfig_Click(null, null);
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

                    if (s.StartsWith("StartWithWindows"))
                    {
                        String[] temp = s.Split('=');
                        autoStartWithWindows.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("StartServerOnLaunch"))
                    {
                        String[] temp = s.Split('=');
                        autoStartServer.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("LastStart"))
                    {
                        String[] temp = s.Split('=');
                        serverStartTime = Convert.ToDateTime(temp[1]);
                    }

                    if (s.StartsWith("TimedRestart"))
                    {
                        String[] temp = s.Split('=');
                        restartServerTimeOption.Checked = Convert.ToBoolean(temp[1]);
                        restartServerTimeOption_CheckedChanged(null, null);
                    }

                    if (s.StartsWith("TimerRestartTime"))
                    {
                        String[] temp = s.Split('=');
                        restartServerTime.Text = Convert.ToString(temp[1]);
                    }

                    if (s.StartsWith("RememberPassword"))
                    {
                        String[] temp = s.Split('=');
                        rememberSteamPass.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("SteamPassword"))
                    {
                        String[] temp = s.Split('=');
                        if (!temp[1].Equals(""))
                        {
                            steamPassword.Text = StringCipher.Decrypt(Convert.ToString(temp[1]), Environment.UserName);
                        }

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
            settings.Add(new Settings { Variable = "ServerName", Value = "My Server", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Server name. Has a soft limit of 255 characters due to Steam server limitations.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "MaxPlayers", Value = "36", Script = "[/Script/Engine.GameSession]", Tooltip = "Maximum player count for the server.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "Password", Value = "", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Server password. Has a soft limit of 255 characters due to Steam server limitations.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "MOTD", Value = "Welcome to the server.", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Server's MOTD, displayed in character creation.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "MaxPlayerClaims", Value = "3", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Maximum claims per group or player.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "MaxZombieCount", Value = "2048", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "The absolute hard-cap for zombie NPCs. If this many zombies are on the server, no more will be allowed to spawn.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "MaxAnimalCount", Value = "100", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "The absolute hard-cap for animal NPCs. If this many animals are on the server, no more will be allowed to spawn.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "MaxBanditCount", Value = "256", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "The absolute hard-cap for non-safezone human NPCs. If this many human NPCs are on the server, no more will be allowed to spawn.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "PVP", Value = "true", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Toggles whether or not PVP is enabled. If this is false, no damage can be inflicted by one player on another.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "FallDamageMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Change how much damage falling does here. 1.0 is full damage, 0 is no damage at all.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "AnimalSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.FlockSpawner]", Tooltip = "How many more animals to spawn than usual.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "ZombieSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.GlobalAISpawner]", Tooltip = "How many more zombies to spawn than usual.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "Timescale", Value = "5.5", Script = "[/Script/DeadMatter.Agenda]", Tooltip = "The timescale, relative to real time. The default value of 5.5 indicates that one real-life second is 5.5 seconds ingame.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "AttackMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How strongly the zombies do damage. Set to zero to disable.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "DefenseMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How much the zombies soak up hits. Set to zero to make them made of paper.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "SteamQueryIP", Value = "0.0.0.0", Script = "[/Script/DeadMatter.ServerInfoProxy]", Tooltip = "Change the Steam query host.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "SteamQueryPort", Value = "27016", Script = "[/Script/DeadMatter.ServerInfoProxy]", Tooltip = "Change the Steam query port.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "WhitelistActive", Value = "false", Script = "[/Script/DeadMatter.SurvivalBaseGamemode]", Tooltip = "If the server whitelist is enabled.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "Port", Value = "7777", Script = "[URL]", Tooltip = "Change the server's port.", IniFile = "Engine.ini" });
            settings.Add(new Settings { Variable = "grass.DensityScale", Value = "1.0", Script = "[/Script/Engine.RenderSettings]", Tooltip = "Set lower for possible performance gains (untested)", IniFile = "Engine.ini" });
            settings.Add(new Settings { Variable = "foliage.DensityScale", Value = "1.0", Script = "[/Script/Engine.RenderSettings]", Tooltip = "Set lower for possible performance gains (untested)", IniFile = "Engine.ini" });


            foreach (Settings s in settings)
            {
                configSettings.Rows.Add(s.Variable, s.Value, s.Script, s.Tooltip, s.IniFile);
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
            steamCMD.StartInfo.Arguments = "+login " + steamID.Text + " " + steamPassword.Text + @" +force_install_dir """ + serverFolderPath.Text + @""" +app_update 1110990 +quit";
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
                        if(configVariable[0].Equals("SteamQueryPort"))
                        {
                            steamQueryPort = Convert.ToInt16(configVariable[1]);
                        }

                        if (configVariable[0].Equals("SteamQueryIP"))
                        {
                            //ipAddress = configVariable[1].ToString();
                        }

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
                if (configVariable[0] == "ServerTags")
                {
                    serverTagsDGV.Rows.Add(configVariable[1]);
                }
            }

            foreach (string configLine in configEngine)
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
            }
        }

        private void saveConfig_Click(object sender, EventArgs e)
        {
            List<WriteConfig> writeConfigs = new List<WriteConfig>();

            //game.ini
            foreach (DataGridViewRow dataGridViewRow in configSettings.Rows)
            {
                if (dataGridViewRow.Cells[4].Value.ToString().Equals("Game.ini"))
                {
                    if (writeConfigs.Exists(p => p.Script == dataGridViewRow.Cells[2].Value))
                    {
                        if (dataGridViewRow.Cells[1].Value != null)
                        {
                            writeConfigs.Find(p => p.Script == dataGridViewRow.Cells[2].Value).Values += Environment.NewLine + dataGridViewRow.Cells[0].Value.ToString() + "=" + dataGridViewRow.Cells[1].Value.ToString();
                        }
                    }
                    else
                    {
                        writeConfigs.Add(new WriteConfig { Script = dataGridViewRow.Cells[2].Value.ToString(), Values = dataGridViewRow.Cells[0].Value + "=" + dataGridViewRow.Cells[1].Value });
                    }
                    scripts.Add(dataGridViewRow.Cells[2].Value.ToString());
                }

                if (dataGridViewRow.Cells[0].Value.ToString().Equals("SteamQueryPort"))
                {
                    steamQueryPort = Convert.ToInt16(dataGridViewRow.Cells[1].Value.ToString());
                }

                if (dataGridViewRow.Cells[0].Value.ToString().Equals("SteamQueryIP"))
                {
                    //ipAddress = dataGridViewRow.Cells[1].ToString();
                }
            }

            foreach (DataGridViewRow row in whitelistDGV.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    writeConfigs.Find(p => p.Script.Equals("[/Script/DeadMatter.DMGameSession]")).Values += Environment.NewLine + "Whitelist=" + row.Cells[0].Value;
                }
            }
            foreach (DataGridViewRow row in adminDGV.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    writeConfigs.Find(p => p.Script.Equals("[/Script/DeadMatter.DMGameSession]")).Values += Environment.NewLine + "Admins=" + row.Cells[0].Value;
                }
            }
            foreach (DataGridViewRow row in superAdminDGV.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    writeConfigs.Find(p => p.Script.Equals("[/Script/DeadMatter.DMGameSession]")).Values += Environment.NewLine + "SuperAdmins=" + row.Cells[0].Value;
                }
            }
            foreach (DataGridViewRow row in serverTagsDGV.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    writeConfigs.Find(p => p.Script.Equals("[/Script/DeadMatter.DMGameSession]")).Values += Environment.NewLine + "ServerTags=" + row.Cells[0].Value;
                }
            }

            string gameIni = "";

            foreach (WriteConfig writeConfig in writeConfigs)
            {
                gameIni += writeConfig.Script + Environment.NewLine + writeConfig.Values + Environment.NewLine;
            }

            FileInfo fileInfo = new FileInfo(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini");
            if (fileInfo.IsReadOnly)
            {
                fileInfo.IsReadOnly = false;
            }
            File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini", gameIni);
            fileInfo.IsReadOnly = true;



            //engine.ini
            writeConfigs.Clear();

            foreach (DataGridViewRow dataGridViewRow in configSettings.Rows)
            {
                if (dataGridViewRow.Cells[4].Value.ToString().Equals("Engine.ini"))
                {
                    if (writeConfigs.Exists(p => p.Script == dataGridViewRow.Cells[2].Value))
                    {
                        if (dataGridViewRow.Cells[1].Value != null)
                        {
                            writeConfigs.Find(p => p.Script == dataGridViewRow.Cells[2].Value).Values += Environment.NewLine + dataGridViewRow.Cells[0].Value.ToString() + "=" + dataGridViewRow.Cells[1].Value.ToString();
                        }
                    }
                    else
                    {
                        writeConfigs.Add(new WriteConfig { Script = dataGridViewRow.Cells[2].Value.ToString(), Values = dataGridViewRow.Cells[0].Value + "=" + dataGridViewRow.Cells[1].Value });
                    }
                    scripts.Add(dataGridViewRow.Cells[2].Value.ToString());
                }
            }

            string defaultEngine = File.ReadAllText("defaultEngine.txt");

            foreach (WriteConfig writeConfig in writeConfigs)
            {
                defaultEngine += Environment.NewLine + writeConfig.Script + Environment.NewLine + writeConfig.Values;
            }

            fileInfo = new FileInfo(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini");
            if (fileInfo.IsReadOnly)
            {
                fileInfo.IsReadOnly = false;
            }

            File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini", defaultEngine);
            fileInfo.IsReadOnly = true;
            MessageBox.Show("Config file saved", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveData()
        {
            string steamPass = "";
            if (rememberSteamPass.Checked)
            {
                steamPass = StringCipher.Encrypt(steamPassword.Text, Environment.UserName);
            }

            File.WriteAllText(configFilePath, "SteamCMDPath=" + steamCMDPath.Text + Environment.NewLine +
                "ServerPath=" + serverFolderPath.Text + Environment.NewLine +
                "SteamID=" + steamID.Text + Environment.NewLine +
                "UpdateServer=" + checkUpdateOnStart.Checked + Environment.NewLine +
                "MaxMemory=" + maxServerMemory.Text + Environment.NewLine +
                "StartWithWindows=" + autoStartWithWindows.Checked + Environment.NewLine +
                "StartServerOnLaunch=" + autoStartServer.Checked + Environment.NewLine +
                "LastStart=" + serverStartTime + Environment.NewLine +
                "TimedRestart=" + restartServerTimeOption.Checked + Environment.NewLine +
                "TimerRestartTime=" + restartServerTime.Text + Environment.NewLine +
                "RememberPassword=" + rememberSteamPass.Checked + Environment.NewLine +
                "SteamPassword=" + steamPass
                );


        }

        private void startServer_Click(object sender, EventArgs e)
        {
            //check for steam_appid.txt
            if (!File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Binaries\Win64\steam_appid.txt"))
            {
                File.Create(serverFolderPath.Text + "\\" + @"deadmatter\Binaries\Win64\steam_appid.txt");
                File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Binaries\Win64\steam_appid.txt", "575440");
            }
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
                dmServer = Process.GetProcessesByName("deadmatterServer");
                if (dmServer.Length != 0)
                {
                    Process[] dmServerShipping = Process.GetProcessesByName("deadmatterServer-Win64-Shipping");
                    if (dmServerShipping.Length != 0)
                    {
                        memory = dmServerShipping[0].WorkingSet64;
                        string memoryGB = SizeSuffix(memory, 2);
                        SetText(memoryUsed, memoryGB, Color.Black, true);
                        SetText(serverStatus, "SERVER RUNNING", Color.Green, true);
                        serverStatus.ForeColor = Color.Green;
                        SetText(startServer, "Start Server", Color.Black, false);
                        SetText(stopServer, "Stop Server", Color.Black, true);
                        
                        
                        SetReadOnly(restartServerTimeOption, false);
                        SetReadOnly(restartServerTime, false);
                        SetReadOnly(maxServerMemory, false);
                        string maxMem = ReadControl(maxServerMemory);
                        if (maxMem == "")
                        {
                            maxMem = "100";
                        }

                        SetProgress(memoryUsedProgressBar, Convert.ToDouble(maxMem), Convert.ToDouble(memory / 1024 / 1024 / 1024));

                        TimeSpan uptime = DateTime.Now - serverStartTime;

                        SetText(serverUptime, uptime.Hours.ToString("00") + ":" + uptime.Minutes.ToString("00") + ":" + uptime.Seconds.ToString("00"), Color.Black, true);

                        if(uptime.Minutes % 1 == 0 && uptime.Seconds % 30 == 0)
                        {
                            try
                            {
                                serverIP = IPAddress.Parse(GetPublicIP());
                                serverInfo = new A2S_INFO(new IPEndPoint(serverIP, steamQueryPort));
                                SetText(onlinePlayers, serverInfo.Players + "/" + serverInfo.MaxPlayers, Color.Black, true);
                            }
                            catch
                            {
                                SetText(onlinePlayers, "", Color.Black, true);
                            }
                        }
                        

                        if (uptime.Minutes % 10 == 0 && uptime.Seconds % 30 == 0)
                        {
                            WebClient webClient = new WebClient();
                            string releaseVersion;
                            try
                            {
                                releaseVersion = webClient.DownloadString("https://www.winglessraven.com/DMSM.html");
                            }
                            catch
                            {
                                releaseVersion = this.ProductVersion.ToString();
                            }
                            Version version = new Version(releaseVersion);
                            //MessageBox.Show(version.ToString() + Environment.NewLine + this.ProductVersion);
                            if (version.CompareTo(new Version(this.ProductVersion)) > 0)
                            {
                                SetText(updateSoftware, "Version " + releaseVersion + " available - click to update now", Color.Blue, true);
                            }
                        }

                        string restartTime = ReadControl(restartServerTime);

                        if ((Convert.ToDouble(memory) / 1024 / 1024 / 1024 > Convert.ToDouble(maxMem) && !killSent) || (restartServerTimeOption.Checked && restartTime == uptime.Hours.ToString()))
                        {
                            int processID = dmServerShipping[0].Id;
                            Process.Start("windows-kill.exe", "-SIGINT " + processID);
                            uptime = new TimeSpan(0, 0, 0);
                            killSent = true;
                        }
                    }
                }
                else
                {
                    SetText(memoryUsed, "0 GB", Color.Black, true);
                    SetText(serverStatus, "SERVER OFFLINE", Color.Red, true);
                    SetText(startServer, "Start Server", Color.Black, true);
                    SetText(stopServer, "Stop Server", Color.Black, false);
                    SetProgress(memoryUsedProgressBar, 100, 0);
                    SetReadOnly(restartServerTimeOption, true);
                    SetReadOnly(restartServerTime, true);
                    SetReadOnly(maxServerMemory, true);
                    SetText(onlinePlayers, "", Color.Black, true);
                    if (serverStarted && firstTimeServerStarted)
                    {
                        if (checkUpdateOnStart.Checked)
                        {
                            //not currently used
                            updateServer_Click(this, null);
                            string serverExe = serverFolderPath.Text + @"\deadmatterServer.exe";
                            Process.Start(serverExe, "-USEALLAVAILABLECORES -log");
                        }
                        else
                        {
                            SetText(startServer, "Start Server", Color.Black, false);
                            Process dmServerExe = new Process();
                            dmServerExe.StartInfo.FileName = serverFolderPath.Text + @"\deadmatterServer.exe";
                            dmServerExe.StartInfo.Arguments = "-USEALLAVAILABLECORES -log";
                            dmServerExe.Start();
                            Thread.Sleep(500);
                            serverStartTime = DateTime.Now;
                            SaveData();
                        }
                        killSent = false;
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
            public string IniFile { get; set; }
        }

        public class WriteConfig
        {
            public string Script { get; set; }
            public string Values { get; set; }
            public bool AlreadyExists { get; set; }
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
                int processID = dmServer[0].Id;
                Process.Start("windows-kill.exe", "-SIGINT " + processID);
            }
            serverStarted = false;
            firstTimeServerStarted = false;
            serverUptime.Text = "00:00:00";
        }

        private void checkUpdateOnStart_CheckedChanged(object sender, EventArgs e)
        {
            SaveData();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://paypal.me/winglessraven");
        }

        private void autoStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (autoStartWithWindows.Checked)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("Dead Matter Server Manager", "\"" + Application.ExecutablePath + "\"");
                }
            }
            else
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.DeleteValue("Dead Matter Server Manager", false);
                }
            }

            SaveData();
        }

        private void autoStartServer_CheckedChanged(object sender, EventArgs e)
        {
            SaveData();
        }

        private void updateSoftware_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://github.com/winglessraven/DeadMatterServerManager/releases/latest/download/DeadMatterServerManager.msi", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
            Environment.Exit(0);
        }

        private void restartServerTimeOption_CheckedChanged(object sender, EventArgs e)
        {
            if (restartServerTimeOption.Checked)
            {
                restartServerTime.Enabled = true;
            }
            else
            {
                restartServerTime.Enabled = false;
            }

            SaveData();
        }

        private void restartServerTime_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void rememberSteamPass_CheckedChanged(object sender, EventArgs e)
        {
            SaveData();
        }

        public static class StringCipher
        {
            // This constant is used to determine the keysize of the encryption algorithm in bits.
            // We divide this by 8 within the code below to get the equivalent number of bytes.
            private const int Keysize = 256;

            // This constant determines the number of iterations for the password bytes generation function.
            private const int DerivationIterations = 1000;

            public static string Encrypt(string plainText, string passPhrase)
            {
                // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
                // so that the same Salt and IV values can be used when decrypting.  
                var saltStringBytes = Generate256BitsOfRandomEntropy();
                var ivStringBytes = Generate256BitsOfRandomEntropy();
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                                {
                                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                    cryptoStream.FlushFinalBlock();
                                    // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                    var cipherTextBytes = saltStringBytes;
                                    cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                    cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Convert.ToBase64String(cipherTextBytes);
                                }
                            }
                        }
                    }
                }
            }

            public static string Decrypt(string cipherText, string passPhrase)
            {
                // Get the complete stream of bytes that represent:
                // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
                var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
                var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
                // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
                var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
                // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
                var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

                using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
                {
                    var keyBytes = password.GetBytes(Keysize / 8);
                    using (var symmetricKey = new RijndaelManaged())
                    {
                        symmetricKey.BlockSize = 256;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;
                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                        {
                            using (var memoryStream = new MemoryStream(cipherTextBytes))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                                {
                                    var plainTextBytes = new byte[cipherTextBytes.Length];
                                    var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                    memoryStream.Close();
                                    cryptoStream.Close();
                                    return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                                }
                            }
                        }
                    }
                }
            }

            private static byte[] Generate256BitsOfRandomEntropy()
            {
                var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with cryptographically secure random bytes.
                    rngCsp.GetBytes(randomBytes);
                }
                return randomBytes;
            }


        }

        public class A2S_INFO
        {
            // \xFF\xFF\xFF\xFFTSource Engine Query\x00 because UTF-8 doesn't like to encode 0xFF
            public static readonly byte[] REQUEST = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69, 0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00 };
            #region Strong Typing Enumerators
            [Flags]
            public enum ExtraDataFlags : byte
            {
                GameID = 0x01,
                SteamID = 0x10,
                Keywords = 0x20,
                Spectator = 0x40,
                Port = 0x80
            }
            public enum VACFlags : byte
            {
                Unsecured = 0,
                Secured = 1
            }
            public enum VisibilityFlags : byte
            {
                Public = 0,
                Private = 1
            }
            public enum EnvironmentFlags : byte
            {
                Linux = 0x6C,   //l
                Windows = 0x77, //w
                Mac = 0x6D,     //m
                MacOsX = 0x6F   //o
            }
            public enum ServerTypeFlags : byte
            {
                Dedicated = 0x64,     //d
                Nondedicated = 0x6C,   //l
                SourceTV = 0x70   //p
            }
            #endregion
            public byte Header { get; set; }        // I
            public byte Protocol { get; set; }
            public string Name { get; set; }
            public string Map { get; set; }
            public string Folder { get; set; }
            public string Game { get; set; }
            public short ID { get; set; }
            public byte Players { get; set; }
            public byte MaxPlayers { get; set; }
            public byte Bots { get; set; }
            public ServerTypeFlags ServerType { get; set; }
            public EnvironmentFlags Environment { get; set; }
            public VisibilityFlags Visibility { get; set; }
            public VACFlags VAC { get; set; }
            public string Version { get; set; }
            public ExtraDataFlags ExtraDataFlag { get; set; }
            #region Extra Data Flag Members
            public ulong GameID { get; set; }           //0x01
            public ulong SteamID { get; set; }          //0x10
            public string Keywords { get; set; }        //0x20
            public string Spectator { get; set; }       //0x40
            public short SpectatorPort { get; set; }   //0x40
            public short Port { get; set; }             //0x80
            #endregion
            public A2S_INFO(IPEndPoint ep)
            {
                UdpClient udp = new UdpClient();
                udp.Send(REQUEST, REQUEST.Length, ep);
                udp.Client.ReceiveTimeout = 60;
                MemoryStream ms = new MemoryStream(udp.Receive(ref ep));    // Saves the received data in a memory buffer
                BinaryReader br = new BinaryReader(ms, Encoding.UTF8);      // A binary reader that treats charaters as Unicode 8-bit
                ms.Seek(4, SeekOrigin.Begin);   // skip the 4 0xFFs
                Header = br.ReadByte();
                Protocol = br.ReadByte();
                Name = ReadNullTerminatedString(ref br);
                Map = ReadNullTerminatedString(ref br);
                Folder = ReadNullTerminatedString(ref br);
                Game = ReadNullTerminatedString(ref br);
                ID = br.ReadInt16();
                Players = br.ReadByte();
                MaxPlayers = br.ReadByte();
                Bots = br.ReadByte();
                ServerType = (ServerTypeFlags)br.ReadByte();
                Environment = (EnvironmentFlags)br.ReadByte();
                Visibility = (VisibilityFlags)br.ReadByte();
                VAC = (VACFlags)br.ReadByte();
                Version = ReadNullTerminatedString(ref br);
                ExtraDataFlag = (ExtraDataFlags)br.ReadByte();
                #region These EDF readers have to be in this order because that's the way they are reported
                if (ExtraDataFlag.HasFlag(ExtraDataFlags.Port))
                    Port = br.ReadInt16();
                if (ExtraDataFlag.HasFlag(ExtraDataFlags.SteamID))
                    SteamID = br.ReadUInt64();
                if (ExtraDataFlag.HasFlag(ExtraDataFlags.Spectator))
                {
                    SpectatorPort = br.ReadInt16();
                    Spectator = ReadNullTerminatedString(ref br);
                }
                if (ExtraDataFlag.HasFlag(ExtraDataFlags.Keywords))
                    Keywords = ReadNullTerminatedString(ref br);
                if (ExtraDataFlag.HasFlag(ExtraDataFlags.GameID))
                    GameID = br.ReadUInt64();
                #endregion
                br.Close();
                ms.Close();
                udp.Close();
            }
            /// <summary>Reads a null-terminated string into a .NET Framework compatible string.</summary>
            /// <param name="input">Binary reader to pull the null-terminated string from.  Make sure it is correctly positioned in the stream before calling.</param>
            /// <returns>String of the same encoding as the input BinaryReader.</returns>
            public static string ReadNullTerminatedString(ref BinaryReader input)
            {
                StringBuilder sb = new StringBuilder();
                char read = input.ReadChar();
                while (read != '\x00')
                {
                    sb.Append(read);
                    read = input.ReadChar();
                }
                return sb.ToString();
            }
        }
        public static string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }
    }
}

