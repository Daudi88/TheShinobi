﻿using System;
using System.Linq;
using System.Threading;
using TheShinobi.Characters;
using TheShinobi.Interfaces;
using TheShinobi.Items;

namespace TheShinobi.HelperMethods
{
    static class Utility
    {
        public const int V = 15;
        public static readonly Random random = new Random();
        public static EnergyDrink energyDrink = new EnergyDrink();
        public static bool isVisitingVillage = true;
        public static bool isBattleBeginning = true;
        public static bool isLeavingHome = true;
        public static int energyCtr = 0;

        /// <summary>
        /// Uses the Random function to to simulate dice's beeing rolled.
        /// </summary>
        /// <param name="dice"></param>
        /// <returns></returns>
        public static int RollDice(string dice)
        {
            string[] parts = dice.Split('d');
            int times = int.Parse(parts[0]);
            int sides = int.Parse(parts[1]);
            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += random.Next(1, sides + 1);
            }
            return result;
        }

        /// <summary>
        /// Resets the energy levels after a fight is 
        /// energy drink has been consumed.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="top"></param>
        public static void EnergyDip(Player player, int top)
        {
            string ending = energyCtr > 1 ? "s are" : " is";
            ColorConsole.WriteSetDelayed($"The effect of the energy drink{ending} wearing off!", top, color: ConsoleColor.Red);
            int energy = energyDrink.Energy * energyCtr;
            player.Stamina.Max -= energy;
            player.Chakra.Max -= energy;
            player.Chakra.Current -= energy;
            if (player.Stamina.Current - energy > 0)
            {
                player.Stamina.Current -= energy;
            }
            else
            {
                player.Stamina.Current = 1;
            }
            if (player.Chakra.Current - energy < 0)
            {
                player.Chakra.Current = 0;
            }
            player.Defence -= energy;
            player.AttackBonus -= energy;
            energyDrink.EnergyCtr = 0;
            energyDrink.IsEnergized = false;
        }

        /// <summary>
        /// Lets the player make a choice while listening 
        /// to see if it is Backpack, Details, Map or Ending.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="choice"></param>
        /// <param name="player"></param>
        /// <param name="std"></param>
        /// <param name="ending"></param>
        /// <returns></returns>
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
                else if (input.ToUpper() == "H")
                {
                    string[] hints = Get.Hints();
                    string hint = hints[random.Next(hints.Length)];
                    ColorConsole.WriteOver($"\t {hint}", ConsoleColor.DarkCyan, 2000);
                }
                else
                {
                    ColorConsole.WriteOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                }
            }
            return result;
        }

        /// <summary>
        /// If the player buys or picks up loot from enemies 
        /// this method is used to add the item/items to the backpack
        /// </summary>
        /// <param name="player"></param>
        /// <param name="thing"></param>
        /// <param name="quantity"></param>
        public static void AddToBackpack(Player player, Item thing, int quantity = 1)
        {
            var matches = player.Backpack.Where(i => i.Name == thing.Name).ToList();
            if (matches.Count > 0)
            {
                foreach (var item in player.Backpack)
                {
                    if (item.Name == thing.Name)
                    {
                        item.Quantity += quantity;
                    }
                }
            }
            else
            {
                thing.Quantity = quantity;
                player.Backpack.Add(thing);
            }
        }

        /// <summary>
        /// Displays the items inside the backpack and gives 
        /// the player the option to use items if any.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool OpenBackpack(Player player)
        {
            if (player.Backpack.Count > 0)
            {
                Console.SetWindowPosition(0, Console.CursorTop - V);
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
                return true;
            }
            else
            {
                ColorConsole.WriteOver("\t Your backpack is empty...", ConsoleColor.Red);
                return false;
            }
        }

        /// <summary>
        /// Removes texts in order to give a cleaner experience.
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
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

        /// <summary>
        /// Waits for the user to press a key.
        /// Places a blinking label at the bottom.
        /// </summary>
        public static void WaitForUser()
        {
            bool isKeyPressed = false;
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(30);
                if (Console.KeyAvailable)
                {
                    isKeyPressed = true;
                    break;
                }
            }
            if (!isKeyPressed)
            {
                Display.Blinking("\t[Press enter to continue]");
            }
            else
            {
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Waiths for the user to press a key. 
        /// Places a blinking label at the bottom with custom placement.
        /// </summary>
        /// <param name="bottom"></param>
        public static void WaitSetForUser(int bottom)
        {
            bool isKeyPressed = false;
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(30);
                if (Console.KeyAvailable)
                {
                    isKeyPressed = true;
                    break;
                }
            }
            if (!isKeyPressed)
            {
                Display.SetBlinking("\t [Press enter to continue]", bottom);
            }
            else
            {
                Console.ReadKey(true);
            }
        }
    }
}
