using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Characters
{
    abstract class Enemy : Character 
    {
        public abstract void DropItems(Player player);
    }
}
