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
    enum OtherStateName
    {
        Name,
        Email,
        Message,
        Review
    }
    class OtherState : State
    {
        public const string OTHER_TEXT_DEF = "Пришлите скриншот и опишите проблему, с которой Вы столкнулись, на адрес deltapro @deltacredit.ru или в этот чат";
        public const string OTHER_TEXT_ONLY = "Добавьте скриншот или нажмите отправить. Ваш текст: ";
        public const string OTHER_PHOTO_ONLY = "Добавьте комментарий, замените фото или нажмите отправить.";
        public const string OTHER_PHOTO_TEXT = "Замените фото или измените комментарий, если необходимо. Когда вы будете готовы, нажмите отправить. Ваш текст: ";
        public const string OTHER_YOUR_NAME = "Ваше имя: ";
        public const string OTHER_YOUR_EMAIL = "Ваш email: ";
        public const string OTHER_YOUR_MESSAGE = "Ваше сообщене: ";

        public OtherState(long cid) : base(cid)
        {

        }

        public override async Task<StateResult> OnMessage(Message message, TelegramBotClient bot)
        {
            if (state == OtherStateName.Message)
            {
                await createMessage(bot, message);
                return new StateResult();
            }
            if(message.Type == MessageType.TextMessage)
            {
                await getNameEmail(bot, message);
            }
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
                        return await SendEmail(bot);
                    case Button.Back:
                        return new StateResult(ChatState.NONE, Action.Back);
                    default:
                        return new StateResult();
                }
            }
            InlineKeyboardMarkup kb_markup = genKeyBoard();
            await bot.EditMessageTextAsync(cid, mid, OTHER_TEXT_DEF, replyMarkup: kb_markup);
            state = OtherStateName.Message;
            return new StateResult();
        }

        #region Private
        private OtherStateName state;
        private string otherPhoto;
        private string otherText;
        private string name;
        private string email;
        private async Task sendReview(TelegramBotClient bot)
        {
            InlineKeyboardMarkup kb_markup = genKeyBoard(true);
            string text = genReviewText();

            if (otherPhoto == null)
            {
                await bot.SendTextMessageAsync(cid, text, replyMarkup: kb_markup);
                return;
            }

            FileToSend f = new FileToSend(otherPhoto);
            await bot.SendPhotoAsync(cid, f, text, replyMarkup: kb_markup);
        }

        private string genReviewText()
        {
            string text;
            if (name != null && email != null)
            {
                text = OTHER_YOUR_NAME + name + "\n";
                text += OTHER_YOUR_EMAIL + email + "\n";
                text += OTHER_YOUR_MESSAGE + otherText + "\n";
            }
            else if (otherText != null && otherPhoto != null)
                text = OTHER_PHOTO_TEXT + otherText;
            else if (otherPhoto != null)
                text = OTHER_PHOTO_ONLY;
            else
                text = OTHER_TEXT_ONLY + otherText;
            return text;
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

        private async Task createMessage(TelegramBotClient bot, Message message)
        {
            switch (message.Type)
            {
                case MessageType.PhotoMessage:
                    int num = message.Photo.Length;
                    otherPhoto = message.Photo[num - 1].FileId;
                    break;
                case MessageType.TextMessage:
                    otherText = message.Text;
                    break;
            }
            await sendReview(bot);
        }

        private async Task getNameEmail(TelegramBotClient bot, Message message)
        {
            switch (state)
            {
                case OtherStateName.Name:
                    name = message.Text;
                    state = OtherStateName.Email;
                    await bot.SendTextMessageAsync(cid, "Ввведите email: ");
                    break;
                case OtherStateName.Email:
                    email = message.Text;
                    state = OtherStateName.Review;
                    await sendReview(bot);
                    break;
            }
        }

        private async Task<StateResult> SendEmail(TelegramBotClient bot)
        {
            if (state == OtherStateName.Message)
            {
                state = OtherStateName.Name;
                await bot.SendTextMessageAsync(cid, "Ввведите имя: ");
            }
            return new StateResult();
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
