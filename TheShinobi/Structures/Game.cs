using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Structures
{
    class Game
    {
        public void Setup()
        {
            Console.Title = "The Shinobi";
            Console.SetWindowSize(130, 50);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            string soundLocation = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\NarutoFinal.Wav");
            SoundPlayer player = new SoundPlayer(soundLocation);
            player.PlayLooping();
        }

        public void Test()
        {
            Player player = CharacterCreation();
            Adventure.AbuHassan(player);
        }

        public void Start()
        {
            Display.Title();
            Player player = CharacterCreation();
            string intro = $"\t You, {player.Name} wake up in the Hidden Leaf Village and sense that something is wrong!" +
                "\n\t Kaguya Otsutsuki have kidnapped Hanare and taken her to his cave in the mountains." +
                "\n\t It is your duty to find and rescue her!";
            Display.Delayed(intro, color: ConsoleColor.Yellow);
            Village.Menu(player);
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
                    ColorConsole.TypeOver("The name cannot contain digits. Try again!", ConsoleColor.Red);
                }
                else if (name.Length < 3)
                {
                    ColorConsole.TypeOver("The name is too short. Try again!", ConsoleColor.Red);
                }
                else if (name.Length > 12)
                {
                    ColorConsole.TypeOver("The name is too long. Try again!", ConsoleColor.Red);
                }
                else
                {
                    name = char.ToUpper(name[0]) + name[1..].ToLower();
                    break;
                }
            }
            Console.WriteLine();
            return name.ToLower() == "robin" ? GodMode() : new Player(name);
        }

        private static Player GodMode()
        {
            Player kakashi = new Player("Kakashi Hatake")
            {
                Hp = 100,
                MaxHp = 100,
                Gold = 100000,
                Armor = new InfiniteArmor(),
                Weapon = new Kubikiribōchō(),
            };
            kakashi.Defence = kakashi.Armor.Defence;
            kakashi.Damage = kakashi.Weapon.Damage;
            Consumable redBull = new EnergyDrink("Red Bull", 50, 20, "You get wings", 10);
            redBull.Quantity = 100;
            kakashi.Backpack.Add(redBull);
            return kakashi;
        }

        public static void ExitGame()
        {
            ColorConsole.Write("\t Exiting game", ConsoleColor.Red);
            Display.Delayed("...", 800, ConsoleColor.Red);
            Environment.Exit(0);
        }
    }
}
