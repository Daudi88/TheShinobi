using static TheShinobi.HelperMethods.Utility;
using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Consumables;
using System.Collections.Generic;
using System.Threading;
using System;
using TheShinobi.Items;

namespace TheShinobi.Structures
{
    class Village
    {
        public static void Menu(Player player)
        {
            bool exit = false;
            while (!exit)
            {
                List<string> options = new List<string>()
                {
                    "1. Go on an Adventure",
                    "2. Eat at Lightning Burger",
                    "3. Heal yourself at Konoha Hospital",
                    "4. Go to the Ninja Tool Shop",
                };
                Display.WithFrame(options, "[Green]VILLAGE[/Green]", true, "Exit Game");

                Action<Player>[] methods = new Action<Player>[]
                {
                    Adventure.GoOnAdventure, LightningBurger, KonohaHospital, NinjaToolShop
                };
                while (true)
                {
                    if (MakeAChoice(methods.Length, out int choice, player, true, true))
                    {                        
                        if (choice > 0)
                        {
                            Console.SetWindowPosition(0, Console.CursorTop - V);
                            methods[choice - 1].DynamicInvoke(player);
                            break;
                        }
                        else
                        {
                            Console.WriteLine();
                            break;
                        }
                    }
                    else
                    {
                        Game.ExitGame();
                        exit = true;
                        break;
                    }
                }                              
            }
        }

        private static void LightningBurger(Player player)
        {
            Consumable[] meals = GetMeals();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n\t Welcome to the Lightning Burger!");
                Console.WriteLine("\t What can we do for you?");
                List<string> menu = new List<string>();
                int ctr = 1;
                foreach (var meal in meals)
                {
                    menu.Add($"{ctr++}. {meal.Name} - {meal.Price}g {meal.BonusText()}");
                }
                Display.WithFrame(menu, "[Yellow]MENU[/Yellow]", ending: "Leave");
                while (true)
                {
                    if (MakeAChoice(meals.Length, out int choice, ending: true))
                    {
                        BuyItem(player, meals[choice - 1], true);
                    }
                    else
                    {
                        ColorConsole.WriteLine("\t Thank you for visiting Lightning Burger!\n", ConsoleColor.Yellow);
                        Thread.Sleep(1800);
                        Console.SetWindowPosition(0, Console.CursorTop - V);
                        exit = true;
                        break;
                    }
                }
            }
        }

        private static void KonohaHospital(Player player)
        {
            Console.SetWindowPosition(0, Console.CursorTop - V);
        }

        private static void NinjaToolShop(Player player)
        {
            Console.WriteLine("\n\t Welcome to the Ninja Tool Shop");
            int top = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(0, top);
                Console.WriteLine("\t What do you want to do?");
                List<string> options = new List<string>()
                {
                    "1. Buy Armor",
                    "2. Buy Weapons",
                    "3. Buy Potions",
                    "4. Sell Items"
                };
                Display.WithFrame(options, "[Yellow]SHOP[/Yellow]", ending: "Leave");
                int bottom = Console.CursorTop;
                Action<Player>[] methods = new Action<Player>[]
                {
                    BuyArmor, BuyWeapons, BuyPotions, SellItems
                };

                if (MakeAChoice(methods.Length, out int choice, ending: true))
                {
                    if (choice != 4 || player.Backpack.Count > 0)
                    {
                        Remove(top, bottom);
                    }                   
                    methods[choice - 1].DynamicInvoke(player);
                }
                else
                {
                    ColorConsole.WriteLine("\t Thank you for visiting!\n", ConsoleColor.Yellow);
                    Thread.Sleep(1800);
                    Console.SetWindowPosition(0, Console.CursorTop - V);
                    break;
                }
            }
        }

        private static void BuyArmor(Player player)
        {
            Armor[] armors = GetArmors();
            Shop(player, "armor", armors);
        }

        private static void BuyWeapons(Player player)
        {
            Weapon[] weapons = GetWeapons();
            Shop(player, "weapon", weapons);
        }

        private static void BuyPotions(Player player)
        {
            Consumable[] potions = GetPotions();
            Shop(player, "potion", potions);
        }
    }
}
