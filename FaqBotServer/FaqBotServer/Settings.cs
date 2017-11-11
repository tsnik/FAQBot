using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqBotServer
{
    struct DBCredentials
    {
        String ServerName;
        String DBName;
        String User;
        String Password;
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

        public String ApiKey
        {
            get
            {
                return apiKey;
            }
        }

        public String SupportEmail
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
        private String apiKey;
        private String supportEmail;
        private DBCredentials dbCreds;
        private static Settings settings;

        private Settings()
        {

        }

        public void LoadSettings()
        {
            return;
        }
        #endregion
    }
}
