using System;
using System.IO;

namespace FaqBotServer
{
    struct DBCredentials
    {
        public string ServerName;
        public string DBName;
        public string User;
        public string Password;

        public DBCredentials(string serverName, string dbName, string user = null, string password = null)
        {
            ServerName = serverName;
            DBName = dbName;
            User = user;
            Password = password;
        }

        public DBCredentials(string line)
        {
            String[] rawDbCreds = line.Trim().Split(';');
            ServerName = rawDbCreds[0];
            DBName = rawDbCreds[1];
            User = null;
            Password = null;
            if (rawDbCreds.Length > 2)
            {
                User = rawDbCreds[2];
                Password = rawDbCreds[3];
            }
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

        public EmailCredentials(string line)
        {
            String[] rawEmailCreds = line.Trim().Split(';');
            ServerName = rawEmailCreds[0];
            Port = int.Parse(rawEmailCreds[1]);
            User = null;
            Password = null;
            if (rawEmailCreds.Length > 2)
            {
                User = rawEmailCreds[2];
                Password = rawEmailCreds[3];
            }
        }
    }

    class Settings
    {
        public static Settings GetSettings()
        {
            if (settings == null)
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

        public EmailCredentials EmailCreds
        {
            get
            {
                return emailCreds;
            }
        }

        public string FromEmail
        {
            get
            {
                return fromEmail;
            }
        }

        #region Private
        private string apiKey;
        private string supportEmail;
        private string fromEmail;
        private DBCredentials dbCreds;
        private EmailCredentials emailCreds;
        private static Settings settings;

        private Settings()
        {

        }

        public void LoadSettings()
        {
            //apiKey
            //serverName;DbName;user;pass
            //supportEmail
            //fromEmail
            //server;port;user;pass
            StreamReader f = new StreamReader("settings.cfg");
            apiKey = f.ReadLine();
            dbCreds = new DBCredentials(f.ReadLine());
            supportEmail = f.ReadLine();
            fromEmail = f.ReadLine();
            emailCreds = new EmailCredentials(f.ReadLine());
            f.Close();
            return;
        }
        #endregion
    }
}
