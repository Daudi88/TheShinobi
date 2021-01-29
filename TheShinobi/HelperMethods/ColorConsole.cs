using System.Text.RegularExpressions;
using System.Threading;
using System;
using System.Reflection;

namespace TheShinobi.HelperMethods
{
    static class ColorConsole
    {
        static Regex colorRegex = new Regex(@"\[(?<color>.*?)\](?<text>[^[]*)\[/\k<color>\]");
        public static string ReadLine()
        {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string text = "";
            ConsoleKey key;
            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);              
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && text.Length > 0)
                {
                    Console.Write("\b \b");
                    text = text[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    if (int.TryParse(keyInfo.KeyChar.ToString(), out int digit))
                    {
                        Console.Write(digit);
                    }
                    else if (text.Length < 1 || text[^1] == ' ')
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
        
        public static void WriteEmbedded(string text)
        {
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

        public static void WriteEmbeddedDelayed(string text)
        {
            bool result;
            while (true)
            {
                var match = colorRegex.Match(text);
                if (match.Length < 1)
                {
                    result = WriteDelayed(content: text);
                    break;
                }
                WriteDelayed(content: text.Substring(0, match.Index));
                string hightlight = match.Groups["text"].Value;
                string color = match.Groups["color"].Value;
                Enum.TryParse(color, out ConsoleColor col);
                WriteDelayed(col, content: hightlight);
                text = text.Substring(match.Index + match.Value.Length);
            }
            if (result)
            {
                Console.ReadKey(true);
            }
            else
            {
                Utility.WaitForUser();
            }
        }

        public static void WriteEmbeddedSetDelayed(string text, int top, int bottom, bool blink = true, int delay = 1800)
        {
            bool result;
            bool firstPart = true;
            while (true)
            {
                var match = colorRegex.Match(text);
                if (match.Length < 1)
                {
                    result = WriteDelayed(content: text);
                    break;
                }
                if (firstPart)
                {
                    WriteSetDelayed(text.Substring(0, match.Index), top);
                    firstPart = false;
                }
                else
                {
                    WriteDelayed(content: text.Substring(0, match.Index));
                }
                string hightlight = match.Groups["text"].Value;
                string color = match.Groups["color"].Value;
                Enum.TryParse(color, out ConsoleColor col);
                WriteDelayed(col, content: hightlight);
                text = text.Substring(match.Index + match.Value.Length);
            }
            if (blink)
            {
                if (result)
                {
                    Console.ReadKey(true);
                }
                else
                {
                    Utility.WaitSetForUser(bottom);
                } 
            }
            else if (!result)
            {
                Thread.Sleep(delay);
            }
        }

        public static bool WriteDelayed(ConsoleColor color = ConsoleColor.White, int delay = 20, bool exitable = true, params string[] content)
        {
            bool isKeyPressed = false;
            Thread.Sleep(delay);
            foreach (var text in content)
            {
                foreach (var letter in text)
                {
                    Write(letter.ToString(), color);
                    if (Console.KeyAvailable && exitable)
                    {
                        isKeyPressed = true;
                    }

                    if (!isKeyPressed)
                    {
                        Thread.Sleep(delay);
                    }
                }
                if (!exitable)
                {
                    Console.SetWindowPosition(0, Console.CursorTop - Utility.V);
                }
            }
            return isKeyPressed;
        }

        public static void WriteDelayedLine(string text, ConsoleColor color = ConsoleColor.White, int delay = 30, bool blink = false)
        {
            bool isKeyPressed = false;
            Thread.Sleep(delay);
            foreach (var letter in text)
            {
                Write(letter.ToString(), color);
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
            Console.WriteLine();
            if (isKeyPressed)
            {
                Console.ReadKey(true);
            }
            
            if (!isKeyPressed && blink)
            {
                Utility.WaitForUser();
            }
        }

        public static void WriteSetDelayed(string text, int top, ConsoleColor color = ConsoleColor.White, int delay = 40)
        {
            Console.SetCursorPosition(10, top);
            bool isKeyPressed = false;
            Thread.Sleep(delay);

            foreach (var letter in text)
            {
                Console.Write(letter.ToString(), color);
                if (Console.KeyAvailable)
                {
                    isKeyPressed = true;
                }

                if (!isKeyPressed)
                {
                    Thread.Sleep(delay);
                }
            }
        }

        public static void WriteOver(string message, ConsoleColor color, int time = 400)
        {
            int left = 9;
            int top = Console.CursorTop;
            bool result = WriteDelayed(color, content: $"{message}\n");
            Thread.Sleep(time);
            Console.SetCursorPosition(left, top--);
            for (int j = 0; j < message.Length; j++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < 100; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(left, top);
            Console.Write("> ");
            if (result)
            {
                Console.ReadKey(true);
            }
        }
    }
}
