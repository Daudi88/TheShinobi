using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class KaguyaOtsutsuki : Enemy
    {
        public KaguyaOtsutsuki()
        {
            Name = "KaguyaOtsutsuki´s Ghost"; // Ghost eller inte?
            Level = 2;
            Hp = 100;
            Exp = 120;
            Armor = new FlakJacket();
            Weapon = new Fists(); // kanske skulle va Bone eller liknande som du pratade om Håkan :)
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level);
        }
    }
}
