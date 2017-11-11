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

        public string ApiKey
        {
            get
            {
                //return apiKey;
                return "483580455:AAF_nwfbVQluNkP1d8wHpVMtwW_x8gvMELM";
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
                //return dbCreds;
                return new DBCredentials();
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
            return;
        }
        #endregion
    }
}
