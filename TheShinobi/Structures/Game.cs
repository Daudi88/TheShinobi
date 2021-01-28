﻿using System;
using System.Linq;
using System.Text;
using TheShinobi.Abilities;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Structures
{
    class Game
    {
        /* This class contains the following methods:
         * Setup()              - Sets the Console up to use UTF8, title and 
         *                        console window size and Starts the music. 
         * Test()               - Used for testing parts of the game.
         * Start()              - Starts the game storyline and opens up the 
         *                        Hidden Leaf Village menu with player choises.
         * CaracterCreation()   - Lets the player choose name and checks for GodMode.
         * GodMode()            - If the player name equals Robin he becomes Kakashi
         *                        Hatake and have a lot of extras from the start.
         * ExitGame()           - Exits the game.
         */

        public void Setup()
        {
            Console.Title = "The Shinobi";
            Console.SetWindowSize(130, 50);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
        }

        public void Test()
        {
            Player player = CharacterCreation();
            Adventure.endPlayer.PlayLooping();
            ColorConsole.WriteDelayed(ConsoleColor.Yellow, delay: 80, exitable: false, content: Get.EndStory(player));
            Console.ReadLine();
        }

        public void Start()
        {
            Adventure.villagePlayer.PlayLooping();
            Display.Title();
            Player player = CharacterCreation();
            string intro = $"\n\t You {player.Name} wake up from a woman screaming! \n" +
            "\t It sound's as if she is beeing killed!!! \n" +
            "\n\t Frightened villagers scream for help “somebody is kidnapping Hanare!“" +
            "\n\t After some intense fighting the attackers manages to vanish north towards the mountains!" +
            "\n\t You couldnt reach them in time... \n" +
            "\n\t It is still dark outside when you prepare to leave The Hidden Leaf Village" +
            "\n\t in search of Hanare and her soon to be dead kidnappers...\n" +
            "\n\t You will probobly need better Weapon's and Armor's! " +
            "\n\t * There are rumors about treasures containing some usefull loot." +
            "\n\t * Make sure you are strong and equipped enough for when you meet Hanare's Kidnapper." +
            "\n\t * Take care and make sure you don't loose yourself in the wild...";
            ColorConsole.WriteDelayedLine(intro, ConsoleColor.Yellow, 40);
            Display.Blinking("\t [Press enter to continue]");
            //Console.WriteLine("\t [Press enter to continue]");
            //Console.ReadKey(true);
            HiddenLeafVillage.Menu(player);
        }

        private static Player CharacterCreation()
        {
            Console.WriteLine("\n\t What is your name?");
            Console.Write("\t > ");
            string name = "";
            while (true)
            {
                name = ColorConsole.ReadLine();
                if (name.Any(char.IsDigit))
                {
                    ColorConsole.WriteOver("\t The name cannot contain digits. Try again!", ConsoleColor.Red);
                }
                else if (name.Length < 3)
                {
                    ColorConsole.WriteOver("\t The name is too short. Try again!", ConsoleColor.Red);
                }
                else if (name.Length > 12)
                {
                    ColorConsole.WriteOver("\t The name is too long. Try again!", ConsoleColor.Red);
                }
                else
                {
                    name = char.ToUpper(name[0]) + name[1..].ToLower();
                    break;
                }
            }
            return name.ToLower() == "robin" ? GodMode() : new Player(name);
        }

        private static Player GodMode()
        {
            Player kakashi = new Player("Kakashi Hatake")
            {
                Level = 10,
                Pos = 2.0,
                Ryō = 100000,
                Stamina = new Ability(1000, 1000),
                Chakra = new Ability(1000, 1000),
                Exp = new Ability(0, 200),
                Armor = new FlakJacket(),
                Weapon = new Kubikiribōchō(),
            };
            kakashi.Ninjutsus.Add(new Ninjutsu("Chidori", "4d16", 100));
            kakashi.Defence = kakashi.Armor.Defence;
            kakashi.Damage = kakashi.Weapon.Damage;
            Item redBull = new EnergyDrink("Red Bull", 50, 10, "You get wings")
            {
                Quantity = 100
            };
            kakashi.Backpack.Add(redBull);
            return kakashi;
        }

        public static void PlayAgain(Player player)
        {
            ColorConsole.WriteDelayedLine("\t Do you want to play again? (y/n)");
            Console.Write("\t > ");
            string choice = ColorConsole.ReadLine();
            if (choice.ToLower() == "y")
            {
                Game game = new Game();
                game.Start();
            }
            else
            {
                ExitGame(player);
            }
        }

        public static void ExitGame(Player player)
        {
            ColorConsole.WriteDelayed(ConsoleColor.Red, content: "\t Exiting game");
            ColorConsole.WriteDelayedLine("...", ConsoleColor.Red, 800);
            Adventure.endPlayer.PlayLooping();
            ColorConsole.WriteDelayed(ConsoleColor.Yellow, delay: 80, exitable: false, content: Get.EndStory(player));
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
