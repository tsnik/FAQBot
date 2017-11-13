using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace FaqBotServer
{
    class BaseState : State
    {
        public const string MAIN_SELECT = "Сделайте выбор: ";

        public BaseState(long cid) : base(cid)
        {
            this.history = new Stack<int>();
        }

        public override async Task<StateResult> OnMessage(Message message, TelegramBotClient bot)
        {
            await showMessage(bot);
            return new StateResult();
        }

        protected override async Task<StateResult> onCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot)
        {
            int id = int.Parse(callbackQuery.Data);
            if (id < 0)
            {
                if ((Button)id == Button.Back)
                {
                    if(history.Count > 0) history.Pop();
                    id = history.Count == 0 ? -1 : history.Peek();
                }
            }
            Answer ans;
            if (currentList != null && id != -1)
            {
                ans = currentList.Find(x => x.id == id);
                history.Push(id);
            }
            else
            {
                ans = QuestionsBase.getQuestionBase().GetElement(id);
            }
            return await openButton(ans, callbackQuery.Message.MessageId, bot);
        }

        public override async Task Resume(TelegramBotClient bot)
        {
            history.Pop();
            await showMessage(bot, history.Peek());
        }

        #region Private
        private Stack<int> history;
        private List<Answer> currentList;

        private InlineKeyboardMarkup genKeyBoard(List<Answer> answer = null)
        {
            InlineKeyboardMarkup kb_markup = new InlineKeyboardMarkup();
            int num = answer == null ? 0 : answer.Count;
            //Если есть история, то создаем место под кнопку назад
            InlineKeyboardButton[][] inlineKeyboard =
                new InlineKeyboardButton[num + (history.Count > 0 ? 1 : 0)][];

            for (int i = 0; i < num; i++)
            {
                string text = answer[i].Title;
                string id = answer[i].id.ToString();
                if (answer[i].Type == AnswerType.Other && answer[i].Title == null)
                {
                    text = OTHER;
                }
                inlineKeyboard[i] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton(text, id)
                };
            }
            if (history.Count > 0)
            {
                inlineKeyboard[num] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton(BACK, ((int)Button.Back).ToString())};
            }
            kb_markup.InlineKeyboard = inlineKeyboard;
            return kb_markup;
        }
        private async Task<StateResult> openButton(Answer ans, int mid, TelegramBotClient bot)
        {
            if (ans.Type == AnswerType.Other)
            {
                return new StateResult(ChatState.OTHER, data: new object[1] {ans.Text});
            }
            String text = ans.Text;
            currentList = null;
            int id = ans.id;
            if (ans.Type == AnswerType.Category)
            {
                currentList = QuestionsBase.getQuestionBase().GetAnswer(id);
                text = MAIN_SELECT;
            }
            InlineKeyboardMarkup kb_markup = genKeyBoard(currentList);
            await bot.EditMessageTextAsync(cid, mid, text, replyMarkup: kb_markup);
            return new StateResult();
        }
        private async Task showMessage(TelegramBotClient bot, int id = -1)
        {
            currentList = QuestionsBase.getQuestionBase().GetAnswer(id);
            InlineKeyboardMarkup kb_markup = genKeyBoard(currentList);
            await sendMessage(bot, MAIN_SELECT, kb_markup);
        }
        #endregion
    }
}
