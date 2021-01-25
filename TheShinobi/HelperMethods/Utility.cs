using TheShinobi.Characters.Enemies;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Potions;
using System;

namespace TheShinobi.HelperMethods
{
    static class Utility
    {
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
               // new Enemy("Orochimaru", 10, 100, new ShinobiBattleArmor(), new ChakraBlade()) Lägg i bossfight

            };
            return enemies;
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

        // Skapa GetArmors()

        public static Weapon[] GetWeapons()
        {
            Weapon[] weapons = new Weapon[]
            {
                new Crossbow(),
                //new ChakraBlade(), Stoppa i MeetHiruzen()
                // Fyll på med basic vapen (inte de exklusiva)
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
    }
}
