using System;
using System.Collections.Generic;
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
        public AnswerType Type;
        public string Text;
        public int id;

        public Answer(AnswerType type, string text, int id)
        {
            Type = type;
            Text = text;
            this.id = id;
        }
    }

    class QuestionsBase
    {
        public static QuestionsBase getQuestionBase()
        {
            if(questionBase == null)
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
        public List<Answer> GetAnswer(int pos=-1, int id=-1)
        {
            List<Answer> answer = new List<Answer>();
            if(pos != -1)
            {
                answer.Add(new Answer(AnswerType.Category, "zq", 0));
                answer.Add(new Answer(AnswerType.Category, "zq", 1));
                answer.Add(new Answer(AnswerType.Category, "xq", 2));
                answer.Add(new Answer(AnswerType.Category, "fq", 3));
                answer.Add(new Answer(AnswerType.Category, "gq", 4));
                answer.Add(new Answer(AnswerType.Category, "dq", 5));
                return answer;
            }
            answer.Add(new Answer(AnswerType.Category, "qq", 0));
            answer.Add(new Answer(AnswerType.Category, "qq", 1));
            answer.Add(new Answer(AnswerType.Category, "qq", 2));
            answer.Add(new Answer(AnswerType.Category, "qq", 3));
            answer.Add(new Answer(AnswerType.Category, "qq", 4));
            answer.Add(new Answer(AnswerType.Category, "qq", 5));
            return answer;
        }

        #region Private
        private static QuestionsBase questionBase;

        private QuestionsBase(DBCredentials creds)
        {

        }

        private void LoadDB()
        {
            return;
        }
        #endregion

    }
}
