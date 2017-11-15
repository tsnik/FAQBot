using SettingsAndDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAQConfigurator
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnLoadSettings_Click(object sender, EventArgs e)
        {
            dlgLoadSettings.ShowDialog();
        }

        private void dlgLoadSettings_FileOk(object sender, CancelEventArgs e)
        {
            Settings.GetSettings().LoadSettings(dlgLoadSettings.FileName);
            fillSettings();

        }

        private void fillSettings()
        {
            Settings st = Settings.GetSettings();
            tbApiKey.Text = st.ApiKey;
            tbDatabaseName.Text = st.DBCreds.DBName;
            tbDatabasePassword.Text = st.DBCreds.Password;
            tbDatabaseUser.Text = st.DBCreds.User;
            tbServerName.Text = st.DBCreds.ServerName;
            tbEmailServer.Text = st.EmailCreds.ServerName;
            tbEmailPort.Text = st.EmailCreds.Port.ToString();
            tbEmailUser.Text = st.EmailCreds.User;
            tbEmailPassword.Text = st.EmailCreds.Password;
            tbEmailFrom.Text = st.FromEmail;
            tbEmailTo.Text = st.SupportEmail;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            dlgSaveSettings.ShowDialog();
        }

        private void dlgSaveSettings_FileOk(object sender, CancelEventArgs e)
        {
            Settings st = Settings.GetSettings();
            st.ApiKey = parseString(tbApiKey, true);
            st.DBCreds = new DBCredentials(parseString(tbServerName, true),
                parseString(tbDatabaseName, true),
                parseString(tbDatabaseUser), parseString(tbDatabasePassword));
            st.EmailCreds = new EmailCredentials(parseString(tbEmailServer, true),
                parseInt(tbEmailPort, true),
                parseString(tbEmailUser), parseString(tbEmailPassword));
            st.FromEmail = parseString(tbEmailFrom, true);
            st.SupportEmail = parseString(tbEmailTo, true);
            st.SaveSettings(dlgSaveSettings.FileName);
        }

        private string parseString(TextBox tb, bool required = false)
        {
            return tb.Text == "" ? null : tb.Text;
        }
        private int parseInt(TextBox tb, bool required = false)
        {
            return int.Parse(tb.Text);
        }
    }
}
