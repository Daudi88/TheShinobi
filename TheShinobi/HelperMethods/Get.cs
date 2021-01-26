using System;
using System.Collections.Generic;
using System.Text;
using TheShinobi.Characters;
using TheShinobi.Characters.Enemies;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;

namespace TheShinobi.HelperMethods
{
    static class Get
    {
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
