using System;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;

namespace TheShinobi.Items
{
    class EnergyDrink : Item, IConsumable
    {
        public int Energy { get; set; }
        public string Text { get; set; }
        public int EnergyCtr { get; set; } = 0;

        public EnergyDrink(string name, int price, int energy, string text)
        {
            Name = name;
            Price = price;
            Energy = energy;
            Text = text;
        }

        public override string BonusText()
        {
            return $"(+{Energy * EnergyCtr} Energy)";
        }

        public void Consume(Player player)
        {
            ColorConsole.WriteOver($"\t {Text} and are energized with {Energy} energy!", ConsoleColor.Yellow);
            EnergyCtr++;
            Utility.isEnergyDrink = true;
            Utility.energyBonus = BonusText();
            player.MaxHp += Energy;
            player.Hp += Energy;
            player.Defence += Energy;
            player.AttackBonus += Energy;
        }

        public void EnergyDip(Player player)
        {
            string ending = EnergyCtr > 1 ? "s are" : " is";
            ColorConsole.WriteOver($"\t The effect of the {Name}{ending} wearing of!", ConsoleColor.Red);
            player.MaxHp -= Energy * EnergyCtr;
            player.Hp -= Energy * EnergyCtr;
            player.Defence -= Energy * EnergyCtr;
            player.AttackBonus -= Energy * EnergyCtr;
            EnergyCtr = 0;
            Utility.isEnergyDrink = false;
            Utility.energyBonus = "";
        }
    }
}
