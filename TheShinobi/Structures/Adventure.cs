﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using static TheShinobi.HelperMethods.Utility;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;
using TheShinobi.Characters.Enemies;

namespace TheShinobi.Structures
{
    class Adventure
    {
        /* This class contains the following methods:
         * Menu()               - 
         * Graveyard()          -
         * Treasure()           -
         * AbuHassan()          -
         * ...
         */
        static string soundLocation1 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\HiddenLeafVillage.Wav");
        static string soundLocation2 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Adventure.wav");
        static string soundLocation3 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Treasure.wav");
        static string soundLocation4 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\AbuHassan.wav");
        static string soundLocation5 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Hiruzen.wav");
        static string soundLocation6 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Graveyard.wav");
        static string soundLocation7 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\KakashiFightSongUP3.wav");


        public static SoundPlayer villagePlayer = new SoundPlayer(soundLocation1);
        static SoundPlayer adventurePlayer = new SoundPlayer(soundLocation2);
        static SoundPlayer treasurePlayer = new SoundPlayer(soundLocation3);
        static SoundPlayer abuHassanPlayer = new SoundPlayer(soundLocation4);
        static SoundPlayer hiruzenPlayer = new SoundPlayer(soundLocation5);
        static SoundPlayer graveyardPlayer = new SoundPlayer(soundLocation6);
        public static SoundPlayer endPlayer = new SoundPlayer(soundLocation7);


        private static bool isTreasureTaken = false;
        private static bool isGraveyardVisited = false;
        private static bool isHiruzenVisited = false;

        public static void Menu(Player player)
        {
            //Adventure.villagePlayer.PlayLooping();
            //Display.Title();
            //Player player = CharacterCreation();
            //string intro = $"\n\t You, {player.Name}, wake up in the Hidden Leaf Village and sense that something is wrong!" +
            //    "\n\t Kaguya Otsutsuki have kidnapped Hanare and taken her to his cave in the mountains." +
            //    "\n\t It is your duty to find and rescue her!";
            //ColorConsole.WriteDelayedLine(intro, ConsoleColor.Yellow, 40);
            //Console.WriteLine("\t [Press enter to continue]");
            //Console.ReadKey(true);
            //HiddenLeafVillage.Menu(player);

            //string intro = $"\n\t You, {player.Name}, wake up from a womans scream's and it sound's as if she is beeing taken away!!!" +
            //"\n\t You hear the villagers scream that somebody kidnapped Hanare!" +
            //    "\n\t It is your duty to find and rescue her!";


            player.Pos += 0.1;
            adventurePlayer.PlayLooping();
            string story = $"\n\t You geared up quickly and are now on your way to leave the Hidden leaf Village! \n" +
                "\n\t Orochimaru have kidnapped Hanare and taken her to his cave way up in the north.\n" +
                "\n\t It is your duty to find and rescue her!\n" +
                "\n\t You will probobly need a better Weapon and Armor \n" +
                "\t There are rumors about treasures containing some usefull loot outside the village. \n" +
                "\n\t Make sure you are strong and equipped enough for when you meet Hanare's Kidnapper.\n" +
                "\n\t Also take care and dont loose yourself in the wild..." +
                "\n\t * There are rumors about treasures containing some usefull loot." +
                "\n\t * Make sure you are strong and equipped enough for when you meet Hanare's Kidnapper." +
                "\n\t * Take care and make sure you don't loose yourself in the wild...";

            ColorConsole.WriteDelayedLine(story, ConsoleColor.Yellow, blink: true);
            bool exit = false;
            while (!exit)
            {
                player.Pos = Math.Round(player.Pos, 1);
                Console.WriteLine("\n");
                var options = Get.Options(player, out List<string> content);
                Display.WithFrame(content, "[Yellow]ADVENTURE[/Yellow]", true);
                ChooseANumber(content.Count, out int choice, player, true);
                if (choice > 0)
                {
                    Console.SetWindowPosition(0, Console.CursorTop - V);
                    exit = options[choice - 1].Invoke(player);
                }
            }
        }

        public static bool North(Player player)
        {
            player.Pos += 0.1;
            CheckForEncounter(player);
            return false;
        }

        public static bool East(Player player)
        {
            player.Pos += 1.0;
            CheckForEncounter(player);
            return false;
        }

        public static bool West(Player player)
        {
            player.Pos -= 1.0;
            CheckForEncounter(player);
            return false;
        }

        public static bool South(Player player)
        {
            player.Pos -= 0.1;
            CheckForEncounter(player);
            return false;
        }

        public static bool ToVillage(Player player)
        {
            villagePlayer.PlayLooping();
            isVisitingVillage = true;
            player.Pos -= 0.1;
            return true;
        }

        public static bool ToGraveyard(Player player)
        {
            graveyardPlayer.PlayLooping();
            player.Pos += 1.0;
            if (!isGraveyardVisited)
            {
                Console.SetWindowPosition(0, Console.CursorTop - V);
                Weapon[] swords = Get.SevenSwords(player);
                Weapon sword = swords[random.Next(swords.Length)];
                AddToBackpack(player, sword);
                ColorConsole.WriteEmbeddedDelayed($"\t You find the [Yellow]{sword.Name}[/Yellow], a sword of the Seven Swordsmen!\n");
                isGraveyardVisited = true;
            }
            else
            {
                ColorConsole.WriteOver("\t The graveyard is dead silent.", ConsoleColor.Red);
            }
            adventurePlayer.PlayLooping();
            return false;
        }
        public static bool ToTreasure(Player player)
        {
            treasurePlayer.PlayLooping();
            player.Pos += 0.1;
            if (!isTreasureTaken)
            {
                Console.SetWindowPosition(0, Console.CursorTop - 30);
                int treasure = random.Next(1000, 10000);
                player.Ryō += treasure;
                ColorConsole.WriteEmbeddedDelayed($"\t You found a treasure and gained [Yellow]{treasure}[/Yellow] gold!\n");                
                isTreasureTaken = true;
            }
            else
            {
                ColorConsole.WriteOver("\t Somebody has allready taken the treasure...", ConsoleColor.Red);
            }
            adventurePlayer.PlayLooping();
            return false;
        }
        public static bool ToAbuHassan(Player player)
        {
            abuHassanPlayer.PlayLooping();
            player.Pos -= 1.0;
            Console.SetWindowPosition(0, Console.CursorTop - V);
            ColorConsole.WriteDelayedLine("\n\n\t Welcome to Abu Hassan's one stop shop for everything" +
                "\n\t a real Shinobi from the hood could ever want!");
            int top = Console.CursorTop;
            int left = 9;
            while (true)
            {
                Console.SetCursorPosition(0, top);
                ColorConsole.WriteDelayedLine("\t What do you want to do?");
                List<string> options = new List<string>()
                {
                    "1. Buy some stuff",
                    "2. Sell some stuff"
                };
                Display.WithFrame(options, "[Yellow]SHOP[/Yellow]", ending: "Leave");
                int bottom = Console.CursorTop;
                if (ChooseANumber(options.Count, out int choice, ending: true))
                {
                    switch (choice)
                    {
                        case 1:
                            Remove(left, top);
                            Store.BuyItems(player, "item", Get.AbuHassanItems());
                            break;
                        case 2:
                            if (player.Backpack.Count > 0)
                            {
                                Remove(left, top);
                            }
                            Store.SellItems(player);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ColorConsole.WriteDelayedLine("\t Thank you for visiting Abu Hassan's!", ConsoleColor.Yellow);
                    break;
                }
            }
            adventurePlayer.PlayLooping();
            return false;
        }

        public static bool ToHiruzen(Player player)
        {
            hiruzenPlayer.PlayLooping();
            player.Pos -= 1.0;
            if (!isHiruzenVisited)
            {
                Console.SetWindowPosition(0, Console.CursorTop - V);
                Weapon weapon = new Kusarigama();
                Armor armor = new InfiniteArmor();
                Consumable[] potions = Get.Potions();
                Consumable potion = potions.Where(p => p.Name.Contains("Superior")).First();
                potion.Quantity = random.Next(5, 11);
                string story = "\n\t An old man with white beard appears in front of you." +
                    "\n\t The man, dressed in red and white, looks upon you as if" +
                    "\n\t he was expecting your arrival with a big smile on his face." +
                    "\n\t You instantly recognice the old man as Hiruzen Sarutobi!\n";
                ColorConsole.WriteDelayed(ConsoleColor.Yellow, content: story);
                string story2 = $"\n\t {player.Name}, he says while smoking on his pipe..." +
                    "\n\t There is little time and you need to go on with your quest" +
                    "\n\t to save Hanare!Take these items and be on your way!\n";
                ColorConsole.WriteDelayed(ConsoleColor.Yellow, content: story2);
                ColorConsole.WriteEmbeddedDelayed($"\n\t You get a [Yellow]{weapon.Name}[/Yellow], " +
                    $"an [Yellow]{armor.Name}[/Yellow] and [Yellow]{potion.Quantity} {potion.Name}s[/Yellow].\n");
                player.Backpack.Add(weapon);
                player.Backpack.Add(armor);
                player.Backpack.Add(potion);
                isHiruzenVisited = true;
            }
            else
            {
                ColorConsole.WriteOver("\t Hiruzen smokes his pipe...", ConsoleColor.Red);
            }
            adventurePlayer.PlayLooping();
            return false;
        }

        public static void CheckForEncounter(Player player)
        {
            Console.SetWindowPosition(0, Console.CursorTop - V);
            int chance = random.Next(0, 10);
            if (chance < 2)
            {
                string[] stories = Get.NoFightStories();
                string story = stories[random.Next(stories.Length)];
                ColorConsole.WriteDelayedLine(story, ConsoleColor.Yellow, blink: true);                
            }
            else
            {
                Battle(player);
            }
        }

        public static bool BossEncounter(Player player)
        {
            if (player.Level >= 10)
            {
                // FightTheBoss(player);
                Display.Credits(player);
            }
            else
            {
                ColorConsole.WriteDelayedLine("\t You are not strong enough to fight this opponent...", ConsoleColor.Red, blink: true);                
            }
            return false;
        }

        private static void Battle(Player player)
        {
            Enemy[] enemies = Get.Enemies().Where(e => e.Level <= player.Level).ToArray();
            Enemy enemy = enemies[random.Next(enemies.Length)];
            string[] stories = Get.FightStories(enemy);
            string story = stories[random.Next(stories.Length)];
            ColorConsole.WriteDelayedLine(story, ConsoleColor.Yellow, blink: true);
            bool exit = false;
            while (!exit)
            {
                int top = Console.CursorTop;
                Console.SetCursorPosition(0, top);
                





                if (energyDrink.IsEnergized)
                {
                    EnergyDip(player);
                }
                break;
            }
        }

        //private static void Battle(Player player)
        //{
        //    if (isRedBull)
        //    {
        //        player.Defence += 2;
        //    }
        //    Character[] enemies = Utility.GetEnemies().Where(e => e.Level <= player.Level).ToArray();
        //    Character enemy = enemies[Utility.RollDice(enemies.Length)];
        //    int battleCtr = 0;
        //    List<string> content = new List<string>();
        //    while (enemy.Hp > 0)
        //    {
        //        content.Clear();
        //        if (battleCtr == 0)
        //        {
        //            content.Add($"You have encountered {enemy.Name}!");
        //            battleCtr++;
        //        }
        //        int damage = player.Attack(enemy);
        //        enemy.Hp -= damage;
        //        content.Add($"You hit {enemy.Name} with your {player.Weapon.Name} dealing {damage} damage!");
        //        if (enemy.Hp <= 0)
        //        {
        //            content.Add($"You defeated {enemy.Name}!");
        //            content.Add($"You gained {enemy.Exp} Exp and {enemy.Gold} gold!");
        //            player.Exp += enemy.Exp;
        //            player.Gold += enemy.Gold;

        //            if (player.Exp >= player.MaxExp)
        //            {
        //                content.Add("Well Done! You leveled up!");
        //                player.LevelUp();
        //            }
        //        }
        //        else
        //        {
        //            damage = enemy.Attack(player);
        //            player.Hp -= damage;
        //            content.Add($"{enemy.Name} hits you dealing {damage} damage!");
        //            if (player.Hp <= 0)
        //            {
        //                content.Add($"You were defeated by { enemy.Name}!");
        //                content.Add("You lose...");
        //            }
        //            else
        //            {
        //                if (player.Hp < player.MaxHp)
        //                {
        //                    content.Add($"{player.Name} Hp: [red]{player.Hp}[/red]");
        //                }
        //                else
        //                {
        //                    content.Add($"{player.Name} Hp: {player.Hp}");
        //                }
        //                content.Add($"{enemy.Name} Hp: {enemy.Hp}");
        //            }
        //        }
        //        Display.WithFrame("[red]BATTLE[/red]", content.ToArray());

        //        if (player.Hp <= 0)
        //        {
        //            Display.LoseScreen();
        //        }
        //        else
        //        {
        //            Console.WriteLine("\t [Press enter to continue]");
        //            Console.ReadLine();
        //        }
        //        Console.SetWindowPosition(0, Console.CursorTop - 30);
        //    }
        //    if (isRedBull == true)
        //    {
        //        player.Defence -= 2;
        //        isRedBull = false;
        //    }
        //}
    }
}

//        private static void BossFight(Player player)
//        {
//            Character boss = new Enemy("Orochimaru", 10, 100, new ShinobiBattleArmor(), new ChakraBlade());
//            player.Hp -= 1000;
//            Console.WriteLine("\n\t You were not ready!");
//            string[] haha = new string[] { "Ha! ", "Ha! ", "Ha! ", };
//            Console.SetWindowPosition(0, Console.CursorTop - 30);
//            Console.Write("\t ");
//            Thread.Sleep(1000);
//            foreach (var ha in haha)
//            {
//                ColorConsole.WriteInRed(ha);
//                Thread.Sleep(1000);
//            }
//        }
//    }
//}
