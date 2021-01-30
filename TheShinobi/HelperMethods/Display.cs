using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using System.Xml.Schema;
using TheShinobi.Abilities.Ninjutsus;
using TheShinobi.Characters;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.Structures;

namespace TheShinobi.HelperMethods
{
    class Display
    {
        /* This class contains the following methods:
         * WithFrame()
         * TopOfFrame()
         * SideOfFrame()
         */

        static string soundLocation = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Credits.Wav");
        static SoundPlayer creditsPlayer = new SoundPlayer(soundLocation);

        /// <summary>
        /// Surrounds <paramref name="content"/> with a frame. 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <param name="std"></param>
        /// <param name="ending"></param>
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

        /// <summary>
        /// Prints out top of a fram with a <paramref name="title"/> embedded.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="length"></param>
        private static void TopOfFrame(string title, int length)
        {
            ColorConsole.WriteEmbedded($"\t┏━{title}");
            for (int i = 0; i < length - (title.Length - Get.ColorLength(title)) + 2; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┓");
        }

        /// <summary>
        /// Prints out the sides of the frame with its <paramref name="content"/> inbetween.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="length"></param>
        private static void SidesOfFrame(List<string> content, int length)
        {
            foreach (string text in content)
            {
                ColorConsole.WriteEmbedded($"\t┃ {text.PadRight(length + Get.ColorLength(text))}  ┃\n");
            }
        }

        /// <summary>
        /// Prints out a dividing line that separates parts of the content from eachother.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="length"></param>
        private static void DividingLine(string title, int length)
        {
            ColorConsole.WriteEmbedded($"\t┣━{title}");
            for (int i = 0; i < length - (title.Length - Get.ColorLength(title)) + 2; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┫");
        }

        /// <summary>
        /// Prints out bottom of the frame.
        /// </summary>
        /// <param name="length"></param>
        private static void BottomOfFrame(int length)
        {
            Console.Write("\t┗");
            for (int i = 0; i < length + 3; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┛");
        }

        /// <summary>
        /// Prints out the content of the backpack surrounded by a frame.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="sell"></param>
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

        /// <summary>
        /// Prints out the details of the player surrounded by a divided frame.
        /// </summary>
        /// <param name="player"></param>
        internal static void Details(Player player)
        {
            Console.SetWindowPosition(0, Console.CursorTop - Utility.V);
            Console.WriteLine("\n");
            string title = "[DarkCyan]DETAILS[/DarkCyan]";
            string color = "Yellow";
            string bonus = "";
            if (Utility.energyDrink.IsEnergized)
            {
                color = "DarkCyan";
                bonus = Utility.energyDrink.BonusText();
            }
            List<string> content1 = new List<string>()
            {
                $"Name: [Yellow]{player.Name}[/Yellow]",
                $"Level: [Yellow]{player.Level}[/Yellow]",
                $"Exp: [Yellow]{player.Exp}[/Yellow]",
                $"Stamina: [{color}]{player.Stamina} {bonus}[/{color}]",
                $"Chakra: [{color}]{player.Chakra} {bonus}[/{color}]",
                $"Defence: [{color}]{player.Defence} {bonus}[/{color}]",
                $"Damage: [{color}]{player.Damage} {bonus}[/{color}]",
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

        /// <summary>
        /// Prints out a frame that displays <see cref="Player"/> and <see cref="Enemy"/> stats.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        /// <param name="title"></param>
        /// <param name="height"></param>
        /// <param name="top"></param>
        /// <param name="reset"></param>
        public static void BattleFrame(Player player, Enemy enemy, int top, bool reset = false)
        {
            string title = "[Red]BATTLE[/Red]";
            int height = 6;
            string[] stats = new string[]
            {
                $"{player.Name}",
                $"{Get.Status(player.Stamina, "Yellow")} Stamina",
                $"[DarkCyan]{player.Chakra.Current}[/DarkCyan] Chakra",
                $"{enemy.Name}",
                $"{Get.Status(enemy.Stamina, "Yellow")} Stamina",
                $"[DarkCyan]{enemy.Chakra.Current}[/DarkCyan] Chakra"
            };

            Console.SetCursorPosition(0, top);
            int length = 41;
            ColorConsole.WriteEmbedded($"\n\t┏━{title}");
            for (int i = 0; i < length - title.Length + Get.ColorLength(title) + 2; i++)
            {
                Console.Write("━");
            }
            Console.Write("┳");
            for (int i = 0; i < length + 3; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┓");
            int colorLength1 = 0;
            for (int i = 0; i < 3; i++)
            {
                colorLength1 += Get.ColorLength(stats[i]);
            }
            int length1 = length + colorLength1;
            string stat = $"{stats[0]}: {stats[1]}, {stats[2]}";
            ColorConsole.WriteEmbedded($"\t┃ {stat.PadRight(length1)}  ");
            int colorLength2 = 0;
            for (int i = 3; i < 6; i++)
            {
                colorLength2 += Get.ColorLength(stats[i]);
            }
            int length2 = length + colorLength2;
            stat = $"{stats[3]}: {stats[4]}, {stats[5]}";
            ColorConsole.WriteEmbedded($"┃ {stat.PadRight(length2)}  ┃\n");
            Console.Write("\t┣");
            for (int i = 0; i < length + 3; i++)
            {
                Console.Write("━");
            }
            Console.Write("┻");
            for (int i = 0; i < length + 3; i++)
            {
                Console.Write("━");
            }
            Console.WriteLine("┫");
            length = length * 2 + 4;
            top = Console.CursorTop;
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("\t┃");
                Console.SetCursorPosition(10 + length + 2, Console.CursorTop);
                Console.WriteLine("┃");
            }
            BottomOfFrame(length);
            int bottom = Console.CursorTop;
            if (reset)
            {
                Utility.WaitForUser();
                for (int i = 0; i < height; i++)
                {
                    Console.SetCursorPosition(9, top++);
                    for (int j = 0; j < length + 3; j++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.SetCursorPosition(0, bottom);
            }
        }

        private static void EmptyBattleFrame(int top)
        {
            int height = 6;
            Console.SetCursorPosition(0, top);
            Console.Write("\n\t");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            top = Console.CursorTop;
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("\t ");
                Console.SetCursorPosition(98, Console.CursorTop);
                Console.WriteLine(" ");
            }
            for (int j = 0; j < 100; j++)
            {
                Console.Write(" ");
            }
        }

        /// <summary>
        /// Prints out the map to the screen.
        /// </summary>
        /// <param name="player"></param>
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

        /// <summary>
        /// Prints out the <see cref="Player"/> position on the map.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="top"></param>
        private static void PlayerOnMap(Player player, int top)
        {
            var positions = Get.Positions();
            var position = positions[player.Pos];
            Console.SetCursorPosition(position.Item1, top += position.Item2);
            ColorConsole.Write("●", ConsoleColor.Red);
        }

        /// <summary>
        /// Flashes the Battleframe 3 times.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        /// <param name="top"></param>
        public static void Flashing3Times(Player player, Enemy enemy, int top)
        {
            for (int i = 0; i < 3; i++)
            {
                BattleFrame(player, enemy, top);
                Thread.Sleep(400);
                EmptyBattleFrame(top);
                Thread.Sleep(400);
            }
        }

        /// <summary>
        /// Prints out a blinking text to promt the player to move on.
        /// </summary>
        /// <param name="text"></param>
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

        /// <summary>
        /// As <see cref="Blinking(string)"/> but with a set position.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="bottom"></param>
        public static void SetBlinking(string text, int bottom)
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.SetCursorPosition(0, bottom);
                    Console.WriteLine("                                        ");
                    Console.ReadKey(true);
                    break;
                }

                Console.SetCursorPosition(0, bottom);
                Console.WriteLine(text);
                Thread.Sleep(600);
                Console.SetCursorPosition(0, bottom);
                Console.WriteLine("                                        ");
                Thread.Sleep(300);
            }
        }

        /// <summary>
        /// Prints out the game title to the screen. 
        /// </summary>
        public static void Title()
        {
            Console.CursorVisible = false;
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

        /// <summary>
        /// Asks the player to play the game again. If player defeats the game, credits are printed to the sceen. 
        /// </summary>
        /// <param name="player"></param>
        public static void Credits(Player player)
        {
            creditsPlayer.PlayLooping();
            if (player.Stamina.Current > 0)
            {
                ColorConsole.WriteDelayed(ConsoleColor.Yellow, 80, false, Get.EndStory(player));
            }
            Game.PlayAgain(player);
        }
    }
}
