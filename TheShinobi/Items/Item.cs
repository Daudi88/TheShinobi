using System;
using System.Collections.Generic;
using System.Text;
using TheShinobi.Interfaces;

namespace TheShinobi.Items
{
    abstract class Item : IStackable
    {
        public string Name { get; set; }

        /// <summary>
        /// Cost in gold.
        /// </summary>
        public int Cost { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
