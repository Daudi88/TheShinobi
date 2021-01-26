using System.Text.RegularExpressions;
using System.Threading;
using System;

namespace TheShinobi.HelperMethods
{
    static class ColorConsole
    {
        public static string ReadLine()
        {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string text = "";
            ConsoleKey key;
            do
            {
                //Varje knapptryck sparas i keyInfo men syns inte på skärmen.
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && text.Length > 0)
                {
                    //Om man trycker Backspace raderas asterisken från skärmen
                    //och tecknet som tidigare sparats i password tas bort.
                    Console.Write("\b \b");
                    text = text[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar)) // kolla efter siffror!
                {
                    //Här skrivs en asterisk ut till skärmen och knapptrycket
                    //sparas till password.
                    if (text.Length < 1 || text[^1] == ' ')
                    {
                        Console.Write(key.ToString().ToUpper());
                    }
                    else if (key == ConsoleKey.Spacebar)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(key.ToString().ToLower());
                    }
                    text += keyInfo.KeyChar;
                }

                //loopen körs så länge man inte trycker på Enter.
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
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

        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
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

        public static void TypeOver(string message, ConsoleColor color, int time = 1800)
        {
            int top = Console.CursorTop;
            WriteLine(message, color);
            Thread.Sleep(time);
            Console.SetCursorPosition(Utility.left, top--);
            for (int j = 0; j < message.Length; j++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(Utility.left, top);
            for (int i = 0; i < 100; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(Utility.left, top);
            Console.Write("> ");
        }
    }
}
