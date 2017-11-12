using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace FaqBotServer
{
    class Client
    {
        public Client(long cid, TelegramBotClient bot)
        {
            this.cid = cid;
            this.bot = bot;
            this.history = new Stack<int>();
        }

        public async Task OnMessage(Message message)
        {

            if (message == null || message.Type != MessageType.TextMessage) return;
            List<Answer> answer = QuestionsBase.getQuestionBase().GetAnswer();
            InlineKeyboardMarkup kb_markup = genKeyBoard(answer);
            await bot.SendTextMessageAsync(message.Chat.Id, "Choose", replyMarkup: kb_markup);
        }

        public async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            int id = int.Parse(callbackQuery.Data);
            switch (id)
            {
                case -1:
                    break;
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
            await openButton(id, callbackQuery.Message.MessageId);
        }

        #region Private
        private long cid;
        private Stack<int> history;
        private TelegramBotClient bot;
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
                inlineKeyboard[i] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton(answer[i].Text, answer[i].id.ToString())
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
        private async Task openButton(int id, int mid)
        {
            List<Answer> answer = QuestionsBase.getQuestionBase().GetAnswer(id);
            String text = "Выберите";
            if(answer[0].Type == AnswerType.Answer)
            {
                text = answer[0].Text;
            }
            InlineKeyboardMarkup kb_markup = genKeyBoard(answer);
            await bot.EditMessageTextAsync(cid, mid, text, replyMarkup: kb_markup);
        }
        #endregion
    }
}
