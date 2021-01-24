using System;
using TheShinobi.Characters;

namespace TheShinobi.Items.Potions
{
    class Potion : Item
    {
        public int Health { get; set; }
        public string Text { get; set; }

        public Potion(string name, int cost, int health, string text)
        {
            Name = name;
            Cost = cost;
            Health = health;
            Text = text;
        }

        public void Consume(Player player)
        {
            Console.Write($"\t {Text} ");
            if (player.Hp < player.MaxHp)
            {
                player.Hp += Health;
                if (player.Hp >= player.MaxHp)
                {
                    player.Hp = player.MaxHp;
                    Console.WriteLine("and gain full health!");
                }
                else
                {
                    Console.WriteLine($"and gain {Health} health");
                }
            }
        }

        public override string ToString()
        {
            return $"{Quantity} {Name} (+{Health} Hp)";
        }
    }
}
