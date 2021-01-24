using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Obito : Character
    {
        class Obito : Character
        {
            public Obito() // "Obito", 1, 10, 4, "1d4", 50
            {
                Name = "Obito´s Ghost";
                Level = 2;
                Hp = 20;
                Exp = 70;
                Gold = Utility.RollDice(100 * Level);
                Armor = new FlakJacket();
                Defence = Armor.Defence;
                Weapon = new Sword();
                Damage = Weapon.Damage;
            }
        }
    }
}
