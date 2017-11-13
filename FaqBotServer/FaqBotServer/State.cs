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

        public State(long cid)
        {
            this.cid = cid;
        }

        public abstract Task<StateResult> OnMessage(Message message, TelegramBotClient bot);

        public async Task<StateResult> OnCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot)
        {
            if(callbackQuery.Message.MessageId != mid)
            {
                return new StateResult();
            }
            return await onCallbackQuery(callbackQuery, bot);
        }

        public virtual Task Resume(TelegramBotClient bot)
        {
            throw new NotImplementedException();
        }


        protected long cid;
        protected long mid;
        protected async void sendMessage(TelegramBotClient bot, string text, InlineKeyboardMarkup kb_markup = null)
        {
            Message m = await bot.SendTextMessageAsync(cid, text, replyMarkup: kb_markup);
            mid = m.MessageId;
        }
        protected abstract Task<StateResult> onCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot);
    }
}
