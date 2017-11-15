using System;
using System.IO;

namespace SettingsAndDB
{
    public struct DBCredentials
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

        public override string ToString()
        {
            string[] tmp;
            if(User != null)
            {
                tmp = new string[4];
                tmp[2] = User;
                tmp[3] = Password;
            }
            else
            {
                tmp = new string[2];
            }
            tmp[0] = ServerName;
            tmp[1] = DBName;
            return string.Join(";", tmp);
        }
    }

    public struct EmailCredentials
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
        public override string ToString()
        {
            string[] tmp;
            if (User != null)
            {
                tmp = new string[4];
                tmp[2] = User;
                tmp[3] = Password;
            }
            else
            {
                tmp = new string[2];
            }
            tmp[0] = ServerName;
            tmp[1] = Port.ToString();
            return string.Join(";", tmp);
        }
    }

    public class Settings
    {
        public const string DEF_FILENAME = "settings.cfg";

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
            set
            {
                apiKey = value;
            }
        }

        public string SupportEmail
        {
            get
            {
                return supportEmail;
            }
            set
            {
                supportEmail = value;
            }
        }

        public DBCredentials DBCreds
        {
            get
            {
                return dbCreds;
            }
            set
            {
                dbCreds = value;
            }
        }

        public EmailCredentials EmailCreds
        {
            get
            {
                return emailCreds;
            }
            set
            {
                emailCreds = value;
            }
        }

        public string FromEmail
        {
            get
            {
                return fromEmail;
            }
            set
            {
                fromEmail = value;
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

        public void LoadSettings(string filename = DEF_FILENAME)
        {
            //apiKey
            //serverName;DbName;user;pass
            //supportEmail
            //fromEmail
            //server;port;user;pass
            StreamReader f = new StreamReader(filename);
            apiKey = f.ReadLine();
            dbCreds = new DBCredentials(f.ReadLine());
            supportEmail = f.ReadLine();
            fromEmail = f.ReadLine();
            emailCreds = new EmailCredentials(f.ReadLine());
            f.Close();
            return;
        }
        public void SaveSettings(string filename = DEF_FILENAME)
        {
            StreamWriter f = new StreamWriter(filename, false);
            f.WriteLine(apiKey);
            f.WriteLine(dbCreds.ToString());
            f.WriteLine(supportEmail);
            f.WriteLine(fromEmail);
            f.WriteLine(emailCreds.ToString());
            f.Flush();
            f.Close();
        }

        #endregion
    }
}
