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
        public List<Item> Backpack { get; set; } = new List<Item>();
        public Player(string name)
        {
            Name = name;
            Level = 1;
            Rank = "Genin";
            Exp = new Ability(0, 200);
            Stamina = new Ability(31, 31);
            Chakra = new Ability(5, 5);
            Armor = new Shirt();
            Defence = Armor.Defence;
            Weapon = new Fists();
            Damage = Weapon.Damage;
            Ninjutsus.Add(Get.Ninjutsu(Rank));
        }

        public void LevelUp()
        {
            if (Get.NewRank(Level, out string rank))
            {
                Ninjutsus.Add(Get.Ninjutsu(rank));
            }
            Rank = rank;
            Chakra.Max += 5;
            Chakra.Current = Chakra.Max;
            Exp.Max = 250 * Level;
            if (Stamina.Max + Level <= 100)
            {
                Stamina.Max += Level;
            }
            Stamina.Current = Stamina.Max;
        }

        public void Attack(Character defender, int top, int choice)
        {
            string text = "";
            if (Utility.RollDice("1d20") + Chakra.Current >= defender.Defence)
            {
                if (choice == 1)
                {
                    text = $"You hit {defender.Name} with your {Weapon.Name}!";
                    ColorConsole.WriteSetDelayed(text, top);
                }             
            }
            else
            {
                text = "You miss!";
                ColorConsole.WriteSetDelayed(text, top);
            }
        }
    }
}
