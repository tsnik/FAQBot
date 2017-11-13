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
        public BaseState(long cid) : base(cid)
        {
            this.history = new Stack<int>();
        }

        public override async Task<StateResult> OnMessage(Message message, TelegramBotClient bot)
        {
            if (message == null || message.Type != MessageType.TextMessage) return new StateResult(ChatState.DEFAULT);
            await showMessage(bot);
            return new StateResult(ChatState.DEFAULT);
        }

        public override async Task<StateResult> OnCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot)
        {
            int id = int.Parse(callbackQuery.Data);
            switch (id)
            {
                case -1:
                    return new StateResult(ChatState.OTHER);
                case -2:
                    history.Pop();
                    if (history.Count == 0)
                    {
                        id = -1;
                        break;
                    }
                    id = history.Peek();
                    break;
                default:
                    history.Push(id);
                    break;
            }
            await openButton(id, callbackQuery.Message.MessageId, bot);
            return new StateResult();
        }

        public override async Task Resume(TelegramBotClient bot)
        {
            await showMessage(bot, history.Peek());
        }

        #region Private
        private Stack<int> history;

        private InlineKeyboardMarkup genKeyBoard(List<Answer> answer)
        {
            InlineKeyboardMarkup kb_markup = new InlineKeyboardMarkup();
            //Не добавлять на кнопки ответы на вопросы
            answer = answer.FindAll(x => x.Type != AnswerType.Answer);
            //Если есть история, то создаем место под кнопку назад
            InlineKeyboardButton[][] inlineKeyboard =
                new InlineKeyboardButton[answer.Count + (history.Count > 0 ? 1 : 0)][];
            for (int i = 0; i < answer.Count; i++)
            {
                string text = answer[i].Text;
                string id = answer[i].id.ToString();
                if (answer[i].Type == AnswerType.Other)
                {
                    text = "Другое";
                    id = "-1";
                }
                inlineKeyboard[i] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton(text, id)
                };
            }
            if (history.Count > 0)
            {
                inlineKeyboard[answer.Count] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton("Назад", "-2")};
            }
            kb_markup.InlineKeyboard = inlineKeyboard;
            return kb_markup;
        }
        private async Task openButton(int id, int mid, TelegramBotClient bot)
        {
            List<Answer> answer = QuestionsBase.getQuestionBase().GetAnswer(id);
            String text = "Выберите";
            if (answer.Count != 0 && answer[0].Type == AnswerType.Answer)
            {
                text = answer[0].Text;
            }
            InlineKeyboardMarkup kb_markup = genKeyBoard(answer);
            await bot.EditMessageTextAsync(cid, mid, text, replyMarkup: kb_markup);
        }
        private async Task showMessage(TelegramBotClient bot, int id = -1)
        {
            List<Answer> answer = QuestionsBase.getQuestionBase().GetAnswer(id);
            InlineKeyboardMarkup kb_markup = genKeyBoard(answer);
            await bot.SendTextMessageAsync(cid, "Choose", replyMarkup: kb_markup);
        }
        #endregion
    }
}
