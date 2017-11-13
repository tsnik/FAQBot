using System;

namespace FaqBotServer
{
    struct DBCredentials
    {
        public string ServerName;
        public string DBName;
        public string User;
        public string Password;

        public DBCredentials(string serverName, string dbName, string user=null, string password=null)
        {
            ServerName = serverName;
            DBName = dbName;
            User = user;
            Password = password;
        }
    }

    struct EmailCredentials
    {
        public string ServerName;
        public int Port;
        public string User;
        public string Password;

        public EmailCredentials(string serverName, int port, string user = null, string password = null)
        {
            ServerName = serverName;
            Port = port;
            User = user;
            Password = password;
        }
    }

    class Settings
    {
        public static Settings GetSettings()
        {
            if(settings == null)
            {
                settings = new Settings();
                settings.LoadSettings();
            }
            return settings;
        }

        public string ApiKey
        {
            get
            {
                return apiKey;
            }
        }

        public string SupportEmail
        {
            get
            {
                return supportEmail;
            }
        }

        public DBCredentials DBCreds
        {
            get
            {
                return dbCreds;
            }
        }

        public EmailCredentials EmailCred
        {
            get
            {
                return emailCreds;
            }
        }

        #region Private
        private string apiKey;
        private string supportEmail;
        private DBCredentials dbCreds;
        private EmailCredentials emailCreds;
        private static Settings settings;

        private Settings()
        {

        }

        public void LoadSettings()
        {
            dbCreds = new DBCredentials(@"NIKITA-PC\SQLEXPRESS", "Questions");
            apiKey = "483580455:AAF_nwfbVQluNkP1d8wHpVMtwW_x8gvMELM";
            supportEmail = "info@nstsyrlin.ru";
            return;
        }
        #endregion
    }
}
