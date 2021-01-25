using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Potions;

namespace TheShinobi.Structures
{
    class Village
    {
        public static void Menu(Player player)
        {
            bool outerExit = false;
            while (!outerExit)
            {
                List<string> options = new List<string>()
                {
                    "1. Go on an Adventure",
                    "2. Eat at Lightning Burger",
                    "3. Heal yourself at Konoha Hospital",
                    "4. Go to the Ninja Tool Shop",
                };
                Display.WithFrame(options, "[Green]VILLAGE[/Green]", true, "Exit Game");
                bool innerExit = true;
                do
                {
                    string choice = ColorConsole.ReadLine();
                    switch (choice.ToUpper())
                    {
                        case "1":
                            Adventure.GoOnAdventure(player);
                            break;
                        case "2":
                            LightningBurger(player);
                            break;
                        case "3":
                            // Tycker den här biten är så liten att den inte behöver en egen metod. 
                            // Om vi utvecklar den men kan vi såklart göra det! :)
                            if (player.Gold >= 300)
                            {
                                player.Gold -= 300;
                                player.Hp = player.MaxHp;
                                ColorConsole.TypeOver("Tsunade patch you up to full health!", ConsoleColor.Yellow, 3000);
                            }
                            else
                            {
                                ColorConsole.TypeOver("You don't have enough gold.", ConsoleColor.Red);
                            }
                            innerExit = false;
                            break;
                        case "4":
                            NinjaToolShop(player);
                            break;
                        case "B":
                            if (!Display.Backpack(player))
                            {
                                innerExit = false;
                            }
                            break;
                        case "D":
                            Display.Details(player);
                            break;
                        case "M":
                            Display.Map(player);
                            break;
                        case "E":
                            Game.ExitGame();
                            break;
                        default:
                            ColorConsole.TypeOver("Invalid choice. Try again!", ConsoleColor.Red);
                            innerExit = false;
                            break;
                    }
                } while (!innerExit);
            }
        }
        
        private static void LightningBurger(Player player)
        {
            Consumable[] meals = Utility.GetMeals();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n\t Welcome to the Lightning Burger!");
                Console.WriteLine("\t What can we do for you?");
                List<string> menu = new List<string>();
                int ctr = 1;
                foreach (var meal in meals)
                {
                    menu.Add($"{ctr++}. {meal}");
                }
                Display.WithFrame(menu, "[Yellow]⚡BURGER[/Yellow]", ending: "Leave");
                while (true)
                {
                    if (Utility.MakeAChoice(meals.Length, out int choice))
                    {
                        Utility.BuyItem(player, meals[choice - 1]);
                    }
                    else
                    {
                        Console.WriteLine("\t Thank you for visiting Lightning Burger!\n");
                        Thread.Sleep(1800);
                        exit = true;
                        break;
                    }
                }
            }
        }

        private static void NinjaToolShop(Player player)
        {
            Console.WriteLine("\n\t Welcome to the Ninja Shop");
            int top = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(Utility.left, top);
                Console.WriteLine("What do you want to do?");
                List<string> options = new List<string>()
                {
                    "1. Buy Armor",
                    "2. Buy Weapons",
                    "3. Buy Potions",
                    "4. Sell Items"
                };
                Display.WithFrame(options, "[Yellow]SHOP[/Yellow]", ending: "Leave");
                int bottom = Console.CursorTop;
                var methods = new Action<Player, int, int>[]
                {
                    BuyArmor, BuyWeapons, BuyPotions, Utility.SellItems
                };

                if (Utility.MakeAChoice(methods.Length, out int choice))
                {
                    methods[choice - 1].DynamicInvoke(player, top, bottom);
                }
                else
                {
                    ColorConsole.WriteLine("\t Thank you for visiting!\n", ConsoleColor.Yellow);
                    break;
                }
            }
        }

        private static void BuyArmor(Player player, int top, int bottom)
        {
            int ctr = top;
            for (int i = 0; i <= bottom - top; i++)
            {
                Console.SetCursorPosition(0, ctr++);
                Console.WriteLine("\t\t\t\t");
            }
            while (true)
            {
                Console.SetCursorPosition(Utility.left, top);
                Console.WriteLine("What armor do you want to buy?");
                Armor[] armors = Utility.GetArmors();
                List<string> options = new List<string>();
                ctr = 1;
                foreach (var armor in armors)
                {
                    options.Add($"{ctr++}. {armor}");
                }
                Display.WithFrame(options, "[Yellow]ARMORS[/Yellow]", ending: "Go back to shop menu");

                if (Utility.MakeAChoice(armors.Length, out int choice))
                {
                    Utility.BuyItem(player, armors[choice - 1]);
                }
                else
                {
                    break;
                }
            }
        }

        private static void BuyWeapons(Player player, int top, int bottom)
        {
            throw new NotImplementedException();
        }

        private static void BuyPotions(Player player, int top, int bottom)
        {
            throw new NotImplementedException();
        }
    }
}
