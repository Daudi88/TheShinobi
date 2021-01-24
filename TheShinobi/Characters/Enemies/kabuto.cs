using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Kabuto : Enemy
    {
        public Kabuto() // "Kabuto", 1, 10, 3, "1d4", 50
        {
            Name = "Kabuto";
            Level = 1;
            Hp = 10;
            Exp = 50;
            Armor = new FlakJacket();
            Weapon = new ChakraBlade();
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level);
        }
    }
}
