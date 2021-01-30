using System;
using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Consumables
{
    /// <summary>
    /// Class for Consumable items that heals the player.
    /// </summary>
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
            string healthText = "";
            string chakraText = "";
            if (player.Stamina.Current < player.Stamina.Max)
            {                
                
                player.Stamina.Current += Health;
                if (player.Stamina.Current >= player.Stamina.Max)
                {
                    player.Stamina.Current = player.Stamina.Max;
                    healthText = "full health";
                }
                else
                {
                    healthText = $"{Health} health";
                }
            }
            else
            {
                healthText = "no health";
            }
            if (player.Chakra.Current < player.Chakra.Max)
            {
                player.Chakra.Current += Health;
                if (player.Chakra.Current >= player.Chakra.Max)
                {
                    player.Chakra.Current = player.Chakra.Max;
                    chakraText = "full chakra";
                }
                else
                {
                    chakraText = $"{Health} chakra";
                }
            }
            else
            {
                chakraText = "no chakra";
            }
            ColorConsole.WriteOver($"\t {Text} {Name} gaining {healthText} and {chakraText}!", ConsoleColor.Yellow);
        }

        public override string BonusText()
        {
            return $"(+{Health} Hp)";
        }
    }
}
