using System.Collections.Generic;
using TheShinobi.Abilities;
using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    class Player : Character
    {
        public double Pos { get; set; } = 2.0;
        public int AttackBonus { get; set; }
        public Ability Chakra { get; set; }
        public Ninjutsu Ninjutsu { get; set; }        
        public List<Item> Backpack { get; set; } = new List<Item>();
        public Player(string name)
        {
            Name = name;
            Level = 1;
            Hp = new Ability(30, 30);
            Exp = new Ability(0, 200);
            Chakra = new Ability(50, 50);
            Ninjutsu = new Ninjutsu("Rasengan", "1d4", 10);
            Armor = new Shirt();
            Defence = Armor.Defence;
            Weapon = new Fists();
            Damage = Weapon.Damage;
        }

        public void LevelUp()
        {
            Level++;
            Ninjutsu.Damage = Get.DamageDice(Level);
            Ninjutsu.Cost += 10;
            Chakra.Max += 10;
            Chakra.Current = Chakra.Max;
            Exp.Max += 500 * Level;
            if (Hp.Max + Level <= 100)
            {
                Hp.Max += Level;
            }
            Hp.Current = Hp.Max;
        }

        public override int Attack(Character defender)
        {
            if (Utility.random.Next(100 + AttackBonus) >= defender.Defence)
            {
                return Utility.RollDice(Damage) + AttackBonus;
            }
            else
            {
                return 0;
            }
        }
    }
}
