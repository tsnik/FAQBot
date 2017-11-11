﻿using System;
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
            return null;
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
