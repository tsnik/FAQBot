using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FaqBotServer
{
    enum Action
    {
        None,
        Main,
        Back
    }

    enum Button
    {
        Back = -2,
        Send = -3,
        Main = -4
    }
    struct StateResult
    {
        public ChatState state;
        public Action action;
        public object[] data;

        public StateResult(ChatState state = ChatState.NONE, Action action = Action.None, object[] data = null)
        {
            this.state = state;
            this.action = action;
            this.data = data;
        }
    }

    abstract class State
    {
        public const string BACK = "Назад";
        public const string SEND = "Отправить";
        public const string MAIN = "Главное меню";

        public State(long cid, long mid = 0)
        {
            this.cid = cid;
            this.mid = mid;
        }

        public abstract Task<StateResult> OnMessage(Message message, TelegramBotClient bot);

        public async Task<StateResult> OnCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot)
        {
            StateResult result = new StateResult();
            if (callbackQuery.Message.MessageId == mid)
            {
                result = await onCallbackQuery(callbackQuery, bot);
            }
            await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
            return result;
        }

        public virtual Task Resume(TelegramBotClient bot)
        {
            throw new NotImplementedException();
        }

        public long Mid
        {
            get
            {
                return mid;
            }
        } 


        protected long cid;
        protected long mid;
        protected async Task sendMessage(TelegramBotClient bot, string text, InlineKeyboardMarkup kb_markup = null)
        {
            Message m = await bot.SendTextMessageAsync(cid, text, replyMarkup: kb_markup);
            mid = m.MessageId;
        }

        protected async Task sendPhoto(TelegramBotClient bot, FileToSend file, string text, InlineKeyboardMarkup kb_markup = null)
        {
            Message m = await bot.SendPhotoAsync(cid, file, text, replyMarkup: kb_markup);
            mid = m.MessageId;
        }

        protected abstract Task<StateResult> onCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot);
    }
}
