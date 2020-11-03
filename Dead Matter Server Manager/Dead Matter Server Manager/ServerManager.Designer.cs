namespace Dead_Matter_Server_Manager
{
    partial class ServerManager
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerManager));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.steamCMDBrowse = new System.Windows.Forms.Button();
            this.serverFolderBrowse = new System.Windows.Forms.Button();
            this.steamCMDPath = new System.Windows.Forms.TextBox();
            this.serverFolderPath = new System.Windows.Forms.TextBox();
            this.updateSteamCMD = new System.Windows.Forms.Button();
            this.steamID = new System.Windows.Forms.TextBox();
            this.steamPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.updateServer = new System.Windows.Forms.Button();
            this.getConfig = new System.Windows.Forms.Button();
            this.saveConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.maxServerMemory = new System.Windows.Forms.TextBox();
            this.startServer = new System.Windows.Forms.Button();
            this.stopServer = new System.Windows.Forms.Button();
            this.serverStatus = new System.Windows.Forms.Label();
            this.memoryUsedProgressBar = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.configSettings = new System.Windows.Forms.DataGridView();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Script = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IniFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.whitelistDGV = new System.Windows.Forms.DataGridView();
            this.whiteListPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adminDGV = new System.Windows.Forms.DataGridView();
            this.adminPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.superAdminDGV = new System.Windows.Forms.DataGridView();
            this.superAdminPlayers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverTagsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.serverTagsDGV = new System.Windows.Forms.DataGridView();
            this.serverTags = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.playersOnlineTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.playersOnlineDGV = new System.Windows.Forms.DataGridView();
            this.playerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectionDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refreshOnlinePlayerList = new System.Windows.Forms.Button();
            this.statisticsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.enableLogging = new System.Windows.Forms.CheckBox();
            this.openLog = new System.Windows.Forms.LinkLabel();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.backgroundColour = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.userEventColour = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.memoryLimitColour = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.timedRestartColour = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.serverCrashColour = new System.Windows.Forms.Button();
            this.discordTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.discordWebHook = new System.Windows.Forms.CheckBox();
            this.webhookURL = new System.Windows.Forms.TextBox();
            this.notifyOnMemoryLimit = new System.Windows.Forms.CheckBox();
            this.notifyOnTimedRestart = new System.Windows.Forms.CheckBox();
            this.notifiyOnCrash = new System.Windows.Forms.CheckBox();
            this.memoryLimitDiscordTxt = new System.Windows.Forms.TextBox();
            this.timedRestartDiscordTxt = new System.Windows.Forms.TextBox();
            this.serverCrashedDiscordTxt = new System.Windows.Forms.TextBox();
            this.testWebhook = new System.Windows.Forms.Button();
            this.webhookTestMsg = new System.Windows.Forms.TextBox();
            this.discordIncludeAdditional = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.notifyOnScheduledRestart = new System.Windows.Forms.CheckBox();
            this.scheduledRestartDiscordTxt = new System.Windows.Forms.TextBox();
            this.backupsTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.enableBackups = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.backupDestinationFolder = new System.Windows.Forms.TextBox();
            this.browseBackupDestinationFolder = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.backupScheduleMinutes = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.backupRetentionQty = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.lastBackupTime = new System.Windows.Forms.Label();
            this.backupNow = new System.Windows.Forms.Button();
            this.backupList = new System.Windows.Forms.ListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.restoreGameIni = new System.Windows.Forms.CheckBox();
            this.restoreEngineIni = new System.Windows.Forms.CheckBox();
            this.restoreWorldSave = new System.Windows.Forms.CheckBox();
            this.restoreNow = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.uptimeLbl = new System.Windows.Forms.Label();
            this.serverUptime = new System.Windows.Forms.Label();
            this.autoStartWithWindows = new System.Windows.Forms.CheckBox();
            this.autoStartServer = new System.Windows.Forms.CheckBox();
            this.updateSoftware = new System.Windows.Forms.LinkLabel();
            this.restartServerTimelbl = new System.Windows.Forms.Label();
            this.restartServerTime = new System.Windows.Forms.TextBox();
            this.restartServerTimeOption = new System.Windows.Forms.CheckBox();
            this.rememberSteamPass = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.changeLaunchParams = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.launchParameters = new System.Windows.Forms.TextBox();
            this.saveConfigOnStart = new System.Windows.Forms.CheckBox();
            this.restartServer = new System.Windows.Forms.Button();
            this.checkUpdateOnStart = new System.Windows.Forms.CheckBox();
            this.onlinePlayers = new System.Windows.Forms.Label();
            this.restartsThisSessionTxt = new System.Windows.Forms.Label();
            this.lastRestartTxt = new System.Windows.Forms.Label();
            this.memoryUsed = new System.Windows.Forms.Label();
            this.allTimeHighPlayersLbl = new System.Windows.Forms.Label();
            this.longestUptimeLbl = new System.Windows.Forms.Label();
            this.scheduledRestartOption = new System.Windows.Forms.CheckBox();
            this.configureRestartSchedule = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.settingsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configSettings)).BeginInit();
            this.userTabPage.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whitelistDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superAdminDGV)).BeginInit();
            this.serverTagsTabPage.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverTagsDGV)).BeginInit();
            this.playersOnlineTabPage.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playersOnlineDGV)).BeginInit();
            this.statisticsTabPage.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.discordTabPage.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.backupsTabPage.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backupScheduleMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backupRetentionQty)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.steamCMDBrowse, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverFolderBrowse, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.steamCMDPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverFolderPath, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.updateSteamCMD, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.steamID, 7, 1);
            this.tableLayoutPanel1.Controls.Add(this.steamPassword, 7, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.updateServer, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.getConfig, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.saveConfig, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 6, 6);
            this.tableLayoutPanel1.Controls.Add(this.maxServerMemory, 7, 6);
            this.tableLayoutPanel1.Controls.Add(this.startServer, 7, 8);
            this.tableLayoutPanel1.Controls.Add(this.stopServer, 7, 9);
            this.tableLayoutPanel1.Controls.Add(this.serverStatus, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.memoryUsedProgressBar, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 7, 10);
            this.tableLayoutPanel1.Controls.Add(this.uptimeLbl, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.serverUptime, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.autoStartWithWindows, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.autoStartServer, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.updateSoftware, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.restartServerTimelbl, 6, 7);
            this.tableLayoutPanel1.Controls.Add(this.restartServerTime, 7, 7);
            this.tableLayoutPanel1.Controls.Add(this.restartServerTimeOption, 5, 7);
            this.tableLayoutPanel1.Controls.Add(this.rememberSteamPass, 7, 3);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel2, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.changeLaunchParams, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.launchParameters, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.saveConfigOnStart, 6, 8);
            this.tableLayoutPanel1.Controls.Add(this.restartServer, 6, 9);
            this.tableLayoutPanel1.Controls.Add(this.checkUpdateOnStart, 6, 10);
            this.tableLayoutPanel1.Controls.Add(this.onlinePlayers, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.restartsThisSessionTxt, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.lastRestartTxt, 4, 7);
            this.tableLayoutPanel1.Controls.Add(this.memoryUsed, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.allTimeHighPlayersLbl, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.longestUptimeLbl, 4, 8);
            this.tableLayoutPanel1.Controls.Add(this.scheduledRestartOption, 5, 8);
            this.tableLayoutPanel1.Controls.Add(this.configureRestartSchedule, 5, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1145, 670);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Steam CMD Path";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Server Folder Path";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamCMDBrowse
            // 
            this.steamCMDBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamCMDBrowse.Location = new System.Drawing.Point(354, 28);
            this.steamCMDBrowse.Name = "steamCMDBrowse";
            this.steamCMDBrowse.Size = new System.Drawing.Size(130, 24);
            this.steamCMDBrowse.TabIndex = 3;
            this.steamCMDBrowse.Text = "Browse";
            this.steamCMDBrowse.UseVisualStyleBackColor = true;
            this.steamCMDBrowse.Click += new System.EventHandler(this.steamCMDBrowse_Click);
            // 
            // serverFolderBrowse
            // 
            this.serverFolderBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverFolderBrowse.Location = new System.Drawing.Point(354, 58);
            this.serverFolderBrowse.Name = "serverFolderBrowse";
            this.serverFolderBrowse.Size = new System.Drawing.Size(130, 24);
            this.serverFolderBrowse.TabIndex = 4;
            this.serverFolderBrowse.Text = "Browse";
            this.serverFolderBrowse.UseVisualStyleBackColor = true;
            this.serverFolderBrowse.Click += new System.EventHandler(this.serverFolderBrowse_Click);
            // 
            // steamCMDPath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.steamCMDPath, 2);
            this.steamCMDPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamCMDPath.Location = new System.Drawing.Point(121, 28);
            this.steamCMDPath.Name = "steamCMDPath";
            this.steamCMDPath.Size = new System.Drawing.Size(227, 20);
            this.steamCMDPath.TabIndex = 5;
            // 
            // serverFolderPath
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.serverFolderPath, 2);
            this.serverFolderPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverFolderPath.Location = new System.Drawing.Point(121, 58);
            this.serverFolderPath.Name = "serverFolderPath";
            this.serverFolderPath.Size = new System.Drawing.Size(227, 20);
            this.serverFolderPath.TabIndex = 6;
            // 
            // updateSteamCMD
            // 
            this.updateSteamCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateSteamCMD.Location = new System.Drawing.Point(490, 28);
            this.updateSteamCMD.Name = "updateSteamCMD";
            this.updateSteamCMD.Size = new System.Drawing.Size(150, 24);
            this.updateSteamCMD.TabIndex = 7;
            this.updateSteamCMD.Text = "Update SteamCMD";
            this.updateSteamCMD.UseVisualStyleBackColor = true;
            this.updateSteamCMD.Click += new System.EventHandler(this.updateSteamCMD_Click);
            // 
            // steamID
            // 
            this.steamID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamID.Location = new System.Drawing.Point(954, 28);
            this.steamID.Name = "steamID";
            this.steamID.Size = new System.Drawing.Size(188, 20);
            this.steamID.TabIndex = 8;
            this.steamID.Leave += new System.EventHandler(this.steamID_Leave);
            // 
            // steamPassword
            // 
            this.steamPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamPassword.Location = new System.Drawing.Point(954, 58);
            this.steamPassword.Name = "steamPassword";
            this.steamPassword.PasswordChar = '*';
            this.steamPassword.Size = new System.Drawing.Size(188, 20);
            this.steamPassword.TabIndex = 9;
            this.steamPassword.Leave += new System.EventHandler(this.steamPassword_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(807, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "Steam ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(807, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 30);
            this.label5.TabIndex = 11;
            this.label5.Text = "Steam Password";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // updateServer
            // 
            this.updateServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateServer.Location = new System.Drawing.Point(646, 28);
            this.updateServer.Name = "updateServer";
            this.updateServer.Size = new System.Drawing.Size(155, 24);
            this.updateServer.TabIndex = 13;
            this.updateServer.Text = "Update Server";
            this.updateServer.UseVisualStyleBackColor = true;
            this.updateServer.Click += new System.EventHandler(this.updateServer_Click);
            // 
            // getConfig
            // 
            this.getConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.getConfig.Location = new System.Drawing.Point(490, 58);
            this.getConfig.Name = "getConfig";
            this.getConfig.Size = new System.Drawing.Size(150, 24);
            this.getConfig.TabIndex = 14;
            this.getConfig.Text = "Get Config";
            this.getConfig.UseVisualStyleBackColor = true;
            this.getConfig.Click += new System.EventHandler(this.getConfig_Click);
            // 
            // saveConfig
            // 
            this.saveConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveConfig.Location = new System.Drawing.Point(646, 58);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(155, 24);
            this.saveConfig.TabIndex = 17;
            this.saveConfig.Text = "Save Config";
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.saveConfig_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(807, 516);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 30);
            this.label1.TabIndex = 18;
            this.label1.Text = "Max Server Memory (GB)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maxServerMemory
            // 
            this.maxServerMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maxServerMemory.Location = new System.Drawing.Point(954, 519);
            this.maxServerMemory.Name = "maxServerMemory";
            this.maxServerMemory.Size = new System.Drawing.Size(188, 20);
            this.maxServerMemory.TabIndex = 19;
            this.maxServerMemory.Leave += new System.EventHandler(this.maxServerMemory_Leave);
            // 
            // startServer
            // 
            this.startServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startServer.Location = new System.Drawing.Point(954, 579);
            this.startServer.Name = "startServer";
            this.startServer.Size = new System.Drawing.Size(188, 24);
            this.startServer.TabIndex = 20;
            this.startServer.Text = "Start Server";
            this.startServer.UseVisualStyleBackColor = true;
            this.startServer.Click += new System.EventHandler(this.startServer_Click);
            // 
            // stopServer
            // 
            this.stopServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopServer.Location = new System.Drawing.Point(954, 609);
            this.stopServer.Name = "stopServer";
            this.stopServer.Size = new System.Drawing.Size(188, 24);
            this.stopServer.TabIndex = 22;
            this.stopServer.Text = "Stop Server";
            this.stopServer.UseVisualStyleBackColor = true;
            this.stopServer.Click += new System.EventHandler(this.stopServer_Click);
            // 
            // serverStatus
            // 
            this.serverStatus.AutoSize = true;
            this.serverStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverStatus.ForeColor = System.Drawing.Color.Red;
            this.serverStatus.Location = new System.Drawing.Point(3, 516);
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.Size = new System.Drawing.Size(112, 30);
            this.serverStatus.TabIndex = 23;
            this.serverStatus.Text = "SERVER OFFLINE";
            this.serverStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // memoryUsedProgressBar
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.memoryUsedProgressBar, 2);
            this.memoryUsedProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryUsedProgressBar.Location = new System.Drawing.Point(121, 519);
            this.memoryUsedProgressBar.Name = "memoryUsedProgressBar";
            this.memoryUsedProgressBar.Size = new System.Drawing.Size(227, 24);
            this.memoryUsedProgressBar.TabIndex = 24;
            // 
            // tabControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControl1, 8);
            this.tabControl1.Controls.Add(this.settingsTabPage);
            this.tabControl1.Controls.Add(this.userTabPage);
            this.tabControl1.Controls.Add(this.serverTagsTabPage);
            this.tabControl1.Controls.Add(this.playersOnlineTabPage);
            this.tabControl1.Controls.Add(this.statisticsTabPage);
            this.tabControl1.Controls.Add(this.discordTabPage);
            this.tabControl1.Controls.Add(this.backupsTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 111);
            this.tabControl1.Name = "tabControl1";
            this.tableLayoutPanel1.SetRowSpan(this.tabControl1, 2);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1139, 402);
            this.tabControl1.TabIndex = 25;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.BackColor = System.Drawing.Color.Transparent;
            this.settingsTabPage.Controls.Add(this.configSettings);
            this.settingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(1131, 376);
            this.settingsTabPage.TabIndex = 1;
            this.settingsTabPage.Text = "Settings";
            // 
            // configSettings
            // 
            this.configSettings.AllowUserToAddRows = false;
            this.configSettings.AllowUserToDeleteRows = false;
            this.configSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.configSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable,
            this.Value,
            this.Script,
            this.IniFile,
            this.Info});
            this.configSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configSettings.Location = new System.Drawing.Point(3, 3);
            this.configSettings.Name = "configSettings";
            this.configSettings.Size = new System.Drawing.Size(1125, 370);
            this.configSettings.TabIndex = 16;
            // 
            // Variable
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Variable.DefaultCellStyle = dataGridViewCellStyle1;
            this.Variable.HeaderText = "Variable";
            this.Variable.Name = "Variable";
            this.Variable.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // Script
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Script.DefaultCellStyle = dataGridViewCellStyle2;
            this.Script.HeaderText = "Script Location";
            this.Script.Name = "Script";
            this.Script.ReadOnly = true;
            // 
            // IniFile
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.IniFile.DefaultCellStyle = dataGridViewCellStyle3;
            this.IniFile.HeaderText = "Ini File";
            this.IniFile.Name = "IniFile";
            this.IniFile.ReadOnly = true;
            // 
            // Info
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Info.DefaultCellStyle = dataGridViewCellStyle4;
            this.Info.HeaderText = "Info";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            // 
            // userTabPage
            // 
            this.userTabPage.BackColor = System.Drawing.Color.Transparent;
            this.userTabPage.Controls.Add(this.tableLayoutPanel2);
            this.userTabPage.Location = new System.Drawing.Point(4, 22);
            this.userTabPage.Name = "userTabPage";
            this.userTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.userTabPage.Size = new System.Drawing.Size(1131, 376);
            this.userTabPage.TabIndex = 0;
            this.userTabPage.Text = "Admin/Whitelist";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.whitelistDGV, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.adminDGV, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.superAdminDGV, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 370F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1125, 370);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // whitelistDGV
            // 
            this.whitelistDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.whitelistDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.whitelistDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.whiteListPlayers});
            this.whitelistDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whitelistDGV.Location = new System.Drawing.Point(3, 3);
            this.whitelistDGV.Name = "whitelistDGV";
            this.whitelistDGV.Size = new System.Drawing.Size(369, 364);
            this.whitelistDGV.TabIndex = 0;
            // 
            // whiteListPlayers
            // 
            this.whiteListPlayers.HeaderText = "Whitelist Players (Steam64 ID)";
            this.whiteListPlayers.Name = "whiteListPlayers";
            // 
            // adminDGV
            // 
            this.adminDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.adminDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adminDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.adminPlayers});
            this.adminDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adminDGV.Location = new System.Drawing.Point(378, 3);
            this.adminDGV.Name = "adminDGV";
            this.adminDGV.Size = new System.Drawing.Size(369, 364);
            this.adminDGV.TabIndex = 1;
            // 
            // adminPlayers
            // 
            this.adminPlayers.HeaderText = "Admin Players (Steam64 ID)";
            this.adminPlayers.Name = "adminPlayers";
            // 
            // superAdminDGV
            // 
            this.superAdminDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.superAdminDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.superAdminDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.superAdminPlayers});
            this.superAdminDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superAdminDGV.Location = new System.Drawing.Point(753, 3);
            this.superAdminDGV.Name = "superAdminDGV";
            this.superAdminDGV.Size = new System.Drawing.Size(369, 364);
            this.superAdminDGV.TabIndex = 2;
            // 
            // superAdminPlayers
            // 
            this.superAdminPlayers.HeaderText = "Super Admin Players (Steam64 ID)";
            this.superAdminPlayers.Name = "superAdminPlayers";
            // 
            // serverTagsTabPage
            // 
            this.serverTagsTabPage.BackColor = System.Drawing.Color.Transparent;
            this.serverTagsTabPage.Controls.Add(this.tableLayoutPanel3);
            this.serverTagsTabPage.Location = new System.Drawing.Point(4, 22);
            this.serverTagsTabPage.Name = "serverTagsTabPage";
            this.serverTagsTabPage.Size = new System.Drawing.Size(1131, 376);
            this.serverTagsTabPage.TabIndex = 2;
            this.serverTagsTabPage.Text = "Server Tags";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.serverTagsDGV, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1131, 376);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // serverTagsDGV
            // 
            this.serverTagsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serverTagsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serverTags});
            this.serverTagsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverTagsDGV.Location = new System.Drawing.Point(3, 3);
            this.serverTagsDGV.Name = "serverTagsDGV";
            this.tableLayoutPanel3.SetRowSpan(this.serverTagsDGV, 2);
            this.serverTagsDGV.Size = new System.Drawing.Size(842, 370);
            this.serverTagsDGV.TabIndex = 0;
            // 
            // serverTags
            // 
            this.serverTags.HeaderText = "Server Tags";
            this.serverTags.Name = "serverTags";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(851, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(277, 188);
            this.label6.TabIndex = 1;
            this.label6.Text = "Server tags format example:\r\n\r\nPVP:No KOS\r\nRP:Casual\r\nCountry:GB";
            // 
            // playersOnlineTabPage
            // 
            this.playersOnlineTabPage.BackColor = System.Drawing.Color.Transparent;
            this.playersOnlineTabPage.Controls.Add(this.tableLayoutPanel4);
            this.playersOnlineTabPage.Location = new System.Drawing.Point(4, 22);
            this.playersOnlineTabPage.Name = "playersOnlineTabPage";
            this.playersOnlineTabPage.Size = new System.Drawing.Size(1131, 376);
            this.playersOnlineTabPage.TabIndex = 3;
            this.playersOnlineTabPage.Text = "Online Players";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.playersOnlineDGV, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.refreshOnlinePlayerList, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1131, 376);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // playersOnlineDGV
            // 
            this.playersOnlineDGV.AllowUserToAddRows = false;
            this.playersOnlineDGV.AllowUserToDeleteRows = false;
            this.playersOnlineDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.playersOnlineDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.playersOnlineDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerName,
            this.connectionDuration});
            this.playersOnlineDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playersOnlineDGV.Location = new System.Drawing.Point(3, 3);
            this.playersOnlineDGV.Name = "playersOnlineDGV";
            this.playersOnlineDGV.ReadOnly = true;
            this.tableLayoutPanel4.SetRowSpan(this.playersOnlineDGV, 2);
            this.playersOnlineDGV.Size = new System.Drawing.Size(559, 370);
            this.playersOnlineDGV.TabIndex = 0;
            // 
            // playerName
            // 
            this.playerName.HeaderText = "Player Name";
            this.playerName.Name = "playerName";
            this.playerName.ReadOnly = true;
            // 
            // connectionDuration
            // 
            this.connectionDuration.HeaderText = "Connection Duration";
            this.connectionDuration.Name = "connectionDuration";
            this.connectionDuration.ReadOnly = true;
            // 
            // refreshOnlinePlayerList
            // 
            this.refreshOnlinePlayerList.Location = new System.Drawing.Point(568, 3);
            this.refreshOnlinePlayerList.Name = "refreshOnlinePlayerList";
            this.refreshOnlinePlayerList.Size = new System.Drawing.Size(75, 23);
            this.refreshOnlinePlayerList.TabIndex = 1;
            this.refreshOnlinePlayerList.Text = "Refresh";
            this.refreshOnlinePlayerList.UseVisualStyleBackColor = true;
            this.refreshOnlinePlayerList.Click += new System.EventHandler(this.refreshOnlinePlayerList_Click);
            // 
            // statisticsTabPage
            // 
            this.statisticsTabPage.BackColor = System.Drawing.Color.Transparent;
            this.statisticsTabPage.Controls.Add(this.tableLayoutPanel5);
            this.statisticsTabPage.Location = new System.Drawing.Point(4, 22);
            this.statisticsTabPage.Name = "statisticsTabPage";
            this.statisticsTabPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statisticsTabPage.Size = new System.Drawing.Size(1131, 376);
            this.statisticsTabPage.TabIndex = 4;
            this.statisticsTabPage.Text = "Logs";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.66666F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Controls.Add(this.enableLogging, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.openLog, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.logTextBox, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.backgroundColour, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.label8, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.label9, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.userEventColour, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.label10, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.memoryLimitColour, 3, 3);
            this.tableLayoutPanel5.Controls.Add(this.label11, 2, 4);
            this.tableLayoutPanel5.Controls.Add(this.timedRestartColour, 3, 4);
            this.tableLayoutPanel5.Controls.Add(this.label12, 2, 5);
            this.tableLayoutPanel5.Controls.Add(this.serverCrashColour, 3, 5);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 10;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1131, 376);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // enableLogging
            // 
            this.enableLogging.AutoSize = true;
            this.enableLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enableLogging.Location = new System.Drawing.Point(3, 3);
            this.enableLogging.Name = "enableLogging";
            this.enableLogging.Size = new System.Drawing.Size(202, 24);
            this.enableLogging.TabIndex = 0;
            this.enableLogging.Text = "Enable Logging";
            this.enableLogging.UseVisualStyleBackColor = true;
            this.enableLogging.Click += new System.EventHandler(this.enableLogging_Click);
            // 
            // openLog
            // 
            this.openLog.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.openLog, 2);
            this.openLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openLog.Location = new System.Drawing.Point(978, 0);
            this.openLog.Name = "openLog";
            this.openLog.Size = new System.Drawing.Size(150, 30);
            this.openLog.TabIndex = 2;
            this.openLog.TabStop = true;
            this.openLog.Text = "Open Log File";
            this.openLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.openLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.openLog_LinkClicked);
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel5.SetColumnSpan(this.logTextBox, 2);
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTextBox.ForeColor = System.Drawing.Color.White;
            this.logTextBox.Location = new System.Drawing.Point(3, 33);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.tableLayoutPanel5.SetRowSpan(this.logTextBox, 9);
            this.logTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(969, 340);
            this.logTextBox.TabIndex = 3;
            this.logTextBox.Text = "";
            // 
            // backgroundColour
            // 
            this.backgroundColour.BackColor = System.Drawing.Color.Black;
            this.backgroundColour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backgroundColour.Location = new System.Drawing.Point(1104, 33);
            this.backgroundColour.Name = "backgroundColour";
            this.backgroundColour.Size = new System.Drawing.Size(24, 19);
            this.backgroundColour.TabIndex = 4;
            this.backgroundColour.UseVisualStyleBackColor = false;
            this.backgroundColour.Click += new System.EventHandler(this.backgroundColour_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(978, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 25);
            this.label8.TabIndex = 5;
            this.label8.Text = "Background Colour";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(978, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 25);
            this.label9.TabIndex = 6;
            this.label9.Text = "User Event Colour";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // userEventColour
            // 
            this.userEventColour.BackColor = System.Drawing.Color.White;
            this.userEventColour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userEventColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userEventColour.Location = new System.Drawing.Point(1104, 58);
            this.userEventColour.Name = "userEventColour";
            this.userEventColour.Size = new System.Drawing.Size(24, 19);
            this.userEventColour.TabIndex = 7;
            this.userEventColour.UseVisualStyleBackColor = false;
            this.userEventColour.Click += new System.EventHandler(this.userEventColour_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(978, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 25);
            this.label10.TabIndex = 8;
            this.label10.Text = "Memory Limit Colour";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // memoryLimitColour
            // 
            this.memoryLimitColour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.memoryLimitColour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryLimitColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.memoryLimitColour.Location = new System.Drawing.Point(1104, 83);
            this.memoryLimitColour.Name = "memoryLimitColour";
            this.memoryLimitColour.Size = new System.Drawing.Size(24, 19);
            this.memoryLimitColour.TabIndex = 9;
            this.memoryLimitColour.UseVisualStyleBackColor = false;
            this.memoryLimitColour.Click += new System.EventHandler(this.memoryLimitColour_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(978, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 25);
            this.label11.TabIndex = 10;
            this.label11.Text = "Planned Restart Colour";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timedRestartColour
            // 
            this.timedRestartColour.BackColor = System.Drawing.Color.Lime;
            this.timedRestartColour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timedRestartColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.timedRestartColour.Location = new System.Drawing.Point(1104, 108);
            this.timedRestartColour.Name = "timedRestartColour";
            this.timedRestartColour.Size = new System.Drawing.Size(24, 19);
            this.timedRestartColour.TabIndex = 11;
            this.timedRestartColour.UseVisualStyleBackColor = false;
            this.timedRestartColour.Click += new System.EventHandler(this.timedRestartColour_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(978, 130);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 25);
            this.label12.TabIndex = 12;
            this.label12.Text = "Server Crash Colour";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // serverCrashColour
            // 
            this.serverCrashColour.BackColor = System.Drawing.Color.Red;
            this.serverCrashColour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverCrashColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serverCrashColour.Location = new System.Drawing.Point(1104, 133);
            this.serverCrashColour.Name = "serverCrashColour";
            this.serverCrashColour.Size = new System.Drawing.Size(24, 19);
            this.serverCrashColour.TabIndex = 13;
            this.serverCrashColour.UseVisualStyleBackColor = false;
            this.serverCrashColour.Click += new System.EventHandler(this.serverCrashColour_Click);
            // 
            // discordTabPage
            // 
            this.discordTabPage.BackColor = System.Drawing.Color.Transparent;
            this.discordTabPage.Controls.Add(this.tableLayoutPanel6);
            this.discordTabPage.Location = new System.Drawing.Point(4, 22);
            this.discordTabPage.Name = "discordTabPage";
            this.discordTabPage.Size = new System.Drawing.Size(1131, 376);
            this.discordTabPage.TabIndex = 5;
            this.discordTabPage.Text = "Discord Webhook";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.43925F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.56075F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel6.Controls.Add(this.discordWebHook, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.webhookURL, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.notifyOnMemoryLimit, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.notifyOnTimedRestart, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.notifiyOnCrash, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.memoryLimitDiscordTxt, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.timedRestartDiscordTxt, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.serverCrashedDiscordTxt, 1, 5);
            this.tableLayoutPanel6.Controls.Add(this.testWebhook, 0, 8);
            this.tableLayoutPanel6.Controls.Add(this.webhookTestMsg, 1, 8);
            this.tableLayoutPanel6.Controls.Add(this.discordIncludeAdditional, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.label13, 1, 6);
            this.tableLayoutPanel6.Controls.Add(this.notifyOnScheduledRestart, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.scheduledRestartDiscordTxt, 1, 4);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 11;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1131, 376);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // discordWebHook
            // 
            this.discordWebHook.AutoSize = true;
            this.discordWebHook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discordWebHook.Location = new System.Drawing.Point(3, 3);
            this.discordWebHook.Name = "discordWebHook";
            this.discordWebHook.Size = new System.Drawing.Size(199, 19);
            this.discordWebHook.TabIndex = 47;
            this.discordWebHook.Text = "Enable Discord Webhook Integration";
            this.discordWebHook.UseVisualStyleBackColor = true;
            this.discordWebHook.CheckedChanged += new System.EventHandler(this.discordWebHook_CheckedChanged);
            this.discordWebHook.Click += new System.EventHandler(this.discordWebHook_Click);
            // 
            // webhookURL
            // 
            this.webhookURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webhookURL.Enabled = false;
            this.webhookURL.Location = new System.Drawing.Point(208, 3);
            this.webhookURL.Name = "webhookURL";
            this.webhookURL.Size = new System.Drawing.Size(845, 20);
            this.webhookURL.TabIndex = 48;
            this.webhookURL.Text = "[Webhook URL]";
            this.webhookURL.Leave += new System.EventHandler(this.webhookURL_Leave);
            // 
            // notifyOnMemoryLimit
            // 
            this.notifyOnMemoryLimit.AutoSize = true;
            this.notifyOnMemoryLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notifyOnMemoryLimit.Location = new System.Drawing.Point(3, 53);
            this.notifyOnMemoryLimit.Name = "notifyOnMemoryLimit";
            this.notifyOnMemoryLimit.Size = new System.Drawing.Size(199, 19);
            this.notifyOnMemoryLimit.TabIndex = 49;
            this.notifyOnMemoryLimit.Text = "Notify on Memory Limit";
            this.notifyOnMemoryLimit.UseVisualStyleBackColor = true;
            this.notifyOnMemoryLimit.CheckedChanged += new System.EventHandler(this.notifyOnMemoryLimit_CheckedChanged);
            this.notifyOnMemoryLimit.Click += new System.EventHandler(this.notifyOnMemoryLimit_Click);
            // 
            // notifyOnTimedRestart
            // 
            this.notifyOnTimedRestart.AutoSize = true;
            this.notifyOnTimedRestart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notifyOnTimedRestart.Location = new System.Drawing.Point(3, 78);
            this.notifyOnTimedRestart.Name = "notifyOnTimedRestart";
            this.notifyOnTimedRestart.Size = new System.Drawing.Size(199, 19);
            this.notifyOnTimedRestart.TabIndex = 50;
            this.notifyOnTimedRestart.Text = "Notify on Timed Restart";
            this.notifyOnTimedRestart.UseVisualStyleBackColor = true;
            this.notifyOnTimedRestart.CheckedChanged += new System.EventHandler(this.notifyOnTimedRestart_CheckedChanged);
            this.notifyOnTimedRestart.Click += new System.EventHandler(this.notifyOnTimedRestart_Click);
            // 
            // notifiyOnCrash
            // 
            this.notifiyOnCrash.AutoSize = true;
            this.notifiyOnCrash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notifiyOnCrash.Location = new System.Drawing.Point(3, 128);
            this.notifiyOnCrash.Name = "notifiyOnCrash";
            this.notifiyOnCrash.Size = new System.Drawing.Size(199, 19);
            this.notifiyOnCrash.TabIndex = 51;
            this.notifiyOnCrash.Text = "Notify on Crash";
            this.notifiyOnCrash.UseVisualStyleBackColor = true;
            this.notifiyOnCrash.CheckedChanged += new System.EventHandler(this.notifiyOnCrash_CheckedChanged);
            this.notifiyOnCrash.Click += new System.EventHandler(this.notifiyOnCrash_Click);
            // 
            // memoryLimitDiscordTxt
            // 
            this.memoryLimitDiscordTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryLimitDiscordTxt.Enabled = false;
            this.memoryLimitDiscordTxt.Location = new System.Drawing.Point(208, 53);
            this.memoryLimitDiscordTxt.Name = "memoryLimitDiscordTxt";
            this.memoryLimitDiscordTxt.Size = new System.Drawing.Size(845, 20);
            this.memoryLimitDiscordTxt.TabIndex = 52;
            this.memoryLimitDiscordTxt.Text = "SERVER RESTARTING | Memory Limit Hit";
            this.memoryLimitDiscordTxt.Leave += new System.EventHandler(this.memoryLimitDiscordTxt_Leave);
            // 
            // timedRestartDiscordTxt
            // 
            this.timedRestartDiscordTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timedRestartDiscordTxt.Enabled = false;
            this.timedRestartDiscordTxt.Location = new System.Drawing.Point(208, 78);
            this.timedRestartDiscordTxt.Name = "timedRestartDiscordTxt";
            this.timedRestartDiscordTxt.Size = new System.Drawing.Size(845, 20);
            this.timedRestartDiscordTxt.TabIndex = 53;
            this.timedRestartDiscordTxt.Text = "SERVER RESTARTING | Timed Restart";
            this.timedRestartDiscordTxt.Leave += new System.EventHandler(this.timedRestartDiscordTxt_Leave);
            // 
            // serverCrashedDiscordTxt
            // 
            this.serverCrashedDiscordTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverCrashedDiscordTxt.Enabled = false;
            this.serverCrashedDiscordTxt.Location = new System.Drawing.Point(208, 128);
            this.serverCrashedDiscordTxt.Name = "serverCrashedDiscordTxt";
            this.serverCrashedDiscordTxt.Size = new System.Drawing.Size(845, 20);
            this.serverCrashedDiscordTxt.TabIndex = 54;
            this.serverCrashedDiscordTxt.Text = "SERVER RESTARTING | Server Crashed";
            this.serverCrashedDiscordTxt.Leave += new System.EventHandler(this.serverCrashedDiscordTxt_Leave);
            // 
            // testWebhook
            // 
            this.testWebhook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testWebhook.Location = new System.Drawing.Point(3, 203);
            this.testWebhook.Name = "testWebhook";
            this.testWebhook.Size = new System.Drawing.Size(199, 24);
            this.testWebhook.TabIndex = 55;
            this.testWebhook.Text = "Test Webhook";
            this.testWebhook.UseVisualStyleBackColor = true;
            this.testWebhook.Click += new System.EventHandler(this.testWebhook_Click);
            // 
            // webhookTestMsg
            // 
            this.webhookTestMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webhookTestMsg.Location = new System.Drawing.Point(208, 203);
            this.webhookTestMsg.Name = "webhookTestMsg";
            this.webhookTestMsg.Size = new System.Drawing.Size(845, 20);
            this.webhookTestMsg.TabIndex = 56;
            this.webhookTestMsg.Text = "Testing Dead Matter Server Manager Webhook Integration";
            this.webhookTestMsg.Leave += new System.EventHandler(this.webhookTestMsg_Leave);
            // 
            // discordIncludeAdditional
            // 
            this.discordIncludeAdditional.AutoSize = true;
            this.discordIncludeAdditional.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discordIncludeAdditional.Location = new System.Drawing.Point(3, 153);
            this.discordIncludeAdditional.Name = "discordIncludeAdditional";
            this.discordIncludeAdditional.Size = new System.Drawing.Size(199, 19);
            this.discordIncludeAdditional.TabIndex = 57;
            this.discordIncludeAdditional.Text = "Include Additional Info";
            this.discordIncludeAdditional.UseVisualStyleBackColor = true;
            this.discordIncludeAdditional.Click += new System.EventHandler(this.discordIncludeAdditional_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(208, 150);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(845, 25);
            this.label13.TabIndex = 58;
            this.label13.Text = "Player count and previous uptime";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // notifyOnScheduledRestart
            // 
            this.notifyOnScheduledRestart.AutoSize = true;
            this.notifyOnScheduledRestart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notifyOnScheduledRestart.Location = new System.Drawing.Point(3, 103);
            this.notifyOnScheduledRestart.Name = "notifyOnScheduledRestart";
            this.notifyOnScheduledRestart.Size = new System.Drawing.Size(199, 19);
            this.notifyOnScheduledRestart.TabIndex = 59;
            this.notifyOnScheduledRestart.Text = "Notify on Scheduled Restart";
            this.notifyOnScheduledRestart.UseVisualStyleBackColor = true;
            this.notifyOnScheduledRestart.CheckedChanged += new System.EventHandler(this.notifyOnScheduledRestart_CheckedChanged);
            this.notifyOnScheduledRestart.Click += new System.EventHandler(this.notifyOnScheduledRestart_Click);
            // 
            // scheduledRestartDiscordTxt
            // 
            this.scheduledRestartDiscordTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduledRestartDiscordTxt.Enabled = false;
            this.scheduledRestartDiscordTxt.Location = new System.Drawing.Point(208, 103);
            this.scheduledRestartDiscordTxt.Name = "scheduledRestartDiscordTxt";
            this.scheduledRestartDiscordTxt.Size = new System.Drawing.Size(845, 20);
            this.scheduledRestartDiscordTxt.TabIndex = 60;
            this.scheduledRestartDiscordTxt.Text = "SERVER RESTARTING | Scheduled Restart";
            this.scheduledRestartDiscordTxt.Leave += new System.EventHandler(this.scheduledRestartDiscordTxt_Leave);
            // 
            // backupsTabPage
            // 
            this.backupsTabPage.BackColor = System.Drawing.Color.Transparent;
            this.backupsTabPage.Controls.Add(this.tableLayoutPanel7);
            this.backupsTabPage.Location = new System.Drawing.Point(4, 22);
            this.backupsTabPage.Name = "backupsTabPage";
            this.backupsTabPage.Size = new System.Drawing.Size(1131, 376);
            this.backupsTabPage.TabIndex = 6;
            this.backupsTabPage.Text = "Backup / Restore";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 6;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.enableBackups, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label14, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.backupDestinationFolder, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.browseBackupDestinationFolder, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.label15, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.backupScheduleMinutes, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.label16, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.backupRetentionQty, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.label17, 0, 4);
            this.tableLayoutPanel7.Controls.Add(this.lastBackupTime, 1, 4);
            this.tableLayoutPanel7.Controls.Add(this.backupNow, 1, 5);
            this.tableLayoutPanel7.Controls.Add(this.backupList, 5, 1);
            this.tableLayoutPanel7.Controls.Add(this.label19, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.restoreGameIni, 4, 1);
            this.tableLayoutPanel7.Controls.Add(this.restoreEngineIni, 4, 2);
            this.tableLayoutPanel7.Controls.Add(this.restoreWorldSave, 4, 3);
            this.tableLayoutPanel7.Controls.Add(this.restoreNow, 4, 5);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 11;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1131, 376);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // enableBackups
            // 
            this.enableBackups.AutoSize = true;
            this.enableBackups.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.enableBackups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.enableBackups.Location = new System.Drawing.Point(3, 3);
            this.enableBackups.Name = "enableBackups";
            this.enableBackups.Size = new System.Drawing.Size(194, 24);
            this.enableBackups.TabIndex = 0;
            this.enableBackups.Text = "Enable Backups";
            this.enableBackups.UseVisualStyleBackColor = true;
            this.enableBackups.CheckedChanged += new System.EventHandler(this.enableBackups_CheckedChanged);
            this.enableBackups.Click += new System.EventHandler(this.enableBackups_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(3, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(194, 30);
            this.label14.TabIndex = 1;
            this.label14.Text = "Backup Destination Folder";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backupDestinationFolder
            // 
            this.backupDestinationFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupDestinationFolder.Location = new System.Drawing.Point(203, 33);
            this.backupDestinationFolder.Name = "backupDestinationFolder";
            this.backupDestinationFolder.ReadOnly = true;
            this.backupDestinationFolder.Size = new System.Drawing.Size(224, 20);
            this.backupDestinationFolder.TabIndex = 2;
            this.backupDestinationFolder.Leave += new System.EventHandler(this.backupDestinationFolder_Leave);
            // 
            // browseBackupDestinationFolder
            // 
            this.browseBackupDestinationFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseBackupDestinationFolder.Enabled = false;
            this.browseBackupDestinationFolder.Location = new System.Drawing.Point(433, 33);
            this.browseBackupDestinationFolder.Name = "browseBackupDestinationFolder";
            this.browseBackupDestinationFolder.Size = new System.Drawing.Size(117, 24);
            this.browseBackupDestinationFolder.TabIndex = 3;
            this.browseBackupDestinationFolder.Text = "Browse";
            this.browseBackupDestinationFolder.UseVisualStyleBackColor = true;
            this.browseBackupDestinationFolder.Click += new System.EventHandler(this.browseBackupDestinationFolder_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(3, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(194, 30);
            this.label15.TabIndex = 4;
            this.label15.Text = "Backup Schedule (minutes)";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backupScheduleMinutes
            // 
            this.backupScheduleMinutes.Dock = System.Windows.Forms.DockStyle.Left;
            this.backupScheduleMinutes.Enabled = false;
            this.backupScheduleMinutes.Location = new System.Drawing.Point(203, 63);
            this.backupScheduleMinutes.Name = "backupScheduleMinutes";
            this.backupScheduleMinutes.Size = new System.Drawing.Size(120, 20);
            this.backupScheduleMinutes.TabIndex = 5;
            this.backupScheduleMinutes.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.backupScheduleMinutes.Scroll += new System.Windows.Forms.ScrollEventHandler(this.backupScheduleMinutes_Scroll);
            this.backupScheduleMinutes.Click += new System.EventHandler(this.backupScheduleMinutes_Click);
            this.backupScheduleMinutes.Leave += new System.EventHandler(this.backupScheduleMinutes_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 90);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(194, 30);
            this.label16.TabIndex = 6;
            this.label16.Text = "Retention (number of backups to keep)";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backupRetentionQty
            // 
            this.backupRetentionQty.Dock = System.Windows.Forms.DockStyle.Left;
            this.backupRetentionQty.Enabled = false;
            this.backupRetentionQty.Location = new System.Drawing.Point(203, 93);
            this.backupRetentionQty.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.backupRetentionQty.Name = "backupRetentionQty";
            this.backupRetentionQty.Size = new System.Drawing.Size(120, 20);
            this.backupRetentionQty.TabIndex = 7;
            this.backupRetentionQty.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.backupRetentionQty.Scroll += new System.Windows.Forms.ScrollEventHandler(this.backupRetentionQty_Scroll);
            this.backupRetentionQty.Click += new System.EventHandler(this.backupRetentionQty_Click);
            this.backupRetentionQty.Leave += new System.EventHandler(this.backupRetentionQty_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(3, 120);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(194, 30);
            this.label17.TabIndex = 9;
            this.label17.Text = "Last Backup Time";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lastBackupTime
            // 
            this.lastBackupTime.AutoSize = true;
            this.lastBackupTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastBackupTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastBackupTime.Location = new System.Drawing.Point(203, 120);
            this.lastBackupTime.Name = "lastBackupTime";
            this.lastBackupTime.Size = new System.Drawing.Size(224, 30);
            this.lastBackupTime.TabIndex = 10;
            this.lastBackupTime.Text = "No Backup Found";
            this.lastBackupTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backupNow
            // 
            this.backupNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupNow.Enabled = false;
            this.backupNow.Location = new System.Drawing.Point(203, 153);
            this.backupNow.Name = "backupNow";
            this.backupNow.Size = new System.Drawing.Size(224, 24);
            this.backupNow.TabIndex = 8;
            this.backupNow.Text = "Backup Now";
            this.backupNow.UseVisualStyleBackColor = true;
            this.backupNow.Click += new System.EventHandler(this.backupNow_Click);
            // 
            // backupList
            // 
            this.backupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backupList.FormattingEnabled = true;
            this.backupList.Location = new System.Drawing.Point(707, 33);
            this.backupList.Name = "backupList";
            this.tableLayoutPanel7.SetRowSpan(this.backupList, 9);
            this.backupList.Size = new System.Drawing.Size(421, 264);
            this.backupList.TabIndex = 11;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Silver;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(556, 0);
            this.label19.Name = "label19";
            this.tableLayoutPanel7.SetRowSpan(this.label19, 11);
            this.label19.Size = new System.Drawing.Size(2, 376);
            this.label19.TabIndex = 12;
            // 
            // restoreGameIni
            // 
            this.restoreGameIni.AutoSize = true;
            this.restoreGameIni.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.restoreGameIni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restoreGameIni.Location = new System.Drawing.Point(564, 33);
            this.restoreGameIni.Name = "restoreGameIni";
            this.restoreGameIni.Size = new System.Drawing.Size(137, 24);
            this.restoreGameIni.TabIndex = 13;
            this.restoreGameIni.Text = "Restore Game.ini";
            this.restoreGameIni.UseVisualStyleBackColor = true;
            this.restoreGameIni.Click += new System.EventHandler(this.restoreGameIni_Click);
            // 
            // restoreEngineIni
            // 
            this.restoreEngineIni.AutoSize = true;
            this.restoreEngineIni.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.restoreEngineIni.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restoreEngineIni.Location = new System.Drawing.Point(564, 63);
            this.restoreEngineIni.Name = "restoreEngineIni";
            this.restoreEngineIni.Size = new System.Drawing.Size(137, 24);
            this.restoreEngineIni.TabIndex = 14;
            this.restoreEngineIni.Text = "Restore Engine.ini";
            this.restoreEngineIni.UseVisualStyleBackColor = true;
            this.restoreEngineIni.Click += new System.EventHandler(this.restoreEngineIni_Click);
            // 
            // restoreWorldSave
            // 
            this.restoreWorldSave.AutoSize = true;
            this.restoreWorldSave.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.restoreWorldSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restoreWorldSave.Location = new System.Drawing.Point(564, 93);
            this.restoreWorldSave.Name = "restoreWorldSave";
            this.restoreWorldSave.Size = new System.Drawing.Size(137, 24);
            this.restoreWorldSave.TabIndex = 15;
            this.restoreWorldSave.Text = "Restore World Save";
            this.restoreWorldSave.UseVisualStyleBackColor = true;
            // 
            // restoreNow
            // 
            this.restoreNow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restoreNow.Location = new System.Drawing.Point(564, 153);
            this.restoreNow.Name = "restoreNow";
            this.restoreNow.Size = new System.Drawing.Size(137, 24);
            this.restoreNow.TabIndex = 16;
            this.restoreNow.Text = "Restore Now";
            this.restoreNow.UseVisualStyleBackColor = true;
            this.restoreNow.Click += new System.EventHandler(this.RestoreNow_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel1.Location = new System.Drawing.Point(954, 636);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(188, 29);
            this.linkLabel1.TabIndex = 26;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Buy me a beer ;)";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // uptimeLbl
            // 
            this.uptimeLbl.AutoSize = true;
            this.uptimeLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uptimeLbl.Location = new System.Drawing.Point(3, 546);
            this.uptimeLbl.Name = "uptimeLbl";
            this.uptimeLbl.Size = new System.Drawing.Size(112, 30);
            this.uptimeLbl.TabIndex = 27;
            this.uptimeLbl.Text = "Server Uptime:";
            this.uptimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // serverUptime
            // 
            this.serverUptime.AutoSize = true;
            this.serverUptime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverUptime.Location = new System.Drawing.Point(121, 546);
            this.serverUptime.Name = "serverUptime";
            this.serverUptime.Size = new System.Drawing.Size(86, 30);
            this.serverUptime.TabIndex = 28;
            this.serverUptime.Text = "0.00:00:00";
            this.serverUptime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoStartWithWindows
            // 
            this.autoStartWithWindows.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.autoStartWithWindows, 2);
            this.autoStartWithWindows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoStartWithWindows.Location = new System.Drawing.Point(3, 609);
            this.autoStartWithWindows.Name = "autoStartWithWindows";
            this.autoStartWithWindows.Size = new System.Drawing.Size(204, 24);
            this.autoStartWithWindows.TabIndex = 29;
            this.autoStartWithWindows.Text = "Auto Start with Windows";
            this.autoStartWithWindows.UseVisualStyleBackColor = true;
            this.autoStartWithWindows.CheckedChanged += new System.EventHandler(this.autoStartWithWindows_CheckedChanged);
            this.autoStartWithWindows.Click += new System.EventHandler(this.autoStartWithWindows_Click);
            // 
            // autoStartServer
            // 
            this.autoStartServer.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.autoStartServer, 2);
            this.autoStartServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoStartServer.Location = new System.Drawing.Point(3, 639);
            this.autoStartServer.Name = "autoStartServer";
            this.autoStartServer.Size = new System.Drawing.Size(204, 23);
            this.autoStartServer.TabIndex = 30;
            this.autoStartServer.Text = "Auto Start Server on Launch";
            this.autoStartServer.UseVisualStyleBackColor = true;
            this.autoStartServer.Click += new System.EventHandler(this.autoStartServer_Click);
            // 
            // updateSoftware
            // 
            this.updateSoftware.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.updateSoftware, 2);
            this.updateSoftware.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateSoftware.Location = new System.Drawing.Point(3, 0);
            this.updateSoftware.Name = "updateSoftware";
            this.updateSoftware.Size = new System.Drawing.Size(204, 25);
            this.updateSoftware.TabIndex = 31;
            this.updateSoftware.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.updateSoftware_LinkClicked);
            // 
            // restartServerTimelbl
            // 
            this.restartServerTimelbl.AutoSize = true;
            this.restartServerTimelbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restartServerTimelbl.Location = new System.Drawing.Point(807, 546);
            this.restartServerTimelbl.Name = "restartServerTimelbl";
            this.restartServerTimelbl.Size = new System.Drawing.Size(141, 30);
            this.restartServerTimelbl.TabIndex = 32;
            this.restartServerTimelbl.Text = "Restart Server Time (Mins)";
            this.restartServerTimelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // restartServerTime
            // 
            this.restartServerTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restartServerTime.Location = new System.Drawing.Point(954, 549);
            this.restartServerTime.Name = "restartServerTime";
            this.restartServerTime.Size = new System.Drawing.Size(188, 20);
            this.restartServerTime.TabIndex = 33;
            this.restartServerTime.Leave += new System.EventHandler(this.restartServerTime_Leave);
            // 
            // restartServerTimeOption
            // 
            this.restartServerTimeOption.AutoSize = true;
            this.restartServerTimeOption.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.restartServerTimeOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restartServerTimeOption.Location = new System.Drawing.Point(646, 549);
            this.restartServerTimeOption.Name = "restartServerTimeOption";
            this.restartServerTimeOption.Size = new System.Drawing.Size(155, 24);
            this.restartServerTimeOption.TabIndex = 34;
            this.restartServerTimeOption.Text = "Timed Restart";
            this.restartServerTimeOption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.restartServerTimeOption.UseVisualStyleBackColor = true;
            this.restartServerTimeOption.CheckedChanged += new System.EventHandler(this.restartServerTimeOption_CheckedChanged);
            this.restartServerTimeOption.Click += new System.EventHandler(this.restartServerTimeOption_Click);
            // 
            // rememberSteamPass
            // 
            this.rememberSteamPass.AutoSize = true;
            this.rememberSteamPass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rememberSteamPass.Location = new System.Drawing.Point(954, 88);
            this.rememberSteamPass.Name = "rememberSteamPass";
            this.rememberSteamPass.Size = new System.Drawing.Size(188, 17);
            this.rememberSteamPass.TabIndex = 35;
            this.rememberSteamPass.Text = "Remember Steam Password";
            this.rememberSteamPass.UseVisualStyleBackColor = true;
            this.rememberSteamPass.Click += new System.EventHandler(this.rememberSteamPass_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel2.Location = new System.Drawing.Point(954, 0);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(188, 25);
            this.linkLabel2.TabIndex = 37;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Change log";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // changeLaunchParams
            // 
            this.changeLaunchParams.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.changeLaunchParams, 2);
            this.changeLaunchParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeLaunchParams.Location = new System.Drawing.Point(354, 88);
            this.changeLaunchParams.Name = "changeLaunchParams";
            this.changeLaunchParams.Size = new System.Drawing.Size(286, 17);
            this.changeLaunchParams.TabIndex = 40;
            this.changeLaunchParams.Text = "Change Launch Parameters";
            this.changeLaunchParams.UseVisualStyleBackColor = true;
            this.changeLaunchParams.CheckedChanged += new System.EventHandler(this.changeLaunchParams_CheckedChanged);
            this.changeLaunchParams.Click += new System.EventHandler(this.changeLaunchParams_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 23);
            this.label7.TabIndex = 41;
            this.label7.Text = "Launch Parameters";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // launchParameters
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.launchParameters, 2);
            this.launchParameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.launchParameters.Location = new System.Drawing.Point(121, 88);
            this.launchParameters.Name = "launchParameters";
            this.launchParameters.ReadOnly = true;
            this.launchParameters.Size = new System.Drawing.Size(227, 20);
            this.launchParameters.TabIndex = 42;
            this.launchParameters.Text = "-USEALLAVAILABLECORES -log";
            this.launchParameters.Leave += new System.EventHandler(this.launchParameters_Leave);
            // 
            // saveConfigOnStart
            // 
            this.saveConfigOnStart.AutoSize = true;
            this.saveConfigOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveConfigOnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveConfigOnStart.Location = new System.Drawing.Point(807, 579);
            this.saveConfigOnStart.Name = "saveConfigOnStart";
            this.saveConfigOnStart.Size = new System.Drawing.Size(141, 24);
            this.saveConfigOnStart.TabIndex = 43;
            this.saveConfigOnStart.Text = "Save Config on Start";
            this.saveConfigOnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.saveConfigOnStart.UseVisualStyleBackColor = true;
            this.saveConfigOnStart.Click += new System.EventHandler(this.saveConfigOnStart_Click);
            // 
            // restartServer
            // 
            this.restartServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restartServer.Location = new System.Drawing.Point(807, 609);
            this.restartServer.Name = "restartServer";
            this.restartServer.Size = new System.Drawing.Size(141, 24);
            this.restartServer.TabIndex = 44;
            this.restartServer.Text = "Restart Server";
            this.restartServer.UseVisualStyleBackColor = true;
            this.restartServer.Click += new System.EventHandler(this.restartServer_Click);
            // 
            // checkUpdateOnStart
            // 
            this.checkUpdateOnStart.AutoSize = true;
            this.checkUpdateOnStart.Location = new System.Drawing.Point(807, 639);
            this.checkUpdateOnStart.Name = "checkUpdateOnStart";
            this.checkUpdateOnStart.Size = new System.Drawing.Size(141, 17);
            this.checkUpdateOnStart.TabIndex = 15;
            this.checkUpdateOnStart.Text = "Check for Update on Server Start";
            this.checkUpdateOnStart.UseVisualStyleBackColor = true;
            this.checkUpdateOnStart.Visible = false;
            this.checkUpdateOnStart.CheckedChanged += new System.EventHandler(this.checkUpdateOnStart_CheckedChanged);
            // 
            // onlinePlayers
            // 
            this.onlinePlayers.AutoSize = true;
            this.onlinePlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onlinePlayers.Location = new System.Drawing.Point(490, 516);
            this.onlinePlayers.Name = "onlinePlayers";
            this.onlinePlayers.Size = new System.Drawing.Size(150, 30);
            this.onlinePlayers.TabIndex = 36;
            this.onlinePlayers.Text = "Online Players\r\n0/0";
            this.onlinePlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // restartsThisSessionTxt
            // 
            this.restartsThisSessionTxt.AutoSize = true;
            this.restartsThisSessionTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restartsThisSessionTxt.Location = new System.Drawing.Point(354, 546);
            this.restartsThisSessionTxt.Name = "restartsThisSessionTxt";
            this.restartsThisSessionTxt.Size = new System.Drawing.Size(130, 30);
            this.restartsThisSessionTxt.TabIndex = 38;
            this.restartsThisSessionTxt.Text = "Restarts This Session\r\n0";
            this.restartsThisSessionTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lastRestartTxt
            // 
            this.lastRestartTxt.AutoSize = true;
            this.lastRestartTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastRestartTxt.Location = new System.Drawing.Point(490, 546);
            this.lastRestartTxt.Name = "lastRestartTxt";
            this.lastRestartTxt.Size = new System.Drawing.Size(150, 30);
            this.lastRestartTxt.TabIndex = 39;
            this.lastRestartTxt.Text = "Last Restart\r\n00/00/0000 00:00:00";
            this.lastRestartTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // memoryUsed
            // 
            this.memoryUsed.AutoSize = true;
            this.memoryUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryUsed.Location = new System.Drawing.Point(354, 516);
            this.memoryUsed.Name = "memoryUsed";
            this.memoryUsed.Size = new System.Drawing.Size(130, 30);
            this.memoryUsed.TabIndex = 21;
            this.memoryUsed.Text = "memused";
            this.memoryUsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // allTimeHighPlayersLbl
            // 
            this.allTimeHighPlayersLbl.AutoSize = true;
            this.allTimeHighPlayersLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allTimeHighPlayersLbl.Location = new System.Drawing.Point(354, 576);
            this.allTimeHighPlayersLbl.Name = "allTimeHighPlayersLbl";
            this.allTimeHighPlayersLbl.Size = new System.Drawing.Size(130, 30);
            this.allTimeHighPlayersLbl.TabIndex = 45;
            this.allTimeHighPlayersLbl.Text = "All Time High Players\r\n0";
            this.allTimeHighPlayersLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // longestUptimeLbl
            // 
            this.longestUptimeLbl.AutoSize = true;
            this.longestUptimeLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.longestUptimeLbl.Location = new System.Drawing.Point(490, 576);
            this.longestUptimeLbl.Name = "longestUptimeLbl";
            this.longestUptimeLbl.Size = new System.Drawing.Size(150, 30);
            this.longestUptimeLbl.TabIndex = 46;
            this.longestUptimeLbl.Text = "Longest Uptime\r\n0.00:00:00";
            this.longestUptimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scheduledRestartOption
            // 
            this.scheduledRestartOption.AutoSize = true;
            this.scheduledRestartOption.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.scheduledRestartOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduledRestartOption.Location = new System.Drawing.Point(646, 579);
            this.scheduledRestartOption.Name = "scheduledRestartOption";
            this.scheduledRestartOption.Size = new System.Drawing.Size(155, 24);
            this.scheduledRestartOption.TabIndex = 47;
            this.scheduledRestartOption.Text = "Scheduled Restart";
            this.scheduledRestartOption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.scheduledRestartOption.UseVisualStyleBackColor = true;
            this.scheduledRestartOption.CheckedChanged += new System.EventHandler(this.scheduledRestartOption_CheckedChanged);
            this.scheduledRestartOption.Click += new System.EventHandler(this.scheduledRestartOption_Click);
            // 
            // configureRestartSchedule
            // 
            this.configureRestartSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configureRestartSchedule.Enabled = false;
            this.configureRestartSchedule.Location = new System.Drawing.Point(646, 609);
            this.configureRestartSchedule.Name = "configureRestartSchedule";
            this.configureRestartSchedule.Size = new System.Drawing.Size(155, 24);
            this.configureRestartSchedule.TabIndex = 48;
            this.configureRestartSchedule.Text = "Configure Restart Schedule";
            this.configureRestartSchedule.UseVisualStyleBackColor = true;
            this.configureRestartSchedule.Click += new System.EventHandler(this.restartSchedule_Click);
            // 
            // ServerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 670);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerManager";
            this.Text = "Dead Matter Server Manager";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.settingsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.configSettings)).EndInit();
            this.userTabPage.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.whitelistDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superAdminDGV)).EndInit();
            this.serverTagsTabPage.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverTagsDGV)).EndInit();
            this.playersOnlineTabPage.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playersOnlineDGV)).EndInit();
            this.statisticsTabPage.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.discordTabPage.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.backupsTabPage.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backupScheduleMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backupRetentionQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button steamCMDBrowse;
        private System.Windows.Forms.Button serverFolderBrowse;
        private System.Windows.Forms.TextBox steamCMDPath;
        private System.Windows.Forms.TextBox serverFolderPath;
        private System.Windows.Forms.Button updateSteamCMD;
        private System.Windows.Forms.TextBox steamID;
        private System.Windows.Forms.TextBox steamPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button updateServer;
        private System.Windows.Forms.Button getConfig;
        private System.Windows.Forms.CheckBox checkUpdateOnStart;
        private System.Windows.Forms.DataGridView configSettings;
        private System.Windows.Forms.Button saveConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox maxServerMemory;
        private System.Windows.Forms.Button startServer;
        private System.Windows.Forms.Label memoryUsed;
        private System.Windows.Forms.Button stopServer;
        private System.Windows.Forms.Label serverStatus;
        private System.Windows.Forms.ProgressBar memoryUsedProgressBar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.TabPage userTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView whitelistDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn whiteListPlayers;
        private System.Windows.Forms.DataGridView adminDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn adminPlayers;
        private System.Windows.Forms.DataGridView superAdminDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn superAdminPlayers;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label uptimeLbl;
        private System.Windows.Forms.Label serverUptime;
        private System.Windows.Forms.CheckBox autoStartWithWindows;
        private System.Windows.Forms.CheckBox autoStartServer;
        private System.Windows.Forms.LinkLabel updateSoftware;
        private System.Windows.Forms.Label restartServerTimelbl;
        private System.Windows.Forms.TextBox restartServerTime;
        private System.Windows.Forms.CheckBox restartServerTimeOption;
        private System.Windows.Forms.CheckBox rememberSteamPass;
        private System.Windows.Forms.TabPage serverTagsTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridView serverTagsDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverTags;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label onlinePlayers;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label restartsThisSessionTxt;
        private System.Windows.Forms.Label lastRestartTxt;
        private System.Windows.Forms.TabPage playersOnlineTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView playersOnlineDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionDuration;
        private System.Windows.Forms.Button refreshOnlinePlayerList;
        private System.Windows.Forms.CheckBox changeLaunchParams;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox launchParameters;
        private System.Windows.Forms.CheckBox saveConfigOnStart;
        private System.Windows.Forms.Button restartServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Script;
        private System.Windows.Forms.DataGridViewTextBoxColumn IniFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Info;
        private System.Windows.Forms.TabPage statisticsTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckBox enableLogging;
        private System.Windows.Forms.Label allTimeHighPlayersLbl;
        private System.Windows.Forms.Label longestUptimeLbl;
        private System.Windows.Forms.LinkLabel openLog;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button backgroundColour;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button userEventColour;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button memoryLimitColour;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button timedRestartColour;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button serverCrashColour;
        private System.Windows.Forms.CheckBox discordWebHook;
        private System.Windows.Forms.TextBox webhookURL;
        private System.Windows.Forms.TabPage discordTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox notifyOnMemoryLimit;
        private System.Windows.Forms.CheckBox notifyOnTimedRestart;
        private System.Windows.Forms.CheckBox notifiyOnCrash;
        private System.Windows.Forms.TextBox memoryLimitDiscordTxt;
        private System.Windows.Forms.TextBox timedRestartDiscordTxt;
        private System.Windows.Forms.TextBox serverCrashedDiscordTxt;
        private System.Windows.Forms.Button testWebhook;
        private System.Windows.Forms.TextBox webhookTestMsg;
        private System.Windows.Forms.CheckBox discordIncludeAdditional;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage backupsTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.CheckBox enableBackups;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox backupDestinationFolder;
        private System.Windows.Forms.Button browseBackupDestinationFolder;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown backupScheduleMinutes;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown backupRetentionQty;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lastBackupTime;
        private System.Windows.Forms.Button backupNow;
        private System.Windows.Forms.ListBox backupList;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox restoreGameIni;
        private System.Windows.Forms.CheckBox restoreEngineIni;
        private System.Windows.Forms.CheckBox restoreWorldSave;
        private System.Windows.Forms.Button restoreNow;
        private System.Windows.Forms.CheckBox scheduledRestartOption;
        private System.Windows.Forms.Button configureRestartSchedule;
        private System.Windows.Forms.CheckBox notifyOnScheduledRestart;
        private System.Windows.Forms.TextBox scheduledRestartDiscordTxt;
    }
}

