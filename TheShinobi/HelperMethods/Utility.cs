using System;
using System.Threading;

namespace TheShinobi.HelperMethods
{
    static class Utility
    {
        public static readonly Random random = new Random();

        public static int RollDice(string dice)
        {
            int times = int.Parse(dice[0].ToString());
            int sides = int.Parse(dice[2..]);
            int result = 0;
            for (int i = 0; i < times; i++)
            {
                result += random.Next(1, sides + 1);
            }
            return result;
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
