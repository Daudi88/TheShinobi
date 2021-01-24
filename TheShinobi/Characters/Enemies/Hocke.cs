using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Hocke : Enemy
    {
        public Hocke()
        {
            Name = "Hocke";
            Level = 9;
            Hp = 1000;
            Exp = 1000;
            Armor = new BulletproofVest();
            Weapon = new AK47();
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level);
        }

        public override void DropItems(Player player)
        {
            base.DropItems(player);
        }
    }
}
