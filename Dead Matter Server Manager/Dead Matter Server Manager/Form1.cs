using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dead_Matter_Server_Manager
{
    public partial class Form1 : Form
    {
        private static List<Settings> settings = new List<Settings>();
        private static string configFilePath = Environment.SpecialFolder.ApplicationData + "\\DMSM.cfg";
        private List<String> scripts = new List<String>();
        public Form1()
        {
            InitializeComponent();
            CheckAppData();
            AddConfigRows();
        }

        private void CheckAppData()
        {
            if(File.Exists(configFilePath))
            {
                string[] cfg = File.ReadAllLines(configFilePath);

                foreach(string s in cfg)
                {
                    if(s.StartsWith("SteamCMDPath"))
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
                }
            }
            else
            {
                File.Create(configFilePath).Close();
            }
        }


        private void AddConfigRows()
        {
            settings.Add(new Settings { Variable = "ServerName", Value = "My Server", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "Server name. Has a soft limit of 255 characters due to Steam server limitations." });
            settings.Add(new Settings { Variable = "MaxPlayers", Value = "36", Script = "[/Script/Engine.GameSession]", Tooltip = "Maximum player count for the server." });
            settings.Add(new Settings { Variable = "Password", Value = "", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "Server password. Has a soft limit of 255 characters due to Steam server limitations." });
            settings.Add(new Settings { Variable = "MOTD", Value = "Welcome to the server.", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "Server's MOTD, displayed in character creation." });
            settings.Add(new Settings { Variable = "MaxPlayerClaims", Value = "3", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "Maximum claims per group or player." });
            settings.Add(new Settings { Variable = "MaxZombieCount", Value = "2048", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "The absolute hard-cap for zombie NPCs. If this many zombies are on the server, no more will be allowed to spawn." });
            settings.Add(new Settings { Variable = "MaxAnimalCount", Value = "100", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "The absolute hard-cap for animal NPCs. If this many animals are on the server, no more will be allowed to spawn." });
            settings.Add(new Settings { Variable = "MaxBanditCount", Value = "256", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "The absolute hard-cap for non-safezone human NPCs. If this many human NPCs are on the server, no more will be allowed to spawn." });
            settings.Add(new Settings { Variable = "PVP", Value = "true", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "Toggles whether or not PVP is enabled. If this is false, no damage can be inflicted by one player on another." });
            settings.Add(new Settings { Variable = "FallDamageMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.SurvivalBaseGameState]", Tooltip = "Change how much damage falling does here. 1.0 is full damage, 0 is no damage at all." });
            settings.Add(new Settings { Variable = "AnimalSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.FlockSpawner]", Tooltip = "How many more animals to spawn than usual." });
            settings.Add(new Settings { Variable = "ZombieSpawnMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.GlobalAISpawner]", Tooltip = "How many more zombies to spawn than usual." });
            settings.Add(new Settings { Variable = "Timescale", Value = "5.5", Script = "[/Script/DeadMatter.Agenda]", Tooltip = "The timescale, relative to real time. The default value of 5.5 indicates that one real-life second is 5.5 seconds ingame." });
            settings.Add(new Settings { Variable = "AttackMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How strongly the zombies do damage. Set to zero to disable." });
            settings.Add(new Settings { Variable = "DefenseMultiplier", Value = "1.0", Script = "[/Script/DeadMatter.ZombiePawn]", Tooltip = "How much the zombies soak up hits. Set to zero to make them made of paper." });

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
            if(result == DialogResult.OK)
            {
                steamCMDPath.Text = folderBrowserDialog.SelectedPath;
            }

            File.WriteAllText(configFilePath, "SteamCMDPath=" + steamCMDPath.Text + Environment.NewLine + "ServerPath=" + serverFolderPath.Text + Environment.NewLine + "SteamID=" + steamID.Text);

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

            File.WriteAllText(configFilePath, "SteamCMDPath=" + steamCMDPath.Text + Environment.NewLine + "ServerPath=" + serverFolderPath.Text + Environment.NewLine + "SteamID=" + steamID.Text);
        }

        private void updateSteamCMD_Click(object sender, EventArgs e)
        {
            if(steamCMDPath.Text == "")
            {
                MessageBox.Show("SteamCMD path not selected", "No Path", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            WebClient downloader = new WebClient();
            downloader.DownloadFile("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip", String.Join("\\",steamCMDPath.Text,"steamcmd.zip"));
            using(ZipArchive zipFile = ZipFile.OpenRead(String.Join("\\", steamCMDPath.Text, "steamcmd.zip")))
            {
                foreach(ZipArchiveEntry zipArchiveEntry in zipFile.Entries)
                {
                    zipArchiveEntry.ExtractToFile(steamCMDPath.Text + "\\" + zipArchiveEntry.Name, true);
                }
            }

            MessageBox.Show("SteamCMD Updated", "SteamCMD Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void updateServer_Click(object sender, EventArgs e)
        {
            if(steamCMDPath.Text.Equals(""))
            {
                MessageBox.Show("Enter SteamCMD path first!", "No SteamCMD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(steamID.Text.Equals(""))
            {
                MessageBox.Show("Enter SteamID first!", "No Steam ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(steamPassword.Text.Equals(""))
            {
                MessageBox.Show("Enter Steam password first!", "No Steam Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                Process.Start(steamCMDPath.Text + "\\steamcmd.exe","+login " + steamID.Text + " " + steamPassword.Text + " +force_install_dir " + serverFolderPath.Text + " +app_update 1110990 +quit");
        }

        private void getConfig_Click(object sender, EventArgs e)
        {
            if(!File.Exists(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini"))
            {
                MessageBox.Show("Game.ini not found, try running the server once to initialize the config files", "Game.ini not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] configGame = File.ReadAllLines(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini");
            string[] configEngine = File.ReadAllLines(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Engine.ini");

            foreach(string configLine in configGame)
            {
                foreach(Settings s in settings)
                {
                    string[] configVariable = configLine.Split('=');
                    if(configVariable[0] == s.Variable)
                    {
                        foreach(DataGridViewRow dataGridViewRow in configSettings.Rows)
                        {
                            if(dataGridViewRow.Cells[0].Value.Equals(s.Variable))
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

            foreach(DataGridViewRow dataGridViewRow in configSettings.Rows)
            {
                if(writeConfigs.Exists(p => p.Script == dataGridViewRow.Cells[2].Value))
                {
                    writeConfigs.Find(p => p.Script == dataGridViewRow.Cells[2].Value).Values += Environment.NewLine + dataGridViewRow.Cells[0].Value.ToString() + "=" + dataGridViewRow.Cells[1].Value.ToString();
                }
                else
                {
                    writeConfigs.Add(new WriteConfig { Script = dataGridViewRow.Cells[2].Value.ToString(), Values = dataGridViewRow.Cells[0].Value + "=" + dataGridViewRow.Cells[1].Value});
                }
                scripts.Add(dataGridViewRow.Cells[2].Value.ToString());
            }

            string gameIni = "";

            foreach(WriteConfig writeConfig in writeConfigs)
            {
                gameIni += Environment.NewLine + writeConfig.Script + Environment.NewLine + writeConfig.Values;
            }

            File.WriteAllText(serverFolderPath.Text + "\\" + @"deadmatter\Saved\Config\WindowsServer\Game.ini", gameIni);
            MessageBox.Show("Config file saved", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
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
}
