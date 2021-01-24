using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;

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
            player.PlayLooping();
        }

        public void Start()
        {
            Display.Title();
            Player player = CharacterCreation();
            string intro = $"You, {player.Name} wake up in the Hidden Leaf Village and sense that something is wrong!" +
                "\n\tKaguya Otsutsuki have kidnapped Hanare and taken her to his cave in the mountains." +
                "\n\tIt is your duty to find and rescue her!";
            Display.Delayed(intro, ConsoleColor.Yellow);
            LeafVillage.Menu();
        }

        private static Player CharacterCreation()
        {
            Console.WriteLine("\n\t What is your name?");
            Console.Write("\t > ");
            string name = "";
            while (true)
            {
                name = ColorConsole.ReadLine();
                if (name.Length < 3)
                {
                    Utility.TypeOverWrongDoings("The name is too short. Try again!");
                }
                else if (name.Length > 12)
                {
                    Utility.TypeOverWrongDoings("The name is too long. Try again!");
                }
                else if (name.Any(char.IsDigit))
                {
                    Utility.TypeOverWrongDoings("The name cannot contain digits. Try again!");
                }
                else
                {
                    break;
                }
            }
            return name.ToLower() == "robin" ? GodMode() : new Player(name);
        }
    }
}
