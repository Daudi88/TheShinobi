namespace TheShinobi.Abilities
{
    class Ninjutsu
    {
        public string Name { get; set; }
        public char Rank { get; set; }
        public string Damage { get; set; }
        public int Cost { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Damage} Damage, -{Cost} Chakra)";
        }
    }
}
