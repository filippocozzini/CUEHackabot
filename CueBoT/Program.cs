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
        #region CREAZIONE EVENTO

        const string MSG_ERRORE_GENERICO = "⚠️ Comando non riconosciuto \nPremi su /start per visualizzare tutta la lista di comandi disponibili";
        const string MSG_UTENTE_NON_DISPONIBILE = "L'utente sembra non utilizzi il servizio, prova con un altra persona";
        const string MSG_CREAZIONE_EVENTO_STEP1 = "🆕 Bene! Possiamo cominciare a creare l'evento. Che nome vuoi assegnare all'<b>evento</b>?";
        const string MSG_CREAZIONE_EVENTO_STEP2 = "Grazie, ora per favore forniscimi una piccola <b>descrizione</b>";
        const string MSG_CREAZIONE_EVENTO_STEP3 = "Perfetto, allegami la <b>posizione</b> dell'evento";
        const string MSG_CREAZIONE_EVENTO_STEP4 = "Ci siamo quasi! Quale sarà la <b>data di inizio</b> dell'evento:\n<i>(il formato deve essere GG/MM/YYYY)</i>";
        const string MSG_CREAZIONE_EVENTO_STEP46ERR = "⚠️ Non riesco a capire la data, per favore inseriscila nuovamente usando il formato GG/MM/YYYY";
        const string MSG_CREAZIONE_EVENTO_STEP5 = "Fantastico! Vuoi impostare anche la <b>data di fine</b> dell'evento?";
        const string MSG_CREAZIONE_EVENTO_STEP6 = "Inserisce la data di fine evento: \n<i>(il formato deve essere GG/MM/YYYY)</i>";
        const string MSG_CREAZIONE_EVENTO_STEP7 = "Bravo! Adesso, ho bisogno che imposti un <b>responsabile</b> dell'evento: \n <i>(inviamelo come contatto o scrivi la sua matricola)</i>";
        const string MSG_CREAZIONE_EVENTO_STEP8 = "Per finire, vuoi rendere questo evento <b>privato</b>?\n<i>(se privato, gli utenti non avranno accesso e non saranno informati)</i>";

        #endregion

        const string MSG_CREAZIONE_PUNTO_STEP1 = "🆕 Bene! Possiamo cominciare a creare il punto.\nChe nome vuoi assegnare al <b>punto</b>?";
        const string MSG_CREAZIONE_PUNTO_STEP2 = "Perfetto, allegami la <b>posizione</b> del punto";
        const string MSG_CREAZIONE_PUNTO_STEP3 = "Bravo! Adesso ho bisogno che imposti un <b>volontario</b> da assegnare al punto:\n<i>(inviamelo come contatto o scrivimi la sua matricola)</i>";
        const string MSG_CREAZIONE_PUNTO_STEP4 = "Ora, vuoi rendere questo punto inizialmente <b>chiuso o aperto</b>?\n<i>(potrai successivamente fornire istruzioni al volontario se aprirlo o chiuderlo)</i>";

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
                        {
                            stati.Add(new Stato { UserId = text.From.Id, State = 0 });
                            user = stati[stati.Count - 1];
                        }

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
                                await bot.SendTextMessageAsync(text.From.Id, "⚙️ <b>Per gestire il bot devi utilizzare i seguenti comandi:</b>\n• /creaevento - <i>Crea un evento</i>\n" +
                                            "• /eliminaevento - <i>Elimina un evento</i>\n" + "• /listaeventi - <i>Visualizza la lista degli eventi attivi</i>\n" +
                                            "• /listavolontari - <i>Visualizza la lista dei volontari</i>\n" + "• /aiuto - <i>Se hai bisogno di aiuto premi qui</i>", ParseMode.Html);
                                break;
                            case LivelloAuth.Responsabile:
                                await bot.SendTextMessageAsync(text.From.Id, "⚙️ <b>Per gestire il bot devi utilizzare i seguenti comandi:</b>\n• /creapunto - <i>Crea un punto di controllo</i>\n" +
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
                    {
                        stati.Add(new Stato { UserId = text.From.Id, State = 1 });
                        user = stati[stati.Count - 1];
                    }

                    var livelloAutorizzazione = GetLivelloAutorizzazione(text.From.Id);

                    switch (livelloAutorizzazione)
                    {
                        case LivelloAuth.Sindaco:
                            user.State = 201;
                            await StampaCreazioneEventoStep1(text.From.Id);
                            return;
                        case LivelloAuth.Responsabile:
                            return;
                        default:
                            await StampaErroreGenerico(text.From.Id);
                            return;
                    }
                }
                else if (text.Text.ToLower().StartsWith("/creapunto"))
                {
                    var user = stati.Find(a => a.UserId == text.From.Id);
                    if (user == null) //Non in lista
                    {
                        stati.Add(new Stato { UserId = text.From.Id, State = 1 });
                        user = stati[stati.Count - 1];
                    }

                    var livelloAutorizzazione = GetLivelloAutorizzazione(text.From.Id);

                    switch (livelloAutorizzazione)
                    {
                        case LivelloAuth.Volontario:
                            user.ObjectState = new CreaPunto();
                            (user.ObjectState as CreaPunto).EventiAssegnati = GetEventiDelResponsabile(text.From.Id);
                            user.State = 301;

                            if ((user.ObjectState as CreaPunto).EventiAssegnati.Count == 0)
                            {
                                //Non ha eventi assegnati
                                await bot.SendTextMessageAsync(text.From.Id, "⚠️ Non ho trovato nessun evento");
                                user.State = 1;
                                user.ObjectState = null;
                                return;
                            }

                            await StampaCreazionePuntoStep1(text.From.Id);
                            break;
                        case LivelloAuth.Utente:
                            break;
                        default:
                            break;
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
                        if (text.Text.ToLower().Contains("si"))
                        {
                            user.State = 206;
                            await StampaCreazioneEventoStep6(text.From.Id);
                        }
                        else if (text.Text.ToLower().Contains("no"))
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
                    else if (user.State == 207)
                    {
                        var idTelegram = 0;
                        if ((idTelegram = IsUserTelefonoRegisteredInBot(text.Text)) != -1)
                        {
                            (user.ObjectState as CreaEvento).Responsabili = new List<string>() { idTelegram.ToString() };
                            user.State = 208;
                            await StampaCreazioneEventoStep8(text.From.Id);
                        }
                        else
                        {
                            if ((idTelegram = IsUserMatricolaRegisteredInBot(text.Text)) != -1)
                            {
                                (user.ObjectState as CreaEvento).Responsabili = new List<string>() { idTelegram.ToString() };
                                user.State = 208;
                                await StampaCreazioneEventoStep8(text.From.Id);
                            }
                            else
                                await StampaUtenteNonDisponibile(text.From.Id);
                        }
                    }
                    else if(user.State == 208)
                    {
                        CreaEvento ev = user.ObjectState as CreaEvento;
                        ev.Privato = (text.Text.ToLower().Contains("si") ? true : false);
                        var lastId = GetLastRow($"INSERT INTO Eventi(id_creatore, nome, descrizione, data_inizio, data_fine, privato) VALUES ({text.From.Id}, \"{ev.Nome}\", \"{ev.Descrizione}\", \"{ev.DataOraInizio.ToString("dd-MM-yyyy")}\", \"{(ev.DataOraFine.HasValue ? ev.DataOraFine.Value.ToString("dd-MM-yyyy") : "NULL")}\", \"{ev.Privato}\"); SELECT last_insert_rowid();");
                        RunCommand($"INSERT INTO EventiResp VALUES ({ev.Responsabili[0]},{lastId});");

                        var utentiRegistrati = GetUtentiRegistrati();

                        //Notifica tutti gli utenti senza filtri
                        for (int i = 0; i < utentiRegistrati.Count; i++)
                        {
                            //TODO

                            if (utentiRegistrati[i].Key != text.From.Id)
                            {
                                switch (utentiRegistrati[i].Value)
                                {
                                    case LivelloAuth.Errore:
                                        break;
                                    case LivelloAuth.Admin:
                                        break;
                                    case LivelloAuth.Sindaco:
                                        break;
                                    case LivelloAuth.Volontario:
                                        if (utentiRegistrati[i].Key.ToString() == ev.Responsabili[0])
                                            await NotificaResp(utentiRegistrati[i].Key, $"❗️ <b>Nuovo evento segnalato</b> ❗️\n\nSei stato segnalato come <b>responsabile</b> di questo evento, se hai bisogno di informazioni utilizza il comando /aiuto\n\n<b>{ev.Nome}</b>\n\n📍{ev.Latitudine.ToString().Replace(",", ".")}, {ev.Longitudine.ToString().Replace(",", ".")}\n📅{ev.DataOraInizio.ToString("dd/MM/yyyy")} - {(ev.DataOraFine.HasValue ? ev.DataOraFine.Value.ToString("dd/MM/yyyy") : "???")}\n\n<i>{ev.Descrizione}</i>");
                                        break;
                                    case LivelloAuth.Utente:
                                        if (!ev.Privato)
                                            await Notifica(utentiRegistrati[i].Key, $"❗️ <b>Nuovo evento segnalato</b> ❗️\n\n<b>{ev.Nome}</b>\n\n📍{ev.Latitudine.ToString().Replace(",", ".")}, {ev.Longitudine.ToString().Replace(",", ".")}\n📅{ev.DataOraInizio.ToString("dd/MM/yyyy")} - {(ev.DataOraFine.HasValue ? ev.DataOraFine.Value.ToString("dd/MM/yyyy") : "???")}\n\n<i>{ev.Descrizione}</i>");
                                        break;
                                }
                            }
                        }

                        user.State = 1;
                        user.ObjectState = null;
                        GC.Collect();
                        await StampaCreazioneEventoFine(text.From.Id, ev);
                    }

                    if (user.State == 301) //creapunto
                    {
                        //Ricevuto il nome
                        (user.ObjectState as CreaPunto).Nome = text.Text;
                        user.State = 302;
                        await StampaCreazionePuntoStep2(text.From.Id);
                    }
                    else if (user.State == 303)
                    {
                        var idTelegram = 0;
                        if ((idTelegram = IsUserTelefonoRegisteredInBot(text.Text)) != -1)
                        {
                            (user.ObjectState as CreaPunto).Volontari = new List<string>() { idTelegram.ToString() };
                            user.State = 304;
                            await StampaCreazionePuntoStep4(text.From.Id);
                        }
                        else
                        {
                            if ((idTelegram = IsUserMatricolaRegisteredInBot(text.Text)) != -1)
                            {
                                (user.ObjectState as CreaPunto).Volontari = new List<string>() { idTelegram.ToString() };
                                user.State = 304;
                                await StampaCreazionePuntoStep4(text.From.Id);
                            }
                            else
                                await StampaUtenteNonDisponibile(text.From.Id);
                        }
                    }
                    else if (user.State == 304)
                    {
                        CreaPunto punto = user.ObjectState as CreaPunto;
                        punto.Aperto = (text.Text.ToLower().Contains("aperto") ? true : false);
                        if (punto.EventiAssegnati.Count > 1)
                        {
                            user.State = 305;
                            await StampaCreazionePuntoStep5(text.From.Id, punto.EventiAssegnati);
                        }
                        else
                        {
                            var oggetto = (user.ObjectState as CreaPunto);
                            var elemento = oggetto.EventiAssegnati[0];
                            RunCommand($"INSERT INTO EventiVolontari VALUES (\"{elemento.Item1}\", \"{oggetto.Volontari[0]}\", \"{text.From.Id}\", \"{oggetto.Latitudine}\", \"{oggetto.Longitudine}\", \"{oggetto.Aperto}\");");
                            await bot.SendTextMessageAsync(text.From.Id, $"Ho aggiunto il punto di controllo, qui puoi trovare il riepilogo:\n\n<b>{oggetto.Nome}</b>\n\n📍{oggetto.Latitudine.ToString().Replace(",", ".")}, {oggetto.Longitudine.ToString().Replace(",", ".")}\n{(oggetto.Aperto ? "🔓 Aperto" : "🔒 Chiuso")}\n\n<i>L'utente assegnato riceverà un notifica per informarlo</i>", ParseMode.Html);
                            user.State = 1; //OKKKK
                            user.ObjectState = null;
                            GC.Collect();
                            return;
                        }
                    }
                }
            }
        }
        private static async Task HandleContact(long id, Contact contact)
        {
            var numeroDiTelefono = contact.PhoneNumber.StartsWith("39") ? contact.PhoneNumber.Substring(2) : contact.PhoneNumber.StartsWith("+39") ? contact.PhoneNumber.Substring(3) : contact.PhoneNumber;
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
                var idTelegram = 0;
                if ((idTelegram = IsUserTelefonoRegisteredInBot(numeroDiTelefono)) != -1)
                {
                    (user.ObjectState as CreaEvento).Responsabili = new List<string>() { idTelegram.ToString() };
                    user.State = 208;
                    await StampaCreazioneEventoStep8(id);
                }
                else
                {
                    await StampaUtenteNonDisponibile(id);
                }
            }
            else if (user.State == 303)
            {
                var idTelegram = 0;
                if ((idTelegram = IsUserTelefonoRegisteredInBot(numeroDiTelefono)) != -1)
                {
                    (user.ObjectState as CreaPunto).Volontari = new List<string>() { idTelegram.ToString() };
                    user.State = 304;
                    await StampaCreazionePuntoStep4(id);
                }
                else
                {
                    await StampaUtenteNonDisponibile(id);
                }
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

                if (user.State == 302)
                {
                    (user.ObjectState as CreaPunto).Latitudine = location.Latitude;
                    (user.ObjectState as CreaPunto).Longitudine = location.Longitude;
                    user.State = 303;
                    await StampaCreazionePuntoStep3(id);
                }
            }
        }
        #endregion

        #region Gestione risposte - callbacks

        private static async Task HandleCallbackQuery(CallbackQuery callback)
        {
            var user = stati.Find(a => a.UserId == callback.From.Id);

            if (user == null) //Agli inizi
            {
                return;
            }

            if (user.State == 305)
            {
                if (int.TryParse(callback.Data, out int valore))
                {
                    await StampaCreazionePuntoStep5(callback.From.Id, (user.ObjectState as CreaPunto).EventiAssegnati, valore, true, callback.Message.MessageId);
                }
                else
                {
                    //TODO: messaggio da fare per conferma evento
                    int pos = int.Parse(callback.Data.Split('-')[1]);
                    var oggetto = (user.ObjectState as CreaPunto);
                    var elemento = oggetto.EventiAssegnati[pos];
                    RunCommand($"INSERT INTO EventiVolontari VALUES (\"{elemento.Item1}\", \"{oggetto.Volontari[0]}\", \"{callback.From.Id}\", \"{oggetto.Latitudine}\", \"{oggetto.Longitudine}\", \"{oggetto.Aperto}\");");
                    await bot.SendTextMessageAsync(callback.From.Id,$"Ho aggiunto il punto di controllo, qui puoi trovare il riepilogo:\n\n<b>{oggetto.Nome}</b>\n\n📍{oggetto.Latitudine.ToString().Replace(",",".")}, {oggetto.Longitudine.ToString().Replace(",",".")}\n{(oggetto.Aperto ? "🔓 Aperto" : "🔒 Chiuso")}\n\n<i>L'utente assegnato riceverà un notifica per informarlo</i>", ParseMode.Html);
                    user.State = 1; //OKKKK
                    user.ObjectState = null;
                    GC.Collect();
                    return;
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
        private static int IsUserTelefonoRegisteredInBot(string tel)
        {
            var sql = $"SELECT id_utente FROM Registrati WHERE tel = '{tel}' AND id_utente IS NOT NULL";
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                            return reader.GetInt32(0);
                    else
                        return -1;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                        if (reader.HasRows)
                            while (reader.Read())
                                return reader.GetInt32(0);
                        else
                            return -1;
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }

            return -2;
        }
        private static int IsUserMatricolaRegisteredInBot(string matricola)
        {
            var sql = $"SELECT id_utente FROM Registrati WHERE matricola = '{matricola}' AND id_utente IS NOT NULL";
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                            return reader.GetInt32(0);
                    else
                        return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                        if (reader.HasRows)
                            while (reader.Read())
                                return reader.GetInt32(0);
                        else
                            return -1;
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }

            return -2;
        }
        private static int GetLastRow(string sql)
        {
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                            return reader.GetInt32(0);
                    else
                        return -1;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                        if (reader.HasRows)
                            while (reader.Read())
                                return reader.GetInt32(0);
                        else
                            return -1;
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }

            return -2;
        }
        static bool GetUtenteRegistrato(long id) => IsEnabledCommand($"SELECT CASE WHEN EXISTS (SELECT * FROM Utenti WHERE Utenti.id_utente = '{id}') THEN CAST (1 AS BIT) ELSE CAST (0 AS BIT) END");
        static LivelloAuth GetLivelloAutorizzazione(long id) => (LivelloAuth)AuthLevel($"SELECT id_auth FROM Utenti, Registrati WHERE Utenti.id_utente = {id} AND Utenti.id_utente = Registrati.id_utente");
        static List<KeyValuePair<int, LivelloAuth>> GetUtentiRegistrati()
        {
            var sql = $"SELECT Utenti.id_utente, id_auth FROM Utenti, Registrati WHERE Utenti.tel = Registrati.tel";
            List<KeyValuePair<int, LivelloAuth>> listaUtenti = new List<KeyValuePair<int, LivelloAuth>>();
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        listaUtenti.Add(new KeyValuePair<int, LivelloAuth>(reader.GetInt32(0), (LivelloAuth)reader.GetInt32(1)));
                return listaUtenti;
            }
            catch (Exception ex)
            {
                listaUtenti.Clear();
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            listaUtenti.Add(new KeyValuePair<int, LivelloAuth>(reader.GetInt32(0), (LivelloAuth)reader.GetInt32(1)));
                    return listaUtenti;
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }
            return null; //Return
        }

        static List<Tuple<int, string, string>> GetEventiDelResponsabile(long id)
        {
            var sql = $"SELECT Eventi.id_evento, nome, descrizione FROM Eventi, (SELECT id_evento FROM EventiResp WHERE EventiResp.id_resp = {id}) AS EventiAssegnati WHERE Eventi.id_evento = EventiAssegnati.id_evento";
            List<Tuple<int, string, string>> listaUtenti = new List<Tuple<int, string, string>>();
            try
            {
                using (var command = new SqliteCommand(sql, dbSqlite))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        listaUtenti.Add(new Tuple<int, string, string>(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                return listaUtenti;
            }
            catch (Exception ex)
            {
                listaUtenti.Clear();
                Console.WriteLine($"Invio SQL Query fallito \"{sql}\", verrà eseguito un nuovo tentativo: {ex.Message}");
                try
                {
                    using (var command = new SqliteCommand(sql, dbSqlite))
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            listaUtenti.Add(new Tuple<int, string, string>(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    return listaUtenti;
                }
                catch (Exception ex2)
                {
                    Console.WriteLine($"Invio SQL Query fallito \"{sql}\" per la seconda volta. {ex2.Message}");
                }
            }
            return null; //Return
        }
        #endregion

        #region BotSendText helper

        #region Metodi Stampa Creazione Evento

        static async Task StampaCreazioneEventoStep1(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP1, ParseMode.Html);
        static async Task StampaCreazioneEventoStep2(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP2, ParseMode.Html);
        static async Task StampaCreazioneEventoStep3(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP3, ParseMode.Html);
        static async Task StampaCreazioneEventoStep4(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP4, ParseMode.Html);
        static async Task StampaCreazioneEventoStep46Err(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP46ERR, ParseMode.Html);
        static async Task StampaCreazioneEventoStep5(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP5,
                               ParseMode.Html, true, false, 0, new ReplyKeyboardMarkup(new[] { new KeyboardButton("Si"), new KeyboardButton("No") }, false, false));
        static async Task StampaCreazioneEventoStep6(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP6, ParseMode.Html, false, false, 0, new ReplyKeyboardRemove());
        static async Task StampaCreazioneEventoStep7(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP7, ParseMode.Html);
        static async Task StampaUtenteNonDisponibile(long id) => await bot.SendTextMessageAsync(id, MSG_UTENTE_NON_DISPONIBILE, ParseMode.Html);
        static async Task StampaCreazioneEventoStep8(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_EVENTO_STEP8,
                               ParseMode.Html, true, false, 0, new ReplyKeyboardMarkup(new[] { new KeyboardButton("Si"), new KeyboardButton("No") }, false, false));
        static async Task StampaCreazioneEventoFine(long id, CreaEvento ev) => await bot.SendTextMessageAsync(id,
                               $"Ho creato l'evento, qui puoi trovare il riepilogo:\n\n{(ev.Privato ? "🔒" : "")}<b>{ev.Nome}</b>\n\n📍{ev.Latitudine.ToString().Replace(",", ".")}, {ev.Longitudine.ToString().Replace(",", ".")}\n📅{ev.DataOraInizio.ToString("dd/MM/yyyy")} - {(ev.DataOraFine.HasValue ? ev.DataOraFine.Value.ToString("dd/MM/yyyy") : "???")}\n\n<i>{ev.Descrizione}</i>", 
                               ParseMode.Html, false, false, 0, new ReplyKeyboardRemove());

        #endregion

        #region Stampa Creazione Punto

        static async Task StampaCreazionePuntoStep1(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_PUNTO_STEP1, ParseMode.Html);
        static async Task StampaCreazionePuntoStep2(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_PUNTO_STEP2, ParseMode.Html);
        static async Task StampaCreazionePuntoStep3(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_PUNTO_STEP3, ParseMode.Html);
        static async Task StampaCreazionePuntoStep4(long id) => await bot.SendTextMessageAsync(id, MSG_CREAZIONE_PUNTO_STEP4, ParseMode.Html,
                            true, false, 0, new ReplyKeyboardMarkup(new[] { new KeyboardButton("Chiuso"), new KeyboardButton("Aperto") }, false, false));

        static async Task StampaCreazionePuntoStep5(long id, List<Tuple<int, string, string>> list, int pos = 0, bool edit = false, int messageId = 0)
        {

            var inlineKeyboard = new List<InlineKeyboardButton>();
            if (pos == 0)
            {
                inlineKeyboard.Add(new InlineKeyboardButton() { Text = "Seleziona", CallbackData = $"SelezionaEvento-{pos}" });
                inlineKeyboard.Add(new InlineKeyboardButton() { Text = "Successivo ➡️", CallbackData = $"{pos + 1}" });
            }
            else if (pos == list.Count - 1)
            {
                inlineKeyboard.Add(new InlineKeyboardButton() { Text = "⬅️ Precedente", CallbackData = $"{pos - 1}" });
                inlineKeyboard.Add(new InlineKeyboardButton() { Text = "Seleziona", CallbackData = $"SelezionaEvento-{pos}" });
            }
            else
            {
                inlineKeyboard.Add(new InlineKeyboardButton() { Text = "⬅️ Precedente", CallbackData = $"{pos - 1}" });
                inlineKeyboard.Add(new InlineKeyboardButton() { Text = "Seleziona", CallbackData = $"SelezionaEvento-{pos}" });
                inlineKeyboard.Add(new InlineKeyboardButton() { Text = "Successivo ➡️", CallbackData = $"{pos + 1}" });
            }


            try
            {
                if (edit)
                    await bot.EditMessageTextAsync(id, messageId, $"Ho trovato più eventi relativi al tuo account, per favore scegli un evento\n\n<b>{list[pos].Item2}</b>\n<i>{list[pos].Item3}</i>\n\nEvento {pos + 1} di {list.Count}",
                       ParseMode.Html, true, new InlineKeyboardMarkup(inlineKeyboard));
                else
                    await bot.SendTextMessageAsync(id, $"Ho trovato più eventi relativi al tuo account, per favore scegli un evento\n\n<b>{list[pos].Item2}</b>\n<i>{list[pos].Item3}</i>\n\nEvento {pos + 1} di {list.Count}",
                       ParseMode.Html, true, false, 0, new InlineKeyboardMarkup(inlineKeyboard));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
            }
        }

        #endregion

        static async Task StampaErroreGenerico(long id) => await bot.SendTextMessageAsync(id, MSG_ERRORE_GENERICO);

        #endregion

        #region BotHelper

        static async Task Notifica(long id, string messaggio)
        {
            try
            {
                await bot.SendTextMessageAsync(id, messaggio, ParseMode.Html, true, false, 0,
                   new InlineKeyboardMarkup(new InlineKeyboardButton[] {new InlineKeyboardButton()
                   { Text="Mi interessa!", CallbackData = "Interessato"}}));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
                //Il bot è stato bloccato male
            }
        }

        static async Task NotificaResp(long id, string messaggio)
        {
            try
            {
                await bot.SendTextMessageAsync(id, messaggio, ParseMode.Html, true, false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore: " + ex.Message);
                //Il bot è stato bloccato male
            }
        }

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

}