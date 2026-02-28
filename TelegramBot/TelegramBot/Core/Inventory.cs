using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Entities;
using TelegramBot.Utils;

namespace TelegramBot.Core
{
    public class Inventory
    {
        private readonly List<Item> items = new();

        private static int inventorySize = 0;
        //public int InventorySize => inventorySize;

        private static int maxItems = 24;
        //public int MaxItems => maxItems;

        public void AddItem(Item item)
        {
            if (inventorySize < maxItems)
            {
                items.Add(item);
                inventorySize++;
            }
        }

        //public IReadOnlyList<Item> Items => items;

        public string ShowInventory(ITelegramBotClient botClient, Update update)
        {
            string inventoryInfo = $"Инвентарь, {inventorySize} / {maxItems} предметов:\n" +
                                   string.Join("\n", items.Select(i => $"{i.Name} - {i.Description}"));

            botClient.SendMessage(Tools.GetChatId(update), inventoryInfo);
            return inventoryInfo;
        }
    }
}