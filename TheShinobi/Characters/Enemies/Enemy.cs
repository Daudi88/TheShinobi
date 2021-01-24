using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Enemy : Character
    {
        public Enemy(string name, int level, int hp, Armor armor, Weapon weapon)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Armor = armor;
            Weapon = weapon;
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level + 1);
            Exp = Utility.random.Next(10 * Level, 40 * Level + 1);
        }
        public void DropItems(Player player)
        {
            if (Name == "Daudi" || Name == "Hocke")
            {

            }
        }
    }
}
