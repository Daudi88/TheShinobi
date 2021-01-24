﻿using System;
using System.Collections.Generic;
using System.Text;
using TheShinobi.Characters;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Weapons
{
    class Weapon : Item, IEquipable
    {
        public string Damage { get; set; }

        public void Equip(Player player, IEquipable item)
        {
            if (player.Weapon.Name != "Fists")
            {
                player.Backpack.Add(player.Weapon);
            }
            Console.WriteLine("\t You equipped {Name} with {Damage} damage!");
            player.Weapon = (Weapon)Weapon;
            player.Damage = Damage;
        }

        public override string ToString()
        {
            return $"{Name} ({Damage} Damage)";
        }
    }
}