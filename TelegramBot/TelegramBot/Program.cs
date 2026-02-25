using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Program
    {
        //private static string nameBot = "@BigBoar_Bot";

        private static void Main(string[] args)
        {
            Host bot = new Host("8578937655:AAHjoyWClWbCbdVvj9_qDHkY8542a3pe6Js");
            bot.Start();
            bot.onMessage += OnMessage;
            Console.ReadKey();
        }

        private static async void OnMessage(ITelegramBotClient botclient, Update update)
        {
            string? commands = update.Message?.Text;
            switch (commands)
            {
                case "/start":
                    await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, "Вас приветствует долбоёб9000");
                    break;

                case "/help":
                    await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, "Список команд:\n/start\n/help");
                    break;

                case "/pig":
                    Pig.CreatePig();
                    await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, "Ваша свинья создана");
                    break;

                case "/stat":
                    Pig.ShowPigStats(botclient, update);
                    break;

                default:
                    if (commands != null && commands.StartsWith("/name"))
                    {
                        string tempName = commands.Substring(5).Trim();
                        Pig.ChangeName(botclient, update, tempName);
                    }
                    break;
            }
        }
    }
}