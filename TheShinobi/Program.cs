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
        // Fixa implementeringen av Abu Hassans Shop. => AK-47, Bulletproof vest, Red Bull
        // Fiender ska droppa items när de dör!
        // Kan man köpa/sälja flera quantity på samma gång? Hur många vill du köpa? Bara om det är en consumable.    
        // Backpack sell måste fixas
        // I EncounterCheck lägg till 2 [] med plats för 5 strings var. 
        // Om encounter = fight 5 random storys för fights
        // om Encounter = ingen fight 5 random storys för ingen strid.
    }
}
