using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace TheShinobi.HelperMethods
{
    class ColorConsole
    {
        public static string ReadLine()
        {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string text = Console.ReadLine().Trim();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            return text;
        }

        public static void Write(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteEmbeddedColor(string text)
        {
            Regex colorRegex = new Regex(@"\[(?<color>.*?)\](?<text>[^[]*)\[/\k<color>\]");
            while (true)
            {
                var match = colorRegex.Match(text);
                if (match.Length < 1)
                {
                    Console.Write(text);
                    break;
                }
                Console.Write(text.Substring(0, match.Index));
                string hightlight = match.Groups["text"].Value;
                string color = match.Groups["color"].Value;
                Enum.TryParse(color, out ConsoleColor col);
                Write(hightlight, col);
                text = text.Substring(match.Index + match.Value.Length);
            }
        }

        public static void TypeOverWrongDoings(string input, string message = "Invalid choice. Try again!")
        {
            ColorConsole.Write($"\t {message}", ConsoleColor.Red);
            Thread.Sleep(1800);
            int left = 9;
            int top = Console.CursorTop;
            Console.SetCursorPosition(left, top--);
            for (int j = 0; j < message.Length; j++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < input.Length + 2; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(left, top);
            Console.Write("> ");
        }
    }
}
