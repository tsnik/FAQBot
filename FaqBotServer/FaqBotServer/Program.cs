using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FaqBotServer
{
    class Program
    {
        static void Main(string[] args)
        {
            bot = new TelegramBotClient(Settings.GetSettings().ApiKey);
            bot.OnMessage += BotOnMessageReceived;
            bot.OnCallbackQuery += BotOnCallbackQuery;

            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }

        private static TelegramBotClient bot;
        private static Dictionary<long, Client> clientList = new Dictionary<long, Client>();

        private static Client getClient(long cid)
        {
            Client client;
            try
            {
                client = clientList[cid];
            }
            catch (KeyNotFoundException)
            {
                client = new Client(cid, bot);
                clientList[cid] = client;
            }
            return client;
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            Client client = getClient(message.Chat.Id);
            await client.OnMessage(message);
        }

        private static async void BotOnCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            CallbackQuery callbackQuery = callbackQueryEventArgs.CallbackQuery;
            Client client = getClient(callbackQuery.Message.Chat.Id);
            await client.OnCallbackQuery(callbackQuery);
        }
    }
}
