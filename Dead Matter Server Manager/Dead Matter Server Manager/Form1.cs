using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi"))
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

                    if(s.StartsWith("LastStart"))
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
            }

            string gameIni = "";

            foreach (WriteConfig writeConfig in writeConfigs)
            {
                gameIni += Environment.NewLine + writeConfig.Script + Environment.NewLine + writeConfig.Values;
            }

            //write whitelist players
            gameIni += Environment.NewLine + "[/Script/DeadMatter.DMGameSession]";
            foreach (DataGridViewRow row in whitelistDGV.Rows)
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
                if (row.Cells[0].Value != null)
                {
                    gameIni += Environment.NewLine + "SuperAdmins=" + row.Cells[0].Value;
                }

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

            foreach(WriteConfig writeConfig in writeConfigs)
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
            File.WriteAllText(configFilePath, "SteamCMDPath=" + steamCMDPath.Text + Environment.NewLine +
                "ServerPath=" + serverFolderPath.Text + Environment.NewLine +
                "SteamID=" + steamID.Text + Environment.NewLine +
                "UpdateServer=" + checkUpdateOnStart.Checked + Environment.NewLine +
                "MaxMemory=" + maxServerMemory.Text + Environment.NewLine +
                "StartWithWindows=" + autoStartWithWindows.Checked + Environment.NewLine +
                "StartServerOnLaunch=" + autoStartServer.Checked + Environment.NewLine +
                "LastStart=" + serverStartTime + Environment.NewLine +
                "TimedRestart=" + restartServerTimeOption.Checked + Environment.NewLine +
                "TimerRestartTime=" + restartServerTime.Text
                );
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            //check for steam_appid.txt
            if(!File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Binaries\Win64\steam_appid.txt"))
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
                    if(dmServerShipping.Length != 0)
                    {
                        memory = dmServerShipping[0].PagedMemorySize64;
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

                        if (Convert.ToDouble(memory) / 1024 / 1024 / 1024 > Convert.ToDouble(maxMem) || (restartServerTimeOption.Checked &&  restartTime == uptime.Hours.ToString()))
                        {
                            dmServerShipping[0].CloseMainWindow();
                            uptime = new TimeSpan(0, 0, 0);
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
                dmServer[0].CloseMainWindow();
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
    }
}
