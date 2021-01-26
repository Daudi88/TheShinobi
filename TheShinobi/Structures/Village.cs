﻿using System;
using System.Collections.Generic;
using System.Threading;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Items.Consumables;
using static TheShinobi.HelperMethods.Utility;

namespace TheShinobi.Structures
{
    class Village
    {
        /* The class Village contains these methods:
         * Menu()               - Method for Menu of the Hidden Leaf Village. 
         * LightningBurger()    - Method that lets the player eat a meal at Lightning Burger to restore health.
         * KonohaHospital()     - Method that lets the player heal by visiting the Doctor.
         * NinjaToolShop()      - Method that lets the player buy and sell items.
         */
        public static void Menu(Player player)
        {
            bool exit = false;
            while (!exit)
            {
                if (isVisitingVillage)
                {
                    Console.WriteLine("\n\t Welcome to the Hidden Leaf Village!" +
                        "\n\t What do you want to do?");
                    isVisitingVillage = false;
                }
                else
                {
                    Console.WriteLine("\n\t What do you want to do?");
                }
                List<string> options = new List<string>()
                {
                    "1. Go on an Adventure",
                    "2. Eat at Lightning Burger",
                    "3. Heal yourself at Konoha Hospital",
                    "4. Go to the Ninja Tool Shop",
                };
                Display.WithFrame(options, "[Yellow]VILLAGE[/Yellow]", true, "Exit Game");

                Action<Player>[] methods = new Action<Player>[]
                {
                    Adventure.GoOnAdventure, LightningBurger, KonohaHospital, NinjaToolShop
                };
                while (true)
                {
                    if (ChooseANumber(methods.Length, out int choice, player, true, true))
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
            Consumable[] meals = Get.Meals();
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
                    if (ChooseANumber(meals.Length, out int choice, ending: true))
                    {
                        Consumable meal = meals[choice - 1];
                        Store.MakePurchase(player, meal, meal.Price, true);
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
            Console.WriteLine("\n\t Welcome to Konoha Hospital!");
            int top = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(0, top);
                Console.WriteLine("\t What do you want to do?");
                List<string> options = new List<string>()
                {
                    "1. See Tsunade the medical-nin (300g)",
                    "2. Buy potions"
                };
                Display.WithFrame(options, "[Yellow]HOSPITAL[/Yellow]", ending: "Leave");
                int bottom = Console.CursorTop;
                if (ChooseANumber(2, out int choice, ending: true))
                {
                    switch (choice)
                    {
                        case 1:
                            SeeTsunade(player);
                            break;
                        case 2:
                            Remove(top, bottom);
                            Store.BuyItems(player, "Potion", Get.Potions(), "Go back");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ColorConsole.WriteLine("\t Thank you for visiting Konoha Hospital!", ConsoleColor.Yellow);
                    Thread.Sleep(1800);
                    Console.SetWindowPosition(0, Console.CursorTop - V);
                    break;
                }
            }
        }

        private static void SeeTsunade(Player player)
        {
            if (player.Hp == player.MaxHp)
            {
                ColorConsole.TypeOver("\t No need to see Tsunade, you have full health!", ConsoleColor.Yellow);
            }
            else if (player.Gold >= 300)
            {
                player.Hp = player.MaxHp;
                player.Gold -= 300;
                ColorConsole.TypeOver("\t Tsunade patch you up and you gain full health!", ConsoleColor.Yellow);
            }
            else
            {
                ColorConsole.TypeOver("\t You don't have enough gold to see Tsunade!", ConsoleColor.Red);
            }
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
                    "3. Sell Items"
                };
                Display.WithFrame(options, "[Yellow]SHOP[/Yellow]", ending: "Leave");
                int bottom = Console.CursorTop;
                if (ChooseANumber(3, out int choice, ending: true))
                {
                    switch (choice)
                    {
                        case 1:
                            Remove(top, bottom);
                            Store.BuyItems(player, "armor", Get.Armors());
                            break;
                        case 2:
                            Remove(top, bottom);
                            Store.BuyItems(player, "weapon", Get.Weapons());
                            break;
                        case 3:
                            if (player.Backpack.Count > 0)
                            {
                                Remove(top, bottom);
                            }
                            Store.SellItems(player);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ColorConsole.WriteLine("\t Thank you for visiting the Ninja Tool Shop!", ConsoleColor.Yellow);
                    Thread.Sleep(1800);
                    Console.SetWindowPosition(0, Console.CursorTop - V);
                    break;
                }
            }
        }
    }
}
