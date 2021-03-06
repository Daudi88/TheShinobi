﻿using System;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Weapons
{
    /// <summary>
    /// <see langword="abstract"/> class for all weapons.
    /// </summary>
    class Weapon : Item, IEquipable
    {
        public string Damage { get; set; }
        

        public void Equip(Player player, IEquipable item)
        {
            if (!(player.Weapon is Fists))
            {
                player.Weapon.Quantity++;
                player.Backpack.Add(player.Weapon);
            }
            ColorConsole.WriteOver($"\t You equipped {Name} with {Damage} damage!", ConsoleColor.Yellow);
            player.Weapon = (Weapon)item;
            player.Damage = Damage;
        }

        public override string BonusText()
        {
            return $"({Damage} Damage)";
        }
    }
}
