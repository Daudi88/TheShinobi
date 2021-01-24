using System;
using System.Threading;

namespace TheShinobi.HelperMethods
{
    class Display
    {
        public static void Title()
        {

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
