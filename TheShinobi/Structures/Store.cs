using System;
using System.Collections.Generic;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;
using TheShinobi.Items;

namespace TheShinobi.Structures
{
    static class Store
    {
        /// <summary>
        /// Method for the shops, ask's the player what 
        /// items to buy that are different depending on 
        /// the shop.Lets the player buy items and checks 
        /// the player can afford them.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="items"></param>
        /// <param name="ending"></param>
        public static void BuyItems(Player player, string name, Item[] items, string ending = "Go back to shop menu")
        {
            bool isFirstTime = true;
            while (true)
            {
                int top = Console.CursorTop;
                if (isFirstTime)
                {
                    ColorConsole.WriteDelayedLine($"\t What {name.ToLower()} do you want to buy?");                    
                    isFirstTime = false;
                }
                else
                {
                    Console.WriteLine($"\t What {name.ToLower()} do you want to buy?");
                }
                List<string> options = new List<string>();
                int ctr = 1;
                foreach (var item in items)
                {
                    options.Add($"{ctr++}. {item.Name} - {item.Price} ryō {item.BonusText()}");
                }
                Display.WithFrame(options, $"[Yellow]{name.ToUpper()}S[/Yellow]", ending: ending);
                int bottom = Console.CursorTop;
                if (Utility.ChooseANumber(items.Length, out int choice, ending: true))
                {
                    Item item = items[choice - 1];
                    if (player.Ryō >= item.Price)
                    {
                        if (item is IConsumable c)
                        {
                            int quantity;
                            Console.Write("                                                               ");
                            Console.SetCursorPosition(0, Console.CursorTop - 1);
                            ColorConsole.WriteDelayedLine($"\t How many {item.Name}s do you want to buy?");                            
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
                                        ColorConsole.WriteOver("\t You naughty ninja!", ConsoleColor.Red);
                                    }
                                    else if (player.Ryō >= price * quantity)
                                    {
                                        price *= quantity;
                                        ColorConsole.WriteOver($"\t You buy {amount} {item.Name}{plural} for {price} ryō.", ConsoleColor.Yellow);
                                        MakePurchase(player, item, price, quantity);
                                        break;
                                    }
                                    else
                                    {
                                        ColorConsole.WriteOver($"\t You don't have enough ryō to buy {amount} {item.Name}{plural}!", ConsoleColor.Red);
                                    }
                                }
                                else
                                {
                                    ColorConsole.WriteOver($"\t Invalid choice! Purshase is canceled...", ConsoleColor.Red);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            ColorConsole.WriteOver($"\t You buy {item.IndefiniteArticle} {item.Name} for {item.Price} ryō!", ConsoleColor.Yellow);
                            MakePurchase(player, item, item.Price);
                        }
                    }
                    else
                    {
                        ColorConsole.WriteOver($"\t You don't have enough ryō to buy {item.IndefiniteArticle} {item.Name}!", ConsoleColor.Red);
                    }
                    Utility.Remove(top, bottom);
                }
                else
                {
                    Utility.Remove(top, bottom);
                    break;
                }
            }
        }

        /// <summary>
        /// Method that makes the transaction and adds item(s) to the backpack.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <param name="price"></param>
        /// <param name="eat"></param>
        public static void MakePurchase(Player player, Item item, int price, int quantity = 1, bool eat = false)
        {
            player.Ryō -= price;
            if (eat && item is IConsumable meal)
            {
                meal.Consume(player);
            }
            else
            {
                Utility.AddToBackpack(player, item, quantity);
            }
        }

        /// <summary>
        /// Lets the player sell items for gold.
        /// </summary>
        /// <param name="player"></param>
        public static void SellItems(Player player)
        {
            if (player.Backpack.Count > 0)
            {
                bool isFirstTime = true;
                while (true)
                {
                    int top = Console.CursorTop;
                    if (isFirstTime)
                    {
                        ColorConsole.WriteDelayedLine("\t What do you want to sell?");
                        isFirstTime = false;
                    }
                    else
                    {
                        Console.WriteLine($"\t What do you want to sell?");
                    }
                    Display.Backpack(player, true);
                    int bottom = Console.CursorTop;
                    if (Utility.ChooseANumber(player.Backpack.Count, out int choice, ending: true))
                    {
                        Item item = player.Backpack[choice - 1];
                        int price = item.Price;
                        int quantity;
                        Console.Write("                                              ");
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ColorConsole.WriteDelayedLine($"\t How many {item.Name}s do you want to sell?");
                        Console.Write("\t > ");
                        while (true)
                        {
                            if (int.TryParse(ColorConsole.ReadLine(), out quantity) && quantity > 0)
                            {
                                if (quantity <= item.Quantity && quantity != 69)
                                {
                                    price *= quantity;
                                    string plural = quantity > 1 ? "s" : "";
                                    ColorConsole.WriteOver($"\t You sell {quantity} {item.Name}{plural} and gain {price} ryō.", ConsoleColor.Yellow);
                                    break;
                                }
                                else if (quantity == 69)
                                {
                                    ColorConsole.WriteOver("\t You naughty ninja!", ConsoleColor.Red);
                                }
                                else
                                {
                                    ColorConsole.WriteOver($"\t You cannot sell that many {item.Name}s...", ConsoleColor.Red);
                                }
                            }
                            else
                            {
                                ColorConsole.WriteOver($"\t Invalid choice! Sale is canceled...", ConsoleColor.Red);
                                break;
                            }
                        }
                        item.Quantity -= quantity;
                        player.Ryō += price;
                        if (item.Quantity < 1)
                        {
                            player.Backpack.Remove(item);
                        }
                        Utility.Remove(top, bottom);
                    }
                    else
                    {
                        Utility.Remove(top, bottom);
                        break;
                    }
                }
            }
            else
            {
                ColorConsole.WriteOver("\t Your backpack is empty...", ConsoleColor.Red);
            }
        }
    }
}
