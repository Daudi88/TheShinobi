using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http.Headers;
using System.Threading;
using TheShinobi.Abilities;
using TheShinobi.Characters;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Structures;

namespace TheShinobi.HelperMethods
{
    class Display
    {
        /* This class contains the following methods:
         * 
         * 
         */

        static string soundLocation = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Credits.Wav");
        static SoundPlayer creditsPlayer = new SoundPlayer(soundLocation);

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

            int length = Get.ContentLength(content);
            TopOfFrame(title, length);
            SidesOfFrame(content, length);
            BottomOfFrame(length);
            Console.Write("\t > ");
        }

        private static void TopOfFrame(string title, int length)
        {
            ColorConsole.WriteEmbedded($"\t┏━{title}");
            int colorLength = 0;
            if (title.Contains("["))
            {
                colorLength = Get.ColorLength(title);
            }

            for (int i = 0; i < length - title.Length + colorLength + 2; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┓");
        }

        private static void SidesOfFrame(List<string> content, int length)
        {
            foreach (string text in content)
            {
                int colorLength = 0;
                if (text.Contains("["))
                {
                    colorLength = Get.ColorLength(text);
                }
                ColorConsole.WriteEmbedded($"\t┃ {text.PadRight(length + colorLength)}  ┃\n");
            }
        }

        private static void DividingLine(string title, int length)
        {
            ColorConsole.WriteEmbedded($"\t┣━{title}");
            int colorLength = 0;
            if (title.Contains("["))
            {
                colorLength = Get.ColorLength(title);
            }

            for (int i = 0; i < length - title.Length + colorLength + 2; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┫");
        }

        private static void BottomOfFrame(int length)
        {
            Console.Write("\t┗");
            for (int i = 0; i < length + 3; i++)
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
            Console.SetWindowPosition(0, Console.CursorTop - Utility.V);
            Console.WriteLine("\n");
            string title = "[DarkCyan]DETAILS[/DarkCyan]";
            string color = Utility.energyDrink.IsEnergized ? "DarkCyan" : "Yellow";
            List<string> content1 = new List<string>()
            {
                $"Name: [Yellow]{player.Name}[/Yellow]",
                $"Level: [Yellow]{player.Level}[/Yellow]",
                $"Exp: [Yellow]{player.Exp}[/Yellow]",
                $"Stamina: [{color}]{player.Stamina} {Utility.energyDrink.BonusText()}[/{color}]",
                $"Chakra: [{color}]{player.Chakra} {Utility.energyDrink.BonusText()}[/{color}]",
                $"Defence: [{color}]{player.Defence} {Utility.energyDrink.BonusText()}[/{color}]",
                $"Damage: [{color}]{player.Damage} {Utility.energyDrink.BonusText()}[/{color}]",
                $"Ryō: [Yellow]{player.Ryō}[/Yellow]",
            };
            string title2 = "[DarkCyan]EQUIPPED[/DarkCyan]";
            Armor armor = player.Armor;
            Weapon weapon = player.Weapon;
            List<string> content2 = new List<string>()
            {
                $"Armor: [Yellow]{armor.Name} {armor.BonusText()}[/Yellow]",
                $"Weapon: [Yellow]{weapon.Name} {weapon.BonusText()}[/Yellow]"
            };
            string title3 = "[DarkCyan]NINJUTSU[/DarkCyan]";
            List<Ninjutsu> ninjutsus = player.Ninjutsus;
            List<string> content3 = new List<string>();
            if (ninjutsus.Count > 0)
            {
                foreach (var jutsu in ninjutsus)
                {
                    content3.Add($"[Yellow]{jutsu}[/Yellow]");
                }
            }
            else
            {
                content3.Add("[Red]You don't know any techniques yet.[/Red]");
            }
            List<string> contents = new List<string>();
            contents.AddRange(content1);
            contents.AddRange(content2);
            contents.AddRange(content3);
            int length = Get.ContentLength(contents);
            TopOfFrame(title, length);
            SidesOfFrame(content1, length);
            DividingLine(title2, length);
            SidesOfFrame(content2, length);
            DividingLine(title3, length);
            SidesOfFrame(content3, length);
            BottomOfFrame(length);
            Utility.WaitForUser();
        }

        internal static void Map(Player player)
        {
            Console.SetWindowPosition(0, Console.CursorTop - Utility.V);
            Console.WriteLine("\n");
            int top = Console.CursorTop;
            ColorConsole.WriteEmbedded("\t┏━[DarkCyan]MAP[/DarkCyan]━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AAA AAA AAA AAA  AAA  AAA[/DarkGray]  [DarkRed]X[/DarkRed]   [DarkGray]AAA AAA AAA A A AAA AAA AA AAA AA AAA AAAA A AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]A AAA  AAA AAA AAA AAAAA      AA A AA AAA AAA AAA AAA AAA A AAA A AAA AA AAA A[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AA[/DarkGray] [Magenta]⌂[/Magenta]           [DarkGray]AAA A AAA      AAA AAA AAA AAA AAA AAA AAA AAA AAA AA A AAA AAA[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AA              AA AAA AAAA                                               AAAA[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AA AAA A AA     A AAA AA AA                                                 AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AA A AAA AAA     AA AA AAA AAA AA AAA     A AA AAA AA AAA AA AAA AAAA      AAA[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]A AAA AAA AAA     AA AAAA AA AAA A AA     AA A AA AAA A AAA AA A AAA        AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]A AA AAA A AA                            AAA AA AA[/DarkGray] [Blue]≈≈≈≈≈≈≈[/Blue][DarkGray] A AA AAA          A[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AA AAA A AA A                             AA AAAA[/DarkGray][Blue] ≈≈≈≈≈≈≈≈≈[/Blue] [Magenta]⌂[/Magenta]               [DarkGray]AA[/DarkGray]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]A[/DarkGray] [Yellow]Ω[/Yellow]    [DarkGray]AA A AA[/DarkGray]     [Blue]≈≈≈≈≈[/Blue]       [Green]### ##      # ### #[/Green] [Blue]≈≈≈≈≈≈≈≈≈[/Blue]                [Green]##[/Green]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AA     AA AA A[/DarkGray]    [Blue]≈≈≈≈≈≈≈≈[/Blue]    [Green]# ## ###     ### ## ###[/Green] [Blue]≈≈≈≈≈≈[/Blue] [Green]## ### ##      ##[/Green]┃\n");
            ColorConsole.WriteEmbedded("\t┃[DarkGray]AA      AA AAA[/DarkGray]     [Blue]≈≈≈≈≈≈[/Blue]      [Green]# ### #     ### ## # #### ## # ### ## ##      #[/Green]┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]##[/Green]       [DarkGray]AA A[/DarkGray]       [Blue]≈≈≈≈[/Blue]     [Green]  ## ###       ## ### # #### ## ### # ###      ##[/Green]┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]###                                                                         ##[/Green]┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]# ##                                                                       ###[/Green]┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]# ### # ## # #### # ## ###[/Green]        [Green]# ### # ### ## ## # ###[/Green]       ┼ ┼ ┼ ┼ ┼ ┼ ┼ ┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]# ### ### # ## # # ### # ##[/Green]      [Green]### ## # ### # ### ### ##[/Green]       ┼ ┼ ┼ ┼ ┼ ┼ ┼┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]#### ### ## # #### ###[/Green] ╔──────────────╗ [Green]## ##### ## ### ##[/Green]      ┼ ┼ ┼ ┼ ┼ ┼ ┼ ┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]##### ### ## ### ### #[/Green] │ [Yellow]Leaf Village[/Yellow] │ [Green]# ### ## ### ## # #[/Green]      ┼ ┼ ┼ ┼ ┼ ┼ ┼┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]###### ### ## # ## ###[/Green] ╚──────────────╝  [Green]### ## ### ## ### #[/Green]                [Yellow]Ω[/Yellow] ┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]# ##### ## # ### # ##[/Green] [DarkGray]AAA AA A AAAA A AAA[/DarkGray] [Green]## ### # ### #### #[/Green]                 ┃\n");
            ColorConsole.WriteEmbedded("\t┃[Green]### ## #### ######[/Green] [DarkGray]AAA AA AAAAA AAA AA AAAA[/DarkGray] [Green]# ### ## # ### ## #### ## ### ####[/Green]┃\n");
            ColorConsole.WriteEmbedded("\t┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛\n");
            int bottom = Console.CursorTop;
            PlayerOnMap(player, top);
            Console.SetCursorPosition(0, bottom);
            Utility.WaitForUser();
        }

        private static void PlayerOnMap(Player player, int top)
        {
            var positions = Get.Positions();
            var position = positions[player.Pos];
            Console.SetCursorPosition(position.Item1, top += position.Item2);
            ColorConsole.Write("●", ConsoleColor.Red);
        }
        
        public static void Blinking(string text)
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.WriteLine("                                        ");
                    Console.ReadKey(true);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    break;
                }
                Console.WriteLine(text);
                Thread.Sleep(600);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine("                                        ");
                Thread.Sleep(300);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
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
            if (isKeyPressed)
            {
                Console.ReadKey(true);
            }
        }

        public static void Credits(Player player)
        {
            creditsPlayer.PlayLooping();
            if (player.Stamina.Current > 0)
            {
                // Håkan win story
            }
            else
            {
                // Håkan loose story
            }
            Game.PlayAgain(player);
        }
    }
}
