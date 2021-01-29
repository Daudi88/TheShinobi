using TheShinobi.Items; 

namespace TheShinobi.Interfaces
{
    /// <summary>
    /// Interface for <see cref="Item"/> that can be stacked.
    /// </summary>
    interface IStackable
    {
        public int Quantity { get; set; }
    }
}
