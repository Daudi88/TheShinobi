using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using TheShinobi.Abilities;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    abstract class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public Ability Stamina { get; set; } = new Ability();
        public Ability Exp { get; set; }
        public Armor Armor { get; set; }
        public Weapon Weapon { get; set; }
        public Ninjutsu Ninjutsu { get; set; }
        public string Affinity { get; set; }
        public int Defence { get; set; }
        public string Damage { get; set; }
        public int Ryō { get; set; }

        public abstract int Attack(Character defender);
    }
}
