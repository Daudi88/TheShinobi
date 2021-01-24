using System;
using TheShinobi.Characters;

namespace TheShinobi.Items.Potions
{
    class HealingPotion : Potion
    {
        public HealingPotion()
        {
            Name = "Healing Potion";
            Cost = 30;
            Health = 5;
            Text = "You drink a refreshing potion and feel recharged";
        }

    }
}
