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
    class OtherState : State
    {
        public const string OTHER_TEXT_DEF = "Пришлите скриншот и опишите проблему, с которой Вы столкнулись, на адрес deltapro @deltacredit.ru или в этот чат";
        public const string OTHER_TEXT_ONLY = "Добавьте скриншот или нажмите отправить. Ваш текст: ";
        public const string OTHER_PHOTO_ONLY = "Добавьте комментарий, замените фото или нажмите отправить.";
        public const string OTHER_PHOTO_TEXT = "Замените фото или измените комментарий, если необходимо. Когда вы будете готовы, нажмите отправить. Ваш текст: ";

        public OtherState(long cid) : base(cid)
        {

        }

        public override async Task<StateResult> OnMessage(Message message, TelegramBotClient bot)
        {
            if (message.Type == MessageType.PhotoMessage)
            {
                int num = message.Photo.Length;
                otherPhoto = message.Photo[num - 1].FileId;
            }
            else if (message.Type == MessageType.TextMessage)
            {
                otherText = message.Text;
            }
            else
            {
                return new StateResult();
            }
            await other(bot);
            return new StateResult();
        }

        public override async Task<StateResult> OnCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot)
        {
            int id = int.Parse(callbackQuery.Data);
            int mid = callbackQuery.Message.MessageId;
            if (id < 0)
            {
                switch ((Button)id)
                {
                    case Button.Send:
                        await SendEmail();
                        return new StateResult(action: Action.Main);
                    case Button.Back:
                        return new StateResult(ChatState.NONE, Action.Back);
                    default:
                        return new StateResult();
                }
            }
            InlineKeyboardMarkup kb_markup = genKeyBoard();
            await bot.EditMessageTextAsync(cid, mid, OTHER_TEXT_DEF, replyMarkup: kb_markup);
            return new StateResult();
        }

        #region Private
        private ChatState state = ChatState.DEFAULT;
        private string otherPhoto;
        private string otherText;
        private async Task other(TelegramBotClient bot)
        {
            InlineKeyboardMarkup kb_markup = genKeyBoard(true);

            if (otherPhoto == null)
            {
                await bot.SendTextMessageAsync(cid, OTHER_TEXT_ONLY + otherText, replyMarkup: kb_markup);
                return;
            }

            string text = OTHER_PHOTO_ONLY;
            if (otherText != null)
            {
                text = OTHER_PHOTO_TEXT + otherText;
            }
            FileToSend f = new FileToSend(otherPhoto);
            await bot.SendPhotoAsync(cid, f, text, replyMarkup: kb_markup);
            return;
        }

        private InlineKeyboardMarkup genKeyBoard(bool send = false)
        {
            InlineKeyboardMarkup kb_markup = new InlineKeyboardMarkup();
            InlineKeyboardButton[][] inlineKeyboard =
                new InlineKeyboardButton[1 + (send ? 1 : 0)][];

            inlineKeyboard[inlineKeyboard.Length - 1] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton("Назад", ((int)Button.Back).ToString())};

            if (send)
            {
                inlineKeyboard[0] = new InlineKeyboardButton[1] {
                    new InlineKeyboardCallbackButton("Отправить", ((int)Button.Send).ToString())};
            }

            kb_markup.InlineKeyboard = inlineKeyboard;
            return kb_markup;
        }

        private async Task SendEmail()
        {
            //FileStream stream = new FileStream("test.jpg", FileMode.Create);
            //        int num = message.Photo.Length - 1;
            //        /*await bot.GetFileAsync(message.Photo[num].FileId, stream);
            //        stream.Flush();
            //        stream.Close();*/
            //        otherPhoto = message.Photo[num].FileId;
        }
        #endregion
    }
}
