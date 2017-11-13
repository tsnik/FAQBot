using System;
using System.Collections.Generic;
using System.IO;
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
    enum ChatState
    {
        NONE,
        DEFAULT,
        OTHER
    }

    class Client
    {
        public Client(long cid, TelegramBotClient bot)
        {
            this.cid = cid;
            this.bot = bot;
            this.history = new Stack<State>();
        }

        public async Task OnMessage(Message message)
        {
            if(history.Count == 0)
            {
                history.Push(new BaseState(cid));
            }
            StateResult res = await history.Peek().OnMessage(message, bot);

            if(res.action == Action.Back)
            {
                history.Pop();
                await history.Peek().Resume(bot);
                return;
            }

            if(res.action == Action.Main)
            {
                history = new Stack<State>();
                history.Push(new BaseState(cid));
                await history.Peek().OnMessage(message, bot);
                return;
            }

            if(res.state == ChatState.OTHER)
            {
                history.Push(new OtherState(cid));
                await history.Peek().OnMessage(message, bot);
                return;
            }
        }

        public async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            if (history.Count == 0)
            {
                history.Push(new BaseState(cid));
            }
            StateResult res = await history.Peek().OnCallbackQuery(callbackQuery, bot);

            if (res.action == Action.Back)
            {
                history.Pop();
                await history.Peek().Resume(bot);
                return;
            }

            if (res.action == Action.Main)
            {
                history = new Stack<State>();
                history.Push(new BaseState(cid));
                await history.Peek().OnCallbackQuery(callbackQuery, bot);
                return;
            }

            if (res.state == ChatState.OTHER)
            {
                history.Push(new OtherState(cid));
                await history.Peek().OnCallbackQuery(callbackQuery, bot);
                return;
            }
        }

        #region Private
        private long cid;
        private Stack<State> history;
        private TelegramBotClient bot;
        #endregion
    }
}
