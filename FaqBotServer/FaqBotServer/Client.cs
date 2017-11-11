using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

        public async 
        Task
OnMessage(Message message)
        {

            if (message == null || message.Type != MessageType.TextMessage) return;

            await bot.SendTextMessageAsync(message.Chat.Id, "Choose" + qid);
            qid++;
        }

        #region Private
        private long cid;
        private int qid;
        private TelegramBotClient bot;
        #endregion
    }
}
