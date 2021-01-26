using System;
using System.Collections.Generic;
using System.Text;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Structures
{
    static class Store
    {
        /* This class contains the following methods:
        * BuyItems()           - Method for the shops, ask's the player what 
        *                        items to buy that are different depending on 
        *                        the shop. Lets the player buy items and checks 
        *                        if the player can afford them.
        * MakePurchase()       - ...
        * SellItems()          - Lets the user sell items for gold.
        */

        public static void BuyItems(Player player, string name, Item[] items, string ending = "Go back to shop menu")
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
                Display.WithFrame(options, $"[Yellow]{name.ToUpper()}S[/Yellow]", ending: ending);
                int bottom = Console.CursorTop;
                if (Utility.ChooseANumber(items.Length, out int choice, ending: true))
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
                                        MakePurchase(player, item, price);
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
                            MakePurchase(player, item, item.Price);
                        }
                    }
                    else
                    {
                        ColorConsole.TypeOver($"\t You don't have enough gold to buy {item.IndefiniteArticle} {item.Name}!", ConsoleColor.Red);
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

        public static void MakePurchase(Player player, Item item, int price, bool eat = false)
        {
            player.Gold -= price;
            if (eat && item is IConsumable meal)
            {
                meal.Consume(player);
            }
            else
            {
                Utility.AddToBackpack(player, item);
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
                    if (Utility.ChooseANumber(player.Backpack.Count, out int choice, ending: true))
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
                        Utility.Remove(top, bottom);
                    }
                    else
                    {
                        break;
                    }
                }
                Utility.Remove(top, bottom);
            }
            else
            {
                ColorConsole.TypeOver("\t Your backpack is empty...", ConsoleColor.Red);
            }
        }
    }
}
