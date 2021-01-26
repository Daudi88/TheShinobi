using TheShinobi.Interfaces;

namespace TheShinobi.Items
{
    abstract class Item : IStackable
    {
        public string Name { get; set; }

        /// <summary>
        /// Cost in gold.
        /// </summary>
        public int Price { get; set; }
        public int Quantity { get; set; } = 1;
        public string IndefiniteArticle { get; set; } = "a";
        public abstract string BonusText();
    }
}
