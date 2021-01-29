using TheShinobi.Items;
using TheShinobi.Characters;

namespace TheShinobi.Interfaces
{
    /// <summary>
    /// Interface for <see cref="Item"/> that can be consumed.
    /// </summary>
    interface IConsumable
    {
        public string Text { get; set; }
        public void Consume(Player player);
    }
}
