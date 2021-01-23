using System;
using System.Collections.Generic;
using System.Text;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Structure;


namespace TheShinobi.Characters.Enemies
{
    class Kabuto : Character
    {
        public kabuto() // "Kabuto", 1, 10, 3, "1d4", 50
        {
            Name = "Kabuto";
            Level = 1;
            Hp = 10;
            Exp = 50;
            Gold = Utility.RollDice(100 * Level);
            Armor = new FlakJacket();
            Defence = Armor.Defence;
            Weapon = new ChakraBlade();
            Damage = Weapon.Damage;
        }
    }
}
