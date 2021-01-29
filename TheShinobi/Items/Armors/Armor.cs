using System;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;

namespace TheShinobi.Items.Armors
{
    /// <summary>
    /// <see langword="abstract"/> class for all armors.
    /// </summary>
    abstract class Armor : Item, IEquipable
    {
        public int Defence { get; set; }

        public void Equip(Player player, IEquipable item)
        {
            if (player.Armor != null)
            {
                player.Armor.Quantity++;
                player.Backpack.Add(player.Armor);
            }
            ColorConsole.WriteOver($"\t You equipped {Name} with {Defence} defence!", ConsoleColor.Yellow);
            player.Armor = (Armor)item;
            player.Defence = Defence;
        }

        public override string BonusText()
        {
            return $"(+{Defence} Defence)";
        }
    }
}
