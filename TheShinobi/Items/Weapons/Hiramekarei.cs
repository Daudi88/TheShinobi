using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Items.Weapons
{
    class Hiramekarei : Weapon
    {
        public Hiramekarei()
        {
            Name = "Hiramekarei";
            Price = 4000;
            Damage = "2d12";
            IndefiniteArticle = "the"; // ett av de sju svärden
        }
    }
}
