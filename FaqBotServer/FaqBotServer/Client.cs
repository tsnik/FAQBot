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
            this.qid = -1;
            this.bot = bot;
        }

        public async Task OnMessage(Message message)
        {

            if (message == null || message.Type != MessageType.TextMessage) return;
            List<Answer> answer = QuestionsBase.getQuestionBase().GetAnswer();
            InlineKeyboardMarkup kb_markup = genKeyBoard(answer);
            await bot.SendTextMessageAsync(message.Chat.Id, "Choose" + qid, replyMarkup: kb_markup);
            qid++;
        }

        public async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            List<Answer> answer = QuestionsBase.getQuestionBase().GetAnswer(int.Parse(callbackQuery.Data));
            InlineKeyboardMarkup kb_markup = genKeyBoard(answer);
            await bot.EditMessageTextAsync(cid, callbackQuery.Message.MessageId, "Choose" + qid, replyMarkup: kb_markup);
        }

        #region Private
        private long cid;
        private int qid;
        private TelegramBotClient bot;
        private InlineKeyboardMarkup genKeyBoard(List<Answer> answer)
        {
            InlineKeyboardMarkup kb_markup = new InlineKeyboardMarkup();
            InlineKeyboardButton[][] inlineKeyboard = new InlineKeyboardButton[answer.Count][];
            for (int i = 0; i < answer.Count; i++)
            {
                inlineKeyboard[i] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton(answer[i].Text, i.ToString())
                };
            }
            kb_markup.InlineKeyboard = inlineKeyboard;
            return kb_markup;
        }
        #endregion
    }
}
