using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters.Enemies
{
    class Kakuzu : Character
    {
        public Kakuzu()
        {
            Name = "Kakuzu";
            Level = 5;
            Hp = 50;
            Exp = 50;
            Gold = Utility.RollDice(100 * Level);
            Armor = new FlackJacket();
            Weapon = new Sword(); // vilket är hans vapen?
            Damage = Weapon.Damage;
        }
    }
}
