using System;
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
        public List<Item> Backpack { get; set; } = new List<Item>();
        public Player(string name)
        {
            Name = name;
            Level = 1;
            Rank = "Genin";
            Exp = new Ability(0, 200);
            Stamina.Max = Utility.random.Next(28, 36);
            Stamina.Current = Stamina.Max;
            Chakra.Max = Utility.random.Next(5, 13);
            Chakra.Current = Chakra.Max;
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

        public string Attack(Character defender)
        {
            string text = "";
            while (true)
            {
                if (int.TryParse(ColorConsole.ReadLine(), out int choice))
                {
                    if (Utility.RollDice("1d20") + Chakra.Current >= defender.Defence)
                    {
                        if (choice == 1)
                        {
                            text = $"You hit {defender.Name} with your {Weapon.Name} dealing [Yellow]{Utility.RollDice(Weapon.Damage)}[/Yellow] damage!";
                            break;
                        }
                        else if (choice - 2 < Ninjutsus.Count)
                        {
                            Ninjutsu jutsu = Ninjutsus[choice - 2];
                            text = $"You hit {defender.Name} with your {jutsu.Name} dealing [Yellow]{Utility.RollDice(jutsu.Damage)}[/Yellow] damage!";
                            break;
                        }
                        else
                        {
                            ColorConsole.WriteOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        text = "You miss!";
                        break;
                    }
                }
                else
                {
                    ColorConsole.WriteOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                }                
            }
            return text;
        }
    }
}
