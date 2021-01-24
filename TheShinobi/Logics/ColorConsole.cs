using System;
using System.Collections.Generic;
using System.Text;

namespace TheShinobi.Logics
{
    class ColorConsole
    {
        public static string ReadLine()
        {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string input = Console.ReadLine().Trim();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            return input;
        }
    }
}
