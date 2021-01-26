using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using System;

namespace TheShinobi.Items.Consumables
{
    class Consumable : Item
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
            if (player.Hp < player.MaxHp)
            {
                player.Hp += Health;
                if (player.Hp >= player.MaxHp)
                {
                    player.Hp = player.MaxHp;
                    ColorConsole.TypeOver($"\t {Text} and gain full health!", ConsoleColor.Yellow);
                }
                else
                {
                    ColorConsole.TypeOver($"\t {Text} and gain {Health} health!", ConsoleColor.Yellow);
                }
            }
            else
            {
                ColorConsole.TypeOver($"\t {Text}!", ConsoleColor.Yellow);
            }
        }

        public override string Bonus()
        {
            return $"(+{Health} Hp)";
        }
    }
}
