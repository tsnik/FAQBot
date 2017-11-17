using SettingsAndDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAQConfigurator
{
    public partial class SettingsForm : Form
    {
        public const string ERROR_TITLE = "Ошибка";
        public const string SUCCESS_TITLE = "Успех";
        public const string ERROR_ENTER_DB = "Пожалуйста заполните имя сервера и базу данных";
        public const string ERRORR_CONNECTION_FAILED = "Не удалось установить соединение с сервером";
        public const string CONNECTION_SUCCESS = "Соединение успешно установлено";
        public const string ERROR_ENTER_PORT = "Пожалуйста укажите номер порта";
        public const string ERROR_ENTER_REQUIRED_FIELDS = "Пожалуйста заполните все обязательные поля";

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
            btnEditDatabase.Enabled = false;
            QuestionsBase.UnLoad();
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

        private bool retrieveSettings()
        {
            Settings st = Settings.GetOrCreateSettings();
            try {
                st.ApiKey = Utils.ParseString(tbApiKey, true);
                st.DBCreds = new DBCredentials(Utils.ParseString(tbServerName, true),
                    Utils.ParseString(tbDatabaseName, true),
                    Utils.ParseString(tbDatabaseUser),
                    Utils.ParseString(tbDatabasePassword, false, false));
                st.EmailCreds = new EmailCredentials(Utils.ParseString(tbEmailServer, true),
                    Utils.ParseInt(tbEmailPort, true),
                    Utils.ParseString(tbEmailUser), Utils.ParseString(tbEmailPassword));
                st.FromEmail = Utils.ParseString(tbEmailFrom, true);
                st.SupportEmail = Utils.ParseString(tbEmailTo, true);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show(ERROR_ENTER_REQUIRED_FIELDS,
                    ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (FormatException)
            {
                MessageBox.Show(ERROR_ENTER_PORT,
                    ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            if (!retrieveSettings())
                return;
            dlgSaveSettings.ShowDialog();
        }

        private void dlgSaveSettings_FileOk(object sender, CancelEventArgs e)
        {
            Settings.GetSettings().SaveSettings(dlgSaveSettings.FileName);
        }

        private void btnEditDatabase_Click(object sender, EventArgs e)
        {
            DatabaseEditForm form = new DatabaseEditForm();
            form.Show();
        }

        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            if(tbServerName.Text == "" || tbDatabaseName.Text == "")
            {
                MessageBox.Show(ERROR_ENTER_DB,
                    ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!retrieveSettings())
                return;
            try {
                QuestionsBase.getQuestionBase();
            } catch (SqlException)
            {
                MessageBox.Show(ERRORR_CONNECTION_FAILED,
                    ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(CONNECTION_SUCCESS,
                    SUCCESS_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnEditDatabase.Enabled = true;
        }
    }
}
