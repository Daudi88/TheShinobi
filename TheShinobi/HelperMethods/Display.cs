using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TheShinobi.Characters;
using TheShinobi.Interfaces;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;

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

        private static void WithDevidedFrame(string title, string[] content, string title2, string[] content2)
        {
            string[] contents = new string[content.Length + content2.Length];
            Array.Copy(content, contents, content.Length);
            Array.Copy(content2, 0, contents, content.Length, content2.Length);
            List<int> lengths = new List<int>();
            foreach (var text in contents)
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
            ColorConsole.WriteEmbeddedColor($"\t┣━{title2}");
            if (title2.Contains("["))
            {
                colorLength = ColorLength(title2);
            }
            for (int i = 0; i < width - title2.Length + colorLength + 2; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┫");
            foreach (string text in content2)
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
        }

        internal static void Backpack(Player player, bool sell = false)
        {
            List<string> content = new List<string>();
            int ctr = 1;
            foreach (var item in player.Backpack)
            {
                string price = sell ? $"- {item.Price}g " : "";
                content.Add($"{ctr++}. [Yellow]{item.Quantity} {item.Name} {price}{item.BonusText()}[/Yellow]");
            }
            WithFrame(content, "[DarkCyan]BACKPACK[/DarkCyan]", ending: "Close backpack");
        }

        internal static void Details(Player player)
        {
            Console.WriteLine();
            string title = "[DarkCyan]DETAILS[/DarkCyan]";
            string color = Utility.isEnergyDrink ? "DarkCyan" : "Yellow";
            string[] content = new string[]
            {
                $"Name: [Yellow]{player.Name}[/Yellow]",
                $"Level: [Yellow]{player.Level}[/Yellow]",
                $"Exp: [Yellow]{player.Exp}/{player.MaxExp}[/Yellow]",
                $"Hp: [{color}]{player.Hp}/{player.MaxHp} {Utility.energyBonus}[/{color}]",
                $"Defence: [{color}]{player.Defence} {Utility.energyBonus}[/{color}]",
                $"Damage: [{color}]{player.Damage} {Utility.energyBonus}[/{color}]",
                $"Gold: [Yellow]{player.Gold}[/Yellow]",
            };
            string title2 = "[DarkCyan]EQUIPPED[/DarkCyan]";
            Armor armor = player.Armor;
            Weapon weapon = player.Weapon;
            string[] content2 = new string[]
            {
                $"Armor: [Yellow]{armor.Name} {armor.BonusText()}[/Yellow]",
                $"Weapon: [Yellow]{weapon.Name} {weapon.BonusText()}[/Yellow]"
            };
            WithDevidedFrame(title, content, title2, content2);
            Console.WriteLine("\t [Press enter to continue]");
            Console.ReadKey(true);
            Console.SetWindowPosition(0, Console.CursorTop - 20);
        }

        internal static void Map(Player player)
        {
            Console.WriteLine();
            int top = Console.CursorTop;
            ColorConsole.WriteEmbeddedColor("\t┏━[DarkCyan]MAP[/DarkCyan]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AAA AAA AAA AAA  AAA  AAA[/DarkGray]  [DarkRed]X[/DarkRed]   [DarkGray]AAA AAA AAA A A AAA AAA AA AAA AA AAA AAAA A AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]A AAA  AAA AAA AAA AAAAA      AA A AA AAA AAA AAA AAA AAA A AAA A AAA AA AAA A[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AA[/DarkGray] [Magenta]⌂[/Magenta]           [DarkGray]AAA A AAA      AAA AAA AAA AAA AAA AAA AAA AAA AAA AA A AAA AAA[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AA              AA AAA AAAA                                               AAAA[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AA AAA A AA     A AAA AA AA                                                 AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AA A AAA AAA     AA AA AAA AAA AA AAA     A AA AAA AA AAA AA AAA AAAA      AAA[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]A AAA AAA AAA     AA AAAA AA AAA A AA     AA A AA AAA A AAA AA A AAA        AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]A AA AAA A AA                            AAA AA AA[/DarkGray] [Blue]≈≈≈≈≈≈≈[/Blue][DarkGray] A AA AAA          A[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AA AAA A AA A                             AA AAAA[/DarkGray][Blue] ≈≈≈≈≈≈≈≈≈[/Blue] [Magenta]⌂[/Magenta]               [DarkGray]AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]A[/DarkGray] [Yellow]Ω[/Yellow]    [DarkGray]AA A AA[/DarkGray]     [Blue]≈≈≈≈≈[/Blue]       [Green]### ##      # ### #[/Green] [Blue]≈≈≈≈≈≈≈≈≈[/Blue]                [Green]##[/Green]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AA     AA AA A[/DarkGray]    [Blue]≈≈≈≈≈≈≈≈[/Blue]    [Green]# ## ###     ### ## ###[/Green] [Blue]≈≈≈≈≈≈[/Blue] [Green]## ### ##      ##[/Green]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[DarkGray]AA      AA AAA[/DarkGray]     [Blue]≈≈≈≈≈≈[/Blue]      [Green]# ### #     ### ## # #### ## # ### ## ##      #[/Green]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]##[/Green]       [DarkGray]AA A[/DarkGray]       [Blue]≈≈≈≈[/Blue]     [Green]  ## ###       ## ### # #### ## ### # ###      ##[/Green]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]###                                                                         ##[/Green]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]# ##                                                                       ###[/Green]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]# ### # ## # #### # ## ###[/Green]        [Green]# ### # ### ## ## # ###[/Green]       ┼ ┼ ┼ ┼ ┼ ┼ ┼ ┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]# ### ### # ## # # ### # ##[/Green]      [Green]### ## # ### # ### ### ##[/Green]       ┼ ┼ ┼ ┼ ┼ ┼ ┼┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]#### ### ## # #### ###[/Green] ╔──────────────╗ [Green]## ##### ## ### ##[/Green]      ┼ ┼ ┼ ┼ ┼ ┼ ┼ ┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]##### ### ## ### ### #[/Green] │ Leaf Village │ [Green]# ### ## ### ## # #[/Green]      ┼ ┼ ┼ ┼ ┼ ┼ ┼┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]###### ### ## # ## ###[/Green] ╚──────────────╝  [Green]### ## ### ## ### #[/Green]                [Yellow]Ω[/Yellow] ┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]# ##### ## # ### # ##[/Green] [DarkGray]AAA AA A AAAA A AAA[/DarkGray] [Green]## ### # ### #### #[/Green]                 ┃\n");
            ColorConsole.WriteEmbeddedColor("\t┃[Green]### ## #### ######[/Green] [DarkGray]AAA AA AAAAA AAA AA AAAA[/DarkGray] [Green]# ### ## # ### ## #### ## ### ####[/Green]┃\n");
            ColorConsole.WriteEmbeddedColor("\t┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛\n");
            PlayerOnMap(top);
            Console.WriteLine("\t [Press enter to continue]");
            Console.ReadKey(true);
            Console.SetWindowPosition(0, Console.CursorTop - 20);
        }

        private static void PlayerOnMap(int top)
        {
            
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
