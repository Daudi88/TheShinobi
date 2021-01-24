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
            Name = "Daudi (Deidara)";
            Level = 9;
            Hp = 90;
            Exp = 200;
            Gold = Utility.RollDice(100 * Level);
            Armor = new BulletproofVest();
            Defence = Armor.Defence;
            Weapon = new AK47(); // osäker på om det är ok att använda siffror eller om det skall skrivas ut?
        }
        public override void DropItems(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
