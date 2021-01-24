using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Hanzō : Enemy
    {
        public Hanzō()
        {
            Name = "Hanzō";
            Level = 7;
            Hp = 100;
            //Exp = 100;
            Armor = new FlakJacket();
            Weapon = new Kusarigama();
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level + 1);
            Exp = Utility.random.Next(10 * Level, 40 * Level + 1);
        }
    }
}
