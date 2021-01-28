﻿using TheShinobi.Characters.Enemies;
using TheShinobi.Characters;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Consumables;
using TheShinobi.Items;
using System.Collections.Generic;
using System;
using TheShinobi.Interfaces;

namespace TheShinobi.HelperMethods
{
    static class Utility
    {
        /* This class contains the folowing methods:
         * RollDice()           - Uses the Random function to to simulate dice's
         *                        beeing rolled.                  
         * ChooseANumber()      - Lets the player make a choice while listening 
         *                        to see if it is Backpack, Details, Map or Ending.        
         * AddToBackpack()      - If the player buys or picks up loot from enemies
         *                        this method is used to add the item / items to 
         *                        the backpack.
         * OpenBackpack()       - Displays the items inside the backpack and gives
         *                        the player the option to use items if any.         
         * Remove()             - Removes texts in order to give a cleaner experience.         
         */

        public const int V = 15;
        public static readonly Random random = new Random();
        public static bool isEnergyDrink = false;
        public static string energyBonus = "";
        public static bool isVisitingVillage = true;


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

        public static bool ChooseANumber(int length, out int choice, Player player = null, bool std = false, bool ending = false)
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
                    if (OpenBackpack(player))
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
                else
                {
                    ColorConsole.WriteOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                }
            }
            return result;
        }

        public static void AddToBackpack(Player player, Item thing)
        {
            if (player.Backpack.Contains(thing))
            {
                foreach (var item in player.Backpack)
                {
                    if (item.Name == thing.Name)
                    {
                        item.Quantity += thing.Quantity;
                    }
                }
            }
            else
            {
                thing.Quantity++;
                player.Backpack.Add(thing);
            }
        }

        public static bool OpenBackpack(Player player)
        {
            if (player.Backpack.Count > 0)
            {
                ColorConsole.WriteDelayedLine($"\n\n\t What do you want to use?");
                int top = Console.CursorTop;
                while (true)
                {

                    Display.Backpack(player);
                    int bottom = Console.CursorTop;
                    if (ChooseANumber(player.Backpack.Count, out int choice, ending: true))
                    {
                        Item item = player.Backpack[choice - 1];
                        if (item is IEquipable e)
                        {
                            e.Equip(player, e);
                        }
                        else if (item is IConsumable c)
                        {
                            c.Consume(player);
                        }
                        item.Quantity--;
                        if (item.Quantity < 1)
                        {
                            player.Backpack.Remove(item);
                        }
                        Remove(top, bottom);
                    }
                    else
                    {
                        break;
                    }
                }
                Console.SetWindowPosition(0, Console.CursorTop - V);
                return true;
            }
            else
            {
                ColorConsole.WriteOver("\t Your backpack is empty...", ConsoleColor.Red);
                return false;
            }
        }

        public static void Remove(int top, int bottom)
        {
            int ctr = top;
            for (int i = 0; i <= bottom - top + 1; i++)
            {
                Console.SetCursorPosition(0, ctr++);
                for (int j = 0; j < 100; j++)
                {
                    Console.Write(" ");
                }
            }
            Console.SetCursorPosition(0, top);
        }

        
    }
}
