using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Orochimaru : Enemy
    {
        public Orochimaru()
        {
            Name = "Orochimaru";
            Level = 5;
            Hp = 50;
            Exp = 50;
            Armor = new InfiniteArmor();
            Weapon = new Shuriken();
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level);
        }
    }
}
