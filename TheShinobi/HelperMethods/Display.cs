using TheShinobi.Characters;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;

namespace TheShinobi.HelperMethods
{
    class Display
    {
        public static void WithFrame(List<string> content, string title = "", bool std = false, string ending = null)
        {
            if (std)
            {
                List<string> standards = new List<string>()
                {
                    "B. Open Backpack",
                    "D. Show Details",
                    "M. Show Map"
                };
                content.AddRange(standards);
            }

            if (ending != null)
            {
                content.Add($"E. {ending}");
            }

            List<int> lengths = new List<int>();
            foreach (var text in content)
            {
                int length = text.Length;
                if (text.Contains("["))
                {
                    length -= ColorLength(text);
                }
                lengths.Add(length);
            }
            int width = lengths.OrderByDescending(i => i).First();
            
            ColorConsole.WriteEmbeddedColor($"\t┏━{title}");
            int colorLength = 0;
            if (title.Contains("["))
            {
                colorLength = ColorLength(title);
            }
            
            for (int i = 0; i < width - title.Length + colorLength + 2; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┓");
            foreach (string text in content)
            {
                colorLength = 0;
                if (text.Contains("["))
                {
                    colorLength = ColorLength(text);
                }                
                ColorConsole.WriteEmbeddedColor($"\t┃ {text.PadRight(width + colorLength)}  ┃\n");
            }
            Console.Write("\t┗");
            for (int i = 0; i < width + 3; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┛");
            Console.Write("\t > ");
        }

        internal static bool Backpack(Player player)
        {
            throw new NotImplementedException();
        }

        internal static void Details(Player player)
        {
            throw new NotImplementedException();
        }

        internal static void Map(Player player)
        {
            throw new NotImplementedException();
        }

        private static int ColorLength(string text)
        {
            int at = text.IndexOf("[");
            int at2 = text.IndexOf("]");
            return text.Substring(at, at2 - at + 1).Length * 2 + 1;
        }

        public static void Delayed(string text, int delay = 40, ConsoleColor color = ConsoleColor.White)
        {
            bool isKeyPressed = false;
            Thread.Sleep(delay);
            foreach (var letter in text)
            {
                ColorConsole.Write(letter.ToString(), color);
                if (Console.KeyAvailable)
                {
                    isKeyPressed = true;
                }

                if (!isKeyPressed)
                {
                    Thread.Sleep(delay);
                }
            }
            Thread.Sleep(800);
            Console.WriteLine("\n");
        }

        public static void Story(params string[] content)
        {
            bool isKeyPressed = false;
            for (int i = 0; i < content.Length; i++)
            {
                Console.Write("\n\t ");
                for (int j = 0; j < content[i].Length; j++)
                {
                    ColorConsole.Write(content[i][j].ToString(), ConsoleColor.Yellow);
                    if (Console.KeyAvailable)
                    {
                        isKeyPressed = true;
                    }

                    if (!isKeyPressed)
                    {
                        Thread.Sleep(40);
                    }
                }
            }
            Console.WriteLine();
        }

        public static void Title()
        {
            int top = 2;
            int left = 8;
            int ctr = 0;
            bool isKeyPressed = false;

            string t = " ▀       █       █       █▀    ▄████████████████████████ █▀    █ █▀    ▀ ██      ▄█▀    ";
            string h = "            ▀      ▄▀    ███████▄███████████████   ▄▀      ▄▀      ▄▀   ████████▄██████▀ ██████    ▄▀      ▄    ";
            string e = "            ▀      ▄▀    ███████▄████████████████  ▄▀  ██  ▄▀  ██  ▄▀  ████  █████▀  ▄████    ██";
            string space = "                                                ";
            string s = "            ▀       █  ▄ ████  █▄████ ▄██████ ███   █  ██   █  ██   █  ██   █  ████ ██████▀ ███▀██  ███ ";
            string i = "         ███████▄██████▀███████   ▌▌▌   ";
            string n = "        ███████ ███████▀████████▄▀      ▄▀      ▄▀      ▄███████ ██████▀ ▄█████ ";
            string o = "         ██████ ▄██████▀█████████      ██      ██      █████████▄██████▀ ██████ ";
            string b = "        ▀   ▀  ▀█  ▄▀  ██████████████████████████  ▄▀  ██  ▄▀  ██  ▄▀  ██  ██  █████████▄██▀▄██▀ ██  ▄█ ";
            string[] title = new string[] { t, h, e, space, s, h, i, n, o, b, i };
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            foreach (var letter in title)
            {
                for (int j = 0; j < letter.Length; j++)
                {
                    ctr++;
                    Console.SetCursorPosition(left, top++);
                    Console.Write(letter[j]);

                    if (Console.KeyAvailable)
                    {
                        isKeyPressed = true;
                    }

                    if (!isKeyPressed)
                    {
                        Thread.Sleep(1);
                    }

                    if (ctr % 10 == 8)
                    {
                        ctr = 0;
                        top = 2;
                        left++;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}
