namespace TelegramBot.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ItemType Type { get; set; }
        public RarityType Rarity { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public bool IsStackable { get; set; }
        public bool Consumable { get; set; }

        public Item(string name, string description, int id,
            ItemType type = ItemType.None, RarityType rarity = RarityType.Gray, int price = 0, int amount = 1, int maxAmount = 99, bool isStackable = true, bool consumable = true)
        {
            Name = name;
            Description = description;

            Id = id;
        }
    }
}