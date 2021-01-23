using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;

namespace TheShinobi.Structures
{
    class Game
    {
        public void Setup()
        {
            Console.Title = "The Shinobi";
            Console.SetWindowSize(130, 75);
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            string soundLocation = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\NarutoFinal.Wav");
            SoundPlayer player = new SoundPlayer(soundLocation);
            player.PlayLooping();
        }

        public void Start()
        {

        }
    }
}
