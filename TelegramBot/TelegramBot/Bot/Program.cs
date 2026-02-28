using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core;
using TelegramBot.Utils;

namespace TelegramBot.Bot
{
    public static class Program
    {
        public static Squad? squad = new();
        public static Account? account;

        private static bool isCreated = false;

        //private static string nameBot = "@BigBoar_Bot";
        private static Dictionary<string, string> command = new()
        {
            { "/start", "Запуск бота" },
            { "/help", "Список команд" },
            { "/boar", "Информация о свинье" },
            { "/name", "Сменить имя свиньи" },
            { "/stat", "Характеристики" },
            { "/squad", "Отряд" },
            { "/account", "Аккаунт"},
            { "/inventory", "Инвентарь"}
        };

        private static void Main(string[] args)
        {
            Host bot = new("8578937655:AAHjoyWClWbCbdVvj9_qDHkY8542a3pe6Js");
            bot.Start();
            bot.onMessage += OnMessage;
            Console.ReadKey();
        }

        private static async void OnMessage(ITelegramBotClient botClient, Update update)
        {
            var rand = new Random();
            long chatId = Tools.GetChatId(update);

            string? commands = update.Message?.Text?.ToLower();
            switch (commands)
            {
                case "/start":
                    await botClient.SendMessage(chatId, "Вас приветствует боевой хряк, что бы начать вам нужно вызвать хряка, коммандой /boar");
                    break;

                case "/help":
                    var text = "Список команд:\n" +
                        string.Join("\n", command.Select(c => $"{c.Key} — {c.Value}"));

                    await botClient.SendMessage(chatId, text);
                    break;

                case "/boar":
                    if (squad?.squadSize < Squad.MAX_SQUAD_SIZE)
                    {
                        squad?.SummonBoar();
                        //squad?.boar = new();
                        squad?.boar.isCreated = true;
                        isCreated = true;
                        await botClient.SendMessage(chatId, $"Вы призвали хряка: {squad?.boar.name}");
                        break;
                    }
                    else
                    {
                        await botClient.SendMessage(chatId, $"Вы не можете иметь больше {Squad.MAX_SQUAD_SIZE} хряков");
                        break;
                    }
                /*else
                {
                    await botClient.SendMessage(chatId, $"У вас уже есть хряк: {squad?.boar.name}");
                    break;
                }*/

                case "/squad":
                    if (squad?.squadSize == 0)
                    {
                        await botClient.SendMessage(chatId, "Ваш отряд пуст");
                        break;
                    }
                    else
                    {
                        await botClient.SendMessage(
                            chatId,
                            $"Ваш отряд состоит из {squad?.squadSize} / {Squad.MAX_SQUAD_SIZE} хряков:\n" +
                            string.Join("\n", squad?.boarSquad.Select((b, i) => $"{i + 1}. {b.name}"))
                        );
                        break;
                    }

                case "/account":
                    account = new();
                    account?.ShowAccountStats(botClient, update);
                    break;

                case "/inventory":
                    account?.inventory.ShowInventory(botClient, update);
                    break;

                case "/give":
                    //Account.inventory.AddItem(ItemStorage.Create(rand.Next(3)));
                    await botClient.SendMessage(chatId, $"Вы получили новый предмет!");
                    break;

                case "/levelup":
                    if (squad.boar.isCreated)
                    {
                        squad?.boar.LevelUp();
                        await botClient.SendMessage(chatId, $"Ваш хряк достиг {squad?.boar?.level} уровня!");
                    }
                    break;

                /*case "/feed":
                    var food = new Food("Нагетс", "Жареный, очень вкусный!", 100, 3);
                    if (Boar.IsCreated)
                        Boar.Feed(botClient, update, food);
                    break;*/

                default:
                    if (isCreated)
                    {
                        if (squad.boar.isCreated)
                        {
                            if (squad?.boar.age <= 5)
                            {
                                if (commands != null && commands.StartsWith("/name"))
                                {
                                    string tempName = commands.Substring(5).Trim();
                                    byte nameMaxChar = 12;

                                    if (string.IsNullOrWhiteSpace(tempName))
                                        await botClient.SendMessage(chatId, "Имя не может быть пустым");
                                    else if (tempName.Length <= nameMaxChar)
                                        squad?.boar?.ChangeName(botClient, update, tempName);
                                    else
                                        await botClient.SendMessage(chatId, $"Имя не может быть длиннее {nameMaxChar} символов");
                                }
                            }
                        }
                        else if (squad?.boar.age > 5)
                            await botClient.SendMessage(chatId, "Вы больше не можете переименовать своего хряка");
                    }
                    else
                    {
                        await NullPigError(botClient, update);
                        return;
                    }

                    if (commands != null && commands.StartsWith("/stat"))
                    {
                        string name = commands.Substring(5).Trim();

                        /*if (Squad.SquadSize == 0)
                        {
                            await botClient.SendMessage(chatId, "Ваш отряд пуст");
                        }*/
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            await botClient.SendMessage(chatId, "Использование: /stat Имя");
                        }
                        else if (name.ToLower() == squad?.boar.name.ToLower())
                        {
                            squad?.boar.ShowBoarStats(botClient, update);
                        }
                        else
                        {
                            await botClient.SendMessage(chatId,
                                $"У вас нет хряка с именем {name}");
                        }
                    }

                    break;
            }
        }

        public static async Task NullPigError(ITelegramBotClient botclient, Update update)
           => await botclient.SendMessage(Tools.GetChatId(update), "У вас нет хряка, создайте еего командой /boar");
    }
}