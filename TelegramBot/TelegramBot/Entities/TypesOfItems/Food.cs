namespace TelegramBot.Entities.TypesOfItems
{
    public class Food : Item
    {
        public int Calories { get; private set; }

        public Food(string name, string description, int calories, int id) : base(name, description, id)
        {
            Name = name;
            Description = description;

            Calories = calories;

            IsStackable = true;
            Consumable = true;

            Id = id;
        }
    }
}
