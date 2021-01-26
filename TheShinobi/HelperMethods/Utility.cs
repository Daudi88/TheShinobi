using TheShinobi.Characters.Enemies;
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
        /* The class Utility contains these methods:
         * RollDice()           - Uses the Random function to to simulate dice's beeing rolled. 
         * Shop()               - Method for the shops, ask's the player what items to buy wich are different depending on the shop.
         * GetAbuHassanItems()  - Gets Abu Hassans special items.
         * MakeAChoice()        - Lets the player make a choice while listening to see if it is E, B, D or M.
         * BuyItem()            - Lets the player buy an item and checks if the player can afford the item / items.
         * AddToBackpack()      - If the player buys or picks up drops from enemies this method is used to add the item / items to the backpack.
         * OpenBackpack()       - Displays the items inside the backpack and gives the player the option to use items if any.
         * SellItems()          - Lets the user sell items for gold.
         * HowMany()            - Asks the player how many of an item to buy and checks if the player have enough gold.
         * Remove()             - Removes texts in order to make the player experience cleaner.
         * GetEnemies()         - Instantiates the Enemies the hero can encountered in the game.
         * GetArmors()          - Instantiates the Armors common enemies use in the game.
         * GetWeapons()         - Instantiates the Weapons common enemies use in the game.
         * GetSevenSwords()     - Instantiates the seven legendary swords in the game.
         * Getpotions()         - Instantiates the common potions used in the game.
         * GetMeals()           - Instantiates the meals in Lightning Burger Shop.
         */
        public const int left = 9;
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

        public static void BuySomeStuff(Player player)
        {
            Item[] items = Get.AbuHassanItems();
            Shop(player, "item", items);
        }

        public static void Shop(Player player, string name, Item[] items)
        {
            while (true)
            {
                int top = Console.CursorTop;
                Console.WriteLine($"\t What {name.ToLower()} do you want to buy?");
                List<string> options = new List<string>();
                int ctr = 1;
                foreach (var item in items)
                {
                    options.Add($"{ctr++}. {item.Name} - {item.Price}g {item.BonusText()}");
                }
                Display.WithFrame(options, $"[Yellow]{name.ToUpper()}S[/Yellow]", ending: "Go back to shop menu");
                int bottom = Console.CursorTop;
                if (MakeAChoice(items.Length, out int choice, ending: true))
                {
                    Item item = items[choice - 1];
                    if (player.Gold >= item.Price)
                    {
                        if (item is IConsumable c)
                        {
                            int quantity;
                            Console.Write("                                              ");
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            Console.WriteLine($"\t How many {item.Name}s do you want to buy?");
                            Console.Write("\t > ");
                            while (true)
                            {
                                if (int.TryParse(ColorConsole.ReadLine(), out quantity) && quantity > 0)
                                {
                                    int price = item.Price;
                                    string amount = quantity > 1 ? $"{quantity}" : item.IndefiniteArticle;
                                    string plural = quantity > 1 ? "s" : "";
                                    if (quantity == 69)
                                    {
                                        ColorConsole.TypeOver("\t You naughty ninja!", ConsoleColor.Red);
                                    }
                                    else if (player.Gold >= price * quantity)
                                    {
                                        price *= quantity;
                                        ColorConsole.TypeOver($"\t You buy {amount} {item.Name}{plural} for {price} gold.", ConsoleColor.Yellow);
                                        BuyItem(player, item, price);
                                        break;
                                    }
                                    else
                                    {
                                        ColorConsole.TypeOver($"\t You don't have enough gold to buy {amount} {item.Name}{plural}!", ConsoleColor.Red);
                                    }
                                }
                                else
                                {
                                    ColorConsole.TypeOver($"\t Invalid choice! Purshase is canceled...", ConsoleColor.Red);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            ColorConsole.TypeOver($"\t You buy {item.IndefiniteArticle} {item.Name} for {item.Price} gold!", ConsoleColor.Yellow);
                            BuyItem(player, item, item.Price);
                        }
                    }
                    else
                    {
                        ColorConsole.TypeOver($"\t You don't have enough gold to buy {item.IndefiniteArticle} {item.Name}!", ConsoleColor.Red);
                    }
                    Remove(top, bottom);
                }
                else
                {
                    Remove(top, bottom);
                    break;
                }
            }
        }

        

        public static bool MakeAChoice(int length, out int choice, Player player = null, bool std = false, bool ending = false)
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
                    ColorConsole.TypeOver("\t Invalid choice. Try again!", ConsoleColor.Red);
                }
            }
            return result;
        }

        public static void BuyItem(Player player, Item item, int price, bool eat = false)
        {
            player.Gold -= price;
            if (eat && item is IConsumable meal)
            {
                meal.Consume(player);
            }
            else
            {
                AddToBackpack(player, item);
            }
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
                player.Backpack.Add(thing);
            }
        }

        public static bool OpenBackpack(Player player)
        {
            if (player.Backpack.Count > 0)
            {
                Console.WriteLine($"\n\t What do you want to use?");
                int top = Console.CursorTop;
                while (true)
                {

                    Display.Backpack(player);
                    int bottom = Console.CursorTop;
                    if (MakeAChoice(player.Backpack.Count, out int choice, ending: true))
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
                ColorConsole.TypeOver("\t Your backpack is empty...", ConsoleColor.Red);
                return false;
            }
        }

        public static void SellItems(Player player)
        {
            if (player.Backpack.Count > 0)
            {
                Console.WriteLine($"\t What do you want to sell?");
                int top = Console.CursorTop;
                int bottom;
                while (true)
                {
                    Display.Backpack(player, true);
                    bottom = Console.CursorTop;
                    if (MakeAChoice(player.Backpack.Count, out int choice, ending: true))
                    {
                        Item item = player.Backpack[choice - 1];
                        int price = item.Price;
                        int quantity;
                        Console.Write("                                              ");
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.WriteLine($"\t How many {item.Name}s do you want to sell?");
                        Console.Write("\t > ");
                        while (true)
                        {
                            if (int.TryParse(ColorConsole.ReadLine(), out quantity) && quantity > 0)
                            {
                                if (quantity <= item.Quantity && quantity != 69)
                                {
                                    price *= quantity;
                                    string plural = quantity > 1 ? "s" : "";
                                    ColorConsole.TypeOver($"\t You sell {quantity} {item.Name}{plural} and gain {price} gold.", ConsoleColor.Yellow);
                                    break;
                                }
                                else if (quantity == 69)
                                {
                                    ColorConsole.TypeOver("\t You naughty ninja!", ConsoleColor.Red);
                                }
                                else
                                {
                                    ColorConsole.TypeOver($"\t You cannot sell that many {item.Name}s...", ConsoleColor.Red);
                                }
                            }
                            else
                            {
                                ColorConsole.TypeOver($"\t Invalid choice! Sale is canceled...", ConsoleColor.Red);
                                break;
                            }
                        }
                        item.Quantity -= quantity;
                        player.Gold += price;
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
                Remove(top, bottom);
            }
            else
            {
                ColorConsole.TypeOver("\t Your backpack is empty...", ConsoleColor.Red);
            }
        }

        private static int HowMany(Player player, Item item, out int price, bool sell = false)
        {
            price = item.Price;
            int quantity;
            Console.Write("                                              ");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            string text = sell ? "sell" : "buy";
            Console.WriteLine($"\t How many {item.Name}s do you want to {text}?");
            Console.Write("\t > ");
            while (true)
            {
                if (int.TryParse(ColorConsole.ReadLine(), out quantity))
                {
                    if (sell && quantity <= item.Quantity && quantity != 69)
                    {
                        price *= quantity;
                        string plural = quantity > 1 ? "s" : "";
                        ColorConsole.TypeOver($"\t You sell {quantity} {item.Name}{plural} and gain {price} gold.", ConsoleColor.Yellow);
                        break;
                    }
                    else if (!sell && player.Gold >= price * quantity)
                    {
                        price *= quantity;
                        break;
                    }
                    else if (quantity == 69)
                    {
                        ColorConsole.TypeOver("\t You naughty ninja!", ConsoleColor.Red);
                    }
                    else
                    {
                        ColorConsole.TypeOver($"\t You cannot {text} that many {item.Name}s...", ConsoleColor.Red);
                    }
                }
                else
                {
                    text = sell ? "Sale" : "Purchase";
                    ColorConsole.TypeOver($"\t Invalid choice! {text} canceled...", ConsoleColor.Red);
                    break;
                }
            }
            return quantity;
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
