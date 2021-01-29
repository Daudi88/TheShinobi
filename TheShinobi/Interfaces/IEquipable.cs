using TheShinobi.Items;
using TheShinobi.Characters;

namespace TheShinobi.Interfaces
{
    /// <summary>
    /// Inteface for <see cref="Item"/> that can be equipped.
    /// </summary>
    interface IEquipable
    {
        public string Name { get; set; }
        public void Equip(Player player, IEquipable item);
    }
}
