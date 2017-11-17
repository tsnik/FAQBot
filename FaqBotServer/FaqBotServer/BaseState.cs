using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using SettingsAndDB;

namespace FaqBotServer
{
    struct MessageToSend
    {
        public string Text;
        public IReplyMarkup KeyboardMarkup;

        public MessageToSend(string text, IReplyMarkup keyboardMarkup = null)
        {
            Text = text;
            KeyboardMarkup = keyboardMarkup;
        }
    }

    class BaseState : State
    {
        public const string MAIN_SELECT = "С чем у вас возникла проблема?";

        public BaseState(long cid) : base(cid)
        {
            this.history = new Stack<int>();
            this.genMessage = this.genMessageText;
        }

        public override async Task<StateResult> OnMessage(Message message, TelegramBotClient bot)
        {
            await showCategory(bot);
            return new StateResult();
        }

        protected override async Task<StateResult> onCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot)
        {
            int id = int.Parse(callbackQuery.Data);
            Answer ans;
            if (id < 0)
            {
                if ((Button)id == Button.Back)
                {
                    if (history.Count > 0) history.Pop();
                    id = history.Count == 0 ? QuestionsBase.ROOT_ID : history.Peek();
                }
                else if ((Button)id == Button.Main)
                {
                    history = new Stack<int>();
                    id = QuestionsBase.ROOT_ID;
                }
                ans = QuestionsBase.getQuestionBase().GetElement(id);
            }
            else
            {
                history.Push(id);
                if (currentList != null && id != QuestionsBase.ROOT_ID)
                {
                    ans = currentList.Find(x => x.id == id);
                }
                else
                {
                    ans = QuestionsBase.getQuestionBase().GetElement(id);
                }
            }
            return await openButton(ans, callbackQuery.Message.MessageId, bot);
        }

        public override async Task Resume(TelegramBotClient bot)
        {
            history.Pop();
            await showCategory(bot, history.Peek());
        }

        #region Private
        private Stack<int> history;
        private List<Answer> currentList;
        private delegate MessageToSend MessageGenerator(string text, int id = QuestionsBase.ROOT_ID);
        private MessageGenerator genMessage;

        private List<InlineKeyboardButton[]> genSpecialButtons()
        {
            List<InlineKeyboardButton[]> result = new List<InlineKeyboardButton[]>();
            if (history.Count > 0)
            {
                result.Add(
                    new InlineKeyboardButton[1]{ new InlineKeyboardCallbackButton(BACK,
                    ((int)Button.Back).ToString()) });
            }
            if (history.Count > 1)
            {
                result.Add(
                    new InlineKeyboardButton[1]{new InlineKeyboardCallbackButton(MAIN,
                    ((int)Button.Main).ToString()) });
            }
            return result;
        }

        private InlineKeyboardMarkup genKeyBoard(List<Answer> answer = null, bool oneline = false)
        {
            InlineKeyboardMarkup kb_markup = new InlineKeyboardMarkup();
            int num = answer == null ? 0 : answer.Count;
            int linecount = oneline ? 1 : num;
            List<InlineKeyboardButton[]> specialButtons = genSpecialButtons();
            InlineKeyboardButton[][] inlineKeyboard =
                new InlineKeyboardButton[linecount + specialButtons.Count][];

            if (oneline)
            {
                inlineKeyboard[0] = new InlineKeyboardButton[num];
            }

            for (int i = 0; i < num; i++)
            {
                string text = answer[i].Title;
                string id = answer[i].id.ToString();
                if (oneline)
                {
                    inlineKeyboard[0][i] = new InlineKeyboardCallbackButton(text, id);
                    continue;
                }
                inlineKeyboard[i] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton(text, id)
                };
            }

            if (specialButtons.Count > 0)
                specialButtons.CopyTo(inlineKeyboard, linecount);

            kb_markup.InlineKeyboard = inlineKeyboard;
            return kb_markup;
        }

        private async Task<StateResult> openButton(Answer ans, int mid, TelegramBotClient bot)
        {
            if (ans.Type == AnswerType.Other)
            {
                return new StateResult(ChatState.OTHER, data: 
                    new object[2] { ans.Text, history });
            }
            int id = ans.id;
            currentList = QuestionsBase.getQuestionBase().GetCategoryChilds(id);
            MessageToSend m;
            if (ans.Type == AnswerType.Category)
            {
                m = genMessageText(MAIN_SELECT, id);
            }
            else
            {
                m = genMessageInlineText(ans.Text, id);
            }
            await bot.EditMessageTextAsync(cid, mid, m.Text, replyMarkup: m.KeyboardMarkup);
            return new StateResult();
        }

        private async Task showCategory(TelegramBotClient bot, int id = QuestionsBase.ROOT_ID)
        {
            currentList = QuestionsBase.getQuestionBase().GetCategoryChilds(id);
            MessageToSend m = genMessage(MAIN_SELECT, id);
            await sendMessage(bot, m.Text, m.KeyboardMarkup);
        }

        private MessageToSend genMessageInlineText(string text, int id = QuestionsBase.ROOT_ID)
        {
            InlineKeyboardMarkup kb_markup = genKeyBoard(currentList);
            return new MessageToSend(text, kb_markup);
        }

        private MessageToSend genMessageText(string text, int id = QuestionsBase.ROOT_ID)
        {
            List<Answer> tmp = new List<Answer>(currentList);
            text += "\n";
            for (int i = 0; i < tmp.Count; i++)
            {
                text += (i + 1).ToString() + ". " + tmp[i].Title + "\n";
                tmp[i] = new Answer(tmp[i].Type, tmp[i].id, (i + 1).ToString());
            }
            InlineKeyboardMarkup kb_markup = genKeyBoard(tmp, true);
            return new MessageToSend(text, kb_markup);
        }
        #endregion
    }
}
