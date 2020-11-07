using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JNogueira.Discord.Webhook.Client;
using Microsoft.Win32;
using Color = System.Drawing.Color;

namespace Dead_Matter_Server_Manager
{
    public partial class ServerManager : Form
    {
        internal static ServerManager serverManager;
        internal static RestartSchedule restartSchedule;

        private static List<Settings> settings = new List<Settings>();
        private static string configFilePath;
        private List<String> scripts = new List<String>();
        delegate void SetTextOnControl(Control controlToChange, string message, Color foreColour, bool enabled);
        delegate void AppendTextOnControl(RichTextBox controlToChange, string message, string type, string discordColour);
        delegate void SetProgressBar(ProgressBar progressBar, double maximum, double current);
        delegate string ReadControls(Control control);
        delegate void SetReadOnlyControl(Control control, bool enabled);
        delegate void SetOnlinePlayersDGV(DataGridView dGV, A2S_PLAYER onlinePlayers);
        delegate void SetListControlItems(ListBox controlToChange, bool addItem, string itemName);
        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private bool serverStarted;
        private bool firstTimeServerStarted;
        private DateTime serverStartTime;
        private IPAddress serverIP;
        private A2S_INFO serverInfo;
        private A2S_PLAYER playerInfo;
        private int steamQueryPort;
        private bool killSent;
        private DateTime killCommandSentAt;
        private TimeSpan timeSinceLastKill;
        private double restartsThisSession;
        private bool sessionStarted;
        private DateTime lastRestart;
        private int killAttempts;
        private int allTimeHighPlayers;
        private TimeSpan longestUptime;
        private string logFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\log.txt";
        TimeSpan uptime;
        bool plannedShutdown;
        private DateTime lastBackup;
        public static List<DateTime> restartSchedules = new List<DateTime>();
        public string currentDBfile = "SaveData_v04.db";

        public ServerManager()
        {
            InitializeComponent();
            VersionCheckOnStart();

            serverManager = this;

            //set form title to include version number
            this.Text = "Dead Matter Server Manager || " + this.ProductVersion;

            //set config file path
            configFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DMSM.cfg";
            AddConfigRows();
            CheckAppData();
            CheckBackups();

            //check if server is already running
            Process[] dmServerShipping = Process.GetProcessesByName("deadmatterServer-Win64-Shipping");
            if (dmServerShipping.Length != 0)
            {
                //server is running
                serverStarted = true;
                firstTimeServerStarted = true;
                sessionStarted = true;
                restartsThisSession += 1;
            }
            MonitorServer(maxServerMemory.Text);
            if (autoStartServer.Checked)
            {
                //auto start server is ticked, so set the flags to start the server
                startServer_Click(this, null);
            }

            GetSavedPlayers();

        }

        private void VersionCheckOnStart()
        {
            //check if previous update file exists
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi"))
            {
                //delete it
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
            }

            WebClient webClient = new WebClient();
            string releaseVersion;
            try
            {
                //try to get the latest release version
                releaseVersion = webClient.DownloadString("https://www.winglessraven.com/DMSM.html");
            }
            catch
            {
                //server not found
                releaseVersion = this.ProductVersion.ToString();
            }
            Version version = new Version(releaseVersion);

            if (version.CompareTo(new Version(this.ProductVersion)) > 0)
            {
                //newer version is available, prompt for update
                DialogResult result = MessageBox.Show("Update available, download now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    //download and run the msi
                    webClient = new WebClient();
                    webClient.DownloadFile("https://github.com/winglessraven/DeadMatterServerManager/releases/latest/download/DeadMatterServerManager.msi", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
                    try
                    {
                        Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
                        Environment.Exit(0);
                    }
                    catch
                    {
                        //can't find installer
                        DialogResult result1 = MessageBox.Show("Download failed, visit github now?", "Download Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (result1 == DialogResult.Yes)
                        {
                            Process.Start("https://github.com/winglessraven/DeadMatterServerManager/releases/latest");
                        }
                    }

                }
            }
        }

        private void CheckAppData()
        {
            if (File.Exists(configFilePath))
            {
                string[] cfg = File.ReadAllLines(configFilePath);

                foreach (string s in cfg)
                {
                    //read in the config and set saved settings
                    if (s.StartsWith("SteamCMDPath"))
                    {
                        String[] temp = s.Split('=');
                        steamCMDPath.Text = temp[1];
                    }

                    if (s.StartsWith("ServerPath"))
                    {
                        String[] temp = s.Split('=');
                        serverFolderPath.Text = temp[1];
                        if (!temp[1].Equals(""))
                        {
                            getConfig_Click(null, null);
                        }
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

                    if (s.StartsWith("AllTimeHighPlayers"))
                    {
                        String[] temp = s.Split('=');
                        try
                        {
                            allTimeHighPlayers = Convert.ToInt32(temp[1]);
                        }
                        catch
                        {
                            //not a number for some reason, reset to 0
                            allTimeHighPlayers = 0;
                        }
                    }

                    if (s.StartsWith("LongestUptime"))
                    {
                        String[] temp = s.Split('=');
                        try
                        {
                            longestUptime = TimeSpan.Parse(Convert.ToString(temp[1]));
                        }
                        catch
                        {
                            //not a valid timespan
                            longestUptime = new TimeSpan(0);
                        }
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
                    }

                    if (s.StartsWith("TimerRestartTime"))
                    {
                        String[] temp = s.Split('=');
                        try
                        {
                            restartServerTime.Text = Convert.ToString(Convert.ToDouble(temp[1]) * 60);
                        }
                        catch
                        {
                            //do nowt
                        }
                    }

                    if (s.StartsWith("MinsTimerRestartTime"))
                    {
                        String[] temp = s.Split('=');
                        if (!Convert.ToString(temp[1]).Equals(""))
                        {
                            restartServerTime.Text = Convert.ToString(Convert.ToDouble(temp[1]));
                        }
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

                    if (s.StartsWith("ChangeLaunchParams"))
                    {
                        String[] temp = s.Split('=');
                        changeLaunchParams.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("LaunchParams"))
                    {
                        String[] temp = s.Split('=');
                        launchParameters.Text = Convert.ToString(temp[1]);
                    }

                    if (s.StartsWith("SaveConfigOnStart"))
                    {
                        String[] temp = s.Split('=');
                        saveConfigOnStart.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("StartServerOnLaunch"))
                    {
                        String[] temp = s.Split('=');
                        autoStartServer.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("StartWithWindows"))
                    {
                        String[] temp = s.Split('=');
                        autoStartWithWindows.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("EnableLogging"))
                    {
                        String[] temp = s.Split('=');
                        enableLogging.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("BackgroundColour"))
                    {
                        String[] temp = s.Split('=');
                        backgroundColour.BackColor = ColorTranslator.FromHtml(temp[1]);
                        logTextBox.BackColor = ColorTranslator.FromHtml(temp[1]);
                    }

                    if (s.StartsWith("UserEventColour"))
                    {
                        String[] temp = s.Split('=');
                        userEventColour.BackColor = ColorTranslator.FromHtml(temp[1]);
                    }

                    if (s.StartsWith("MemoryLimitColour"))
                    {
                        String[] temp = s.Split('=');
                        memoryLimitColour.BackColor = ColorTranslator.FromHtml(temp[1]);
                    }

                    if (s.StartsWith("RestartTimerColour"))
                    {
                        String[] temp = s.Split('=');
                        timedRestartColour.BackColor = ColorTranslator.FromHtml(temp[1]);
                    }

                    if (s.StartsWith("ServerCrashColour"))
                    {
                        String[] temp = s.Split('=');
                        serverCrashColour.BackColor = ColorTranslator.FromHtml(temp[1]);
                    }

                    if (s.StartsWith("WebhookURL"))
                    {
                        String[] temp = s.Split('=');
                        webhookURL.Text = temp[1];
                    }

                    if (s.StartsWith("DiscordIntegration"))
                    {
                        String[] temp = s.Split('=');
                        discordWebHook.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("NotifyOnMemoryLimit"))
                    {
                        String[] temp = s.Split('=');
                        notifyOnMemoryLimit.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("NotifiyOnTimedRestart"))
                    {
                        String[] temp = s.Split('=');
                        notifyOnTimedRestart.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("NotifiyOnScheduledRestart"))
                    {
                        String[] temp = s.Split('=');
                        notifyOnScheduledRestart.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("NotifyOnCrash"))
                    {
                        String[] temp = s.Split('=');
                        notifiyOnCrash.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("DiscordTxtMemoryLimit"))
                    {
                        String[] temp = s.Split('=');
                        memoryLimitDiscordTxt.Text = temp[1];
                    }

                    if (s.StartsWith("DiscordTxtCrash"))
                    {
                        String[] temp = s.Split('=');
                        serverCrashedDiscordTxt.Text = temp[1];
                    }

                    if (s.StartsWith("DiscordTxtTimedRestart"))
                    {
                        String[] temp = s.Split('=');
                        timedRestartDiscordTxt.Text = temp[1];
                    }

                    if (s.StartsWith("DiscordTxtScheduledRestart"))
                    {
                        String[] temp = s.Split('=');
                        scheduledRestartDiscordTxt.Text = temp[1];
                    }

                    if (s.StartsWith("DiscordWebhookTest"))
                    {
                        String[] temp = s.Split('=');
                        webhookTestMsg.Text = temp[1];
                    }

                    if (s.StartsWith("DiscordIncludeAdditionalInfo"))
                    {
                        String[] temp = s.Split('=');
                        discordIncludeAdditional.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("EnableBackups"))
                    {
                        String[] temp = s.Split('=');
                        enableBackups.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("RestoreGameIni"))
                    {
                        String[] temp = s.Split('=');
                        restoreGameIni.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("RestoreEngineIni"))
                    {
                        String[] temp = s.Split('=');
                        restoreEngineIni.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("RestoreWorld"))
                    {
                        String[] temp = s.Split('=');
                        restoreWorldSave.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("BackupDestinationFolder"))
                    {
                        String[] temp = s.Split('=');
                        backupDestinationFolder.Text = Convert.ToString(temp[1]);
                    }

                    if (s.StartsWith("BackupSchedule"))
                    {
                        String[] temp = s.Split('=');
                        backupScheduleMinutes.Value = Convert.ToDecimal(temp[1]);
                    }

                    if (s.StartsWith("BackupRetention"))
                    {
                        String[] temp = s.Split('=');
                        backupRetentionQty.Value = Convert.ToDecimal(temp[1]);
                    }

                    if (s.StartsWith("ScheduledRestartOption"))
                    {
                        String[] temp = s.Split('=');
                        scheduledRestartOption.Checked = Convert.ToBoolean(temp[1]);
                    }

                    if (s.StartsWith("ScheduledRestartTimes"))
                    {
                        String[] temp = s.Split('=');
                        String[] times = temp[1].Split(',');
                        foreach (string time in times)
                        {
                            if(time != "")
                            {
                                restartSchedules.Add(Convert.ToDateTime(time));
                            }
                        }
                    }
                }
            }
            else
            {
                //config doesn't exist, create it
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "DeadMatterServerManager");
                Directory.CreateDirectory(specificFolder);
                File.Create(configFilePath).Close();
            }
        }


        private void AddConfigRows()
        {
            //add rows for server settings
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
            settings.Add(new Settings { Variable = "bVACSecure", Value = "true", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Whether or not to turn on VAC and EAC.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "bIsHardcore", Value = "false", Script = "[/Script/DeadMatter.DMGameSession]", Tooltip = "Whether or not to turn on hardcore mode.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "AnimalSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.FlockSpawner]", Tooltip = "How many more animals to spawn than usual.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "ZombieSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.GlobalAISpawner]", Tooltip = "How many more zombies to spawn than usual.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "Timescale", Value = "5.5", Script = "[/Script/DeadMatter.Agenda]", Tooltip = "The timescale, relative to real time. The default value of 5.5 indicates that one real-life second is 5.5 seconds ingame.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "AttackMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How strongly the zombies do damage. Set to zero to disable.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "DefenseMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How much the zombies soak up hits. Set to zero to make them made of paper.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "Host", Value = "0.0.0.0", Script = "[Steam]", Tooltip = "Host to advertise to Steam.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "SteamQueryPort", Value = "27016", Script = "[Steam]", Tooltip = "The port used to query A2S_INFO requests. This is what tells players who's on the server from the server browser.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "Port", Value = "7777", Script = "[Steam]", Tooltip = "Change the Steam advertised gameserver port. If this is absent it'll just use the server's port.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "SteamPort", Value = "7778", Script = "[Steam]", Tooltip = "Change the Steam communications port.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "WhitelistActive", Value = "false", Script = "[/Script/DeadMatter.SurvivalBaseGamemode]", Tooltip = "If the server whitelist is enabled.", IniFile = "Game.ini" });
            settings.Add(new Settings { Variable = "Port", Value = "7777", Script = "[URL]", Tooltip = "Change the server's port.", IniFile = "Engine.ini" });
            settings.Add(new Settings { Variable = "grass.DensityScale", Value = "1.1", Script = "[/Script/Engine.RenderSettings]", Tooltip = "Set lower for possible performance gains (untested)", IniFile = "Engine.ini" });
            settings.Add(new Settings { Variable = "foliage.DensityScale", Value = "1.1", Script = "[/Script/Engine.RenderSettings]", Tooltip = "Set lower for possible performance gains (untested)", IniFile = "Engine.ini" });


            foreach (Settings s in settings)
            {
                //add the settings to the DGV
                configSettings.Rows.Add(s.Variable, s.Value, s.Script, s.IniFile, s.Tooltip);
            }

            configSettings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void steamCMDBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "SteamCMD Folder"
            };
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                steamCMDPath.Text = folderBrowserDialog.SelectedPath;
            }

            SaveData();

        }

        private void serverFolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Dead Matter Server Folder"
            };
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

            if (!Directory.Exists(steamCMDPath.Text))
            {
                Directory.CreateDirectory(steamCMDPath.Text);
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

            if (!File.Exists(steamCMDPath.Text + "\\steamcmd.exe"))
            {
                DialogResult result = MessageBox.Show("SteamCMD not found - get it now?", "No SteamCMD", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    updateSteamCMD_Click(this, null);
                }
                else
                {
                    return;
                }
            }

            Process steamCMD = new Process();
            steamCMD.StartInfo.FileName = steamCMDPath.Text + "\\steamcmd.exe";
            steamCMD.StartInfo.Arguments = "+login " + steamID.Text + " " + steamPassword.Text + @" +force_install_dir """ + serverFolderPath.Text + @""" +app_update 1110990 +quit";
            steamCMD.Start();
            steamCMD.WaitForExit();
            if (steamCMD.ExitCode.Equals(5))
            {
                MessageBox.Show("Steam login error!" + Environment.NewLine + "Username or Password could be incorrect", "Steam Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void getConfig_Click(object sender, EventArgs e)
        {
            if (!File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini"))
            {
                //can't find the game.ini file
                MessageBox.Show("Game.ini not found, try running the server once to initialize the config files", "Game.ini not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] configGame = File.ReadAllLines(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini");
            string[] configEngine = File.ReadAllLines(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini");

            //clear existing data before reading in
            whitelistDGV.Rows.Clear();
            adminDGV.Rows.Clear();
            superAdminDGV.Rows.Clear();
            serverTagsDGV.Rows.Clear();

            foreach (string configLine in configGame)
            {
                string[] configVariable = configLine.Split('=');
                foreach (Settings s in settings)
                {
                    if (configVariable[0] == s.Variable)
                    {
                        if (configVariable[0].Equals("SteamQueryPort"))
                        {
                            steamQueryPort = Convert.ToInt32(configVariable[1]);
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
                if (dataGridViewRow.Cells[3].Value.ToString().Equals("Game.ini"))
                {
                    if (writeConfigs.Exists(p => p.Script.Equals(dataGridViewRow.Cells[2].Value)))
                    {
                        if (dataGridViewRow.Cells[1].Value != null)
                        {
                            writeConfigs.Find(p => p.Script.Equals(dataGridViewRow.Cells[2].Value)).Values += Environment.NewLine + dataGridViewRow.Cells[0].Value.ToString() + "=" + dataGridViewRow.Cells[1].Value.ToString();
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
                    steamQueryPort = Convert.ToInt32(dataGridViewRow.Cells[1].Value.ToString());
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
                    writeConfigs.Find(p => p.Script.Equals("[/Script/DeadMatter.SurvivalBaseGamemode]")).Values += Environment.NewLine + "Whitelist=" + row.Cells[0].Value;
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
            if (File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini"))
            {
                if (fileInfo.IsReadOnly)
                {
                    fileInfo.IsReadOnly = false;
                }
                File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini", gameIni);
                fileInfo.IsReadOnly = true;
            }

            //engine.ini
            writeConfigs.Clear();

            foreach (DataGridViewRow dataGridViewRow in configSettings.Rows)
            {
                if (dataGridViewRow.Cells[3].Value.ToString().Equals("Engine.ini"))
                {
                    if (writeConfigs.Exists(p => p.Script.Equals(dataGridViewRow.Cells[2].Value)))
                    {
                        if (dataGridViewRow.Cells[1].Value != null)
                        {
                            writeConfigs.Find(p => p.Script.Equals(dataGridViewRow.Cells[2].Value)).Values += Environment.NewLine + dataGridViewRow.Cells[0].Value.ToString() + "=" + dataGridViewRow.Cells[1].Value.ToString();
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
            if (File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini"))
            {
                if (fileInfo.IsReadOnly)
                {
                    fileInfo.IsReadOnly = false;
                }

                File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini", defaultEngine);
                fileInfo.IsReadOnly = true;
            }

            if (e != null)
            {
                MessageBox.Show("Config file saved", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            WriteLog("Config file saved", "INFO", null);
        }

        public void SaveData()
        {
            string steamPass = "";
            if (rememberSteamPass.Checked)
            {
                steamPass = StringCipher.Encrypt(steamPassword.Text, Environment.UserName);
            }

            string restartTimes = "";
            if (scheduledRestartOption.Checked)
            {
                foreach (DateTime time in restartSchedules)
                {
                    restartTimes += time.ToString("HH:mm") + ",";
                }

                //remove last ,
                if (restartTimes.Length != 0)
                {
                    restartTimes = restartTimes.Substring(0, restartTimes.Length - 1);
                }

            }

            File.WriteAllText(configFilePath, "SteamCMDPath=" + steamCMDPath.Text + Environment.NewLine +
                "ServerPath=" + serverFolderPath.Text + Environment.NewLine +
                "SteamID=" + steamID.Text + Environment.NewLine +
                "MaxMemory=" + maxServerMemory.Text + Environment.NewLine +
                "LastStart=" + serverStartTime + Environment.NewLine +
                "MinsTimerRestartTime=" + restartServerTime.Text + Environment.NewLine +
                "SteamPassword=" + steamPass + Environment.NewLine +
                "LaunchParams=" + launchParameters.Text + Environment.NewLine +
                "AllTimeHighPlayers=" + allTimeHighPlayers.ToString() + Environment.NewLine +
                "LongestUptime=" + longestUptime.ToString("c") + Environment.NewLine +
                "BackgroundColour=" + ColorTranslator.ToHtml(backgroundColour.BackColor) + Environment.NewLine +
                "UserEventColour=" + ColorTranslator.ToHtml(userEventColour.BackColor) + Environment.NewLine +
                "MemoryLimitColour=" + ColorTranslator.ToHtml(memoryLimitColour.BackColor) + Environment.NewLine +
                "RestartTimerColour=" + ColorTranslator.ToHtml(timedRestartColour.BackColor) + Environment.NewLine +
                "ServerCrashColour=" + ColorTranslator.ToHtml(serverCrashColour.BackColor) + Environment.NewLine +
                "BackgroundColour=" + ColorTranslator.ToHtml(backgroundColour.BackColor) + Environment.NewLine +
                "WebhookURL=" + webhookURL.Text + Environment.NewLine +
                "DiscordIntegration=" + discordWebHook.Checked + Environment.NewLine +
                "NotifyOnMemoryLimit=" + notifyOnMemoryLimit.Checked + Environment.NewLine +
                "NotifiyOnTimedRestart=" + notifyOnTimedRestart.Checked + Environment.NewLine +
                "NotifiyOnScheduledRestart=" + notifyOnScheduledRestart.Checked + Environment.NewLine +
                "NotifyOnCrash=" + notifiyOnCrash.Checked + Environment.NewLine +
                "DiscordTxtMemoryLimit=" + memoryLimitDiscordTxt.Text + Environment.NewLine +
                "DiscordTxtCrash=" + serverCrashedDiscordTxt.Text + Environment.NewLine +
                "DiscordTxtTimedRestart=" + timedRestartDiscordTxt.Text + Environment.NewLine +
                "DiscordTxtScheduledRestart=" + scheduledRestartDiscordTxt.Text + Environment.NewLine +
                "DiscordWebhookTest=" + webhookTestMsg.Text + Environment.NewLine +
                "DiscordIncludeAdditionalInfo=" + discordIncludeAdditional.Checked + Environment.NewLine +
                "EnableLogging=" + enableLogging.Checked + Environment.NewLine +
                "SaveConfigOnStart=" + saveConfigOnStart.Checked + Environment.NewLine +
                "UpdateServer=" + checkUpdateOnStart.Checked + Environment.NewLine +
                "StartServerOnLaunch=" + autoStartServer.Checked + Environment.NewLine +
                "TimedRestart=" + restartServerTimeOption.Checked + Environment.NewLine +
                "RememberPassword=" + rememberSteamPass.Checked + Environment.NewLine +
                "ChangeLaunchParams=" + changeLaunchParams.Checked + Environment.NewLine +
                "StartWithWindows=" + autoStartWithWindows.Checked + Environment.NewLine +
                "EnableBackups=" + enableBackups.Checked + Environment.NewLine +
                "BackupDestinationFolder=" + backupDestinationFolder.Text + Environment.NewLine +
                "BackupSchedule=" + backupScheduleMinutes.Value + Environment.NewLine +
                "BackupRetention=" + backupRetentionQty.Value + Environment.NewLine +
                "RestoreGameIni=" + restoreGameIni.Checked + Environment.NewLine +
                "RestoreEngineIni=" + restoreEngineIni.Checked + Environment.NewLine +
                "RestoreWorld=" + restoreWorldSave.Checked + Environment.NewLine +
                "ScheduledRestartOption=" + scheduledRestartOption.Checked + Environment.NewLine +
                "ScheduledRestartTimes=" + restartTimes
                );
        }

        private void startServer_Click(object sender, EventArgs e)
        {
            if (saveConfigOnStart.Checked)
            {
                saveConfig_Click(this, null);
            }

            uptime = new TimeSpan(0);

            //check for steam_appid.txt
            try
            {
                if (!File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Binaries\Win64\steam_appid.txt"))
                {
                    File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Binaries\Win64\steam_appid.txt", "575440");
                }

                WriteLog("SERVER START REQUEST SENT BY USER", "INFO", null);

                firstTimeServerStarted = true;
                serverStarted = true;
                sessionStarted = true;
            }
            catch
            {
                //folder/path not found
            }

        }

        private async void MonitorServer(string maxMemory)
        {
            SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            await Task.Delay(500);
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
                        SetText(memoryUsed, "Memory Used" + Environment.NewLine + memoryGB, Color.Black, true);
                        SetText(serverStatus, "SERVER RUNNING", Color.Green, true);
                        serverStatus.ForeColor = Color.Green;
                        SetText(startServer, "Start Server", Color.Black, false);
                        SetText(stopServer, "Stop Server", Color.Black, true);

                        SetReadOnly(restartServerTimeOption, false);
                        SetReadOnly(restartServerTime, false);
                        SetReadOnly(maxServerMemory, false);
                        SetReadOnly(restartServer, true);
                        SetReadOnly(restoreNow, false);
                        string maxMem = ReadControl(maxServerMemory);
                        if (maxMem == "")
                        {
                            maxMem = "100";
                        }

                        SetProgress(memoryUsedProgressBar, Convert.ToDouble(maxMem), Convert.ToDouble(memory / 1024 / 1024 / 1024));

                        uptime = DateTime.Now - serverStartTime;

                        SetText(serverUptime, uptime.Days.ToString("0") + "." + uptime.Hours.ToString("00") + ":" + uptime.Minutes.ToString("00") + ":" + uptime.Seconds.ToString("00"), Color.Black, true);

                        if (uptime.Minutes % 1 == 0 && uptime.Seconds % 30 == 0)
                        {
                            try
                            {
                                serverIP = IPAddress.Parse(GetPublicIP());
                                serverInfo = null;
                                serverInfo = new A2S_INFO(new IPEndPoint(serverIP, steamQueryPort));
                                SetText(onlinePlayers, "Online Players" + Environment.NewLine + serverInfo.Players + "/" + serverInfo.MaxPlayers, Color.Black, true);
                            }
                            catch (Exception ex)
                            {
                                SetText(onlinePlayers, "", Color.Black, true);
                            }

                            try
                            {
                                playerInfo = new A2S_PLAYER(new IPEndPoint(serverIP, steamQueryPort));
                                SetOnlinePlayers(playersOnlineDGV, playerInfo);
                            }
                            catch
                            {
                                SetOnlinePlayers(playersOnlineDGV, null);
                            }

                        }

                        if (uptime > longestUptime)
                        {
                            longestUptime = uptime;
                        }

                        if (serverInfo != null)
                        {
                            if (serverInfo.Players > allTimeHighPlayers)
                            {
                                allTimeHighPlayers = serverInfo.Players;
                                SaveData();
                            }
                        }


                        SetText(allTimeHighPlayersLbl, "All Time High Players" + Environment.NewLine + allTimeHighPlayers, Color.Black, true);
                        SetText(longestUptimeLbl, "Longest Uptime" + Environment.NewLine + longestUptime.ToString(@"d\.hh\:mm\:ss"), Color.Black, true);


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

                        if (killSent)
                        {
                            timeSinceLastKill = DateTime.Now - killCommandSentAt;
                        }

                        bool scheduleRestartTime = false;
                        if (scheduledRestartOption.Checked)
                        {
                            foreach (DateTime time in restartSchedules)
                            {
                                if (DateTime.Now.ToString("HH:mm").Equals(time.ToString("HH:mm")) && uptime.TotalMinutes > 1)
                                {
                                    scheduleRestartTime = true;
                                }
                            }
                        }

                        if ((Convert.ToDouble(memory) / 1024 / 1024 / 1024 > Convert.ToDouble(maxMem) && !killSent) || (restartServerTimeOption.Checked && restartTime == ((uptime.Hours * 60) + uptime.Minutes).ToString() && !killSent) || scheduleRestartTime && !killSent || killSent && timeSinceLastKill.Minutes >= 1 && killAttempts <= 3)
                        {
                            int processID = dmServerShipping[0].Id;
                            Process.Start("windows-kill.exe", "-SIGINT " + processID);

                            killSent = true;
                            killAttempts += 1;
                            killCommandSentAt = DateTime.Now;

                            if (Convert.ToDouble(memory) / 1024 / 1024 / 1024 > Convert.ToDouble(maxMem))
                            {
                                string tmp = null;
                                if (notifyOnMemoryLimit.Checked)
                                {
                                    tmp = memoryLimitDiscordTxt.Text;
                                }
                                WriteLog("MAX MEMORY HIT: " + Convert.ToDouble(memory) / 1024 / 1024 / 1024 + "/" + Convert.ToDouble(maxMem), "MEM LIMIT", tmp);
                            }

                            if (restartServerTimeOption.Checked && restartTime == ((uptime.Hours * 60) + uptime.Minutes).ToString())
                            {
                                string tmp = null;
                                if (notifyOnTimedRestart.Checked)
                                {
                                    tmp = timedRestartDiscordTxt.Text;
                                }
                                WriteLog("SERVER UPTIME LIMIT REACHED", "UPTIME LIMIT", tmp);
                            }

                            if (scheduleRestartTime)
                            {
                                string tmp = null;
                                if (notifyOnScheduledRestart.Checked)
                                {
                                    tmp = scheduledRestartDiscordTxt.Text;
                                }
                                WriteLog("SCHEDULED SERVER RESTART TIME REACHED", "SCHEDULED", tmp);
                            }

                            WriteLog("SERVER SHUTDOWN REQEST " + killAttempts + " SENT: Players Online= " + serverInfo.Players + ", Uptime= " + uptime.ToString(@"d\.hh\:mm\:ss"), "INFO", null);
                            plannedShutdown = true;
                        }
                        else
                        {
                            if (killSent && timeSinceLastKill.Minutes >= 1)
                            {
                                dmServerShipping[0].CloseMainWindow();

                                WriteLog("GRACEFUL SHUTDOWN FAIL:  Force Close Initiated", "ERROR", null);
                            }
                        }
                    }
                }
                else
                {
                    SetText(memoryUsed, "Memory Used" + Environment.NewLine + "0 GB", Color.Black, true);
                    SetText(serverStatus, "SERVER OFFLINE", Color.Red, true);
                    SetText(startServer, "Start Server", Color.Black, true);
                    SetText(stopServer, "Stop Server", Color.Black, false);
                    SetProgress(memoryUsedProgressBar, 100, 0);
                    SetReadOnly(restartServerTimeOption, true);
                    SetReadOnly(restartServerTime, true);
                    SetReadOnly(maxServerMemory, true);
                    SetReadOnly(restartServer, false);
                    SetReadOnly(restoreNow, true);
                    SetText(onlinePlayers, "", Color.Black, true);

                    SetText(allTimeHighPlayersLbl, "All Time High Players" + Environment.NewLine + allTimeHighPlayers, Color.Black, true);
                    SetText(longestUptimeLbl, "Longest Uptime" + Environment.NewLine + longestUptime.ToString(@"d\.hh\:mm\:ss"), Color.Black, true);

                    if (serverStarted && firstTimeServerStarted)
                    {
                        if (checkUpdateOnStart.Checked)
                        {
                            //not currently used
                            updateServer_Click(this, null);
                            string serverExe = serverFolderPath.Text + @"\deadmatterServer.exe";
                            Process.Start(serverExe, launchParameters.Text);
                        }
                        else
                        {
                            SetText(startServer, "Start Server", Color.Black, false);

                            int players = 0;
                            if (serverInfo != null)
                            {
                                players = serverInfo.Players;
                            }

                            if (uptime.Ticks != 0 && !plannedShutdown)
                            {
                                string tmp = null;
                                if (notifiyOnCrash.Checked)
                                {
                                    tmp = serverCrashedDiscordTxt.Text;
                                }
                                WriteLog("SERVER CRASHED - RESTARTED: Previous session uptime= " + uptime.ToString(@"d\.hh\:mm\:ss") + ", Players Online= " + players, "ERROR", tmp);
                            }
                            if (uptime.Ticks != 0 && plannedShutdown)
                            {
                                WriteLog("SERVER RESTARTED: Previous session uptime= " + uptime.ToString(@"d\.hh\:mm\:ss") + ", Players Online= " + players, "INFO", null);
                            }

                            Process dmServerExe = new Process();
                            dmServerExe.StartInfo.FileName = serverFolderPath.Text + @"\deadmatterServer.exe";
                            dmServerExe.StartInfo.Arguments = launchParameters.Text;
                            dmServerExe.Start();
                            Thread.Sleep(500);
                            serverStartTime = DateTime.Now;

                            plannedShutdown = false;

                            SaveData();

                            if (sessionStarted)
                            {
                                lastRestart = DateTime.Now;
                                SetText(restartsThisSessionTxt, "Restarts this Session" + Environment.NewLine + restartsThisSession, Color.Black, true);
                                SetText(lastRestartTxt, "Last Restart" + Environment.NewLine + lastRestart.ToString(), Color.Black, true);
                                restartsThisSession += 1;
                            }

                        }
                        killSent = false;
                        killAttempts = 0;
                    }
                }

                //backup schedule
                if (enableBackups.Checked && lastBackup.AddMinutes(Convert.ToDouble(backupScheduleMinutes.Value)) < DateTime.Now)
                {
                    backupNow_Click(this, null);
                }

            });

            await Monitor(maxServerMemory.Text);

        }

        public Task Monitor(string maxMemory)
        {
            MonitorServer(maxMemory);
            return Task.CompletedTask;
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

        public void AppendText(RichTextBox controlToChange, string message, string type, string discordMessage)
        {
            if (controlToChange.InvokeRequired)
            {
                AppendTextOnControl DDD = new AppendTextOnControl(AppendText);
                controlToChange.Invoke(DDD, controlToChange, message, type, discordMessage);
            }
            else
            {
                if (enableLogging.Checked)
                {
                    controlToChange.SelectionStart = controlToChange.TextLength;
                    controlToChange.SelectionLength = 0;

                    if (type.Equals("INFO"))
                    {
                        controlToChange.SelectionColor = userEventColour.BackColor;
                        SendDiscordWebHook(discordMessage, userEventColour.BackColor);
                    }

                    if (type.Equals("UPTIME LIMIT"))
                    {
                        controlToChange.SelectionColor = timedRestartColour.BackColor;
                        SendDiscordWebHook(discordMessage, timedRestartColour.BackColor);
                    }

                    if (type.Equals("SCHEDULED"))
                    {
                        controlToChange.SelectionColor = timedRestartColour.BackColor;
                        SendDiscordWebHook(discordMessage, timedRestartColour.BackColor);
                    }

                    if (type.Equals("MEM LIMIT"))
                    {
                        controlToChange.SelectionColor = memoryLimitColour.BackColor;
                        SendDiscordWebHook(discordMessage, memoryLimitColour.BackColor);
                    }

                    if (type.Equals("ERROR"))
                    {
                        controlToChange.SelectionColor = serverCrashColour.BackColor;
                        SendDiscordWebHook(discordMessage, serverCrashColour.BackColor);
                    }

                    controlToChange.AppendText(Environment.NewLine + message);
                    controlToChange.SelectionColor = controlToChange.ForeColor;
                }
                else
                {
                    SendDiscordWebHook(discordMessage, controlToChange.ForeColor);
                }

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

        public void SetOnlinePlayers(DataGridView dGV, A2S_PLAYER playerInfo)
        {
            if (dGV.InvokeRequired)
            {
                SetOnlinePlayersDGV DDD = new SetOnlinePlayersDGV(SetOnlinePlayers);
                dGV.Invoke(DDD, dGV, playerInfo);
            }
            else
            {
                dGV.Rows.Clear();

                if (playerInfo != null)
                {
                    foreach (A2S_PLAYER.Player player in playerInfo.Players)
                    {
                        TimeSpan time = TimeSpan.FromSeconds(player.Duration);
                        dGV.Rows.Add(player.Name, time.ToString(@"hh\:mm\:ss"));
                    }
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

        public class BackupFiles
        {
            public string FileName { get; set; }
            public DateTime CreatedDateTime { get; set; }
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
            sessionStarted = false;
            restartsThisSession = 0;
            WriteLog("SERVER SHUTDOWN REQUEST SENT BY USER", "INFO", null);
            SaveData();
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
        }

        private void updateSoftware_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://github.com/winglessraven/DeadMatterServerManager/releases/latest/download/DeadMatterServerManager.msi", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
            try
            {
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\DeadMatterServerManager.msi");
                Environment.Exit(0);
            }
            catch
            {
                //can't find installer
                DialogResult result = MessageBox.Show("Download failed, visit github now?", "Download Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    Process.Start("https://github.com/winglessraven/DeadMatterServerManager/releases/latest");
                }
            }
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
            public long SpectatorPort { get; set; }   //0x40
            public long Port { get; set; }             //0x80
            #endregion
            public A2S_INFO(IPEndPoint ep)
            {
                UdpClient udp = new UdpClient();
                udp.Send(REQUEST, REQUEST.Length, ep);
                udp.Client.ReceiveTimeout = 300;
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
                //#region These EDF readers have to be in this order because that's the way they are reported
                //if (ExtraDataFlag.HasFlag(ExtraDataFlags.Port))
                //    Port = br.ReadInt32();
                //if (ExtraDataFlag.HasFlag(ExtraDataFlags.SteamID))
                //    SteamID = br.ReadUInt64();
                //if (ExtraDataFlag.HasFlag(ExtraDataFlags.Spectator))
                //{
                //    SpectatorPort = br.ReadInt32();
                //    Spectator = ReadNullTerminatedString(ref br);
                //}
                //if (ExtraDataFlag.HasFlag(ExtraDataFlags.Keywords))
                //    Keywords = ReadNullTerminatedString(ref br);
                //if (ExtraDataFlag.HasFlag(ExtraDataFlags.GameID))
                //    GameID = br.ReadUInt64();
                //#endregion
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/winglessraven/DeadMatterServerManager/releases");
        }

        // <summary>Adopted from Valve description: https://developer.valvesoftware.com/wiki/Server_queries#A2S_PLAYER </summary>
        public class A2S_PLAYER
        {
            public struct Player
            {
                public Player(ref BinaryReader br)
                {
                    Index = br.ReadByte();
                    Name = ReadNullTerminatedString(ref br);
                    Score = br.ReadInt32();
                    Duration = br.ReadSingle();
                }
                public byte Index { get; set; }
                public string Name { get; set; }
                public int Score { get; set; }
                public float Duration { get; set; }

                public override string ToString()
                {
                    return Name;
                }
            }

            // \xFF\xFF\xFF\xFFU\xFF\xFF\xFF\xFF
            public static readonly byte[] REQUEST = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF };

            public byte Header { get; set; }        // D
            public Player[] Players { get; set; }

            public A2S_PLAYER(IPEndPoint ep)
            {
                UdpClient udp = new UdpClient();
                udp.Send(REQUEST, REQUEST.Length, ep); // Request Challenge.
                udp.Client.ReceiveTimeout = 300;
                byte[] challenge_response = udp.Receive(ref ep);
                if (challenge_response.Length == 9 && challenge_response[4] == 0x41) //B
                {
                    challenge_response[4] = 0x55; //U
                                                  // \xFF\xFF\xFF\xFFU[CHALLENGE]
                    udp.Send(challenge_response, challenge_response.Length, ep); // Request data.

                    MemoryStream ms = new MemoryStream(udp.Receive(ref ep));    // Saves the received data in a memory buffer
                    BinaryReader br = new BinaryReader(ms, Encoding.UTF8);      // A binary reader that treats charaters as Unicode 8-bit
                    ms.Seek(4, SeekOrigin.Begin);   // skip the 4 0xFFs
                    Header = br.ReadByte(); // D
                    Players = new Player[br.ReadByte()];
                    for (int i = 0; i < Players.Length; i++)
                        Players[i] = new Player(ref br);
                    br.Close();
                    ms.Close();
                    udp.Close();
                }
                else
                    throw new Exception("Response invalid.");

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

        private void refreshOnlinePlayerList_Click(object sender, EventArgs e)
        {
            serverIP = IPAddress.Parse(GetPublicIP());
            try
            {
                playerInfo = new A2S_PLAYER(new IPEndPoint(serverIP, steamQueryPort));
                SetOnlinePlayers(playersOnlineDGV, playerInfo);
            }
            catch
            {
                //no response from steam
                playersOnlineDGV.Rows.Clear();
            }

        }

        private void changeLaunchParams_CheckedChanged(object sender, EventArgs e)
        {
            if (changeLaunchParams.Checked)
            {
                launchParameters.ReadOnly = false;
            }
            else
            {
                launchParameters.ReadOnly = true;
                launchParameters.Text = "-USEALLAVAILABLECORES -log";
            }
        }

        private void launchParameters_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void restartServer_Click(object sender, EventArgs e)
        {
            Process[] dmServer;
            dmServer = Process.GetProcessesByName("deadmatterServer-Win64-Shipping");
            if (dmServer.Length != 0)
            {
                int processID = dmServer[0].Id;
                Process.Start("windows-kill.exe", "-SIGINT " + processID);
            }
        }

        private void WriteLog(string logText, string type, string discordMessage)
        {
            if (enableLogging.Checked)
            {
                if (!File.Exists(logFilePath))
                {
                    var file = File.Create(logFilePath);
                    file.Close();
                }

                AppendText(logTextBox, DateTime.Now.ToString("G") + "> " + logText, type, discordMessage);

                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    sw.WriteLine(DateTime.Now.ToString("G") + "> " + logText);
                }
            }
            else
            {
                AppendText(logTextBox, null, type, discordMessage);
            }
        }

        private void enableLogging_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void rememberSteamPass_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void changeLaunchParams_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void restartServerTimeOption_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void saveConfigOnStart_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void autoStartWithWindows_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void autoStartServer_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void openLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(logFilePath))
            {
                Process.Start(logFilePath);
            }
            else
            {
                MessageBox.Show("No log exists yet.  Run with logging enabled to create the file", "No Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundColour_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                backgroundColour.BackColor = colorDialog1.Color;
            }

            logTextBox.BackColor = backgroundColour.BackColor;

            SaveData();
        }

        private void userEventColour_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userEventColour.BackColor = colorDialog1.Color;
            }

            SaveData();
        }

        private void memoryLimitColour_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                memoryLimitColour.BackColor = colorDialog1.Color;
            }

            SaveData();
        }

        private void timedRestartColour_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                timedRestartColour.BackColor = colorDialog1.Color;
            }

            SaveData();
        }

        private void serverCrashColour_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                serverCrashColour.BackColor = colorDialog1.Color;
            }

            SaveData();
        }

        private void discordWebHook_CheckedChanged(object sender, EventArgs e)
        {
            if (discordWebHook.Checked)
            {
                webhookURL.Enabled = true;
            }
            else
            {
                webhookURL.Enabled = false;
                webhookURL.Text = "";
            }
        }

        private void discordWebHook_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private async void SendDiscordWebHook(string message, Color colour)
        {
            if (message != null)
            {
                try
                {
                    int players = 0;
                    int maxPlayers = 0;
                    if (serverInfo != null)
                    {
                        players = serverInfo.Players;
                        maxPlayers = serverInfo.MaxPlayers;
                    }

                    var client = new DiscordWebhookClient(webhookURL.Text);
                    DiscordMessage messageTxt;
                    if (discordIncludeAdditional.Checked)
                    {
                        messageTxt = new DiscordMessage(
                        " ",
                        embeds: new[]
                        {
                            new DiscordMessageEmbed(
                                message,
                                color: HexConverter(colour),
                                fields: new[]
                                {
                                    new DiscordMessageEmbedField("Previous Player Count", players + "/" + maxPlayers),
                                    new DiscordMessageEmbedField("Previous Uptime", uptime.ToString(@"d\.hh\:mm\:ss"))
                                },
                                footer: new DiscordMessageEmbedFooter("Powered by Dead Matter Server Manager","https://www.winglessraven.com/DMSM/images/DMSM-Icon.jpg")
                            )
                        }
                        );
                    }
                    else
                    {
                        messageTxt = new DiscordMessage(
                        " ",
                        embeds: new[]
                        {
                            new DiscordMessageEmbed(
                                message,
                                color: HexConverter(colour),
                                footer: new DiscordMessageEmbedFooter("Powered by Dead Matter Server Manager","https://www.winglessraven.com/DMSM/images/DMSM-Icon.jpg")
                            )
                        }
                        );
                    }

                    await client.SendToDiscord(messageTxt);

                }
                catch
                {
                    //fail!!
                }
            }

        }
        private static int HexConverter(Color c)
        {
            string hex = c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }

        private void webhookURL_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void notifyOnMemoryLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (notifyOnMemoryLimit.Checked)
            {
                memoryLimitDiscordTxt.Enabled = true;
            }
            else
            {
                memoryLimitDiscordTxt.Enabled = false;
            }

        }

        private void notifyOnTimedRestart_CheckedChanged(object sender, EventArgs e)
        {
            if (notifyOnTimedRestart.Checked)
            {
                timedRestartDiscordTxt.Enabled = true;
            }
            else
            {
                timedRestartDiscordTxt.Enabled = false;
            }
        }

        private void notifiyOnCrash_CheckedChanged(object sender, EventArgs e)
        {
            if (notifiyOnCrash.Checked)
            {
                serverCrashedDiscordTxt.Enabled = true;
            }
            else
            {
                serverCrashedDiscordTxt.Enabled = false;
            }
        }

        private void notifyOnMemoryLimit_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void notifyOnTimedRestart_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void notifiyOnCrash_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void testWebhook_Click(object sender, EventArgs e)
        {
            SaveData();
            SendDiscordWebHook(webhookTestMsg.Text, Color.Gold);
        }

        private void webhookTestMsg_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void memoryLimitDiscordTxt_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void timedRestartDiscordTxt_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void serverCrashedDiscordTxt_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void discordIncludeAdditional_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void enableBackups_MouseClick(object sender, MouseEventArgs e)
        {
            SaveData();
        }

        private void enableBackups_CheckedChanged(object sender, EventArgs e)
        {
            if (enableBackups.Checked)
            {
                backupDestinationFolder.ReadOnly = false;
                browseBackupDestinationFolder.Enabled = true;
                backupScheduleMinutes.Enabled = true;
                backupRetentionQty.Enabled = true;
                backupNow.Enabled = true;
            }
            else
            {
                backupDestinationFolder.ReadOnly = true;
                browseBackupDestinationFolder.Enabled = false;
                backupScheduleMinutes.Enabled = false;
                backupRetentionQty.Enabled = false;
                backupNow.Enabled = false;
            }
        }

        private void enableBackups_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void backupDestinationFolder_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void browseBackupDestinationFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Select Backup Folder Location"
            };
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                backupDestinationFolder.Text = folderBrowserDialog.SelectedPath;
            }

            SaveData();
        }

        private void backupScheduleMinutes_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void backupScheduleMinutes_Scroll(object sender, ScrollEventArgs e)
        {
            SaveData();
        }

        private void backupScheduleMinutes_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void backupRetentionQty_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void backupRetentionQty_Scroll(object sender, ScrollEventArgs e)
        {
            SaveData();
        }

        private void backupRetentionQty_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private async void backupNow_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    lastBackup = DateTime.Now;

                    var backupFile = ZipFile.Open(backupDestinationFolder.Text + "\\" + @"DM-Backup-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".zip", ZipArchiveMode.Create);

                    string gameIni = serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini";
                    string engineIni = serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini";


                    //get all db files (in case version updates change them)
                    string[] saveDB = Directory.GetFiles(serverFolderPath.Text + "\\" + @"deadmatter\Saved\sqlite3", "*.db", SearchOption.AllDirectories);

                    DateTime mostRecent = new DateTime(1990, 1, 1);
                    string mostRecentFile = "";

                    foreach (string s in saveDB)
                    {
                        FileInfo file = new FileInfo(s);
                        if (file.LastWriteTime > mostRecent)
                        {
                            mostRecent = file.LastWriteTime;
                            mostRecentFile = s;
                        }
                    }

                    backupFile.CreateEntryFromFile(mostRecentFile, Path.GetFileName(mostRecentFile), CompressionLevel.Optimal);

                    backupFile.CreateEntryFromFile(gameIni, Path.GetFileName(gameIni), CompressionLevel.Optimal);
                    backupFile.CreateEntryFromFile(engineIni, Path.GetFileName(engineIni), CompressionLevel.Optimal);

                    backupFile.Dispose();

                    WriteLog("Backup created successfully", "INFO", null);

                    CheckBackups();
                }
                catch (Exception ex)
                {
                    //file already exists for this second - don't dooo eeeeet
                    WriteLog("Backup Failed! " + ex.Message, "ERROR", null);
                }
            });
        }

        private void restoreGameIni_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void restoreEngineIni_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void RestoreNow_Click(object sender, EventArgs e)
        {
            SaveData();

            if (backupList.SelectedItem != null)
            {
                string backupFile = backupList.SelectedItem.ToString();
                string tempExtractPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeadMatterServerManager\\restore";

                if (!Directory.Exists(tempExtractPath))
                {
                    Directory.CreateDirectory(tempExtractPath);
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(tempExtractPath);

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                string gameIni = serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini";
                string engineIni = serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini";
                string worldSave = serverFolderPath.Text + "\\" + @"deadmatter\Saved\sqlite3\";

                string extractGameIni = tempExtractPath + @"\\Game.ini";
                string extractEngineIni = tempExtractPath + @"\\Engine.ini";

                ZipFile.ExtractToDirectory(backupFile, tempExtractPath);
                if (restoreGameIni.Checked)
                {
                    if (File.Exists(gameIni))
                    {
                        _ = new FileInfo(gameIni)
                        {
                            IsReadOnly = false
                        };
                        File.Delete(gameIni);
                    }
                    File.Move(extractGameIni, gameIni);
                    _ = new FileInfo(gameIni)
                    {
                        IsReadOnly = true
                    };
                }

                if (restoreEngineIni.Checked)
                {
                    if (File.Exists(engineIni))
                    {
                        _ = new FileInfo(engineIni)
                        {
                            IsReadOnly = false
                        };
                        File.Delete(engineIni);
                    }
                    File.Move(extractEngineIni, engineIni);
                    _ = new FileInfo(engineIni)
                    {
                        IsReadOnly = true
                    };
                }

                if (restoreWorldSave.Checked)
                {
                    string[] saveDB = Directory.GetFiles(tempExtractPath, "*.db", SearchOption.AllDirectories);

                    foreach (string s in saveDB)
                    {
                        FileInfo file = new FileInfo(s);

                        if (File.Exists(worldSave + file.Name))
                        {
                            _ = new FileInfo(worldSave + file.Name)
                            {
                                IsReadOnly = false
                            };
                            File.Delete(worldSave + file.Name);
                        }

                        File.Move(s, worldSave + file.Name);
                    }
                }

                WriteLog("Backup restored successfully", "INFO", null);
                MessageBox.Show("Backup restored!", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Select Backup to Restore First", "Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void CheckBackups()
        {
            if (backupDestinationFolder.Text != "")
            {
                ListControl(backupList, false, null);

                try
                {
                    if (Directory.Exists(backupDestinationFolder.Text))
                    {
                        string[] backupFiles = Directory.GetFiles(backupDestinationFolder.Text);

                        //filter down so we just get the files we want
                        List<BackupFiles> backupsList = new List<BackupFiles>();

                        DateTime mostRecent = new DateTime(0);
                        foreach (string s in backupFiles)
                        {
                            if (s.EndsWith(".zip") && s.StartsWith(backupDestinationFolder.Text + "\\" + @"DM-Backup"))
                            {
                                FileInfo fileInfo = new FileInfo(s);
                                if (fileInfo.LastWriteTime > mostRecent)
                                {
                                    mostRecent = fileInfo.LastWriteTime;
                                }
                                backupsList.Add(new BackupFiles() { FileName = s, CreatedDateTime = fileInfo.LastWriteTime });
                            }
                        }


                        //check qty of backups
                        if (backupsList.Count > backupRetentionQty.Value)
                        {
                            backupsList.OrderBy(o => o.CreatedDateTime).ToList();
                            decimal qtyToRemove = backupsList.Count - backupRetentionQty.Value;
                            for (int i = 1; i <= qtyToRemove; i++)
                            {
                                File.Delete(backupsList[0].FileName);
                                backupsList.RemoveAt(0);
                            }
                        }

                        foreach (BackupFiles files in backupsList)
                        {
                            ListControl(backupList, true, files.FileName);
                        }

                        if (mostRecent != new DateTime(0))
                        {
                            SetText(lastBackupTime, mostRecent.ToString(), Color.Black, true);
                            lastBackup = mostRecent;
                        }

                    }
                }
                catch
                {
                    SetText(lastBackupTime, "No Backup Found", Color.Black, true);
                }

            }
        }

        public void ListControl(ListBox controlToChange, bool addItem, string itemName)
        {
            if (controlToChange.InvokeRequired)
            {
                SetListControlItems DDD = new SetListControlItems(ListControl);
                controlToChange.Invoke(DDD, controlToChange, addItem, itemName);
            }
            else
            {
                if (addItem)
                {
                    controlToChange.Items.Add(itemName);
                }
                else
                {
                    controlToChange.Items.Clear();
                }
            }
        }

        private void scheduledRestartOption_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void scheduledRestartOption_CheckedChanged(object sender, EventArgs e)
        {
            if (scheduledRestartOption.Checked)
            {
                configureRestartSchedule.Enabled = true;
            }
            else
            {
                configureRestartSchedule.Enabled = false;
            }
        }

        private void restartSchedule_Click(object sender, EventArgs e)
        {
            RestartSchedule restartSchedule = new RestartSchedule();
            restartSchedule.Show();
        }

        private void notifyOnScheduledRestart_CheckedChanged(object sender, EventArgs e)
        {
            if (notifyOnScheduledRestart.Checked)
            {
                scheduledRestartDiscordTxt.Enabled = true;
            }
            else
            {
                scheduledRestartDiscordTxt.Enabled = false;
            }
        }

        private void scheduledRestartDiscordTxt_Leave(object sender, EventArgs e)
        {
            SaveData();
        }

        private void notifyOnScheduledRestart_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void GetSavedPlayers()
        {
            string connectionString = @"Data Source=" + serverFolderPath.Text + "\\" + @"deadmatter\Saved\sqlite3\" + currentDBfile + ";Version=3;";

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();

                string queryTxt = "SELECT DocumentID,OwningPlayerID,CharacterIDs FROM PlayerData";
                SQLiteCommand command = new SQLiteCommand(queryTxt, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    PlayerSteamInfo playerSteamInfo = new PlayerSteamInfo();
                    //string tmp = reader[1].ToString().Substring(13, 17);
                    playerSteamInfo.SteamName = GetSteamName(reader[1].ToString());
                    playerSteamInfo.CharacterIDs = reader[2].ToString();
                    serverPlayers.Items.Add(playerSteamInfo);
                }
            }
            catch
            {
                //error connecting to db - do something
            }

        }

        private string GetSteamName(string communityID)
        {
            try
            {
                WebClient client = new WebClient();
                string name = client.DownloadString("https://www.winglessraven.com/DMSM/getSteamName.php?userID=" + communityID);
                return name;
            }
            catch
            {
                return null;
            }
        }

        public class PlayerSteamInfo
        {
            public string CharacterIDs { get; set; }
            public string SteamName { get; set; }

            public override string ToString()
            {
                return SteamName;
            }
        }

        public class Character
        {
            public int CharacterKey { get; set; }
            public string Name { get; set; }
            public override string ToString()
            {
                return Name;
            }
        }

        public class CharacterLocation
        {
            public int CharacterKey { get; set; }
            public double TranslationX { get; set; }
            public double TranslationY { get; set; }
            public double TranslationZ { get; set; }
        }

        private void serverPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            playerCharacters.Items.Clear();
            xPosition.Text = "";
            yPosition.Text = "";
            zPosition.Text = "";
            inventoryData.Text = "";

            PlayerSteamInfo selectedPlayer = (PlayerSteamInfo)serverPlayers.SelectedItem;
            string tmp = selectedPlayer.CharacterIDs.Substring(15, selectedPlayer.CharacterIDs.Length - 15);
            tmp = tmp.Replace(")","");
            string[] characters = tmp.Split(',');
            string connectionString = @"Data Source=" + serverFolderPath.Text + "\\" + @"deadmatter\Saved\sqlite3\" + currentDBfile + ";Version=3;Read Only=true";

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();

                foreach(string s in characters)
                {
                    string queryTxt = "SELECT BasicData FROM Characters WHERE CharacterKey = '" + s + "'";
                    SQLiteCommand command = new SQLiteCommand(queryTxt, connection);
                    SQLiteDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Character character = new Character();
                        //string tmp = reader[1].ToString().Substring(13, 17);
                        character.CharacterKey = Convert.ToInt32(s);

                        string[] temp = reader[0].ToString().Split('=');
                        string name = Regex.Match(temp[1],"\"[^\"]*\"").ToString();
                        name += " " + Regex.Match(temp[2], "\"[^\"]*\"").ToString();
                        name = name.Replace("\"","");
                        character.Name = name;
                        playerCharacters.Items.Add(character);
                    }
                }
            }
            catch
            {
                //error connecting to db - do something
            }
        }

        private void playerCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            inventoryData.Text = "";
            Character character = (Character)playerCharacters.SelectedItem;
            int tmp = character.CharacterKey;

            string connectionString = @"Data Source=" + serverFolderPath.Text + "\\" + @"deadmatter\Saved\sqlite3\" + currentDBfile + ";Version=3;Read Only=True";

            SQLiteConnection connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();

                string queryTxt = "SELECT CharacterTransform, InventoryData FROM Characters WHERE CharacterKey = '" + tmp + "'";
                SQLiteCommand command = new SQLiteCommand(queryTxt, connection);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string[] temp = reader[0].ToString().Split('(');
                    string[] temp1 = temp[3].Split('=');
                    string xPos = temp1[1].Split(',')[0];
                    string yPos = temp1[2].Split(',')[0];
                    string zPos = temp1[3].Split(')')[0];

                    CharacterLocation characterLocation = new CharacterLocation();
                    characterLocation.CharacterKey = tmp;
                    characterLocation.TranslationX = Convert.ToDouble(xPos);
                    characterLocation.TranslationY = Convert.ToDouble(yPos);
                    characterLocation.TranslationZ = Convert.ToDouble(zPos);

                    xPosition.Text = "Position X: " + characterLocation.TranslationX;
                    yPosition.Text = "Position Y: " + characterLocation.TranslationY;
                    zPosition.Text = "Position Z: " + characterLocation.TranslationZ;

                    string[] items = reader[1].ToString().Split(new string[] { "ItemId=" }, StringSplitOptions.None);
                    List<string> itemNames = new List<string>();

                    foreach(string s in items)
                    {
                        string[] split = s.Split(',');
                        itemNames.Add(split[0]);
                    }

                    foreach(string s in itemNames)
                    {
                        if(!s.StartsWith("(EquipmentInventory"))
                        {
                            string trim = s.Replace(")","");
                            trim = trim.Replace("\"", "");
                            inventoryData.AppendText(trim + Environment.NewLine);
                        }
                    }
                }
            }
            catch
            {
                //error connecting to db - do something
                xPosition.Text = "";
                yPosition.Text = "";
                zPosition.Text = "";
                inventoryData.Text = "";
            }
        }
    }
}

