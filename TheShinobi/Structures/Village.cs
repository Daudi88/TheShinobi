using System.Collections.Generic;
using TheShinobi.Characters;
using TheShinobi.HelperMethods;

namespace TheShinobi.Structures
{
    class Village
    {
        public static void Menu(Player player)
        {
            while (true)
            {
                List<string> options = new List<string>()
                {
                    "1. Go on an Adventure",
                    "2. Eat at Lightning Burger",
                    "3. Heal yourself at Konoha Hospital",
                    "4. Go to the Ninja Tool Shop",
                };
                Display.WithFrame(options, "[Green]VILLAGE[/Green]", 1, "E. Exit Game");
                bool exit = true;
                do
                {
                    string choice = ColorConsole.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Adventure.GoOnAdventure(player);
                            break;
                        case "2":
                            LightningBurger(player);
                            break;
                        case "3":
                            KonohaHospital(player);
                            break;
                        case "4":
                            NinjaToolShop(player);
                            break;
                        case "B":
                            if (!Display.Backpack(player))
                            {
                                exit = false;
                            }
                            break;
                        case "D":
                            Display.Details(player);
                            break;
                        case "M":
                            Display.Map(player);
                            break;
                        case "E":
                            Game.ExitGame();
                            break;
                        default:
                            ColorConsole.TypeOverWrongDoings(choice);
                            break;
                    }
                } while (!exit);
            }
        }
    }
}
