namespace TheShinobi.Items.Potions
{
    class RedBull : Consumable
    {
        public int AttackBonus { get; set; }
        public RedBull(int quantity)
        {
            Name = "Red Bull";
            Cost = 50;
            Health = 20;
            Text = "You get wings";
            AttackBonus = 10;
            Quantity = quantity;
        }
    }
}
