using TheShinobi.HelperMethods;
using TheShinobi.Characters;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Items.Potions;
using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Media;
using System.Collections.Generic;

namespace TheShinobi.Structures
{
    class Game
    {
        public void Setup()
        {
            Console.Title = "The Shinobi";
            Console.SetWindowSize(130, 75);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            string soundLocation = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\NarutoFinal.Wav");
            SoundPlayer player = new SoundPlayer(soundLocation);
            //player.PlayLooping();
        }

        public void Test()
        {
            Console.WriteLine();
            List<string> list = new List<string>()
            {
                "1. [Yellow]Eat[/Yellow]", 
                "2. [DarkCyan]Sleep[/DarkCyan]", 
                "3. [Green]Repeat[/Green]"
            };
            Display.WithFrame(list, "[Red]CHOICES[/Red]", 1, "E. [Magenta]Go home[/Magenta]");
            Console.WriteLine("\n");

            List<string> list1 = new List<string>()
            {
                "1. [Yellow]Eat[/Yellow]",
                "2. [DarkCyan]Sleep[/DarkCyan]",
                "3. [Green]Repeat[/Green]"
            };
            Display.WithFrame(content: list1, std: 1, ending: "E. [Magenta]Go home[/Magenta]");
            Console.WriteLine("\n");

            List<string> list2 = new List<string>()
            {
                "1. [Yellow]Eat[/Yellow]",
                "2. [DarkCyan]Sleep[/DarkCyan]",
                "3. [Green]Repeat[/Green]"
            };
            Display.WithFrame(content: list2);
            Console.ReadLine();
        }

        public void Start()
        {
            Display.Title();
            Player player = CharacterCreation();
            string intro = $"You, {player.Name} wake up in the Hidden Leaf Village and sense that something is wrong!" +
                "\n\tKaguya Otsutsuki have kidnapped Hanare and taken her to his cave in the mountains." +
                "\n\tIt is your duty to find and rescue her!";
            Display.Delayed(intro, ConsoleColor.Yellow);
            Village.Menu();
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
                    Utility.TypeOverWrongDoings(name, "The name cannot contain digits. Try again!");
                }
                else if (name.Length < 3)
                {
                    Utility.TypeOverWrongDoings(name, "The name is too short. Try again!");
                }
                else if (name.Length > 12)
                {
                    Utility.TypeOverWrongDoings(name, "The name is too long. Try again!");
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
                Hp = 100,
                MaxHp = 100,
                Gold = 100000,
                Armor = new InfiniteArmor(),
                Weapon = new Kubikiribōchō(),
            };
            kakashi.Defence = kakashi.Armor.Defence;
            kakashi.Damage = kakashi.Weapon.Damage;
            Potion redBull = new Potion("Red Bull", 50, 20, "You get wings");
            redBull.Quantity = 100;
            kakashi.Backpack.Add(redBull);
            return kakashi;
        }
    }
}
