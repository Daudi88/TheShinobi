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
        // Fixa implementeringen av Abu Hassans Shop. => AK-47, Bulletproof vest, Red Bull
        // Fixa implementeringen Så man får något vid Treasure.
        // Fiender ska droppa items när de dör!
        // Kan man köpa/sälja flera quantity på samma gång? Hur många vill du köpa? Bara om det är en consumable.    
        // Backpack sell måste fixas
    }
}
