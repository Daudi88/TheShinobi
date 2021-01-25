using System;
using System.Collections.Generic;
using System.Threading;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
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
                Display.WithFrame(options, "[Green]VILLAGE[/Green]", 1, "E. Exit Game");
                bool innerExit = true;
                do
                {
                    string choice = ColorConsole.ReadLine();
                    switch (choice.ToUpper())
                    {
                        case "1":
                            //Adventure.GoOnAdventure(player);
                            break;
                        case "2":
                            LightningBurger(player);
                            break;
                        case "3":
                            //KonohaHospital(player);
                            break;
                        case "4":
                            //NinjaToolShop(player);
                            break;
                        case "B":
                            //if (!Display.Backpack(player))
                            {
                                innerExit = false;
                            }
                            break;
                        case "D":
                            //Display.Details(player);
                            break;
                        case "M":
                            //Display.Map(player);
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
            bool outerExit = false;
            while (!outerExit)
            {
                Console.WriteLine("\n\t Welcome to the Lightning Burger!");
                Console.WriteLine("\t What can we do for you?");
                List<string> menu = new List<string>();
                int ctr = 1;
                foreach (var meal in meals)
                {
                    menu.Add($"{ctr++}. {meal}");
                }
                Display.WithFrame(menu, "[Yellow]⚡BURGER[/Yellow]", ending: "E. Leave");
                bool innerExit = false;
                while (!innerExit)
                {
                    string input = ColorConsole.ReadLine();
                    int.TryParse(input, out int choice);
                    if (choice > 0 && choice - 1 < meals.Length)
                    {
                        Consumable meal = meals[choice - 1];
                        if (player.Gold >= meal.Cost)
                        {
                            meal.Consume(player);
                        }
                        else
                        {
                            ColorConsole.TypeOver("You don't have enough gold.", ConsoleColor.Red);
                        }
                    }
                    else if (input.ToUpper() == "E")
                    {
                        Console.WriteLine("\t Thank you for visiting Lightning Burger!\n");
                        Thread.Sleep(1800);
                        innerExit = true;
                        outerExit = true;
                    }
                    else
                    {
                        ColorConsole.TypeOver("Invalid choice. Try again!", ConsoleColor.Red);
                    }
                } 
            }
        }
    }
}
