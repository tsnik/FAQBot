using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

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
        public State(long cid)
        {
            this.cid = cid;
        }

        public abstract Task<StateResult> OnMessage(Message message, TelegramBotClient bot);

        public abstract Task<StateResult> OnCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot);

        public virtual Task Resume(TelegramBotClient bot)
        {
            throw new NotImplementedException();
        }

        protected long cid;
    }
}
