using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;

namespace TheShinobi.Items
{
    class EnergyDrink : Item, IConsumable
    {
        public int Energy { get; set; }
        public string Text { get; set; }

        public EnergyDrink(string name, int price, int energy, string text)
        {
            Name = name;
            Price = price;
            Energy = energy;
            Text = text;
        }

        public override string BonusText()
        {
            return $"(+{Energy} Energy)";
        }

        public void Consume(Player player)
        {
            Utility.isEnergyDrink = true;
            Utility.energyBonus = BonusText();
            player.MaxHp += Energy;
            player.Hp += Energy;
            player.Defence += Energy;
            player.AttackBonus += Energy;
        }
    }
}
