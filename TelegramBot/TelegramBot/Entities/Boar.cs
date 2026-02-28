using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Core;
using TelegramBot.Entities.TypesOfItems;
using TelegramBot.Utils;

namespace TelegramBot.Entities
{
    public class Boar
    {
        public bool isCreated = false;
        //public static bool IsCreated => isCreated;

        public string name = "";
        //public static string Name => name;

        private List<string> nameVariation =
        [
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
        ];

        public int age;
        //public static int Age => age;
        public const int MAX_AGE = 20;

        public int weight;
        public const int MAX_WEIGHT = 310;

        public int strength; //Сила
        public int dexterity; //Ловкость
        public int constitution; //Телосложение
        public int intelligence; //Интеллект
        //public int Wisdom; //Мудрость
        public int charisma; //Харизма
        public int luck; //Удача

        public int level;
        //public static int Level => level;

        public BoarClassType classType = BoarClassType.None;
        public BoarRaceType raceType = BoarRaceType.Pig;
        public BoarMutations pigMutations = BoarMutations.None;

        public int damage;
        public int critChance;
        public int critDamage;

        public int maxHealth;
        public int health;
        public int defense;

        public int mana;
        public int maxMana;
        //public int manaRegen;

        public Boar()
        {
            var rand = new Random();
            isCreated = true;

            GetBoarStat();
            GetBoarClass();
            GetClassBuff();

            name = nameVariation[rand.Next(0, nameVariation.Count)];

            age = rand.Next(1, 3 + 1);

            if (age != 0)
                weight = rand.Next(4, 9 + 1) * age;
            else
                weight = rand.Next(4, 9 + 1);

            level = 1;

            CalculateBoarStat();
        }

        public void ChangeName(ITelegramBotClient botclient, Update update, string newName)
        {
            name = newName;
            botclient.SendMessage(Tools.GetChatId(update), $"Имя вашего хряка изменено на: {name}");
        }

        public void GetBoarClass()
        {
            BoarClassType[] boarClasses = (BoarClassType[])Enum.GetValues(typeof(BoarClassType));
            classType = boarClasses[Random.Shared.Next(boarClasses.Length)];

            if (classType == BoarClassType.None)
                classType = BoarClassType.Warrior;
        }

        public void GetBoarStat()
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

        public void CalculateBoarStat()
        {
            var rand = new Random();

            critChance = rand.Next(4, 8 + 1);
            critDamage = 120;

            if (constitution >= 2)
                defense = constitution * 3 / 2;
            else
                defense = 3;

            if (strength >= 2)
                damage = rand.Next(1, 5 + 1) * (weight * strength / 10);
            else
                damage = rand.Next(1, 5 + 1);

            if (luck >= 2)
                critChance += luck * (2 / 10);
            else
                critChance += 2;

            critDamage += luck * (5 / 10);

            maxHealth = 10 + constitution * 10 / 2;
            health = maxHealth;

            maxMana = 10 + intelligence * 10 / 2;
            mana = maxMana;
        }

        public void LevelUp()
        {
            var rand = new Random();

            level++;

            weight += rand.Next(1, 15 + 1);

            strength += 1;
            dexterity += 1;
            constitution += 1;
            intelligence += 1;
            //Wisdom += 1;
            charisma += 1;
            //luck += 1;
            CalculateBoarStat();
        }

        public void GetClassBuff()
        {
            var rand = new Random();

            if (classType == BoarClassType.Warrior)
            {
                weight += rand.Next(4, 10 + 1);
                health += 5;

                strength += 2;
                constitution += 1;
                intelligence -= 1;
                charisma -= 1;
            }
            if (classType == BoarClassType.Archer)
            {
                weight -= rand.Next(1, 3 + 1);

                health -= 5;
                defense -= 1;

                damage += rand.Next(1, 3 + 1);
                critChance += 3;
                critDamage += 30;

                strength -= 1;
                dexterity += 2;
                constitution -= 1;
                charisma += 1;
            }
            if (classType == BoarClassType.Mage)
            {
                weight -= rand.Next(1, 3 + 1);

                health -= 5;
                defense -= 3;

                damage += rand.Next(1, 7 + 1);
                critChance += 5;
                critDamage += 50;

                strength -= 2;
                dexterity -= 2;
                intelligence += 3;
                charisma += 1;
            }
        }

        public void ShowBoarStats(ITelegramBotClient botclient, Update update)
        {
            if (classType != BoarClassType.Mage)
            {
                botclient.SendMessage(Tools.GetChatId(update),
                    $"🐗Имя: {name}\n" +
                    Tools.DrawBorder() +
                    $"📅Возраст: {age}\n" +
                    $"📜Уровень: {level}\n" +
                    $"🪨Масса: {weight} кг\n" +
                    Tools.DrawBorder() +
                    $"🎓Класс: {classType}\n" +
                    $"🐷Раса: {raceType}\n" +
                    $"🦠Мутация: {pigMutations}\n" +
                    Tools.DrawBorder() +
                    $"❤️Здоровье: {maxHealth} / {health}\n" +
                    $"🛡️Защита: {defense}\n" +
                    Tools.DrawBorder() +
                    $"🗡️Урон: {damage}\n" +
                    //$"Мана: {maxMana} / {mana}\n" +
                    $"🩸Крит шанс: {critChance}%\n" +
                    $"⚔️Крит урон: {critDamage}%\n" +
                    Tools.DrawBorder() +
                    $"💪Сила: {strength}\n" +
                    $"🏹Ловкость: {dexterity}\n" +
                    $"🐖Телосложение: {constitution}\n" +
                    $"🧠Интеллект: {intelligence}\n" +
                    //$"Мудрость: {Wisdom}\n" +
                    $"🎭Харизма: {charisma}\n" +
                    $"🍀Удача: {luck}");
            }
            else
            {
                botclient.SendMessage(Tools.GetChatId(update),
                    $"🐗Имя: {name}\n" +
                    Tools.DrawBorder() +
                    $"📅Возраст: {age}\n" +
                    $"📜Уровень: {level}\n" +
                    $"🪨Масса: {weight} кг\n" +
                    Tools.DrawBorder() +
                    $"🎓Класс: {classType}\n" +
                    $"🐷Раса: {raceType}\n" +
                    $"🦠Мутация: {pigMutations}\n" +
                    Tools.DrawBorder() +
                    $"❤️Здоровье: {maxHealth} / {health}\n" +
                    $"🛡️Защита: {defense}\n" +
                    Tools.DrawBorder() +
                    $"🗡️Урон: {damage}\n" +
                    $"🧿Мана: {maxMana} / {mana}\n" +
                    $"🩸Крит шанс: {critChance}%\n" +
                    $"⚔️Крит урон: {critDamage}%\n" +
                    Tools.DrawBorder() +
                    $"💪Сила: {strength}\n" +
                    $"🏹Ловкость: {dexterity}\n" +
                    $"🐖Телосложение: {constitution}\n" +
                    $"🧠Интеллект: {intelligence}\n" +
                    //$"Мудрость: {Wisdom}\n" +
                    $"🎭Харизма: {charisma}\n" +
                    $"🍀Удача: {luck}");
            }
        }

        public void Feed(ITelegramBotClient botClient, Update update, Food food)
    => botClient.SendMessage(Tools.GetChatId(update), $"Вы съели {food.Name} и восстановили {food.Calories} калорий!");
    }
}