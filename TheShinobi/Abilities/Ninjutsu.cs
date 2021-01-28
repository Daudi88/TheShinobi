namespace TheShinobi.Abilities
{
    class Ninjutsu
    {
        public string Name { get; set; }
        public string Damage { get; set; }
        public int Cost { get; set; }
        public Ninjutsu(string name, string damage, int cost)
        {
            Name = name;
            Damage = damage;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"{Name} ({Damage} Damage, -{Cost} Chakra)"; 
        }
    }
}
