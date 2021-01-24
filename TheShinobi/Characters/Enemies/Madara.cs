using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Madara : Character
    {
        class Madara : Character
        {
            public Madara() // "Madara", 1, 10, 5, "1d4", 50
            {
                Name = "Madara";
                Level = 1;
                Hp = 10;
                Exp = 5;
                Gold = Utility.RollDice(100 * Level);
                Armor = new FlakJacket();
                Defence = Armor.Defence;
                Weapon = new Gunbai();
                Damage = Weapon.Damage;
            }
        }
    }
}
