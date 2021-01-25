﻿using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using System;

namespace TheShinobi.Items.Potions
{
    class Consumable : Item
    {
        public int Health { get; set; }
        public string Text { get; set; }

        public Consumable(string name, int cost, int health, string text = "You drink a potion")
        {
            Name = name;
            Cost = cost;
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
                    ColorConsole.TypeOver($"{Text} and gain full health!", ConsoleColor.Yellow);
                }
                else
                {
                    ColorConsole.TypeOver($"{Text} and gain {Health} health!", ConsoleColor.Yellow);
                }
            }
            else
            {
                ColorConsole.TypeOver($"{Text}!", ConsoleColor.Yellow);
            }
        }

        public override string ToString()
        {
            return $"{Name} - {Cost}g (+{Health} Hp)";
        }
    }
}