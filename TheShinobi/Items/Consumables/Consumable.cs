using System;
using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Consumables
{
    class Consumable : Item, IConsumable
    {
        public int Health { get; set; }
        public string Text { get; set; }

        public Consumable(string name, int price, int health, string text = "You drink a")
        {
            Name = name;
            Price = price;
            Health = health;
            Text = text;
        }

        public void Consume(Player player)
        {
            if (player.Stamina.Current < player.Stamina.Max)
            {
                player.Stamina.Current += Health;
                if (player.Stamina.Current >= player.Stamina.Max)
                {
                    player.Stamina.Current = player.Stamina.Max;
                    ColorConsole.WriteOver($"\t {Text} {Name} and gain full health!", ConsoleColor.Yellow);
                }
                else
                {
                    ColorConsole.WriteOver($"\t {Text} {Name} and gain {Health} health!", ConsoleColor.Yellow);
                }
            }
            else
            {
                ColorConsole.WriteOver($"\t {Text} {Name} but already have full health!", ConsoleColor.Yellow);
            }
        }

        public override string BonusText()
        {
            return $"(+{Health} Hp)";
        }
    }
}
