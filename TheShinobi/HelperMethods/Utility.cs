using TheShinobi.Characters.Enemies;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
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
                new Enemy("Hanzō", 7, 100, new FlakJacket(), new Kusarigama()),
                new Enemy("Daudi", 9, 100, new BulletproofVest(), new AK47()),
                new Enemy("Hocke", 9, 100, new BulletproofVest(), new AK47()),
                new Enemy("Deidara", 7, 100, new FlakJacket(), new Gunbai()),
                new Enemy("Ginkaku", 2, 100, new FlakJacket(), new Shichiseiken()),
                new Enemy("Haku", 4, 100, new FlakJacket(), new Crossbow()),
                new Enemy("Kabuto", 1, 100, new FlakJacket(), new ChakraBlade()),
                new Enemy("KaguyaOtsutsuki", 2, 100, new FlakJacket(), new FistsOfBones()),
                new Enemy("Kakuzu", 5, 100, new InfiniteArmor(), new Sword()),
                new Enemy("Kimimaro", 3, 100, new FlakJacket(), new FistsOfBones()),
                new Enemy("Kisame", 1, 100, new FlakJacket(), new Kunai()),
                new Enemy("Madara", 1, 100, new FlakJacket(), new Gunbai()),
                new Enemy("Nagato", 2, 100, new FlakJacket(), new Sword()),
                new Enemy("Obito", 2, 100, new FlakJacket(), new Shichiseiken()),
                new Enemy("Orochimaru", 5, 100, new ShinobiBattleArmor(), new Spear())
            };
            return enemies;
        }
    }
}
