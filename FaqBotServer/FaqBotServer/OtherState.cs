using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;
using SettingsAndDB;

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
        public const string OTHER_TEXT_ONLY = "Добавьте скриншот или нажмите отправить. Ваш текст: ";
        public const string OTHER_PHOTO_ONLY = "Добавьте комментарий, замените фото или нажмите отправить.";
        public const string OTHER_PHOTO_TEXT = "Замените фото или измените комментарий, если необходимо. Когда вы будете готовы, нажмите отправить. Ваш текст: ";
        public const string OTHER_YOUR_NAME = "Имя: ";
        public const string OTHER_YOUR_EMAIL = "Email: ";
        public const string OTHER_YOUR_MESSAGE = "Cообщение: ";
        public const string OTHER_ENTER_NAME = "Введите ваше имя: ";
        public const string OTHER_ENTER_EMAIL = "Введите ваш email: ";
        public const string OTHER_SUBJECT = "Чат-бот DeltaPro";
        public const string OTHER_SUCCESS = "Письмо успешно отправлено.";
        public const string OTHER_CATEGORY = "Категория: ";

        public OtherState(long cid, long mid, object[] data) : base(cid, mid)
        {
            this.text = (string)data[0];
            if(data[1] != null)
            {
                this.history = (Stack<int>)data[1];
            }
        }

        public override async Task<StateResult> OnMessage(Message message, TelegramBotClient bot)
        {
            if (state == OtherStateName.Message)
            {
                await createMessage(bot, message);
                return new StateResult();
            }
            if (message.Type == MessageType.TextMessage)
            {
                await getNameEmail(bot, message);
            }
            return new StateResult();
        }

        protected override async Task<StateResult> onCallbackQuery(CallbackQuery callbackQuery, TelegramBotClient bot)
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
                    case Button.Main:
                        return new StateResult(ChatState.NONE, Action.Main);
                    default:
                        return new StateResult();
                }
            }
            InlineKeyboardMarkup kb_markup = genKeyBoard();
            await bot.EditMessageTextAsync(cid, mid, text, replyMarkup: kb_markup);
            state = OtherStateName.Message;
            return new StateResult();
        }

        #region Private
        private OtherStateName state;
        private string otherPhoto;
        private string otherText;
        private string name;
        private string email;
        private string text;
        private Stack<int> history;
        private async Task sendReview(TelegramBotClient bot)
        {
            InlineKeyboardMarkup kb_markup = genKeyBoard(true);
            string text = genReviewText();

            if (otherPhoto == null)
            {
                await sendMessage(bot, text, kb_markup);
                return;
            }

            FileToSend f = new FileToSend(otherPhoto);
            await sendPhoto(bot, f, text, kb_markup);
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
                    new InlineKeyboardCallbackButton(BACK, ((int)Button.Back).ToString())};

            if (send)
            {
                inlineKeyboard[0] = new InlineKeyboardButton[1] {
                    new InlineKeyboardCallbackButton(SEND, ((int)Button.Send).ToString())};
            }

            kb_markup.InlineKeyboard = inlineKeyboard;
            return kb_markup;
        }

        private InlineKeyboardMarkup genReturnMainKeyBoard()
        {
            InlineKeyboardMarkup kb_markup = new InlineKeyboardMarkup();
            InlineKeyboardButton[][] inlineKeyboard = new InlineKeyboardButton[1][];
            inlineKeyboard[0] = new InlineKeyboardButton[1]{
                    new InlineKeyboardCallbackButton(MAIN, ((int)Button.Main).ToString())};
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
                    await sendMessage(bot, OTHER_ENTER_EMAIL);
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
                await sendMessage(bot, OTHER_ENTER_NAME);
                return new StateResult();
            }

            MailAddress from = new MailAddress(Settings.GetSettings().FromEmail, name);
            MailAddress to = new MailAddress(Settings.GetSettings().SupportEmail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = OTHER_SUBJECT;
            m.Body = OTHER_YOUR_EMAIL + email + "\n";
            m.Body += OTHER_CATEGORY + genHistoryLine() + "\n";
            if (otherText != null)
                m.Body += OTHER_YOUR_MESSAGE + otherText;
            if (otherPhoto != null)
            {
                Telegram.Bot.Types.File photo = await bot.GetFileAsync(otherPhoto);
                string ext = photo.FilePath.Split('.').Last();
                m.Attachments.Add(new Attachment(photo.FileStream, "file." + ext));
            }
            m.BodyEncoding = Encoding.UTF8;
            EmailCredentials creds = Settings.GetSettings().EmailCreds;
            SmtpClient smtp = new SmtpClient(creds.ServerName, creds.Port);
            if(creds.User != null) smtp.Credentials = new NetworkCredential(creds.User, creds.Password);
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            await sendMessage(bot, OTHER_SUCCESS, genReturnMainKeyBoard());
            return new StateResult();
        }

        private string genHistoryLine()
        {
            List<Answer> l = QuestionsBase.getQuestionBase().GetElements(history.ToList());
            return string.Join("->", l.ConvertAll<string>((x => x.Title)).ToArray());
        }
        #endregion
    }
}
