using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using System;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Consumables
{
    class Consumable : Item, IConsumable
    {
        public int Health { get; set; }
        public string Text { get; set; }

        public Consumable(string name, int price, int health, string text = "You drink a potion")
        {
            Name = name;
            Price = price;
            Health = health;
            Text = text;
        }

        public void Consume(Player player)
        {
            if (player.Hp.Current < player.Hp.Max)
            {
                player.Hp.Current += Health;
                if (player.Hp.Current >= player.Hp.Max)
                {
                    player.Hp.Current = player.Hp.Max;
                    ColorConsole.WriteOver($"\t {Text} and gain full health!", ConsoleColor.Yellow);
                }
                else
                {
                    ColorConsole.WriteOver($"\t {Text} and gain {Health} health!", ConsoleColor.Yellow);
                }
            }
            else
            {
                ColorConsole.WriteOver($"\t {Text} but already have full health!", ConsoleColor.Yellow);
            }
        }

        public override string BonusText()
        {
            return $"(+{Health} Hp)";
        }
    }
}
