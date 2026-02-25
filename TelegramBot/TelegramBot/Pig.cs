using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class Pig
    {
        private static string name = "";

        private static string[] nameVariation =
        {
            "Кровохрюк",
            "Бивнерез",
            "Хрякобой",
            "Железный Клык",
            "Брутал Хряк",
            "Мясорез",
            "Череполом",
            "Кабанор",
            "Громохрюк",
            "Стальной Пятач",
            "Вепрь Судьбы",
            "Резня",
            "Клык Тьмы",
            "Хряк-Палач",
            "Бронехряк",
            "Кабанья Ярость",
            "Красный Вепрь",
            "Костолом",
            "Хрякомет",
            "Чёрный Кабан",
            "Клык Войны",
            "Гнилоклык",
            "Хряк Разрушитель",
            "Вепрь Апокалипсиса",
            "Последний Хрюк"
        };

        private static int age;
        private const int MAX_AGE = 20;

        private static int weight;
        private const int MAX_WEIGHT = 270;

        private static int strength; //Сила
        private static int dexterity; //Ловкость
        private static int constitution; //Телосложение
        private static int intelligence; //Интеллект
        //private static int Wisdom; //Мудрость
        private static int charisma; //Харизма
        private static int luck; //Удача

        private static int level;
        private static PigClassType classType = PigClassType.None;
        private static PigRaceType raceType = PigRaceType.Pig;
        private static PigMutations pigMutations = PigMutations.None;

        private static int damage;
        private static int critChance;
        private static int critDamage;

        private static int maxHealth;
        private static int health;
        private static int defense;

        public static void CreatePig()
        {
            var rand = new Random();

            for (int i = 0; i < nameVariation.Length; i++)
                name = nameVariation[i];

            age = rand.Next(1, 3 + 1);

            if (age != 0)
                weight = rand.Next(4, 9 + 1) * age;
            else
                weight = rand.Next(4, 9 + 1);

            level = 1;
            GetPigClass();

            critChance = rand.Next(4, 8 + 1);
            critDamage = 120;

            GetPigStat();

            if (constitution >= 2)
                defense = constitution * 3 / 2;
            else
                defense = 3;

            if (strength >= 2)
                damage = rand.Next(1, 5 + 1) * ((weight + strength) / 10);
            else
                damage = rand.Next(1, 5 + 1);

            maxHealth = 20 + (constitution * 10) / 2;
            health = maxHealth;

            if (classType == PigClassType.Mage)
            {

            }
        }

        public static void ChangeName(ITelegramBotClient botclient, Update update, string newName)
        {
            name = newName;
            botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893, $"Имя вашей свиньи изменено на: {name}");
        }

        public static void GetPigClass()
        {
            PigClassType[] pigClasses = (PigClassType[])System.Enum.GetValues(typeof(PigClassType));
            classType = pigClasses[Random.Shared.Next(pigClasses.Length)];

            if (classType == PigClassType.None)
                classType = PigClassType.Warrior;
        }

        public static void GetPigStat()
        {
            var rand = new Random();
            strength = rand.Next(1, 4 + 1);
            dexterity = rand.Next(1, 4 + 1);
            constitution = rand.Next(1, 4 + 1);
            intelligence = rand.Next(1, 4 + 1);
            //Wisdom = rand.Next(1, 4 + 1);
            charisma = rand.Next(1, 4 + 1);
            luck = rand.Next(1, 2 + 1);
        }

        public static string DrawBorder()
        {
            return "══════════════════════════════════\n";
        }

        public static void ShowPigStats(ITelegramBotClient botclient, Update update)
        {
            botclient.SendMessage(update.Message?.Chat.Id ?? 1510162893,
                $"Имя: {name}\n" +
                DrawBorder() +
                $"Возраст: {age}\n" +
                $"Уровень: {level}\n" +
                $"Масса: {weight} кг\n" +
                DrawBorder() +
                $"Класс: {classType}\n" +
                $"Раса: {raceType}\n" +
                $"Мутация: {pigMutations}\n" +
                DrawBorder() +
                $"Здоровье: {maxHealth} / {health}\n" +
                $"Защита: {defense}\n" +
                DrawBorder() +
                $"Урон: {damage}\n" +
                $"Крит шанс: {critChance}%\n" +
                $"Крит урон: {critDamage}%\n" +
                DrawBorder() +
                $"Сила: {strength}\n" +
                $"Ловкость: {dexterity}\n" +
                $"Телосложение: {constitution}\n" +
                $"Интеллект: {intelligence}\n" +
                //$"Мудрость: {Wisdom}\n" +
                $"Харизма: {charisma}\n" +
                $"Удача: {luck}");
        }
    }
}