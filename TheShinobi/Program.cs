﻿using TheShinobi.Structures;

namespace TheShinobi
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            game.Setup();
            game.Start();
        }

        // TODO
        // StoryTime()
        // Implementera MeetHiruzen() i Adventure => hocke :)
        // I EncounterCheck lägg till 2 [] med plats för 5 strings var. 
        // Om encounter = fight 5 random storys för fights
        // om Encounter = ingen fight 5 random storys för ingen strid.
    }
}
