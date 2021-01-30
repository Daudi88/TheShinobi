using System;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;

namespace TheShinobi.Items
{
    /// <summary>
    /// Class for energy drinks that boost the player with energy over a period.
    /// </summary>
    class EnergyDrink : Item, IConsumable
    {
        public int Energy { get; set; }
        public string Text { get; set; }
        public int EnergyCtr { get; set; }
        public bool IsEnergized { get; set; }

        public EnergyDrink() { }
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
            ColorConsole.WriteOver($"\t {Text} and are energized with {Energy} energy!", ConsoleColor.Yellow);
            player.Stamina.Max += Energy;
            player.Stamina.Current += Energy;
            player.Chakra.Max += Energy;
            player.Chakra.Current += Energy;
            player.Defence += Energy;
            player.AttackBonus += Energy;
            Utility.energyCtr++;
            Utility.energyDrink.EnergyCtr++;
            Utility.energyDrink.Name = Name;
            Utility.energyDrink.Energy += Energy;
            Utility.energyDrink.IsEnergized = true;
        }        
    }
}
