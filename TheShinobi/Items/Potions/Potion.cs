using System;
using TheShinobi.Characters;

namespace TheShinobi.Items.Potions
{
    class Potion : Item
    {
        public int Health { get; set; }
        public string Text { get; set; }
        public void Consume(Player player)
        {
            Console.WriteLine($"\t {Text}");
            if (player.Hp < player.MaxHp)
            {
                player.Hp += Health;
                if (player.Hp >= player.MaxHp)
                {
                    player.Hp = player.MaxHp;
                    Console.WriteLine("\t You gained full health!");
                }
                else
                {
                    Console.WriteLine($"\t You gained {Health} health");
                }
            }
        }

        public override string ToString()
        {
            return $"{Quantity} {Name} (+{Health} Hp)";
        }
    }
}
