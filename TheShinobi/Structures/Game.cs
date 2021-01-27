﻿using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Structures
{
    class Game
    {
        /* This class contains the following methods:
         * Setup()              - Sets the Console up to use UTF8, title and 
         *                        console window size and Starts the music. 
         * Test()               - Used for testing parts of the game.
         * Start()              - Starts the game storyline and opens up the 
         *                        Hidden Leaf Village menu with player choises.
         * CaracterCreation()   - Lets the player choose name and checks for GodMode.
         * GodMode()            - If the player name equals Robin he becomes Kakashi
         *                        Hatake and have a lot of extras from the start.
         * ExitGame()           - Exits the game.
         */

        public void Setup()
        {
            Console.Title = "The Shinobi";
            Console.SetWindowSize(130, 50);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            string soundLocation = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\NarutoFinal.Wav");
            SoundPlayer player = new SoundPlayer(soundLocation);
            player.PlayLooping();
        }

        public void Test()
        {
            Player player = CharacterCreation();
            Adventure.MeetHiruzen(player);
            Console.ReadLine();
        }

        public void Start()
        {
            Display.Title();
            Player player = CharacterCreation();
            string intro = $"\n\t You, {player.Name} wake up in the Hidden Leaf Village and sense that something is wrong!" +
                "\n\t Kaguya Otsutsuki have kidnapped Hanare and taken her to his cave in the mountains." +
                "\n\t It is your duty to find and rescue her!";
            Display.Delayed(intro, color: ConsoleColor.Yellow);
            Village.Menu(player);
        }

        private static Player CharacterCreation()
        {
            Console.WriteLine("\n\t What is your name?");
            Console.Write("\t > ");
            string name = "";
            while (true)
            {
                name = ColorConsole.ReadLine();
                if (name.Any(char.IsDigit))
                {
                    ColorConsole.TypeOver("\t The name cannot contain digits. Try again!", ConsoleColor.Red);
                }
                else if (name.Length < 3)
                {
                    ColorConsole.TypeOver("\t The name is too short. Try again!", ConsoleColor.Red);
                }
                else if (name.Length > 12)
                {
                    ColorConsole.TypeOver("\t The name is too long. Try again!", ConsoleColor.Red);
                }
                else
                {
                    name = char.ToUpper(name[0]) + name[1..].ToLower();
                    break;
                }
            }
            return name.ToLower() == "robin" ? GodMode() : new Player(name);
        }

        private static Player GodMode()
        {
            Player kakashi = new Player("Kakashi Hatake")
            {
                Hp = 500,
                MaxHp = 1000,
                Gold = 200,
                Armor = new FlakJacket(),
                Weapon = new Kubikiribōchō()
            };
            kakashi.Defence = kakashi.Armor.Defence;
            kakashi.Damage = kakashi.Weapon.Damage;
            Item redBull = new EnergyDrink("Red Bull", 50, 10, "You get wings");
            redBull.Quantity = 100;
            kakashi.Backpack.Add(redBull);
            return kakashi;
        }

        public static void ExitGame()
        {
            ColorConsole.Write("\t Exiting game", ConsoleColor.Red);
            Display.Delayed("...", 800, ConsoleColor.Red);
            Environment.Exit(0);
        }
    }
}
