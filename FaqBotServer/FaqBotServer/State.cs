using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    struct StateResult
    {
        public ChatState state;
        public Action action;

        public StateResult(ChatState state = ChatState.NONE, Action action = Action.None)
        {
            this.state = state;
            this.action = action;
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
