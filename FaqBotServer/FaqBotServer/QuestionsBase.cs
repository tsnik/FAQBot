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
        AnswerType Type;
        String Text;
        int id;
    }

    class QuestionsBase
    {
        static QuestionsBase questionBase;
         
        private QuestionsBase (DBCredentials creds)
        {

        }

        public static QuestionsBase getQuestionBase()
        {
            if(questionBase == null)
            {
                questionBase = new QuestionsBase(Settings.GetSettings().DBCreds);
                questionBase.LoadDB();
            }
            return questionBase;
        }

        public void LoadDB()
        {
            return;
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
            return null;
        }
    }
}
