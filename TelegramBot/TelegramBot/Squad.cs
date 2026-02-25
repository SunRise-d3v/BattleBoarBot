namespace TelegramBot
{
    public class Squad
    {
        private static Pig[] pigSquad;
        private static int squadSize = 0;
        public static int SquadSize
        {
            get => squadSize;
        }
        private static int maxSquadSize = 4;
        public static int MaxSquadSiz
        {
            get => maxSquadSize;
        }

        /*public static void AddPigToSquad(Pig pig)
        {
            pigSquad[squadSize] = pig;
            squadSize++;
        }*/
    }
}