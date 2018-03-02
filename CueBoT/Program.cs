using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;

namespace CueBoT
{
    class Program
    {
        const string MSG_ERRORE_GENERICO = "Comando non riconosciuto\nPremi su /aiuto per visualizzare la lista di comandi disponibili";
        const string MSG_CREAZIONE_EVENTO_STEP1 = "Bene! per proseguire dimmi che nome vuoi assegnare all'evento";
        const string MSG_CREAZIONE_EVENTO_STEP2 = "Grazie, ora per favore forniscimi una descrizione";
        const string MSG_CREAZIONE_EVENTO_STEP3 = "Perfetto, allegami la posizione dell'evento";
        const string MSG_CREAZIONE_EVENTO_STEP4 = "Ora inviami la data di inizio dell'evento\n(il formato deve essere GG/MM/YYYY)";
        const string MSG_CREAZIONE_EVENTO_STEP46ERR = "Non riesco a capire la data, per favore inseriscila nuovamente";
        const string MSG_CREAZIONE_EVENTO_STEP5 = "Fantastico! Vuoi impostare anche la data di fine evento?";
        const string MSG_CREAZIONE_EVENTO_STEP6 = "Ok, ora inviami la data di fine evento\n (il formato deve essere GG/MM/YYYY)";
        const string MSG_CREAZIONE_EVENTO_STEP7 = "Ora ho bisogno di impostare un responsabile dell'evento, inviamelo come contatto o scrivi la sua matricola";

        static SqliteConnection dbSqlite;
        static List<Stato> stati;
        static TelegramBotClient bot;
        public static void Main(string[] args)
        {
            bot = new TelegramBotClient("532972105:AAEgUAI4R68IK5AAQYaiJV_w02yc_ehiUbQ"); //Token Telegram

            SetupSqlite(); //Imposto e mi connetto al DB SQLite
            SettingDbSqlite(); //Creo le varie tabelle (se non esistono) e leggo il contenuto

            stati = new List<Stato>(); //Creo la lista di stati utente

            Console.WriteLine("TelegraBot started"); //Log
            MainAsync().GetAwaiter().GetResult();
        }

        //TELEGRAM BOT API
        #region Metodo principale - Scaricamento UPDATE
        private static async Task MainAsync() //Gestione updates
        {
            int offset = 0;
            while (true)
            {
                var updates = new Update[0];

                try
                {
                    updates = await bot.GetUpdatesAsync(offset);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore durante Update: {ex.Message}");
                }

                foreach (var update in updates)
                {
                    switch (update.Type)
                    {
                        case UpdateType.Unknown:
                            break;
                        case UpdateType.Message:
                            await HandleMessage(update.Message);
                            break;
                        case UpdateType.InlineQuery:
                            break;
                        case UpdateType.ChosenInlineResult:
                            break;
                        case UpdateType.CallbackQuery:
                            await HandleCallbackQuery(update.CallbackQuery);
                            break;
                        case UpdateType.EditedMessage:
                            break;
                        case UpdateType.ChannelPost:
                            break;
                        case UpdateType.EditedChannelPost:
                            break;
                        case UpdateType.ShippingQuery:
                            break;
                        case UpdateType.PreCheckoutQuery:
                            break;
                        default:
                            break;
                    }

                    offset = update.Id + 1;
                }
            }
        }
        #endregion

        #region Gestione update

        private static async Task HandleMessage(Message message)
        {
            switch (message.Type)
            {
                case MessageType.Unknown:
                    break;
                case MessageType.Text:
                    await HandleMessageText(message);
                    break;
                case MessageType.Photo:
                    break;
                case MessageType.Audio:
                    break;
                case MessageType.Video:
                    break;
                case MessageType.Voice:
                    break;
                case MessageType.Document:
                    break;
                case MessageType.Sticker:
                    break;
                case MessageType.Location:
                    await HandleLocation(message.From.Id, message.Location);
                    break;
                case MessageType.Contact:
                    await HandleContact(message.From.Id, message.Contact);
                    break;
                case MessageType.Venue:
                    break;
                case MessageType.Game:
                    break;
                case MessageType.VideoNote:
                    break;
                case MessageType.Invoice:
                    break;
                case MessageType.SuccessfulPayment:
                    break;
                case MessageType.WebsiteConnected:
                    break;
                case MessageType.ChatMembersAdded:
                    break;
                case MessageType.ChatMemberLeft:
                    break;
                case MessageType.ChatTitleChanged:
                    break;
                case MessageType.ChatPhotoChanged:
                    break;
                case MessageType.MessagePinned:
                    break;
                case MessageType.ChatPhotoDeleted:
                    break;
                case MessageType.GroupCreated:
                    break;
                case MessageType.SupergroupCreated:
                    break;
                case MessageType.ChannelCreated:
                    break;
                case MessageType.MigratedToSupergroup:
                    break;
                case MessageType.MigratedFromGroup:
                    break;
                default:
                    break;
            }
        }
        private static async Task HandleMessageText(Message text)
        {
            if (text.Text.StartsWith("/")) //Verifica comando
            {
                if (text.Text.ToLower().StartsWith("/start"))
                {
                    bool UtenteRegistrato = GetUtenteRegistrato(text.From.Id);
                    if (!UtenteRegistrato)
                    {
                        var user = stati.Find(a => a.UserId == text.From.Id);
                        if (user == null) //Non in lista
                            stati.Add(new Stato { UserId = text.From.Id, State = 0 });

                        await bot.SendTextMessageAsync(text.From.Id, "Ciao!\nSembra che sia la prima volta che stai usando questo bot! Perfavore registrati inviando il tuo numero di telefono!",
                                   ParseMode.Default, true, false, 0,
                                   new ReplyKeyboardMarkup(new[] { KeyboardButton.WithRequestContact("Invia numero di telefono") }, false, true));

                    }
                    else
                    {
                        var user = stati.Find(a => a.UserId == text.From.Id);
                        if (user == null) //Non in lista
                            stati.Add(new Stato { UserId = text.From.Id, State = 1 });
                        else
                            user.State = 1;

                        LivelloAuth livelloAutorizzazioneUtente = GetLivelloAutorizzazione(text.From.Id);

                        switch (livelloAutorizzazioneUtente)
                        {
                            case LivelloAuth.Admin:
                                //ADMIN
                                break;
                            case LivelloAuth.Sindaco:
                                await bot.SendTextMessageAsync(text.From.Id, "⚙️<b>Per gestire il bot devi utilizzare i seguenti comandi:</b>\n• /creaevento - <i>Crea un evento</i>\n" +
                                            "• /eliminaevento - <i>Elimina un evento</i>\n" + "• /listaeventi - <i>Visualizza la lista degli eventi attivi</i>\n" +
                                            "• /listavolontari - <i>Visualizza la lista dei volontari</i>\n" + "• /aiuto - <i>Se hai bisogno di aiuto premi qui</i>", ParseMode.Html);
                                break;
                            case LivelloAuth.Responsabile:
                                await bot.SendTextMessageAsync(text.From.Id, "⚙️<b>Per gestire il bot devi utilizzare i seguenti comandi:</b>\n• /creapunto - <i>Crea un punto di controllo</i>\n" +
                                            "• /eliminapunto - <i>Elimina un punto di controllo</i>\n" + "• /listapunti - <i>Visualizza la lista dei punti di controllo</i>\n" +
                                            "• /listavolontari - <i>Visualizza la lista dei volontari dell'evento</i>\n" + "• /aiuto - <i>Se hai bisogno di aiuto premi qui</i>", ParseMode.Html);
                                break;
                            case LivelloAuth.Volontario:
                                //TODO
                                break;
                            case LivelloAuth.Utente:
                                //TODO
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (text.Text.ToLower().StartsWith("/creaevento"))
                {
                    var user = stati.Find(a => a.UserId == text.From.Id);
                    if (user == null) //Non in lista
                        stati.Add(new Stato { UserId = text.From.Id, State = 1 });
                    else
                        user.State = 1;

                    var livelloAutorizzazione = GetLivelloAutorizzazione(text.From.Id);

                    switch (livelloAutorizzazione)
                    {
                        case LivelloAuth.Sindaco:
                            user.State = 201;
                            await StampaCreazioneEventoStep1(text.From.Id);
                            return;
                        default:
                            await StampaErroreGenerico(text.From.Id);
                            return;
                    }
                }
            }
            else
            {
                var user = stati.Find(a => a.UserId == text.From.Id);
                if (user == null) //Non in lista
                {
                    await StampaErroreGenerico(text.From.Id);
                    return;
                }
                else
                {
                    if (user.State == 201) //Nome /creaevento
                    {
                        if (user.ObjectState == null)
                            user.ObjectState = new CreaEvento();
                        else if (!(user.ObjectState is CreaEvento))
                            user.ObjectState = new CreaEvento();

                        //Richiesta nome già inviata
                        (user.ObjectState as CreaEvento).Nome = text.Text;
                        user.State = 202;
                        await StampaCreazioneEventoStep2(text.From.Id);
                    }
                    else if (user.State == 202) //Descrizione /creaevento
                    {
                        //Richiesta descrizione già inviata
                        (user.ObjectState as CreaEvento).Descrizione= text.Text;
                        user.State = 203;
                        await StampaCreazioneEventoStep3(text.From.Id);
                    }
                    else if(user.State == 204)
                    {
                        if (DateTime.TryParse(text.Text, out DateTime result))
                        {
                            (user.ObjectState as CreaEvento).DataOraInizio = DateTime.Parse(text.Text);
                            user.State = 205;
                            await StampaCreazioneEventoStep5(text.From.Id);
                        }
                        else
                        {
                            await StampaCreazioneEventoStep46Err(text.From.Id);
                        }
                    }
                    else if(user.State == 205)
                    {
                        if (text.Text.ToLower().Contains("Si"))
                        {
                            user.State = 206;
                            await StampaCreazioneEventoStep6(text.From.Id);
                        }
                        else if (text.Text.ToLower().Contains("No"))
                        {
                            user.State = 207;
                            await StampaCreazioneEventoStep7(text.From.Id);
                        }

                    }
                    else if (user.State == 206)
                    {
                        if (DateTime.TryParse(text.Text, out DateTime result))
                        {
                            (user.ObjectState as CreaEvento).DataOraFine = DateTime.Parse(text.Text);
                            user.State = 207;
                            await StampaCreazioneEventoStep7(text.From.Id);
                        }
                        else
                        {
                            await StampaCreazioneEventoStep46Err(text.From.Id);
                        }
                    }
                    else if(user.State == 207)
                    {
                        //Query su Matricola
                    }

                }
            }
        }
        private static async Task HandleContact(long id, Contact contact)
        {
            var numeroDiTelefono = contact.PhoneNumber.StartsWith("39") ? contact.PhoneNumber.Substring(2) : contact.PhoneNumber;
            var user = stati.Find(a => a.UserId == id);

            if (user == null) //Agli inizi
            {
                stati.Add(new Stato { UserId = id, State = 0 });
                await bot.SendTextMessageAsync(id, "Ciao!\nSembra che sia la prima volta che stai usando questo bot! Perfavore registrati inviando il tuo numero di telefono!",
                               ParseMode.Default, true, false, 0,
                               new ReplyKeyboardMarkup(new[] { KeyboardButton.WithRequestContact("Invia numero di telefono") }, false, true));
                return;
            }

            if (user.State == 0)
            {
                if (user.UserId != contact.UserId)
                {
                    await bot.SendTextMessageAsync(id, "Sembra che il numero di telefono non corrisponda al tuo account di Telegram!",
                               ParseMode.Default, true, false, 0,
                               new ReplyKeyboardMarkup(new[] { KeyboardButton.WithRequestContact("Invia numero di telefono") }, false, true));
                    return;
                }
                else
                {
                    var utenteSearchResult = GetNomeRegistrato($"SELECT nome, id_auth FROM Registrati WHERE tel=\"{numeroDiTelefono}\";"); //Temporary fix

                    switch (utenteSearchResult.Item2)
                    {
                        case -1:
                            await bot.SendTextMessageAsync(id, "Errore! Riprova più tardi.",
                               ParseMode.Default, true, false, 0,
                               new ReplyKeyboardMarkup(new[] { KeyboardButton.WithRequestContact("Invia numero di telefono") }, false, true));
                            return;
                        case 0:
                            RunCommand($"INSERT INTO Registrati VALUES (\"{numeroDiTelefono}\", {id}, \"\", \"\", \"\", \"\", \"\", \"5\");");
                            await bot.SendTextMessageAsync(id, $"Benvenuto, {contact.FirstName}!", //TODO
                               ParseMode.Default, true, false, 0);
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            RunCommand($"UPDATE Registrati SET id_utente = {id} WHERE tel = \"{numeroDiTelefono}\"");
                            await bot.SendTextMessageAsync(id, $"Benvenuto, {utenteSearchResult.Item1}!", //TODO
                               ParseMode.Default, true, false, 0);
                            break;
                    }
                    RunCommand($"INSERT INTO Utenti VALUES ({id}, {numeroDiTelefono});");

                    user.State = 1;
                    var indexUser = stati.FindIndex(a => a.UserId == id);
                    if (indexUser == -1)
                        stati.Add(user);
                }
            }
            else if(user.State == 207)
            {
                
            }
            else
            {
                await bot.SendTextMessageAsync(id, $"Comando errato.", //TODO
                               ParseMode.Default, true, false, 0);
            }
        }
        private static async Task HandleLocation(long id, Location location)
        {
            var user = stati.Find(a => a.UserId == id);
            if (user == null)
            {
                await StampaErroreGenerico(id);
                return;
            }
            else
            {
                if (user.State == 203) //Posizione /creaevento
                {
                    //Richiesta posizione già inviata
                    (user.ObjectState as CreaEvento).Latitudine = location.Latitude;
                    (user.ObjectState as CreaEvento).Longitudine = location.Longitude;
                    user.State = 204;
                    await StampaCreazioneEventoStep4(id);
                }
            }
        }
        #endregion

        #region Gestione risposte - callbacks

        private static async Task HandleCallbackQuery(CallbackQuery callback)
        {
            if (callback.Data == "DONE")
            {
                //await bot.SendTextMessageAsync(callback.From.Id, $"Callback OK");
                try
                {
                    await bot.AnswerCallbackQueryAsync(callback.Id, "PROVA", false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERRORE: {ex.Message}");
                }
            }
        }

        #endregion

        //SQLITE
        private static void SetupSqlite()
        {
            if (!System.IO.File.Exists("cue.sqlite"))
                System.IO.File.Create("cue.sqlite");

            dbSqlite = new SqliteConnection("Data Source=cue.sqlite;");
            dbSqlite.Open();
            Console.WriteLine("DB Aperto");
        }

        private static void SettingDbSqlite()
        {
            RunCommand(@"CREATE TABLE IF NOT EXISTS Auth (id_auth int(3) NOT NULL, permesso varchar(20) NOT NULL, PRIMARY KEY (id_auth));" +
/* @"INSERT INTO Auth values (1, ""ADMIN""); INSERT INTO Auth values(2, ""SINDACO""); INSERT INTO Auth values(3, ""RESPONSABILE""); INSERT INTO Auth values(4, ""VOLONTARIO""); INSERT INTO Auth values(5, ""UTENTE"");" + */
@"CREATE TABLE IF NOT EXISTS Utenti (id_utente INTEGER NOT NULL, tel varchar(15) DEFAULT NULL, PRIMARY KEY (id_utente));
CREATE TABLE IF NOT EXISTS Registrati (tel varchar(15) NOT NULL, id_utente INTEGER, nome varchar(50), cognome varchar(50), data_nascita date, matricola int(11), sesso varchar(1), id_auth int(3) DEFAULT ""5"", PRIMARY KEY (tel), FOREIGN KEY (id_auth) REFERENCES Auth(id_auth));");
        }

        #region SQL helper

        private static void RunCommand(string sql)
        {
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                    command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");

                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                        command.ExecuteNonQuery();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }
        }

        private static bool IsEnabledCommand(string sql)
        {
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        return reader.GetBoolean(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            return reader.GetBoolean(0);
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }

            return false;
        }

        private static (string, int) GetNomeRegistrato(string sql)
        {
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            return (reader.GetString(0), reader.GetInt32(1));
                    }
                    else
                    {
                        return ("", 0); //Utente nuovo
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                return (reader.GetString(0), reader.GetInt32(1));
                        }
                        else
                        {
                            return ("", 0); //Utente nuovo
                        }
                    }
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }

            return ("", -1); //Errore interno
        }

        private static int AuthLevel(string sql)
        {
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        return reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            return reader.GetInt32(0);
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }
            return -1; //Return
        }

        #endregion

        #region Simplifier

        static async Task StampaCreazioneEventoStep1(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP1);
        static async Task StampaCreazioneEventoStep2(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP2);
        static async Task StampaCreazioneEventoStep3(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP3);
        static async Task StampaCreazioneEventoStep4(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP4);
        static async Task StampaCreazioneEventoStep46Err(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP46ERR);
        static async Task StampaCreazioneEventoStep5(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP5,
                               ParseMode.Default, true, false, 0, new ReplyKeyboardMarkup(new[] { new KeyboardButton("Si"), new KeyboardButton("No") }, false, true));
        static async Task StampaCreazioneEventoStep6(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP6);
        static async Task StampaCreazioneEventoStep7(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP7);
        static async Task StampaErroreGenerico(long id) => await bot.SendTextMessageAsync(id, MSG_ERRORE_GENERICO);
        static bool GetUtenteRegistrato(long id) => IsEnabledCommand($"SELECT CASE WHEN EXISTS (SELECT * FROM Utenti WHERE Utenti.id_utente = '{id}') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END");
        static LivelloAuth GetLivelloAutorizzazione(long id) => (LivelloAuth)AuthLevel($"SELECT id_auth FROM Utenti, Registrati WHERE Utenti.id_utente = {id} AND Utenti.id_utente = Registrati.id_utente");
        
        #endregion
    }

    public class Stato
    {
        public long UserId { get; set; }
        public int State { get; set; }
        public object ObjectState { get; set; }
    }

    public enum LivelloAuth
    {
        Errore = -1,
        Admin = 1,
        Sindaco = 2,
        Responsabile = 3,
        Volontario = 4,
        Utente = 5
    }

    public class CreaEvento
    {
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public float Latitudine { get; set; }
        public float Longitudine { get; set; }
        public DateTime DataOraInizio { get; set; }
        public DateTime? DataOraFine { get; set; }
        public List<string> Responsabili { get; set; }
        public bool Privato { get; set; }
    }
}