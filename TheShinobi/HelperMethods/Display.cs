using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TheShinobi.Characters;

namespace TheShinobi.HelperMethods
{
    class Display
    {
        public static void Title()
        {

        }

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

        public static void Delayed(string text, int delay = 50, ConsoleColor color = ConsoleColor.White)
        {
            Thread.Sleep(delay);
            foreach (var letter in text)
            {
                ColorConsole.Write(letter.ToString(), color);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}
