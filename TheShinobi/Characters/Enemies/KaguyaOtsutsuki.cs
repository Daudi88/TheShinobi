using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class KaguyaOtsutsuki : Character
    {
        public KaguyaOtsutsuki()
        {
            Name = "KaguyaOtsutsuki´s Ghost";
            Level = 2;
            Hp = 100;
            Exp = 120;
            Gold = Utility.RollDice(100 * Level);
            Armor = new FlakJacket();
            Defence = Armor.Defence;
            Weapon = new Fists();
            Damage = Weapon.Damage;
        }
    }
}
