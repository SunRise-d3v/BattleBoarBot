using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot;
using TelegramBot.Utils;

namespace TelegramBot.Core
{
    public class Account
    {
        public Inventory inventory = new();
        private static int level = 1;

        public void ShowAccountStats(ITelegramBotClient botClient, Update update)
        {
            string? userName = update.Message?.From?.FirstName + update.Message?.From?.LastName;
            string? userId = update.Message?.From?.Username;
            if (userName != null)
            {
                string accountInfo = $"Аккаунт: {userName}\n" +
                                     $"Айди: @{userId}\n" +
                                     $"Уровень: {level}\n" +
                                     $"{Tools.DrawBorder()}";
                                     //$"Текущая свинья: {(Squad.boar?.isCreated == true ? Squad.boar.name : "отсутствует...")}";

                botClient.SendMessage(Tools.GetChatId(update), accountInfo);
            }
        }
    }
}
