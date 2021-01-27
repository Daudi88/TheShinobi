using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Items.Weapons
{
    class Kabutowari : Weapon
    {
        public Kabutowari()
        {
            Name = "Kabutowari";
            Price = 4000;
            Damage = "2d10";
            IndefiniteArticle = "the";
        }
    }
}
