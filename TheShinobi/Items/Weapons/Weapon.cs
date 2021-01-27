using System;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Weapons
{
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
            ColorConsole.TypeOver($"\t You equipped {Name} with {Damage} damage!", ConsoleColor.Yellow);
            player.Weapon = (Weapon)item;
            player.Damage = Damage;
        }

        public override string BonusText()
        {
            return $"({Damage} Damage)";
        }
    }
}
