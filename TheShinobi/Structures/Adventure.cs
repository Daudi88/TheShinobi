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
        static string soundLocation8 = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Battle.wav");

        public static SoundPlayer villagePlayer = new SoundPlayer(soundLocation1);
        static SoundPlayer adventurePlayer = new SoundPlayer(soundLocation2);
        static SoundPlayer treasurePlayer = new SoundPlayer(soundLocation3);
        static SoundPlayer abuHassanPlayer = new SoundPlayer(soundLocation4);
        static SoundPlayer hiruzenPlayer = new SoundPlayer(soundLocation5);
        static SoundPlayer graveyardPlayer = new SoundPlayer(soundLocation6);
        public static SoundPlayer endPlayer = new SoundPlayer(soundLocation7);
        static SoundPlayer battlePlayer = new SoundPlayer(soundLocation8);

        private static bool isTreasureTaken = false;
        private static bool isGraveyardVisited = false;
        private static bool isHiruzenVisited = false;

        public static void Menu(Player player)
        {
            player.Pos += 0.1;
            adventurePlayer.PlayLooping();
            string story = $"\n\t All of your clan members are away to practice at Daisan Enshūjō. \n" +
                "\t They are five days away in the south and have taken most of the \n" +
                "\t village's equipment with them. \n" +
                "\n\t You will have to rescue Hanare on your own! \n" +
                "\n\t You took the little gear you had and are now on your way to leave \n" +
                "\t the Hidden Leaf Village. \n" +
                "\n\t Hanare's kidnappers brought her towards the mountains up in the north." +
                "\n\t It is now your duty to quickly kill all the enemies intruding your territory \n " +
                "\t and search for better gear so you can go and rescue her! \n" +
                "\n\t * There are rumors about treasures containing some usefull loot outside the village." +
                "\n\t * Also take care and dont loose yourself in the wild...";

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
                Battle(player, true);
                Display.Credits(player);
            }
            else
            {
                ColorConsole.WriteDelayedLine("\t You are not strong enough to fight this opponent...", ConsoleColor.Red, blink: true);
            }
            return false;
        }

        public static void Battle(Player player, bool isBoss = false)
        {
            Enemy[] enemies = Get.Enemies().Where(e => e.Level <= player.Level).ToArray();
            Enemy enemy = enemies[random.Next(enemies.Length)];
            string[] stories = Get.FightStories(enemy);
            string story = stories[random.Next(stories.Length)];
            
            if (isBoss)
            {
                enemy = new Enemy("Orochimaru", "", 10, new ShinobiBattleArmor(), new ChakraBlade());
                story = Get.OrochimaruStory();
            }

            int top = Console.CursorTop;
            int bottom = 0;
            int textTop = top + 4;
            Console.SetCursorPosition(0, textTop);
            ColorConsole.WriteDelayedLine(story, ConsoleColor.Yellow);
            battlePlayer.PlayLooping();
            bool exit = false;
            string battleText = "";
            player.Stamina.Current = 1;
            while (!exit)
            {
                textTop = top + 4;
                bool reset = true;
                Display.BattleFrame(player, enemy, "[Red]BATTLE[/Red]", 6, top, reset);
                if (player.Stamina.Current > 0)
                {
                    Console.WriteLine("\t Choose a weapon or jutsu to attack with:");
                    int ctr = 1;
                    Console.WriteLine($"\t {ctr++}. {player.Weapon.Name} {player.Weapon.BonusText()}");
                    foreach (var jutsu in player.Ninjutsus)
                    {
                        Console.WriteLine($"\t {ctr++}. {jutsu}");
                    }
                    Console.Write("\t > ");
                    bottom = Console.CursorTop + 1;
                    battleText = player.Attack(enemy);
                    ColorConsole.WriteEmbeddedSetDelayed(battleText, textTop, false, delay: 0);
                    Display.BattleFrame(player, enemy, "[Red]BATTLE[/Red]", 6, top);
                    for (int i = 0; i < ctr + 1; i++)
                    {
                        Console.WriteLine("\t                                                       ");
                    }
                }

                if (enemy.Stamina.Current > 0)
                {
                    textTop++;
                    battleText = enemy.Attack(player);
                    ColorConsole.WriteEmbeddedSetDelayed(battleText, textTop, false, delay: 0);
                    Display.BattleFrame(player, enemy, "[Red]BATTLE[/Red]", 6, top);
                    if (player.Stamina.Current <= 0)
                    {
                        textTop++;
                        battleText = $"You were defeated by {enemy.Name}...";
                        ColorConsole.WriteSetDelayed(battleText, textTop);
                        WaitSetForUser(bottom - 4);
                        exit = true;
                        Display.Credits(player);
                    }
                }
                else
                {
                    textTop++;
                    battleText = $"You defeated {enemy.Name}!";
                    ColorConsole.WriteSetDelayed(battleText, textTop);
                    WaitSetForUser(bottom - 6);
                    exit = true;
                    if (isBoss)
                    {
                        Display.Credits(player);
                    }
                }
            }

            if (energyDrink.IsEnergized)
            {
                EnergyDip(player);
            }
        }
    }
}



