using System;
using Telegram.Bot;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.IO;
using Telegram.Bot.Types.InputFiles;

namespace TestBot
{
    class MainClass
    {
        static TelegramBotClient bot;
        public static void Main(string[] args)
        {
            bot = new TelegramBotClient("532972105:AAEgUAI4R68IK5AAQYaiJV_w02yc_ehiUbQ"); //Token Telegram
            Console.WriteLine("TelegraBot started"); //Log
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync() //Gestione updates
        {
            int offset = 0;
            while (true)
            {
                var updates = await bot.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    switch (update.Type) //Gestisce update
                    {
                        case UpdateType.Message:
                            await HandleMessage(update.Message);
                            break;
                        default:
                            break;
                    }

                    offset = update.Id + 1;
                }
            }
        }

        private static async Task HandleMessage(Message message)
        {
            switch (message.Type)
            {
                case MessageType.Document:

                    using (Stream ss = new FileStream("/Users/filippo/Desktop/" + message.Document.FileName, FileMode.CreateNew))
                        await bot.GetInfoAndDownloadFileAsync(message.Document.FileId, ss); //Invia file da Telegram e salva sul PC nel percorso definito in ss
                    break;
                default:
                    break;
            }

            await bot.SendChatActionAsync(message.From.Id, ChatAction.Typing); //Intestazione di cosa sta facendo il bot - "Sta scrivendo..."
                                                                               //await Task.Delay(1500); //Aspetto 1,5 sec
            await bot.SendTextMessageAsync(message.From.Id, $"Ho ricevuto {message.Text}"); //Rispondo ad un messaggio che ricevo
            //await bot.SetChatTitleAsync(message.From.Id, "HUEEE"); //Cambia titolo gruppo se Admin
            await bot.SendTextMessageAsync(message.From.Id, "buuuu",
                                           ParseMode.Default, true, false, 0,
                                           new InlineKeyboardMarkup(new InlineKeyboardButton[] {new InlineKeyboardButton()
                { Text="Button 1", Url="http://marconirovereto.it"},new InlineKeyboardButton()
                { Text="Button 2", Url="http://fad.marconirovereto.it"}})); //Rispondo a messaggio con testo e pulsante per aprire un link (in questo caso 2 pulsanti)

            await bot.SendTextMessageAsync(message.From.Id, "Example keyboard",
                                           ParseMode.Default, true, false, 0,
                                           new ReplyKeyboardMarkup(new KeyboardButton("Keyboard 1"))); //Risponde a messaggio con testo e visualizzo una tastiera personalizzata

            await bot.SendTextMessageAsync(message.From.Id, "Example Button Keyboard",
                                           ParseMode.Default, true, false, 0,
                                           new ReplyKeyboardMarkup(new[] { KeyboardButton.WithRequestLocation("Posizione") })); //Risponde a un messaggio con test e visualizza una tastiera che permette di inviare direttamente la posizione o un contatto
            await bot.SendLocationAsync(message.From.Id, 18.203940f,11.20392f, 0, false); //Inva la posizione in base alle coordinate
            await bot.SendVenueAsync(message.From.Id, 45.891267f, 11.045128f, "Point name", "Point description"); //Pos con titolo e sottotiolo
            await bot.SendTextMessageAsync(message.From.Id, $"<b>Ho ricevuto {message.Text}</b><a href=\"http://marconirovereto.it\">Link example</a>", ParseMode.Html); //Invio messaggio formatattato in HTML con link nel testo (con preview)

            using (Stream ss = new FileStream("/Users/filippo/Desktop/test.py", FileMode.Open))
                await bot.SendDocumentAsync(message.From.Id, new InputOnlineFile(ss) { FileName = "File di prova.py" }); //Invia un file preso dal PC

            //Abdel fai inline actione query
        }
                
    }
}
