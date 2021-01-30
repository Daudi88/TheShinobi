using System;
using System.Linq;
using System.Text;
using TheShinobi.Abilities;
using TheShinobi.Abilities.Ninjutsus;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Structures
{
    class Game
    {
        /// <summary>
        /// Sets the Console up to use UTF8, title and 
        /// console window size and Starts the music.
        /// </summary>
        public void Setup()
        {
            Console.Title = "The Shinobi";
            Console.SetWindowSize(130, 50);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Used for testing parts of the game.
        /// </summary>
        public void Test()
        {
            Adventure.adventurePlayer.Play();
            Player player = CharacterCreation();
            Adventure.Battle(player, true);
            Console.ReadLine();
        }

        /// <summary>
        /// Starts the game storyline and opens up the
        /// Hidden Leaf Village menu with player choises.
        /// </summary>
        public void Start()
        {
            Console.Clear();
            Adventure.villagePlayer.PlayLooping();
            Display.Title();
            Player player = CharacterCreation();
            string intro = $"\n\n\t You, {player.Name}, suddenly wake up from hearing someone screaming!" +
            "\n\t It sound's as if there is real trouble..." +
            "\n\t Frightened villagers call for help “somebody is kidnapping Hanare!“" +
            "\n\n\t After some intense fighting with the villagers the attackers manages" +
            "\n\t to vanish north towards the mountains with her!" +
            "\n\n\t You couldn't reach them in time “Kono-kuso onna!!!“" +
            "\n\n\t It is still dark outside as you prepare to leave The Hidden Leaf Village" +
            "\n\t in search of Hanare and her soon to be dead kidnappers..." +
            "\n\n\t You should probobly buy better equipment from the stores before you leave!";
            ColorConsole.WriteDelayedLine(intro, ConsoleColor.Yellow, 40, blink: true);
            HiddenLeafVillage.Menu(player);
        }

        /// <summary>
        /// Lets the player choose name and checks for HokageMode.
        /// </summary>
        /// <returns><see cref="Player"/> character</returns>
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
                else if (name.Length < 4)
                {
                    ColorConsole.WriteOver("\t The name is too short. Try again!", ConsoleColor.Red);
                }
                else if (name.Length > 14)
                {
                    ColorConsole.WriteOver("\t The name is too long. Try again!", ConsoleColor.Red);
                }
                else
                {
                    name = char.ToUpper(name[0]) + name[1..].ToLower();
                    if (name.ToLower() == "robin")
                    {
                        ColorConsole.WriteDelayedLine("\t Hokage mode accepted!", ConsoleColor.Yellow);
                    }
                    break;
                }
            }
            return name.ToLower() == "robin" ? HokageMode() : new Player(name);
        }

        /// <summary>
        /// If the player enter Robin as name he becomes Kakashi
        /// Hatake and have a lot of extras from the start.
        /// </summary>
        /// <returns>Strong <see cref="Player"/> character</returns>
        private static Player HokageMode()
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
            kakashi.Ninjutsus.Add(new FlameBullet());
            kakashi.Ninjutsus.Add(new Chidori());
            kakashi.Ninjutsus.Add(new TailedBeastBall());
            kakashi.Defence = kakashi.Armor.Defence;
            kakashi.Damage = kakashi.Weapon.Damage;
            Item redBull = new EnergyDrink("Red Bull", 50, 10, "You get wings")
            {
                Quantity = 100
            };
            kakashi.Backpack.Add(redBull);
            return kakashi;
        }

        /// <summary>
        /// Asks if the player wants to restart the game.
        /// </summary>
        /// <param name="player"></param>
        public static void PlayAgain(Player player)
        {
            ColorConsole.WriteDelayedLine("\t Do you want to play again? (Y/N)");
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

        /// <summary>
        /// Exits the game.
        /// </summary>
        /// <param name="player"></param>
        public static void ExitGame(Player player)
        {
            ColorConsole.WriteDelayed(ConsoleColor.Red, content: "\t Exiting game");
            ColorConsole.WriteDelayedLine("...", ConsoleColor.Red, 800);
            Environment.Exit(0);
        }
    }
}
