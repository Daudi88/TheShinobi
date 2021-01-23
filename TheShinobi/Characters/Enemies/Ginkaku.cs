﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Ginkaku : Character
    {
        public Ginkaku() // "Ginkaku", 2, 20, 6, "2d4", 100
        {
            Name = "Ginkaku";
            Level = 2;
            Hp = 35;
            Exp = 10;
            Gold = Utility.RollDice(100 * Level);
            Armor = new FlakJacket();
            Defence = Armor.Defence;
            Weapon = new Shichiseiken();
            Damage = Weapon.Damage;
        }
        
        
    }
}
