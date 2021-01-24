using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Orochimaru : Character
    {
        public Orochimaru()
        {
            Name = "Orochimaru";
            Level = 5;
            Hp = 50;
            Exp = 50;
            Gold = Utility.RollDice(100 * Level);
            Armor = new InfiniteArmor();
            Defence = Armor.Defence;
            Weapon = new Shuriken;
            Damage = Weapon.Damage;
        }
    }
}
