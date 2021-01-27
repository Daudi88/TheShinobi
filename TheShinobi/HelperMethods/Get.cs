using System;
using System.Collections.Generic;
using TheShinobi.Characters;
using TheShinobi.Characters.Enemies;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;
using TheShinobi.Structures;

namespace TheShinobi.HelperMethods
{
    static class Get
    {
        /* This class contains the following methods:
         * GetEnemies()         - Returns an array of Enemies the player can 
         *                        encountered in the game.
         * GetArmors()          - Returns an array of Armors common enemies use 
         *                        in the game and that can be purchased at the  
         *                        Ninja Tool Shop.
         * GetWeapons()         - Returns an array of Weapons common enemies use
         *                        in the game that can be purchased at the  
         *                        Ninja Tool Shop.
         * GetSevenSwords()     - Returns an array of the seven legendary swords 
         *                        that can be found at the graveyard.
         * Getpotions()         - Returns an array of common potions used in the game.
         * GetMeals()           - Returns an array of meals for Lightning Burger Shop.
         * GetAbuHassanItems()  - Returns an array of Abu Hassan's special items.
         */

        public static int ColorLength(string text)
        {
            int at = text.IndexOf("[");
            int at2 = text.IndexOf("]");
            return text.Substring(at, at2 - at + 1).Length * 2 + 1;
        }

        public static List<Action<Player>> Options(Player player, out List<string> content)
        {
            var positions = Positions();
            string choices = positions[player.Pos].Item3;
            int ctr = 1;
            content = new List<string>();
            var options = new List<Action<Player>>();
            foreach (var letter in choices)
            {
                switch (letter)
                {
                    case 'N':
                        content.Add($"{ctr++}. Go North");
                        options.Add(Adventure.North);
                        break;
                    case 'E':
                        content.Add($"{ctr++}. Go East");
                        options.Add(Adventure.East);
                        break;
                    case 'W':
                        content.Add($"{ctr++}. Go West");
                        options.Add(Adventure.West);
                        break;
                    case 'S':
                        content.Add($"{ctr++}. Go South");
                        options.Add(Adventure.South);
                        break;
                    case 'V':
                        content.Add($"{ctr++}. Go back home");
                        options.Add(Adventure.ToVillage);
                        break;
                    case 'H':
                        content.Add($"{ctr++}. Meet Hiruzen");
                        options.Add(Adventure.ToHiruzen);
                        break;
                    default:
                        break;
                }
            }
            return options;
        }

        public static Dictionary<double, Tuple<int, int, string>> Positions()
        {
            var positions = new Dictionary<double, Tuple<int, int, string>>();
            positions.Add(0.1, new Tuple<int, int, string>(15, 15, "NE"));
            positions.Add(0.2, new Tuple<int, int, string>(13, 10, "TS"));
            positions.Add(0.3, new Tuple<int, int, string>(12, 4, "AE"));
            positions.Add(1.1, new Tuple<int, int, string>(25, 15, "NEW"));
            positions.Add(1.2, new Tuple<int, int, string>(25, 9, "NES"));
            positions.Add(1.3, new Tuple<int, int, string>(21, 4, "WS"));
            positions.Add(2.0, new Tuple<int, int, string>(38, 19, "N"));
            positions.Add(2.1, new Tuple<int, int, string>(38, 15, "NEWV"));
            positions.Add(2.2, new Tuple<int, int, string>(36, 9, "EWS"));
            positions.Add(2.3, new Tuple<int, int, string>(38, 5, "NE"));
            positions.Add(2.4, new Tuple<int, int, string>(36, 2, "BS"));
            positions.Add(3.1, new Tuple<int, int, string>(49, 15, "NEW"));
            positions.Add(3.2, new Tuple<int, int, string>(48, 9, "NWS"));
            positions.Add(3.3, new Tuple<int, int, string>(48, 5, "EW"));
            positions.Add(4.0, new Tuple<int, int, string>(72, 21, "NE"));
            positions.Add(4.1, new Tuple<int, int, string>(69, 15, "EWS"));
            positions.Add(4.2, new Tuple<int, int, string>(70, 10, "HE"));
            positions.Add(4.3, new Tuple<int, int, string>(65, 5, "EW"));
            positions.Add(5.0, new Tuple<int, int, string>(85, 21, "GW"));
            positions.Add(5.1, new Tuple<int, int, string>(82, 15, "NW"));
            positions.Add(5.2, new Tuple<int, int, string>(82, 10, "NWS"));
            positions.Add(5.3, new Tuple<int, int, string>(80, 5, "WS"));
            return positions;
    }

        public static Enemy[] Enemies()
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

        public static Armor[] Armors()
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

        public static Weapon[] Weapons()
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

        public static Weapon[] SevenSwords(Player player)
        {
            List<Weapon> weapons = new List<Weapon>()
            {
                new Kabutowari(),
                new Hiramekarei(),
                new Samehada(),
                new Kiba(),
                new Nuibari(),
                new Shibuki()
            };

            if (player.Name != "Kakashi Hatake")
            {
                weapons.Add(new Kubikiribōchō());
            }
            return weapons.ToArray();
        }

        public static Consumable[] Potions()
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

        public static Consumable[] Meals()
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

        public static Item[] AbuHassanItems()
        {
            Item[] items = new Item[]
            {
                new BulletproofVest(), new AK47(),
                new EnergyDrink("Red Bull", 50, 10, "You get wings")
            };
            return items;
        }
    }
}
