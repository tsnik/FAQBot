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
    public partial class QuestionEditForm : Form
    {
        public QuestionEditForm(Question question)
        {
            this.question = question;
            InitializeComponent();
            tbTitle.Text = question.title;
            tbText.Text = question.text;
            int index = 0;
            if (question.answer)
                index = 1;
            if (question.other)
                index = 2;
            cbType.SelectedIndex = index;
        }

        private Question question;

        private void btnOk_Click(object sender, EventArgs e)
        {
            question.title = Utils.ParseString(tbTitle);
            question.text = Utils.ParseString(tbText);
            QuestionsBase.getQuestionBase().Update();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
