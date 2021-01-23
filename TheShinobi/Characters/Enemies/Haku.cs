using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Haku : Character
    {
        public Haku()
        {
            Name = "Haku";
            Level = 4;
            Hp = 50;
            Exp = 50;
            Gold = Utility.RollDice(100 * Level);
            Armor = new FlackJacket();
            Defence = Armor.Defence;
            Weapon = new Crossbow
        }
    }
}
