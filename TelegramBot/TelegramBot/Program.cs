using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Program
    {
        //private static string nameBot = "@BigBoar_Bot";
        private static List<string> command = new List<string>()
        {
            "/start",
            "/help",
            "/pig",
            "/name",
            "/stat",
            "/squad"
        };

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
                    await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, $"Список команд:{command}");                  
                    break;

                case "/pig":
                    if (Squad.SquadSize < 4)
                    {
                        Pig.CreatePig();
                        //Squad.AddPigToSquad(new Pig());

                        await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, "Ваша свинья создана");
                        break;
                    }
                    else
                    {
                        await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, "Вы не можете создать больше 4 свиней");
                        break;
                    }
                case "/stat":
                    Pig.ShowPigStats(botclient, update);
                    break;
                case "/squad":
                    await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, $"Ваш отряд состоит из {Squad.SquadSize} свиней");
                    break;

                default:
                    if (Pig.Age <= 5)
                    {
                        if (commands != null && commands.StartsWith("/name"))
                        {
                            string tempName = commands.Substring(5).Trim();
                            Pig.ChangeName(botclient, update, tempName);
                        }
                    }
                    else
                    {
                        await botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, "Вы больше не можете переименовать своего хряка");
                    }
                    break;
            }
        }
    }
}