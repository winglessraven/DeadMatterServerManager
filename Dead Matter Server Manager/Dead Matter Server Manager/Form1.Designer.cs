namespace Dead_Matter_Server_Manager
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.softwareVersion = new System.Windows.Forms.Label();
            this.updateServer = new System.Windows.Forms.Button();
            this.getConfig = new System.Windows.Forms.Button();
            this.checkUpdateOnStart = new System.Windows.Forms.CheckBox();
            this.configSettings = new System.Windows.Forms.DataGridView();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Script = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveConfig = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.07775F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.92225F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.steamCMDBrowse, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverFolderBrowse, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.steamCMDPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverFolderPath, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.updateSteamCMD, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.steamID, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.steamPassword, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.softwareVersion, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.updateServer, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.getConfig, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkUpdateOnStart, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.configSettings, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.saveConfig, 4, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 14;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1144, 615);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 30);
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
            this.label3.Size = new System.Drawing.Size(122, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Server Folder Path";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // steamCMDBrowse
            // 
            this.steamCMDBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamCMDBrowse.Location = new System.Drawing.Point(460, 28);
            this.steamCMDBrowse.Name = "steamCMDBrowse";
            this.steamCMDBrowse.Size = new System.Drawing.Size(82, 24);
            this.steamCMDBrowse.TabIndex = 3;
            this.steamCMDBrowse.Text = "Browse";
            this.steamCMDBrowse.UseVisualStyleBackColor = true;
            this.steamCMDBrowse.Click += new System.EventHandler(this.steamCMDBrowse_Click);
            // 
            // serverFolderBrowse
            // 
            this.serverFolderBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverFolderBrowse.Location = new System.Drawing.Point(460, 58);
            this.serverFolderBrowse.Name = "serverFolderBrowse";
            this.serverFolderBrowse.Size = new System.Drawing.Size(82, 24);
            this.serverFolderBrowse.TabIndex = 4;
            this.serverFolderBrowse.Text = "Browse";
            this.serverFolderBrowse.UseVisualStyleBackColor = true;
            this.serverFolderBrowse.Click += new System.EventHandler(this.serverFolderBrowse_Click);
            // 
            // steamCMDPath
            // 
            this.steamCMDPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamCMDPath.Location = new System.Drawing.Point(131, 28);
            this.steamCMDPath.Name = "steamCMDPath";
            this.steamCMDPath.Size = new System.Drawing.Size(323, 20);
            this.steamCMDPath.TabIndex = 5;
            // 
            // serverFolderPath
            // 
            this.serverFolderPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverFolderPath.Location = new System.Drawing.Point(131, 58);
            this.serverFolderPath.Name = "serverFolderPath";
            this.serverFolderPath.Size = new System.Drawing.Size(323, 20);
            this.serverFolderPath.TabIndex = 6;
            // 
            // updateSteamCMD
            // 
            this.updateSteamCMD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateSteamCMD.Location = new System.Drawing.Point(548, 28);
            this.updateSteamCMD.Name = "updateSteamCMD";
            this.updateSteamCMD.Size = new System.Drawing.Size(120, 24);
            this.updateSteamCMD.TabIndex = 7;
            this.updateSteamCMD.Text = "Update SteamCMD";
            this.updateSteamCMD.UseVisualStyleBackColor = true;
            this.updateSteamCMD.Click += new System.EventHandler(this.updateSteamCMD_Click);
            // 
            // steamID
            // 
            this.steamID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamID.Location = new System.Drawing.Point(962, 28);
            this.steamID.Name = "steamID";
            this.steamID.Size = new System.Drawing.Size(179, 20);
            this.steamID.TabIndex = 8;
            // 
            // steamPassword
            // 
            this.steamPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.steamPassword.Location = new System.Drawing.Point(962, 58);
            this.steamPassword.Name = "steamPassword";
            this.steamPassword.PasswordChar = '*';
            this.steamPassword.Size = new System.Drawing.Size(179, 20);
            this.steamPassword.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(804, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "Steam ID";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(804, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 30);
            this.label5.TabIndex = 11;
            this.label5.Text = "Steam Password";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // softwareVersion
            // 
            this.softwareVersion.AutoSize = true;
            this.softwareVersion.Dock = System.Windows.Forms.DockStyle.Right;
            this.softwareVersion.Location = new System.Drawing.Point(1081, 0);
            this.softwareVersion.Name = "softwareVersion";
            this.softwareVersion.Size = new System.Drawing.Size(60, 25);
            this.softwareVersion.TabIndex = 12;
            this.softwareVersion.Text = "Version 0.1";
            this.softwareVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // updateServer
            // 
            this.updateServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateServer.Location = new System.Drawing.Point(674, 28);
            this.updateServer.Name = "updateServer";
            this.updateServer.Size = new System.Drawing.Size(124, 24);
            this.updateServer.TabIndex = 13;
            this.updateServer.Text = "Update Server";
            this.updateServer.UseVisualStyleBackColor = true;
            this.updateServer.Click += new System.EventHandler(this.updateServer_Click);
            // 
            // getConfig
            // 
            this.getConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.getConfig.Location = new System.Drawing.Point(548, 58);
            this.getConfig.Name = "getConfig";
            this.getConfig.Size = new System.Drawing.Size(120, 24);
            this.getConfig.TabIndex = 14;
            this.getConfig.Text = "Get Config";
            this.getConfig.UseVisualStyleBackColor = true;
            this.getConfig.Click += new System.EventHandler(this.getConfig_Click);
            // 
            // checkUpdateOnStart
            // 
            this.checkUpdateOnStart.AutoSize = true;
            this.checkUpdateOnStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkUpdateOnStart.Location = new System.Drawing.Point(131, 88);
            this.checkUpdateOnStart.Name = "checkUpdateOnStart";
            this.checkUpdateOnStart.Size = new System.Drawing.Size(323, 20);
            this.checkUpdateOnStart.TabIndex = 15;
            this.checkUpdateOnStart.Text = "Check for Update on Server Start";
            this.checkUpdateOnStart.UseVisualStyleBackColor = true;
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
            this.Info});
            this.tableLayoutPanel1.SetColumnSpan(this.configSettings, 7);
            this.configSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configSettings.Location = new System.Drawing.Point(3, 136);
            this.configSettings.Name = "configSettings";
            this.tableLayoutPanel1.SetRowSpan(this.configSettings, 8);
            this.configSettings.Size = new System.Drawing.Size(1138, 256);
            this.configSettings.TabIndex = 16;
            // 
            // Variable
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Variable.DefaultCellStyle = dataGridViewCellStyle4;
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
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Script.DefaultCellStyle = dataGridViewCellStyle5;
            this.Script.HeaderText = "Script Location";
            this.Script.Name = "Script";
            this.Script.ReadOnly = true;
            // 
            // Info
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Info.DefaultCellStyle = dataGridViewCellStyle6;
            this.Info.HeaderText = "Info";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            // 
            // saveConfig
            // 
            this.saveConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveConfig.Location = new System.Drawing.Point(674, 58);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(124, 24);
            this.saveConfig.TabIndex = 17;
            this.saveConfig.Text = "Save Config";
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.saveConfig_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 615);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configSettings)).EndInit();
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
        private System.Windows.Forms.Label softwareVersion;
        private System.Windows.Forms.Button updateServer;
        private System.Windows.Forms.Button getConfig;
        private System.Windows.Forms.CheckBox checkUpdateOnStart;
        private System.Windows.Forms.DataGridView configSettings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Script;
        private System.Windows.Forms.DataGridViewTextBoxColumn Info;
        private System.Windows.Forms.Button saveConfig;
    }
}

