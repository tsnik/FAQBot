using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaqBotServer
{
    enum AnswerType
    {
        Category,
        Answer,
        Other
    }

    struct Answer
    {
        public const string TITLE_DEF = "Другое";
        public const string TEXT_DEF = "Пришлите скриншот и опишите проблему, с которой Вы столкнулись, на адрес deltapro @deltacredit.ru или в этот чат";



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

        public Answer(AnswerType type, int id, string title=null, string text=null)
        {
            Type = type;
            this.text = text;
            this.id = id;
            this.title = title;
        }

        private string title;
        private string text;
    }

    class QuestionsBase
    {
        public static QuestionsBase getQuestionBase()
        {
            if (questionBase == null)
            {
                questionBase = new QuestionsBase(Settings.GetSettings().DBCreds);
                questionBase.LoadDB();
            }
            return questionBase;
        }

        /// <summary>
        /// Вернёт список, если есть несколько ккатегорий на этом уровне
        /// Вернет список из одного элемента, если достигли самого нижнего уровня
        /// </summary>
        /// <param name="pos">Позиция элемента в списке. Для верхнего уровня -1.</param>
        /// <param name="id">Идентификатор родителя. Для верхнего уровня -1.</param>
        /// <returns></returns>
        public List<Answer> GetAnswer(int id = -1)
        {
            Question curr;
            if (id == -1 || !(curr = dataBase.Questions.First<Question>(x => x.Id == id)).answer)
            {
                List<Answer> l = new List<Answer>(dataBase.Questions.Where(x => x.parent == id)
                    .OrderBy(x => x.pos)
                    .Select<Question, Answer>(GenAnswerFromQuestion));
                return l;
            }
            List<Answer> answer = new List<Answer>();
            answer.Add(new Answer(AnswerType.Answer, curr.Id, curr.text));
            return answer;
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

        #region Private
        private static QuestionsBase questionBase;
        private DBCredentials creds;
        private DataBaseDataContext dataBase;

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
            dataBase = new DataBaseDataContext(builder.ConnectionString);
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
