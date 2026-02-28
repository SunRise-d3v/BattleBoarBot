using TelegramBot.Entities;

namespace TelegramBot.Core
{
    public class Squad
    {
        public List<Boar> boarSquad = new();
        public const int MAX_SQUAD_SIZE = 4;
        public int squadSize = 0;

        public Boar boar;
        //public static Boar BOAR => boar;
        //public int SquadSize => boarSquad.Count;
        //public int MaxSquadSize => maxSquadSize;

        //public IReadOnlyList<Boar> Boars => boarSquad;

        public void SummonBoar()
        {
            boar = new();
            AddBoarToSquad(boar);
        }

        private Boar AddBoarToSquad(Boar boar)
        {
            this.boar = boar;
            boarSquad.Add(boar);
            squadSize++;

            return boar;
        }
    }
}