using TheShinobi.Structures;

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
        // Fixa implementeringen av Abu Hassans Shop. => AK-47, Bulletproof vest, Red Bull
        // Fiender ska droppa guld när de dör!
        // I EncounterCheck lägg till 2 [] med plats för 5 strings var. 
        // Om encounter = fight 5 random storys för fights
        // om Encounter = ingen fight 5 random storys för ingen strid.
    }
}
