namespace TheShinobi.Characters
{
    class Player : Character
    {
        public double Pos { get; set; } = 0.0;
        public int MaxHp { get; set; }
        public int MaxExp { get; set; }
        public List<Item> Backpack { get; set; } = new List<Item>();
        public Player()
        {
            MaxHp = MaxHp;
            MaxExp = 200 * Level;
            Weapon = new Fists();
        }
        public void LevelUp()
        {
            LevelUp++;
            MaxExp += 2++ * LevelUp;
            MaxHp += LevelUp;
            if (MaxHp > 1000)
            {
                MaxHp = 1000;
            }
            MaxHp = MaxHp;
        }
    }
}
