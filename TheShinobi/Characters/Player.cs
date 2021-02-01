using System;
using System.Collections.Generic;
using TheShinobi.Abilities;
using TheShinobi.Abilities.Ninjutsus;
using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    /// <summary>
    /// Class for player characters. Some base abilities are randomized.
    /// </summary>
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
            Ryō = Utility.random.Next(200, 1000);
        }

        /// <summary>
        /// Increases the player stats when enough experience points are earned.
        /// </summary>
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

        /// <summary>
        /// <see cref="Player"/> character attempts on attacking 
        /// <see cref="Enemy"/>. Choice of action is decided by the player.
        /// </summary>
        /// <param name="defender"></param>
        /// <returns></returns>
        public override string Attack(Character defender)
        {
            int damage = 0;
            string attack = "";
            string text = "";
            while (true)
            {                
                if (int.TryParse(ColorConsole.ReadLine(), out int choice))
                {
                    if (choice == 1)
                    {
                        damage = Utility.RollDice(Weapon.Damage);
                        attack = Weapon.Name;
                        break;
                    }
                    else if (choice - 2 < Ninjutsus.Count)
                    {
                        Ninjutsu jutsu = Ninjutsus[choice - 2];
                        if (Chakra.Current >= jutsu.Cost)
                        {
                            damage = Utility.RollDice(jutsu.Damage);
                            attack = jutsu.Name;
                            Chakra.Current -= jutsu.Cost;
                            break;
                        }
                        else
                        {
                            ColorConsole.WriteOver($"\t You don't have enough chakra to use {jutsu.Name}. Try something else!", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        ColorConsole.WriteOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                    }                    
                }
                else
                {
                    ColorConsole.WriteOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                } 
            }

            if (Utility.RollDice("1d20") + Level >= defender.Defence)
            {
                text = $"You hit {defender.Name} with your {attack} dealing [Yellow]{damage}[/Yellow] damage!";
                defender.Stamina.Current -= damage;
                if (defender.Stamina.Current < 0)
                {
                    defender.Stamina.Current = 0;
                }
            }
            else
            {
                text = $"You try to hit {defender.Name} with your {attack} but you miss!";
            }
            return text;
        }
    }
}
