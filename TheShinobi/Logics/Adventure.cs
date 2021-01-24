using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Logics
{
    class Adventure
    {
        private static bool treasureTaken = false;
        private static bool haveYouMetHim = false;
        private static bool graveyardVisited = false;
        private static bool isRedBull = false;
        public static void GoOnAdventure()
        {
            Player player = Game.player;
            Console.WriteLine("\n\t You start your adventure by going north...");
            Console.WriteLine("\n\t Be carefull not to loose yourself in the wild!");
            player.Pos = 0.1;
            while (true)
            {
                player.Pos = Math.Round(player.pos, 1);
                bool outerExit = false;
                bool innerExit = true;
                if(player.Pos == 0.0)
                {
                    break;
                }
                else if (player.Pos == 0.1)
                {
                    while (!outerExit)
                    {
                        Console.WriteLine("\n\t What do you want to do?");
                        string[] content = new string[]
                        {
                            "1. Go North",
                            "2. Go East",
                            "3. Go West",
                            "4. Go back home",
                            "D. Show Details",
                            "B. Open Bacpack",
                            "M. Open Map"
                        };
                        Display.WithFrame("", content);
                        Console.Write("\t > ");
                        innerExit = true;
                        do
                        {
                            string choice = ColorConsole.ReadInBlue();
                            switch (choice.ToUpper())
                            {
                                case "1":
                                    Console.WriteLine("\t You went north...");
                                    player.Pos += 0.1;
                                    EncounterCheck();
                                    outerExit = true;
                                    break;
                                case "2":
                                    Console.WriteLine("\t You went east...");
                                    player.Pos += 1.0;
                                    EncounterCheck();
                                    outerExit = true;
                                    break;
                                case "3":
                                    Console.WriteLine("\t You went west...");
                                    player.Pos -= 1.2;
                                    EncounterCheck();
                                    outerExit = true;
                                    break;
                                case "4":
                                    Console.WriteLine("\t You went back home...");
                                    player.Pos -= 0.1;
                                    outerExit = true;
                                    break;
                                case "D":
                                    Display.Details();
                                    break;
                                case "B":
                                    if (!Display.Backpack())
                                    {
                                        innerExit = false;
                                    }
                                    break;
                                case "M":
                                    Display.Map();
                                    break;
                                default:
                                    Utility.TypeOverWrongDoings("Invalid choice. Try again!");
                                    innerExit = false;
                                    break;
                            }
                        } while (!innerExit);
                    }
                }
                else if (player.Pos == -1.1 || player.Pos == 1.1)
                {
                    while (!outerExit)
                    {
                        Console.WriteLine("\n\t What do you want to do?");
                        string[] content = new string[]
                        {
                          "1. Go North",
                            "2. Go East",
                            "3. Go West",
                            "D. Show Details",
                            "B. Open Backpack",
                            "M. Open Map"
                        };
                        Display.WithFrame("", content);
                        Console.Write("\t > ");
                        innerExit = true;
                        do
                        {
                            string choice = ColorConsole.ReadInBlue();
                            switch (choice.ToUpper())
                            {
                                case "1":
                                    Console.WriteLine("\t You went north...");
                                    if (player.Pos == -1.1)
                                    {
                                        player.Pos -= 0.1;
                                    }
                                    else
                                    {
                                        player.Pos += 0.1; 
                                    }
                                    EnCounterCheck();
                                    outerExit = true;
                                    break;
                                case "2":
                                    Console.WriteLine("\t You went east...");
                                    if (player.Pos == -1.1)
                                    {
                                        player.Pos += 1.2;
                                    }
                                    else
                                    {
                                        player.Pos += 1.0;
                                    }
                                    EncounterCheck();
                                    outerExit = true;
                                    break;
                                case "3":
                                    Console.WriteLine("\t You went west...");
                                    player.pos -= 1.0;
                                    EncounterCheck();
                                    outerExit = true;
                                    break;
                                case "D":
                                    Display.Details();
                                    break;
                                case "B":
                                    if (!Display.Backpack())
                                    {
                                        innerExit = false;
                                    }
                                    break;
                                case "M":
                                    Display.map();
                                    break;
                                default:
                                    Utility.TypeOverWrongDoings("Invald choise. Try again!");
                                    innerExit = false;
                                    break;
                            }

                        } while (!innerExit);

                    }
                }
            }
        }
    }
}
