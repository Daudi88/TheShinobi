using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Nagato
    {
        class Nagato : Character
        {
            public Nagato()
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
