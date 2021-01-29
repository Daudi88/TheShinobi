using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Consumables;
using TheShinobi.Interfaces;

namespace TheShinobi.Items
{
    /// <summary>
    /// <see langword="abstract"/> class for <see cref="Armor"/>, <see cref="Weapon"/> and <see cref="Consumable"/> classes.
    /// </summary>
    abstract class Item : IStackable
    {
        public string Name { get; set; }

        /// <summary>
        /// Price in ryō.
        /// </summary>
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string IndefiniteArticle { get; set; } = "a";
        public abstract string BonusText();
    }
}
