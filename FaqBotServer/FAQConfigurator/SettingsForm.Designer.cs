namespace FAQConfigurator
{
    partial class SettingsForm
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
            this.btnLoadSettings = new System.Windows.Forms.Button();
            this.dlgLoadSettings = new System.Windows.Forms.OpenFileDialog();
            this.gbDB = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDatabasePassword = new System.Windows.Forms.TextBox();
            this.tbDatabaseUser = new System.Windows.Forms.TextBox();
            this.tbDatabaseName = new System.Windows.Forms.TextBox();
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.gbEmail = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbEmailTo = new System.Windows.Forms.TextBox();
            this.tbEmailFrom = new System.Windows.Forms.TextBox();
            this.tbEmailPassword = new System.Windows.Forms.TextBox();
            this.tbEmailUser = new System.Windows.Forms.TextBox();
            this.tbEmailPort = new System.Windows.Forms.TextBox();
            this.tbEmailServer = new System.Windows.Forms.TextBox();
            this.tbApiKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.dlgSaveSettings = new System.Windows.Forms.SaveFileDialog();
            this.btnEditDatabase = new System.Windows.Forms.Button();
            this.btnCheckConnection = new System.Windows.Forms.Button();
            this.gbDB.SuspendLayout();
            this.gbEmail.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadSettings
            // 
            this.btnLoadSettings.Location = new System.Drawing.Point(29, 35);
            this.btnLoadSettings.Name = "btnLoadSettings";
            this.btnLoadSettings.Size = new System.Drawing.Size(142, 23);
            this.btnLoadSettings.TabIndex = 0;
            this.btnLoadSettings.Text = "Загрузить настройки";
            this.btnLoadSettings.UseVisualStyleBackColor = true;
            this.btnLoadSettings.Click += new System.EventHandler(this.btnLoadSettings_Click);
            // 
            // dlgLoadSettings
            // 
            this.dlgLoadSettings.AutoUpgradeEnabled = false;
            this.dlgLoadSettings.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgLoadSettings_FileOk);
            // 
            // gbDB
            // 
            this.gbDB.Controls.Add(this.label7);
            this.gbDB.Controls.Add(this.label6);
            this.gbDB.Controls.Add(this.label5);
            this.gbDB.Controls.Add(this.label4);
            this.gbDB.Controls.Add(this.tbDatabasePassword);
            this.gbDB.Controls.Add(this.tbDatabaseUser);
            this.gbDB.Controls.Add(this.tbDatabaseName);
            this.gbDB.Controls.Add(this.tbServerName);
            this.gbDB.Location = new System.Drawing.Point(22, 113);
            this.gbDB.Name = "gbDB";
            this.gbDB.Size = new System.Drawing.Size(224, 124);
            this.gbDB.TabIndex = 1;
            this.gbDB.TabStop = false;
            this.gbDB.Text = "База данных";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Пароль";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(114, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Пользователь";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "База данных";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Сервер";
            // 
            // tbDatabasePassword
            // 
            this.tbDatabasePassword.Location = new System.Drawing.Point(7, 95);
            this.tbDatabasePassword.Name = "tbDatabasePassword";
            this.tbDatabasePassword.Size = new System.Drawing.Size(100, 20);
            this.tbDatabasePassword.TabIndex = 3;
            // 
            // tbDatabaseUser
            // 
            this.tbDatabaseUser.Location = new System.Drawing.Point(7, 70);
            this.tbDatabaseUser.Name = "tbDatabaseUser";
            this.tbDatabaseUser.Size = new System.Drawing.Size(100, 20);
            this.tbDatabaseUser.TabIndex = 2;
            // 
            // tbDatabaseName
            // 
            this.tbDatabaseName.Location = new System.Drawing.Point(7, 45);
            this.tbDatabaseName.Name = "tbDatabaseName";
            this.tbDatabaseName.Size = new System.Drawing.Size(100, 20);
            this.tbDatabaseName.TabIndex = 1;
            // 
            // tbServerName
            // 
            this.tbServerName.Location = new System.Drawing.Point(7, 20);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(100, 20);
            this.tbServerName.TabIndex = 0;
            // 
            // gbEmail
            // 
            this.gbEmail.Controls.Add(this.label11);
            this.gbEmail.Controls.Add(this.label10);
            this.gbEmail.Controls.Add(this.label9);
            this.gbEmail.Controls.Add(this.label8);
            this.gbEmail.Controls.Add(this.label3);
            this.gbEmail.Controls.Add(this.label2);
            this.gbEmail.Controls.Add(this.tbEmailTo);
            this.gbEmail.Controls.Add(this.tbEmailFrom);
            this.gbEmail.Controls.Add(this.tbEmailPassword);
            this.gbEmail.Controls.Add(this.tbEmailUser);
            this.gbEmail.Controls.Add(this.tbEmailPort);
            this.gbEmail.Controls.Add(this.tbEmailServer);
            this.gbEmail.Location = new System.Drawing.Point(252, 113);
            this.gbEmail.Name = "gbEmail";
            this.gbEmail.Size = new System.Drawing.Size(213, 157);
            this.gbEmail.TabIndex = 2;
            this.gbEmail.TabStop = false;
            this.gbEmail.Text = "Настройки email";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(113, 136);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Адрес поддержки";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(113, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Исходящий адрес";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(113, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Пароль";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(114, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Пользователь";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Порт";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Сервер";
            // 
            // tbEmailTo
            // 
            this.tbEmailTo.Location = new System.Drawing.Point(7, 130);
            this.tbEmailTo.Name = "tbEmailTo";
            this.tbEmailTo.Size = new System.Drawing.Size(100, 20);
            this.tbEmailTo.TabIndex = 5;
            // 
            // tbEmailFrom
            // 
            this.tbEmailFrom.Location = new System.Drawing.Point(7, 105);
            this.tbEmailFrom.Name = "tbEmailFrom";
            this.tbEmailFrom.Size = new System.Drawing.Size(100, 20);
            this.tbEmailFrom.TabIndex = 4;
            // 
            // tbEmailPassword
            // 
            this.tbEmailPassword.Location = new System.Drawing.Point(7, 80);
            this.tbEmailPassword.Name = "tbEmailPassword";
            this.tbEmailPassword.Size = new System.Drawing.Size(100, 20);
            this.tbEmailPassword.TabIndex = 3;
            // 
            // tbEmailUser
            // 
            this.tbEmailUser.Location = new System.Drawing.Point(7, 55);
            this.tbEmailUser.Name = "tbEmailUser";
            this.tbEmailUser.Size = new System.Drawing.Size(100, 20);
            this.tbEmailUser.TabIndex = 2;
            // 
            // tbEmailPort
            // 
            this.tbEmailPort.Location = new System.Drawing.Point(113, 30);
            this.tbEmailPort.Name = "tbEmailPort";
            this.tbEmailPort.Size = new System.Drawing.Size(46, 20);
            this.tbEmailPort.TabIndex = 1;
            // 
            // tbEmailServer
            // 
            this.tbEmailServer.Location = new System.Drawing.Point(7, 30);
            this.tbEmailServer.Name = "tbEmailServer";
            this.tbEmailServer.Size = new System.Drawing.Size(100, 20);
            this.tbEmailServer.TabIndex = 0;
            // 
            // tbApiKey
            // 
            this.tbApiKey.Location = new System.Drawing.Point(29, 74);
            this.tbApiKey.Name = "tbApiKey";
            this.tbApiKey.Size = new System.Drawing.Size(325, 20);
            this.tbApiKey.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ключ к api";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(216, 35);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(138, 23);
            this.btnSaveSettings.TabIndex = 5;
            this.btnSaveSettings.Text = "Сохранить настройки";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // dlgSaveSettings
            // 
            this.dlgSaveSettings.DefaultExt = "cfg";
            this.dlgSaveSettings.Filter = "Config files|*.cfg";
            this.dlgSaveSettings.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgSaveSettings_FileOk);
            // 
            // btnEditDatabase
            // 
            this.btnEditDatabase.Enabled = false;
            this.btnEditDatabase.Location = new System.Drawing.Point(22, 304);
            this.btnEditDatabase.Name = "btnEditDatabase";
            this.btnEditDatabase.Size = new System.Drawing.Size(178, 23);
            this.btnEditDatabase.TabIndex = 6;
            this.btnEditDatabase.Text = "Редактировать базу данных";
            this.btnEditDatabase.UseVisualStyleBackColor = true;
            this.btnEditDatabase.Click += new System.EventHandler(this.btnEditDatabase_Click);
            // 
            // btnCheckConnection
            // 
            this.btnCheckConnection.Location = new System.Drawing.Point(22, 244);
            this.btnCheckConnection.Name = "btnCheckConnection";
            this.btnCheckConnection.Size = new System.Drawing.Size(149, 23);
            this.btnCheckConnection.TabIndex = 7;
            this.btnCheckConnection.Text = "Проверить соединение";
            this.btnCheckConnection.UseVisualStyleBackColor = true;
            this.btnCheckConnection.Click += new System.EventHandler(this.btnCheckConnection_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 359);
            this.Controls.Add(this.btnCheckConnection);
            this.Controls.Add(this.btnEditDatabase);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbApiKey);
            this.Controls.Add(this.gbEmail);
            this.Controls.Add(this.gbDB);
            this.Controls.Add(this.btnLoadSettings);
            this.MaximumSize = new System.Drawing.Size(493, 397);
            this.MinimumSize = new System.Drawing.Size(493, 397);
            this.Name = "SettingsForm";
            this.Text = "Редактор настроек";
            this.gbDB.ResumeLayout(false);
            this.gbDB.PerformLayout();
            this.gbEmail.ResumeLayout(false);
            this.gbEmail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadSettings;
        private System.Windows.Forms.OpenFileDialog dlgLoadSettings;
        private System.Windows.Forms.GroupBox gbDB;
        private System.Windows.Forms.TextBox tbDatabasePassword;
        private System.Windows.Forms.TextBox tbDatabaseUser;
        private System.Windows.Forms.TextBox tbDatabaseName;
        private System.Windows.Forms.TextBox tbServerName;
        private System.Windows.Forms.GroupBox gbEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbEmailTo;
        private System.Windows.Forms.TextBox tbEmailFrom;
        private System.Windows.Forms.TextBox tbEmailPassword;
        private System.Windows.Forms.TextBox tbEmailUser;
        private System.Windows.Forms.TextBox tbEmailPort;
        private System.Windows.Forms.TextBox tbEmailServer;
        private System.Windows.Forms.TextBox tbApiKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SaveFileDialog dlgSaveSettings;
        private System.Windows.Forms.Button btnEditDatabase;
        private System.Windows.Forms.Button btnCheckConnection;
    }
}

