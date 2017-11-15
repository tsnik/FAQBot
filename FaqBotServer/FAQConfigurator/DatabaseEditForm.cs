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
    public partial class DatabaseEditForm : Form
    {
        public DatabaseEditForm()
        {
            InitializeComponent();
            loadTreeView();
            
        }

        private void dgvQuestions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            QuestionsBase.getQuestionBase().Update();
        }

        private void loadTreeView()
        {
            QuestionsBase db =  QuestionsBase.getQuestionBase();
            tvQuestions.Nodes.AddRange(getChilds());
        }

        private TreeNode fromQuestionToTreeNode(Question q)
        {
            TreeNode tn = new TreeNode(q.title == null ? Answer.TITLE_DEF : q.title,
                getChilds(q.Id));
            tn.Tag = q;
            return tn;
        }

        private TreeNode[] getChilds(int id = -1)
        {
            QuestionsBase db = QuestionsBase.getQuestionBase();
            return db.GetTable().Where(x => x.parent == id).Select<Question, TreeNode>(fromQuestionToTreeNode).ToArray();
        }

        private void tvQuestions_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            QuestionEditForm f = new QuestionEditForm((Question)e.Node.Tag);
            f.ShowDialog();
        }
    }
}
