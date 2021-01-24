using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Ginkaku : Enemy
    {
        public Ginkaku()
        {
            Name = "Ginkaku";
            Level = 2;
            Hp = 35;
            Exp = 10;
            Armor = new FlakJacket();
            Weapon = new Shichiseiken();
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level);
        }

    }
}
