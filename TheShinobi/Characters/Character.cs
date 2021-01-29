using System.Collections.Generic;
using TheShinobi.Abilities;
using TheShinobi.Abilities.Ninjutsus;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    abstract class Character
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public int Level { get; set; }
        public Ability Stamina { get; set; } = new Ability();
        public Ability Chakra { get; set; } = new Ability();
        public Ability Exp { get; set; } = new Ability();
        public Armor Armor { get; set; }
        public Weapon Weapon { get; set; }
        public List<Ninjutsu> Ninjutsus { get; set; } = new List<Ninjutsu>();
        public string Affinity { get; set; }
        public int Defence { get; set; }
        public string Damage { get; set; }
        public int Ryō { get; set; }
        public abstract string Attack(Character defender);
    }
}
