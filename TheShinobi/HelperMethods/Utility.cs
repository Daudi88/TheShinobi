using TheShinobi.Characters.Enemies;
using TheShinobi.Characters;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Potions;
using TheShinobi.Items;
using System.Collections.Generic;
using System;

namespace TheShinobi.HelperMethods
{
    static class Utility
    {
        public const int left = 9;
        public static readonly Random random = new Random();
        public static bool isCaffeine = false;

        public static int RollDice(string dice)
        {
            int times = int.Parse(dice[0].ToString());
            int sides = int.Parse(dice[2..]);
            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += random.Next(1, sides + 1);
            }
            return result;
        }

        public static void Shop(Player player, string name, Item[] items, bool eat = false)
        {
            while (true)
            {
                int top = Console.CursorTop;
                Console.WriteLine($"\t What {name.ToLower()} do you want to buy?");
                List<string> options = new List<string>();
                int ctr = 1;
                foreach (var item in items)
                {
                    options.Add($"{ctr++}. {item}");
                }
                Display.WithFrame(options, $"[Yellow]{name.ToUpper()}S[/Yellow]", ending: "Go back to shop menu");
                int bottom = Console.CursorTop;
                if (MakeAChoice(items.Length, out int choice))
                {
                    BuyItem(player, items[choice - 1], eat);
                }
                else
                {
                    Remove(top, bottom);
                    break;
                }
            }
        }

        public static bool MakeAChoice(int length, out int choice, Player player = null, bool std = false, bool ending = false) // Implementera BDM
        {
            bool result = false;
            while (true)
            {
                string input = ColorConsole.ReadLine();
                int.TryParse(input, out choice);
                if (choice > 0 && choice <= length)
                {
                    result = true;
                    break;
                }
                else if (ending && input.ToUpper() == "E")
                {
                    break;
                }
                else if (std && player != null && input.ToUpper() == "B")
                {
                    if (Display.Backpack(player))
                    {
                        result = true;
                        break;
                    }
                }
                else if (std && player != null && input.ToUpper() == "D")
                {
                    Display.Details(player);
                    result = true;
                    break;
                }
                else if (std && player != null && input.ToUpper() == "M")
                {
                    Display.Map(player);
                    result = true;
                    break;
                }
                else if (choice == 0)
                {
                    ColorConsole.TypeOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                }
            }
            return result;
        }

        public static void BuyItem(Player player, Item item, bool eat = false)
        {
            if (player.Gold >= item.Cost)
            {
                if (eat && item is Consumable meal)
                {
                    meal.Consume(player);
                }
                else
                {
                    AddToBackpack(player, item);
                }
            }
            else
            {
                ColorConsole.TypeOver("\t You don't have enough gold.", ConsoleColor.Red);
            }
        }
        
        public static void AddToBackpack(Player player, Item item)
        {
            
            throw new NotImplementedException();
        }

        public static void SellItems(Player player)
        {
            throw new NotImplementedException();
        }

        public static void Remove(int top, int bottom)
        {
            int ctr = top;
            for (int i = 0; i <= bottom - top; i++)
            {
                Console.SetCursorPosition(0, ctr++);
                for (int j = 0; j < 100; j++)
                {
                    Console.Write(" ");
                }
            }
            Console.SetCursorPosition(left, top);
        }

        public static Enemy[] GetEnemies()
        {
            Enemy[] enemies = new Enemy[]
            {
                new Enemy("Sakon", 1, 30, new FlakJacket(), new Kiba()),
                new Enemy("Tayuya", 1, 30, new Shirt(), new Fists()),
                new Enemy("Ukon", 2, 32, new Shirt(), new Kusarigama()),
                new Enemy("Sasori", 2, 32, new Shirt(), new TekagiShuko()),
                new Enemy("Konan", 3, 35, new FlakJacket(), new Kubikiribōchō()),
                new Enemy("Nagato", 3, 35, new SteamArmor(), new Kunai()),
                new Enemy("Haku", 4, 39, new FlakJacket(), new Shuriken()),
                new Enemy("Obito", 4, 39, new ChakraArmor(), new Gunbai()),
                new Enemy("Kaguya", 5, 44, new FlakJacket(), new FistsOfBones()),
                new Enemy("Ginkaku", 5, 44, new SteamArmor(), new Shichiseiken()),
                new Enemy("Madara", 6, 50, new InfiniteArmor(), new Gunbai()),
                new Enemy("Hanzō", 6, 50, new InfiniteArmor(), new Kusarigama()),
                new Enemy("Deidara", 7, 57, new SteamArmor(), new Shuriken()),
                new Enemy("Kimimaro", 7, 57, new ChakraArmor(), new FistsOfBones()),
                new Enemy("Kabuto", 7, 57, new SteamArmor(), new Shichiseiken()),
                new Enemy("Kisame", 8, 65, new FlakJacket(), new Kunai()),
                new Enemy("Kakuzu", 8, 65, new InfiniteArmor(), new Kusarigama()),
                new Enemy("Hocke", 8, 70, new BulletproofVest(), new AK47()),
                new Enemy("Daudi", 9, 74, new BulletproofVest(), new AK47()),

            };
            return enemies;
        }

        public static Armor[] GetArmors()
        {
            Armor[] armors = new Armor[]
            {
                new Shirt(),
                new FlakJacket(),
                new SteamArmor(),
                new ShinobiBattleArmor(),
                new ChakraArmor(),
                new InfiniteArmor()
            };
            return armors;
        }

        public static Weapon[] GetWeapons()
        {
            Weapon[] weapons = new Weapon[]
            {
                new Kunai(),
                new Shuriken(),
                new TekagiShuko(),
                new ChakraBlade(),
                new Gunbai(),
                new Kusarigama(),
                new Shichiseiken()

            };
            return weapons;
        }
        public static Weapon[] GetSevenSwords()
        {
            Weapon[] weapons = new Weapon[]
            {
                new Kabutowari(),
                new Hiramekarei(),
                new Kubikiribōchō(),
                new Samehada(),
                new Kiba(),
                new Nuibari(),
                new Shibuki()
            };
            return weapons;
        }
        public static Consumable[] GetPotions()
        {
            Consumable[] potions = new Consumable[]
            {
                new Consumable("Lesser Healing Potion", 15, 5),
                new Consumable("Healing Potion", 20, 8),
                new Consumable("Greater Healing Potion", 25, 10),
                new Consumable("Superior Healing Potion", 30, 12)
            };
            return potions;
        }

        internal static Consumable[] GetMeals()
        {
            Consumable[] meals = new Consumable[]
            {
                new Consumable("Chips", 10, 5, "You eat some Chips"),
                new Consumable("Chūnin Exams Burger Combo", 80, 50, "You eat the Chūnin Exams Burger Combo"),
                new Consumable("Green Chilli Hamburger", 50, 30, "You eat a Green Chilli Hamburger"),
                new Consumable("Habanero Burger", 35, 20, "You eat a Habanero Burger"),
                new Consumable("Jolokia Burger", 25, 15, "You eat a Jolokia Burger"),
                new Consumable("Super sour Lemon Burger", 45, 25, "You eat a Super sour Lemon Burger")
            };
            return meals;
        }
    }
}
