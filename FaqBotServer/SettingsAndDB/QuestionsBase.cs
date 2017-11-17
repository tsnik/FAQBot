using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettingsAndDB
{
    public enum AnswerType
    {
        Category,
        Answer,
        Other
    }

    public struct Answer
    {
        public const string TITLE_DEF = "Другое";
        public const string TEXT_DEF = "Пришлите скриншот и опишите проблему, с которой Вы столкнулись, на адрес deltapro@deltacredit.ru или в этот чат";



        public AnswerType Type;
        public string Text
        {
            get
            {
                if (text == null && Type == AnswerType.Other)
                    return TEXT_DEF;
                return text;
            }
        }
        public string Title
        {
            get
            {
                if (title == null && Type == AnswerType.Other)
                    return TITLE_DEF;
                return title;
            }
        }
        public int id;

        public Answer(AnswerType type, int id, string title = null, string text = null)
        {
            Type = type;
            this.text = text;
            this.id = id;
            this.title = title;
        }

        private string title;
        private string text;
    }

    public class QuestionsBase
    {
        public static QuestionsBase getQuestionBase()
        {
            if (questionBase == null || !questionBase.loaded)
            {
                questionBase = new QuestionsBase(Settings.GetSettings().DBCreds);
                questionBase.LoadDB();
            }
            return questionBase;
        }

        public static void UnLoad()
        {
            questionBase = null;
        }

        /// <summary>
        /// Вернёт список, если есть несколько ккатегорий на этом уровне
        /// Вернет список из одного элемента, если достигли самого нижнего уровня
        /// </summary>
        /// <param name="id">Идентификатор родителя. Для верхнего уровня -1.</param>
        /// <returns></returns>
        public List<Answer> GetAnswer(int id = -1)
        {
            return new List<Answer>(GetChilds(id)
                .Select<Question, Answer>(GenAnswerFromQuestion));
        }

        public List<Answer> GetElements(List<int> ids)
        {
            return new List<Answer>(dataBase.Questions
                .Where(x => ids.Contains(x.Id))
                .ToList()
                .OrderBy(x => ids.IndexOf(x.Id))
                .Select<Question, Answer>(GenAnswerFromQuestion));
        }

        public Answer GetElement(int id)
        {
            if (id < 0)
            {
                return new Answer(AnswerType.Category, -1);
            }
            return GenAnswerFromQuestion(dataBase.Questions.Where(x => x.Id == id).First());
        }

        public void Update()
        {
            dataBase.SubmitChanges();
            dataBase.Refresh(RefreshMode.OverwriteCurrentValues, dataBase.Questions);
        }

        public void Insert(Question q)
        {
            dataBase.Questions.InsertOnSubmit(q);
            Update();
        }

        public void Delete(Question q)
        {
            dataBase.Questions.DeleteOnSubmit(q);
            Update();
        }

        public IQueryable<Question> GetChilds(int id = -1)
        {
            return dataBase.Questions.Where(x => x.parent == id).OrderBy(x => x.pos);
        }

        #region Private
        private static QuestionsBase questionBase;
        private DBCredentials creds;
        private DataBaseDataContext dataBase;
        private bool loaded;

        private QuestionsBase(DBCredentials creds)
        {
            this.creds = creds;
        }

        private void LoadDB()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = creds.ServerName;
            if (creds.User == null)
                builder.IntegratedSecurity = true;
            else
            {
                builder.UserID = creds.User;
                builder.Password = creds.Password;
            }
            builder.InitialCatalog = creds.DBName;
            builder.ConnectTimeout = 1;
            dataBase = new DataBaseDataContext(builder.ConnectionString);
            dataBase.Questions.Count();
            loaded = true;
        }

        private Answer GenAnswerFromQuestion(Question q)
        {
            AnswerType t = AnswerType.Category;
            if (q.other)
                t = AnswerType.Other;
            if (q.answer)
                t = AnswerType.Answer;
            return new Answer(t, q.Id, q.title, q.text);
        }
        #endregion

    }
}
