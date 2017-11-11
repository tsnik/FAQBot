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
        private String apiKey;
        private String supportEmail;
        private DBCredentials dbCreds;

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

        public void LoadSettings()
        {
            return;
        }

    }
}
