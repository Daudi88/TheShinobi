using System;
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
            Display.Blinking("\t [Press enter to continue]");
            Console.WriteLine("\t Det gick");
            Console.ReadLine();
        }

        public void Start()
        {
            Adventure.villagePlayer.PlayLooping();
            Display.Title();
            Player player = CharacterCreation();
            string intro = $"\n\t You, {player.Name}, wake up in the Hidden Leaf Village and sense that something is wrong!" +
                "\n\t Kaguya Otsutsuki have kidnapped Hanare and taken her to his cave in the mountains." +
                "\n\t It is your duty to find and rescue her!";
            if (ColorConsole.WriteDelayedLine(intro, ConsoleColor.Yellow, 40))
            {
                Console.ReadKey(true);
            }
            else
            {
                Display.Blinking("\t [Press enter to continue]");
            }
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

        public static void PlayAgain()
        {
            ColorConsole.WriteDelayed("\t Do you want to play again? (y/n)");
            Console.Write("\t > ");
            string choice = ColorConsole.ReadLine();
            if (choice.ToLower() == "y")
            {
                Game game = new Game();
                game.Start();
            }
            else
            {
                ExitGame();
            }
        }

        public static void ExitGame()
        {
            ColorConsole.WriteDelayed("\t Exiting game", ConsoleColor.Red);
            ColorConsole.WriteDelayedLine("...", ConsoleColor.Red, 800);
            Environment.Exit(0);
        }
    }
}
