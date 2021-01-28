namespace TheShinobi.Abilities
{
    class Ability
    {
        public int Current { get; set; }
        public int Max { get; set; }
        public Ability() { }
        public Ability(int current, int max)
        {
            Current = current;
            Max = max;
        }

        public override string ToString()
        {
            return $"{Current}/{Max}";
        }
    }
}
