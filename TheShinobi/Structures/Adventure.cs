using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using TheShinobi.Items.Weapons;
using System;
using System.Threading;
using System.Collections.Generic;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using System.Linq;

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

        private static bool isTreasureTaken;
        private static bool isGraveyardVisited;
        private static bool haveYouMetHim;

        public static void Menu(Player player)
        {
            var positions = Get.Positions();
            string options = positions[0.1].Item3;
        }

        private static void Graveyard(Player player)
        {
            if (!isGraveyardVisited)
            {
                Weapon[] swords = Get.SevenSwords(player);
                Weapon sword = swords[Utility.random.Next(swords.Length)];
                ColorConsole.WriteLine($"\t You find {sword.Name}, a sword of the Seven Swordsmen!", ConsoleColor.Yellow);
                Utility.AddToBackpack(player, sword);
                isGraveyardVisited = true;
                Thread.Sleep(1800);
            }
            else
            {
                ColorConsole.TypeOver("\t The graveyard is dead silent.", ConsoleColor.Red);
            }
        }
        private static void Treasure(Player player)
        {
            if (!isTreasureTaken)
            {
                int treasure = Utility.random.Next(1000, 10000);
                player.Gold += treasure;
                Console.WriteLine($"\n\t You found a treasure and gained {treasure} gold!");
                isTreasureTaken = true;
                Thread.Sleep(1800);
            }
            else
            {
                ColorConsole.TypeOver("\t Somebody has allready taken the treasure...", ConsoleColor.Red);
            }
        }
        public static void AbuHassan(Player player)
        {
            Console.WriteLine("\n\t Welcome to Abu Hassan's one stop shop for everything\n\t a real Shinobi from the hood could ever want!");
            int top = Console.CursorTop;
            while (true)
            {
                Console.SetCursorPosition(0, top);
                Console.WriteLine("\t What do you want to do?");
                List<string> options = new List<string>()
                {
                    "1. Buy some stuff",
                    "2. Sell some stuff"
                };
                Display.WithFrame(options, "[Yellow]SHOP[/Yellow]", ending: "Leave");
                int bottom = Console.CursorTop;
                if (Utility.ChooseANumber(options.Count, out int choice, ending: true))
                {
                    switch (choice)
                    {
                        case 1:
                            Utility.Remove(Utility.left, top);
                            Store.BuyItems(player, "item", Get.AbuHassanItems());
                            break;
                        case 2:
                            if (player.Backpack.Count > 0)
                            {
                                Utility.Remove(Utility.left, top);
                            }
                            Store.SellItems(player);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ColorConsole.WriteLine("\t Thank you for visiting Abu Hassan's!\n", ConsoleColor.Yellow);
                    Thread.Sleep(1800);
                    Console.SetWindowPosition(0, Console.CursorTop - Utility.V);
                    break;
                }
            }
        }
        public static void MeetHiruzen(Player player)
        {
            if (!haveYouMetHim)
            {
                Weapon weapon = new Kusarigama();
                Armor armor = new InfiniteArmor();
                Consumable[] potions = Get.Potions();
                Consumable potion = potions.Where(p => p.Name.Contains("Superior")).First();
                potion.Quantity = Utility.random.Next(5, 11);
                string story = "\n\t An old man with white beard appears in front of you." +
                    "\n\t The man, dressed in red and white, looks upon you as if" +
                    "\n\t he was expecting your arrival with a big smile on his face." +
                    "\n\t You instantly recognice the old man as Hiruzen Sarutobi!";        
                Display.Delayed(story, color: ConsoleColor.Yellow);
                string story2 = $"\n\t {player.Name}, he says while smoking on his pipe..." +
                    "\n\t There is little time and you need to go on with your quest" +
                    "\n\t to save Hanare!Take these items and be on your way!";
                Display.Delayed(story2, color: ConsoleColor.Yellow);
                ColorConsole.WriteEmbeddedColor($"\n\t You get a [DarkCyan]{weapon.Name}[/DarkCyan], " +
                    $"an [DarkCyan]{armor.Name}[/DarkCyan] and [DarkCyan]{potion.Quantity} {potion.Name}s[/DarkCyan].\n");
                player.Backpack.Add(weapon);
                player.Backpack.Add(armor);
                player.Backpack.Add(potion);
                haveYouMetHim = true;
                Console.WriteLine("\t [Press enter to continue]");
                Console.ReadKey(true);
            }
            else
            {
                ColorConsole.TypeOver("\t Hiruzen smokes his pipe...", ConsoleColor.Yellow);
            }
            //Console.SetWindowPosition(0, Console.CursorTop - 30);

        }
        public static void EncounterCheck()
        {
            int chance = Utility.random.Next(1, 10);
            if (chance <= 5)
            {
                int noFight = Utility.random.Next(1, 6);
                if (noFight == 1)
                {
                    Console.WriteLine("\t As you walk up the mountain trail you sense a group of ninjas from the Uchicha clan ahead.");
                    Console.WriteLine("\t You easily sneak past them on your way to rescue Hanare!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (noFight == 2)
                {
                    Console.WriteLine("\t You hear something in the distance and use Mangekyō to see a group of Ninjas lying in ambush for travelers.");
                    Console.WriteLine("\t If it wasnt that you are in a rush to rescue Hanare you would have made minced meat of them all!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (noFight == 3)
                {
                    Console.WriteLine("\t You use your senses and feel that no enemy is near, you set up camp and light a fire");
                    Console.WriteLine("\t You eat your delicious Green Chilli Burger while watching the Sunset ove The Hidden Leaf Village...");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (noFight == 4)
                {
                    Console.WriteLine("\t As the sun sets over the mountains you see a clan who has setup camp on your land!");
                    Console.WriteLine("\t There is no time to engage them now, first you must rescue Hanare!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (noFight == 5)
                {
                    Console.WriteLine("\t As you walk through The Shikkotsu Forest you sense Hanare is further North!");
                    Console.WriteLine("\t You start running towards the montains!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (noFight == 6)
                {
                    Console.WriteLine("\t You aproach a empty camp, the fire is still smoking a little and there are signs of someone who has been tied up!");
                    Console.WriteLine("\t In the soil you see one of hanares black glove, you follow their trail...");
                    Console.WriteLine("\t [Press enter to continue]");
                }
            }
            else
            {
                int fight = Utility.random.Next(1, 5);
                if (fight == 1)
                {
                    Console.WriteLine("\t You walked down the bushy trail and sence there is troubble ahead!");
                    Console.WriteLine($"\t You can smell that dirty Ōtsutsuki member miles away and you attack instantly!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (fight == 2)
                {
                    Console.WriteLine("\t While climbing up the mountains in search of Hanare you get ambushed by some filthy Team Doso villains!");
                    Console.WriteLine("\t You attack, maybe they know more about Hanare´s kidnapping!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (fight == 3)
                {
                    Console.WriteLine("\t You have walked for hours when you decide to set up camp at the lake.\n\t when some low life from the Akatsuki clan attacks you!");
                    Console.WriteLine("\t Four of them run away when they see it is you but one stays to fight!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (fight == 4)
                {
                    Console.WriteLine("\t The sky is full of dark clouds and when lightning strikes hits the ground vibrates...");
                    Console.WriteLine("\t As you set up camp in the mountains to escape the bad weather for the night some villains from the Uchicha clan attacks!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                else if (fight == 5)
                {
                    Console.WriteLine("\t The sky is blue and the sun is shining. As you walk past the graveyard you hear them!");
                    Console.WriteLine("\t Some lowlife from the Aburame clan attacks you!");
                    Console.WriteLine("\t [Press enter to continue]");
                }
                Console.WriteLine();
                //Battle();
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

//        private static bool treasureTaken = false;
//        private static bool haveYouMetHim = false;
//        private static bool graveyardVisited = false;
//        private static bool isRedBull = false;
//        public static void GoOnAdventure()
//        {
//            Player player = Game.player;
//            Console.WriteLine("\n\t You start your adventure by going north...");
//            Console.WriteLine("\n\t Be carefull not to loose yourself in the wild!");
//            player.Pos = 0.1;
//            while (true)
//            {
//                player.Pos = Math.Round(player.pos, 1);
//                bool outerExit = false;
//                bool innerExit = true;
//                if (player.Pos == 0.0)
//                {
//                    break;
//                }
//                else if (player.Pos == 0.1)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go North",
//                            "2. Go East",
//                            "3. Go West",
//                            "4. Go back home",
//                            "D. Show Details",
//                            "B. Open Bacpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went north...");
//                                    player.Pos += 0.1;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went east...");
//                                    player.Pos += 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "3":
//                                    Console.WriteLine("\t You went west...");
//                                    player.Pos -= 1.2;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "4":
//                                    Console.WriteLine("\t You went back home...");
//                                    player.Pos -= 0.1;
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == -1.1 || player.Pos == 1.1)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                          "1. Go North",
//                            "2. Go East",
//                            "3. Go West",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went north...");
//                                    if (player.Pos == -1.1)
//                                    {
//                                        player.Pos -= 0.1;
//                                    }
//                                    else
//                                    {
//                                        player.Pos += 0.1;
//                                    }
//                                    EnCounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went east...");
//                                    if (player.Pos == -1.1)
//                                    {
//                                        player.Pos += 1.2;
//                                    }
//                                    else
//                                    {
//                                        player.Pos += 1.0;
//                                    }
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "3":
//                                    Console.WriteLine("\t You went west...");
//                                    player.pos -= 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invald choise. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }

//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == -1.2)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go North",
//                            "2. Go East",
//                            "3. Go South",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went north...");
//                                    player.Pos -= 0.1;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went east...");
//                                    player.Pos += 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "3":
//                                    Console.WriteLine("\t You went south...");
//                                    player.Pos += 0.1;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == 0.2 || player.Pos == 2.1 || player.Pos == 1.3)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go East",
//                            "2. Go South",
//                            "3. Go West",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went east...");
//                                    player.Pos += 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went South...");
//                                    player.Pos -= 0.1;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "3":
//                                    Console.WriteLine("\t You went west...");
//                                    if (player.Pos == 0.2)
//                                    {
//                                        player.Pos -= 1.4;
//                                    }
//                                    else
//                                    {
//                                        player.Pos -= 1.0;
//                                    }
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == 1.2 || player.Pos == 3.2)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go North",
//                            "2. Go South",
//                            "3. Go West",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went north...");
//                                    player.Pos += 0.1;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went south...");
//                                    player.Pos -= 0.1;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "3":
//                                    Console.WriteLine("\t You went west...");
//                                    player.Pos -= 1.0;
//                                    if (player.Pos == 2.2)
//                                    {
//                                        MeetHiruzen(player);
//                                    }
//                                    else
//                                    {
//                                        EncounterCheck();
//                                    }
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == -1.3 || player.Pos == 3.3)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go South",
//                            "2. Go West",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went south...");
//                                    if (player.Pos == -1.3)
//                                    {
//                                        player.Pos += 0.1;
//                                    }
//                                    else
//                                    {
//                                        player.Pos -= 0.1;
//                                    }
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went west...");
//                                    player.Pos -= 1.0;
//                                    if (player.Pos == -2.3)
//                                    {
//                                        AbuHassansShop(player);
//                                    }
//                                    else
//                                    {
//                                        EncounterCheck();
//                                    }
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == -2.1 || player.Pos == 2.0 || player.Pos == 0.3)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go North",
//                            "2. Go East",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went north...");
//                                    if (player.Pos == -2.1)
//                                    {
//                                        player.Pos -= 0.1;
//                                    }
//                                    else
//                                    {
//                                        player.Pos += 0.1;
//                                    }
//                                    if (player.Pos == 0.4)
//                                    {
//                                        BossEncounter(player);
//                                    }
//                                    else if (player.Pos == -2.2)
//                                    {
//                                        Treasure(player);
//                                    }
//                                    else
//                                    {
//                                        EncounterCheck();
//                                    }
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went east...");
//                                    player.Pos += 1.0;
//                                    if (player.Pos == 3.0)
//                                    {
//                                        Graveyard(player);
//                                    }
//                                    else
//                                    {
//                                        EncounterCheck();
//                                    }
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == 3.1)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go North",
//                            "2. Go West",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went north...");
//                                    player.Pos += 0.1;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went west...");
//                                    player.Pos -= 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == 2.3)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go East",
//                            "2. Go West",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went east...");
//                                    player.Pos += 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "2":
//                                    Console.WriteLine("\t You went west...");
//                                    player.Pos -= 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else if (player.Pos == 2.2 || player.Pos == -2.3 || player.Pos == -3.2)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go East",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        while (!innerExit)
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went east...");
//                                    player.Pos += 1.0;
//                                    EncounterCheck();
//                                    innerExit = true;
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    innerExit = true;
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = true;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    innerExit = true;
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    break;
//                            }
//                        }
//                    }
//                }
//                else if (player.Pos == 3.0)
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go West",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went west...");
//                                    player.Pos -= 1.0;
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//                else
//                {
//                    while (!outerExit)
//                    {
//                        Console.WriteLine("\n\t What do you want to do?");
//                        string[] content = new string[]
//                        {
//                            "1. Go South",
//                            "D. Show Details",
//                            "B. Open Backpack",
//                            "M. Open Map"
//                        };
//                        Display.WithFrame("", content);
//                        Console.Write("\t > ");
//                        innerExit = true;
//                        do
//                        {
//                            string choice = ColorConsole.ReadInBlue();
//                            switch (choice.ToUpper())
//                            {
//                                case "1":
//                                    Console.WriteLine("\t You went south...");
//                                    if (player.Pos == -2.2)
//                                    {
//                                        player.Pos += 0.1;
//                                    }
//                                    else
//                                    {
//                                        player.Pos -= 0.1;
//                                    }
//                                    EncounterCheck();
//                                    outerExit = true;
//                                    break;
//                                case "D":
//                                    Display.Details();
//                                    break;
//                                case "B":
//                                    if (!Display.Backpack())
//                                    {
//                                        innerExit = false;
//                                    }
//                                    break;
//                                case "M":
//                                    Display.Map();
//                                    break;
//                                default:
//                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
//                                    innerExit = false;
//                                    break;
//                            }
//                        } while (!innerExit);
//                    }
//                }
//            }
//        }



//        private static void AbuHassansShop(Player player)
//        {
//            Console.WriteLine("\n\t Welcome to Abu hassan's one stop shop for everyting a good adventurer could ever need");
//            Console.WriteLine("\t Welcome back again soon!");
//            Console.WriteLine("\t [Press enter to go back the way you came]");
//            Console.ReadLine();
//        }




//        

//        private static void BossEncounter(Player player)
//        {
//            if (player.Level >= 10)
//            {
//                FightTheBoss(player);
//                if (player.Hp > 0)
//                {
//                    Display.WinScreen();
//                }
//                else
//                {
//                    Display.LoseScreen();
//                }
//            }
//            else
//            {
//                Console.WriteLine("\t You are not strong enough to fight the opponent...");
//                Console.WriteLine("\t [Press enter to continue]");
//                Console.ReadLine();
//            }

//        }

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
