namespace TheShinobi.Items.Consumables
{
    class EnergyDrink : Consumable
    {
        public int Caffeine { get; set; }
        public EnergyDrink(string name, int cost, int health, string text, int caffeine) : base(name, cost, health, text)
        {
            Caffeine = caffeine;
        }
    }
}
