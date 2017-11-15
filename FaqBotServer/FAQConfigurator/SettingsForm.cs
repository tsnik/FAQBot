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
            st.ApiKey = Utils.ParseString(tbApiKey, true);
            st.DBCreds = new DBCredentials(Utils.ParseString(tbServerName, true),
                Utils.ParseString(tbDatabaseName, true),
                Utils.ParseString(tbDatabaseUser), Utils.ParseString(tbDatabasePassword));
            st.EmailCreds = new EmailCredentials(Utils.ParseString(tbEmailServer, true),
                Utils.ParseInt(tbEmailPort, true),
                Utils.ParseString(tbEmailUser), Utils.ParseString(tbEmailPassword));
            st.FromEmail = Utils.ParseString(tbEmailFrom, true);
            st.SupportEmail = Utils.ParseString(tbEmailTo, true);
            st.SaveSettings(dlgSaveSettings.FileName);
        }

        private void btnEditDatabase_Click(object sender, EventArgs e)
        {
            DatabaseEditForm form = new DatabaseEditForm();
            form.Show();
        }
    }
}
