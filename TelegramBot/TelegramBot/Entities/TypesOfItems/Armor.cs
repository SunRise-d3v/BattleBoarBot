namespace TelegramBot.Entities.TypesOfItems
{
    public class Armor : Item
    {
        public int Health { get; private set; }
        public int Defense { get; private set; }

        public int Strength { get; private set; }
        public int Dexterity { get; private set; }
        public int Constitution { get; private set; }
        public int Intelligence { get; private set; }
        public int Charisma { get; private set; }
        public int Luck { get; private set; }

        public int RequiredLevel { get; private set; }

        public int RequiredStrength { get; private set; }
        public int RequiredDexterity { get; private set; }
        public int RequiredConstitution { get; private set; }
        public int RequiredIntelligence { get; private set; }
        public int RequiredCharisma { get; private set; }

        public Armor(string name, string description, int id) : base(name, description, id)
        {
            Name = name;
            Description = description;

            IsStackable = false;
            Consumable = false;

            Id = id;
        }
    }
}
