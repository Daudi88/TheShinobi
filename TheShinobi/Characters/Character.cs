using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    abstract class Character
    {
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public int Hp { get; set; }
        public int Exp { get; set; } = 0;
        public int Defence { get; set; } = 0;
        public string Damage { get; set; }
        public int Gold { get; set; } = 0;
        public Armor Armor { get; set; }
        public Weapon Weapon { get; set; }

        public virtual int Attack(Character defender)
        {
            if (Utility.random.Next(1, 21) >= defender.Defence)
            {
                return Utility.RollDice(Damage);
            }
            else
            {
                return 0; // När den här metoden returnerar 0 ska det skrivas ut att attacken missade/misslyckades :)
            }
        }
    }
}
