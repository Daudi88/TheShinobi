using TheShinobi.Characters;

namespace TheShinobi.Interfaces
{
    interface IEquipable
    {
        public string Name { get; set; }
        public void Equip(Player player, IEquipable item);
    }
}
