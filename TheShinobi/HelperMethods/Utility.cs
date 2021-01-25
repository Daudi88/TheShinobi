using TheShinobi.Characters.Enemies;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Potions;
using System;
using TheShinobi.Characters;
using TheShinobi.Items;
using TheShinobi.Interfaces;

namespace TheShinobi.HelperMethods
{
    static class Utility
    {
        public const int left = 9;
        public static readonly Random random = new Random();

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

        public static bool MakeAChoice(int length, out int choice, bool std = true) // Implementera BDM
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
                else if (input.ToUpper() == "E")
                {
                    break;
                }
                else if (choice == 0)
                {
                    ColorConsole.TypeOver("Invalid choice. Try again!", ConsoleColor.Red);
                }
            }
            return result;
        }

        public static void BuyItem(Player player, Item item, bool eat = false)
        {
            if (player.Gold >= item.Cost)
            {
                if (eat && item is Consumable meal)
                {
                    meal.Consume(player);
                }
                else
                {
                    AddToBackpack(player, item);
                }
            }
            else
            {
                ColorConsole.TypeOver("You don't have enough gold.", ConsoleColor.Red);
            }
        }

        private static void AddToBackpack(Player player, Item item)
        {
            throw new NotImplementedException();
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
                new Enemy("Nagato", 3, 35, new SteamArmor(), new Sword()),
                new Enemy("Haku", 4, 39, new FlakJacket(), new Crossbow()),
                new Enemy("Obito", 4, 39, new ChakraArmor(), new Gunbai()),
                new Enemy("Kaguya", 5, 44, new FlakJacket(), new FistsOfBones()),
                new Enemy("Ginkaku", 5, 44, new SteamArmor(), new Shichiseiken()),
                new Enemy("Madara", 6, 50, new InfiniteArmor(), new Gunbai()),
                new Enemy("Hanzō", 6, 50, new InfiniteArmor(), new Kusarigama()),
                new Enemy("Deidara", 7, 57, new SteamArmor(), new Shuriken()),
                new Enemy("Kimimaro", 7, 100, new ChakraArmor(), new FistsOfBones()),
                new Enemy("Kabuto", 8, 65, new SteamArmor(), new Shichiseiken()),
                new Enemy("Kisame", 8, 65, new FlakJacket(), new Kunai()),
                new Enemy("Daudi", 9, 74, new BulletproofVest(), new AK47()),
                new Enemy("Hocke", 9, 74, new BulletproofVest(), new AK47()),
                new Enemy("Kakuzu", 10, 84, new InfiniteArmor(), new Spear())
               

            };
            return enemies;
        }

        public static Armor[] GetArmors()
        {
            Armor[] armors = new Armor[]
            {

            };
            return armors;
        }

        public static Weapon[] GetWeapons()
        {
            Weapon[] weapons = new Weapon[]
            {
                new Crossbow(),
                new ChakraBlade(),
                new Sword(),
                new Gunbai(),
                new Kusarigama(),
                new Kunai(),
                new Shuriken(),
                new Spear(),
                new BowAndArrow(),
                new Kiba(),
                new Shichiseiken(),
                new Kubikiribōchō(),
                new TekagiShuko()
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

        internal static void SellItems(Player player, int top, int bottom)
        {
            throw new NotImplementedException();
        }
    }
}
