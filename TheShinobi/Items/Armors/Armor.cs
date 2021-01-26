using System;
using TheShinobi.Characters;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Armors
{
    abstract class Armor : Item, IEquipable
    {
        public int Defence { get; set; }

        public void Equip(Player player, IEquipable item)
        {
            if (player.Armor != null)
            {
                player.Backpack.Add(player.Armor);
            }
            Console.WriteLine($"\t You equipped {item.Name} with {Defence} defence!");
            player.Armor = (Armor)item;
            player.Defence = Defence;
        }

        public override string Bonus()
        {
            return $"({Defence} Defence)";
        }
    }
}
