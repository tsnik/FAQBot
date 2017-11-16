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
            QuestionsBase db = QuestionsBase.getQuestionBase();
            TreeNode baseNode = new TreeNode("Справочник");
            Question baseCat = new Question();
            baseCat.Id = -1;
            baseNode.Tag = baseCat;
            baseNode.Nodes.AddRange(getChilds());
            tvQuestions.Nodes.Add(baseNode);
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
            return db.GetChilds(id).Select<Question, TreeNode>(fromQuestionToTreeNode).ToArray();
        }

        private void tvQuestions_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            QuestionEditForm f = new QuestionEditForm((Question)e.Node.Tag);
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                QuestionsBase.getQuestionBase().Update();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TreeNode parent = tvQuestions.SelectedNode;
            Question parentQuestion = (Question)parent.Tag;

            QuestionEditForm f = new QuestionEditForm();
            DialogResult result = f.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            Question q = f.Question;
            q.parent = parentQuestion.Id;
            q.pos = parent.Nodes.Count;
            QuestionsBase.getQuestionBase().Insert(q);
            parent.Nodes.Add(fromQuestionToTreeNode(q));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            TreeNode node = tvQuestions.SelectedNode;
            Question question = (Question)node.Tag;
            QuestionsBase.getQuestionBase().Delete(question);
            node.Parent.Nodes.Remove(node);
        }
    }
}
