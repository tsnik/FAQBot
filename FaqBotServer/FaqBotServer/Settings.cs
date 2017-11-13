using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqBotServer
{
    struct DBCredentials
    {
        public String ServerName;
        public String DBName;
        public String User;
        public String Password;

        public DBCredentials(string serverName, string dbName, string user=null, string password=null)
        {
            ServerName = serverName;
            DBName = dbName;
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

        #region Private
        private string apiKey;
        private string supportEmail;
        private DBCredentials dbCreds;
        private static Settings settings;

        private Settings()
        {

        }

        public void LoadSettings()
        {
            dbCreds = new DBCredentials(@"NIKITA-PC\SQLEXPRESS", "Questions");
            apiKey = "483580455:AAF_nwfbVQluNkP1d8wHpVMtwW_x8gvMELM";
            return;
        }
        #endregion
    }
}
