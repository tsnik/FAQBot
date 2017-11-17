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
        public const string DELETE_APPROVE = "Вы уверены, что хотите удалить этот элемент и все элементы, вложенные в него?";
        public const string DELETE_APPROVE_TITLE = "Удаление элемента";


        public DatabaseEditForm()
        {
            InitializeComponent();
            loadTreeView();

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
            if (((Question)e.Node.Tag).Id != -1)
            {
                QuestionEditForm f = new QuestionEditForm((Question)e.Node.Tag);
                DialogResult result = f.ShowDialog();
                if (result == DialogResult.OK)
                {
                    QuestionsBase.getQuestionBase().Update();
                }
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
            DialogResult result = MessageBox.Show(DELETE_APPROVE,
                DELETE_APPROVE_TITLE, MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                RemoveNode(tvQuestions.SelectedNode);
            }
        }

        private void RemoveNode(TreeNode node)
        {
            Question question = (Question)node.Tag;
            QuestionsBase.getQuestionBase().Delete(question);
            TreeNode[] nodes = new TreeNode[node.Nodes.Count];
            node.Nodes.CopyTo(nodes, 0);
            foreach (TreeNode child in nodes)
            {
                RemoveNode(child);
            }
            if (((Question)node.Tag).Id != -1)
                node.Remove();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            TreeNode node = tvQuestions.SelectedNode;
            swapNodes(node.PrevNode, node, true);
            
            
        }

        private void swapNodes(TreeNode first, TreeNode second, bool secondSelected = false)
        {
            TreeNode parent = first.Parent;
            int firstIndex = first.Index;
            int secondIndex = second.Index;
            Question firstQuestion = (Question)first.Tag;
            Question secondQuestion = (Question)second.Tag;
            first.Remove();
            second.Remove();
            parent.Nodes.Insert(firstIndex, second);
            parent.Nodes.Insert(secondIndex, first);
            second.TreeView.SelectedNode = secondSelected ? second : first;
            second.TreeView.Focus();
            firstQuestion.pos = secondIndex;
            secondQuestion.pos = firstIndex;
            QuestionsBase.getQuestionBase().Update();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            TreeNode node = tvQuestions.SelectedNode;
            swapNodes(node, node.NextNode);
        }

        private void tvQuestions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(((Question)e.Node.Tag).Id == -1)
            {
                btnUp.Enabled = false;
                btnDown.Enabled = false;
                btnRemove.Enabled = false;
                btnLeft.Enabled = false;
                btnRight.Enabled = false;
                return;
            }
            btnRemove.Enabled = true;
            btnUp.Enabled = btnRight.Enabled = e.Node.Index != 0;
            btnLeft.Enabled = ((Question)e.Node.Parent.Tag).Id != -1;
            btnDown.Enabled = e.Node.Index != e.Node.Parent.Nodes.Count - 1;
        }
    }
}
