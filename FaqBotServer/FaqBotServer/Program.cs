using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace FaqBotServer
{
    class Program
    {
        static void Main(string[] args)
        {
            bot = new TelegramBotClient(Settings.GetSettings().ApiKey);
            bot.OnMessage += BotOnMessageReceived;

            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }

        private static TelegramBotClient bot;
        private static Dictionary<long, Client> clientList = new Dictionary<long, Client>();

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            Client client;

            try
            {
                client = clientList[message.Chat.Id];
            }
            catch (KeyNotFoundException)
            {
                client = new Client(message.Chat.Id, bot);
                clientList[message.Chat.Id] = client;
            }
            await client.OnMessage(message);
        }
    }
}
