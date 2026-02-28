using TelegramBot.Entities;
using TelegramBot.Entities.TypesOfItems;

namespace TelegramBot.Data
{
    public class ItemStorage
    {
        private static readonly Dictionary<int, Item> items = new()
        {
            { 1, new Weapon("Меч", "Обычный меч для ближнего боя", 1) },
            { 2, new Weapon("Лук", "Оружие дальнего боя", 2) }
        };

        public static Item Create(int id)
        {
            if (!items.ContainsKey(id))
                throw new Exception($"Item with id {id} not found");

            Item template = items[id];

            return new Item(template.Name, template.Description, template.Id);
        }
    }
}