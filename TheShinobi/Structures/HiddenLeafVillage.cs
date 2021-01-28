﻿using System;
using System.Collections.Generic;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Items.Consumables;
using static TheShinobi.HelperMethods.Utility;

namespace TheShinobi.Structures
{
    class HiddenLeafVillage
    {
        /* This class contains the foloowing methods:
         * Menu()               - Menu of the Hidden Leaf Village. 
         * LightningBurger()    - Lets the player eat at Lightning Burger to 
         *                        restore health.
         * KonohaHospital()     - Lets the player heal by visiting Tsunade or
         *                        buy potions.
         * NinjaToolShop()      - Lets the player buy and sell items.
         */

        public static void Menu(Player player)
        {
            bool exit = false;
            while (!exit)
            {
                Console.SetWindowPosition(0, Console.CursorTop - V);
                if (isVisitingVillage)
                {
                    ColorConsole.WriteDelayed("\n\n\t Welcome to the Hidden Leaf Village!\n");
                }
                else
                {
                    Console.WriteLine();
                }
                ColorConsole.WriteDelayedLine("\t What do you want to do?");
                isVisitingVillage = false;
                List<string> options = new List<string>()
                {
                    "1. Go on an Adventure",
                    "2. Eat at Lightning Burger",
                    "3. Heal yourself at Konoha Hospital",
                    "4. Go to the Ninja Tool Shop",
                };
                Display.WithFrame(options, "[Yellow]HIDDEN LEAF VILLAGE[/Yellow]", true, "Exit Game");

                Action<Player>[] methods = new Action<Player>[]
                {
                    Adventure.Menu, LightningBurger, KonohaHospital, NinjaToolShop
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
                ColorConsole.WriteDelayed("\n\n\t Welcome to the Lightning Burger!\n");
                ColorConsole.WriteDelayedLine("\t What can we do for you?");                
                List<string> menu = new List<string>();
                int ctr = 1;
                foreach (var meal in meals)
                {
                    menu.Add($"{ctr++}. {meal.Name} - {meal.Price}g {meal.BonusText()}");
                }
                Display.WithFrame(menu, "[Yellow]LIGHTNING BURGER[/Yellow]", ending: "Leave");
                while (true)
                {
                    if (ChooseANumber(meals.Length, out int choice, ending: true))
                    {
                        Consumable meal = meals[choice - 1];
                        if (player.Ryō >= meal.Price)
                        {
                            Store.MakePurchase(player, meal, meal.Price, true);
                        }
                        else
                        {
                            ColorConsole.WriteOver($"\t You don't have enough gold to buy a {meal.Name}", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        ColorConsole.WriteDelayedLine("\t Thank you for visiting Lightning Burger!\n", ConsoleColor.Yellow);
                        exit = true;
                        break;
                    }
                }
            }
        }

        private static void KonohaHospital(Player player)
        {
            ColorConsole.WriteDelayed("\n\n\t Welcome to Konoha Hospital!\n");
            int top = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(0, top);
                ColorConsole.WriteDelayedLine("\t What do you want to do?");                
                List<string> options = new List<string>()
                {
                    "1. See Tsunade the medical-nin (300 Ryō)",
                    "2. Buy potions"
                };
                Display.WithFrame(options, "[Yellow]KONOHA HOSPITAL[/Yellow]", ending: "Leave");
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
                    ColorConsole.WriteDelayedLine("\t Thank you for visiting Konoha Hospital\n!", ConsoleColor.Yellow);
                    break;
                }
            }
        }

        private static void SeeTsunade(Player player)
        {
            if (player.Stamina.Current == player.Stamina.Max)
            {
                ColorConsole.WriteOver("\t No need to see Tsunade, you have full health!", ConsoleColor.Yellow);
            }
            else if (player.Ryō >= 300)
            {
                player.Stamina.Current = player.Stamina.Max;
                player.Ryō -= 300;
                ColorConsole.WriteOver("\t Tsunade patch you up and you gain full health!", ConsoleColor.Yellow);
            }
            else
            {
                ColorConsole.WriteOver("\t You don't have enough Ryō to see Tsunade!", ConsoleColor.Red);
            }
        }

        private static void NinjaToolShop(Player player)
        {
            ColorConsole.WriteDelayed("\n\n\t Welcome to the Ninja Tool Shop\n");
            int top = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(0, top);
                ColorConsole.WriteDelayedLine("\t What do you want to do?");                
                List<string> options = new List<string>()
                {
                    "1. Buy Armor",
                    "2. Buy Weapons",
                    "3. Sell Items"
                };
                Display.WithFrame(options, "[Yellow]NINJA TOOL SHOP[/Yellow]", ending: "Leave");
                int bottom = Console.CursorTop;
                if (ChooseANumber(3, out int choice, ending: true))
                {
                    switch (choice)
                    {
                        case 1:
                            Remove(top, bottom);
                            Store.BuyItems(player, "Armor", Get.Armors());
                            break;
                        case 2:
                            Remove(top, bottom);
                            Store.BuyItems(player, "Weapon", Get.Weapons());
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
                    ColorConsole.WriteDelayedLine("\t Thank you for visiting the Ninja Tool Shop\n!", ConsoleColor.Yellow);
                    break;
                }
            }
        }
    }
}
