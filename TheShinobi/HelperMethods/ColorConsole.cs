﻿using System.Text.RegularExpressions;
using System.Threading;
using System;

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
                    result = WriteDelayed(text);
                    break;
                }
                WriteDelayed(text.Substring(0, match.Index));
                string hightlight = match.Groups["text"].Value;
                string color = match.Groups["color"].Value;
                Enum.TryParse(color, out ConsoleColor col);
                WriteDelayed(hightlight, col);
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

        public static bool WriteDelayed(ConsoleColor color = ConsoleColor.White, int delay = 20, bool exitable = true, params string[] texts)
        {
            bool isKeyPressed = false;
            Thread.Sleep(delay);
            foreach (var text in texts)
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
            Thread.Sleep(600);
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

        public static void WriteOver(string message, ConsoleColor color, int time = 400)
        {
            int left = 9;
            int top = Console.CursorTop;
            bool result = WriteDelayed($"{message}\n", color);
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
