using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Items.Potions
{
    class EnergyDrink : Consumable
    {
        public int Caffeine { get; set; }
        public EnergyDrink(string name, int cost, int health, string text, int caffeine) : base(name, cost, health, text)
        {
            Caffeine = caffeine;
        }
    }
}
