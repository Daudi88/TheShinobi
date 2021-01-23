using System;
using System.Collections.Generic;
using System.Text;
using TheShinobi.Characters;

namespace TheShinobi.Enemies
{
    class Deidara : Enemy
    {
        public Deidara()
        {
            Name = "Deidara";
            Level = 4;
            Hp = 40;
            Exp = 80;
            Gold = Utility.RollDice(100 * Level);
            Armor = new FlakJacket;
            Defence = Armor.Defence;
            Weapon = new Fist();
        }
        public override void DropItems(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
