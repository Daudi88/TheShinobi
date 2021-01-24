using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Hanzo : Character
    {
        public Hanzo() // "Hanzo", 6, 80, 10, "3d8", 300
        {
            Name = "Hocke (Hanzō)";
            Level = 7;
            Hp = 100;
            Exp = 100;
            Gold = Utility.RollDice(150 * level);
            Armor = new BulletproofVest;
            Weapon = new Ak47();
            Damage = Weapon.Damage;
        }
    }
}
