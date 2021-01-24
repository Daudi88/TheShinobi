using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TheShinobi.HelperMethods
{
    class Display
    {
        public static void Title()
        {

        }

        public static void WithFrame(List<string> content, string title = "", int ?std = null, string ending = null)
        {
            if (std.HasValue)
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
                content.Add(ending);
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

        private static int ColorLength(string text)
        {
            int at = text.IndexOf("[");
            int at2 = text.IndexOf("]");
            return text.Substring(at, at2 - at + 1).Length * 2 + 1;
        }

        public static void Delayed(string text, ConsoleColor color)
        {
            foreach (var letter in text)
            {
                ColorConsole.Write(letter.ToString(), color);
                Thread.Sleep(50);
            }
            Console.WriteLine();
        }
    }
}
