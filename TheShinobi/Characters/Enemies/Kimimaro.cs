using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Kimimaro : Character
    {
        public Kimimaro() // "Kimimaro", 3, 30, 7, "2d6", 150
        {
            Name = "Kimimaro´s Ghost";
            Level = 3;
            Hp = 55;
            Exp = 15;
            Gold = Utility.RollDice(100 * Level);
            Armor = new FlakJacket();
            Defence = Armor.Defence;
            Weapon = new Fists();
            Damage = Weapon.Damage;
        }
    }
}
