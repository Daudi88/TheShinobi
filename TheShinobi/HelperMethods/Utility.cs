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
        public const int left = 9;
        public const int V = 15;
        public static readonly Random random = new Random();
        public static bool isCaffeine = false;

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

        public static void Shop(Player player, string name, Item[] items, bool eat = false)
        {
            while (true)
            {
                int top = Console.CursorTop;
                Console.WriteLine($"\t What {name.ToLower()} do you want to buy?");
                List<string> options = new List<string>();
                int ctr = 1;
                foreach (var item in items)
                {
                    options.Add($"{ctr++}. {item.Name} - {item.Price}g {item.Bonus()}");
                }
                Display.WithFrame(options, $"[Yellow]{name.ToUpper()}S[/Yellow]", ending: "Go back to shop menu");
                int bottom = Console.CursorTop;
                if (MakeAChoice(items.Length, out int choice, ending: true))
                {
                    Item item = items[choice - 1];
                    if (!eat && item is Consumable c)
                    {
                        item.Quantity = HowMany(player, item, out int price);
                        item.Price = price;
                    }
                    if (item.Quantity > 0)
                    {
                        BuyItem(player, item, eat);
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

        internal static Item[] GetAbuHassanItems()
        {
            throw new NotImplementedException();
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

        public static void BuyItem(Player player, Item item, bool eat = false)
        {
            if (player.Gold >= item.Price)
            {
                player.Gold -= item.Price;
                if (eat && item is Consumable meal)
                {
                    meal.Consume(player);
                }
                else
                {
                    string plural = item.Quantity > 1 ? "s" : "";
                    ColorConsole.TypeOver($"\t You buy {item.Quantity} {item.Name}{plural} for {item.Price} gold.", ConsoleColor.Yellow);
                    AddToBackpack(player, item);
                }
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
                        else if (item is Consumable c)
                        {
                            c.Consume(player);
                            if (item is EnergyDrink energy)
                            {
                                isCaffeine = true;
                                player.AttackBonus += energy.Caffeine;
                            }
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
                        item.Quantity -= HowMany(player, item, out int price, true);
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

        public static Enemy[] GetEnemies()
        {
            Enemy[] enemies = new Enemy[]
            {
                new Enemy("Sakon", 1, 30, new FlakJacket(), new Kiba()),
                new Enemy("Tayuya", 1, 30, new Shirt(), new Fists()),
                new Enemy("Ukon", 2, 32, new Shirt(), new Kusarigama()),
                new Enemy("Sasori", 2, 32, new Shirt(), new TekagiShuko()),
                new Enemy("Konan", 3, 35, new FlakJacket(), new Kubikiribōchō()),
                new Enemy("Nagato", 3, 35, new SteamArmor(), new Kunai()),
                new Enemy("Haku", 4, 39, new FlakJacket(), new Shuriken()),
                new Enemy("Obito", 4, 39, new ChakraArmor(), new Gunbai()),
                new Enemy("Kaguya", 5, 44, new FlakJacket(), new FistsOfBones()),
                new Enemy("Ginkaku", 5, 44, new SteamArmor(), new Shichiseiken()),
                new Enemy("Madara", 6, 50, new InfiniteArmor(), new Gunbai()),
                new Enemy("Hanzō", 6, 50, new InfiniteArmor(), new Kusarigama()),
                new Enemy("Deidara", 7, 57, new SteamArmor(), new Shuriken()),
                new Enemy("Kimimaro", 7, 57, new ChakraArmor(), new FistsOfBones()),
                new Enemy("Kabuto", 7, 57, new SteamArmor(), new Shichiseiken()),
                new Enemy("Kisame", 8, 65, new FlakJacket(), new Kunai()),
                new Enemy("Kakuzu", 8, 65, new InfiniteArmor(), new Kusarigama()),
                new Enemy("Hocke", 8, 70, new BulletproofVest(), new AK47()),
                new Enemy("Daudi", 9, 74, new BulletproofVest(), new AK47()),

            };
            return enemies;
        }

        public static Armor[] GetArmors()
        {
            Armor[] armors = new Armor[]
            {
                new FlakJacket(),
                new SteamArmor(),
                new ShinobiBattleArmor(),
                new ChakraArmor(),
                new InfiniteArmor()
            };
            return armors;
        }

        public static Weapon[] GetWeapons()
        {
            Weapon[] weapons = new Weapon[]
            {
                new Kunai(),
                new Shuriken(),
                new TekagiShuko(),
                new ChakraBlade(),
                new Gunbai(),
                new Kusarigama(),
                new Shichiseiken()

            };
            return weapons;
        }
        public static Weapon[] GetSevenSwords()
        {
            Weapon[] weapons = new Weapon[]
            {
                new Kabutowari(),
                new Hiramekarei(),
                new Kubikiribōchō(),
                new Samehada(),
                new Kiba(),
                new Nuibari(),
                new Shibuki()
            };
            return weapons;
        }
        public static Consumable[] GetPotions()
        {
            Consumable[] potions = new Consumable[]
            {
                new Consumable("Lesser Healing Potion", 15, 5),
                new Consumable("Healing Potion", 20, 8),
                new Consumable("Greater Healing Potion", 25, 10),
                new Consumable("Superior Healing Potion", 30, 12)
            };
            return potions;
        }

        internal static Consumable[] GetMeals()
        {
            Consumable[] meals = new Consumable[]
            {
                new Consumable("Chips", 10, 5, "You eat some Chips"),
                new Consumable("Chūnin Exams Burger Combo", 80, 50, "You eat the Chūnin Exams Burger Combo"),
                new Consumable("Green Chilli Hamburger", 50, 30, "You eat a Green Chilli Hamburger"),
                new Consumable("Habanero Burger", 35, 20, "You eat a Habanero Burger"),
                new Consumable("Jolokia Burger", 25, 15, "You eat a Jolokia Burger"),
                new Consumable("Super sour Lemon Burger", 45, 25, "You eat a Super sour Lemon Burger")
            };
            return meals;
        }
    }
}
