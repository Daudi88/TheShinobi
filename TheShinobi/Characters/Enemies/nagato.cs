using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Nagato : Enemy
    {
        public Nagato()
        {
            Name = "Nagato";
            Level = 2;
            Hp = 20;
            Exp = 70;
            Armor = new FlakJacket();
            Weapon = new Sword();
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level);
        }
    }
}
