using System;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CleaningBot
{
    class Program
    { 
        private static List<Dictionary<string, string>> weeklySchedule = new List<Dictionary<string, string>>
        {
            new Dictionary<string, string>
            {
                {"Туалет", "Мария"}, {"Ванная" , "Даша"}, {"Кухня","Жасмин"}, {"Коридор","Вика"}
            },
            new Dictionary<string, string>
            {
                {"Туалет", "Варя"}, {"Ванная" , "Аник"}, {"Кухня","Ксюша"}, {"Коридор","Шанси"}
            },
        };
        static void Main(string[] args) 
        {
            var client = new TelegramBotClient("***");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }
        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message != null) 
            {
                if(message.Text.ToLower() == "/start")
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "Че надо, напиши /schedule");
                }
                else if (message.Text.ToLower() == "/schedule")
                {
                    string schMessage = "";
                    
                    foreach (var t in weeklySchedule[0])
                    {
                        schMessage += $"{t.Key} - {t.Value}\n";
                    }

                    await client.SendTextMessageAsync(message.Chat.Id, schMessage);
                }
                else
                {
                    await client.SendTextMessageAsync(message.Chat.Id, "тут пока только два варианта");
                }
            }

        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}