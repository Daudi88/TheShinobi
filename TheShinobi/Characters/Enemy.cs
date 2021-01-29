using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    class Enemy : Character
    {
        public string Clan { get; set; }
        public Enemy(string name, string clan, int level, int stamina, Armor armor, Weapon weapon)
        {
            Name = name;
            Clan = clan;
            Level = level;
            Stamina.Current = stamina;
            Armor = armor;
            Weapon = weapon;
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Ryō = Utility.random.Next(1, 100 * Level + 1);
            Exp.Current = Utility.random.Next(10 * Level, 40 * Level + 1);
        }

        public void Attack(Character defender, int top)
        {
            if (Utility.random.Next(100) >= defender.Defence)
            {
                
            }
            else
            {
                
            }
        }

        public void DropItems(Player player)
        {
            int rnd = Utility.random.Next(10);
            bool eDrop = false;
            bool cDrop = false;
            string eDropText = "";
            string cDropText = "";
            if (rnd > 7)
            {
                eDrop = true;
                Item equipable;
                if (rnd > 8)
                {
                    equipable = Weapon;
                }
                else
                {
                    equipable = Armor;
                }
                eDropText = $"{equipable.IndefiniteArticle} [Yellow]{equipable.Name}[/Yellow]";
            }
            Item consumable;
            if (Name == "Hocke" || Name == "Daudi")
            {
                cDrop = true;
                consumable = new EnergyDrink("Monster Energy", 100, 20, "You unleash the beast");
                consumable.Quantity = Utility.random.Next(1, 11);
                player.Backpack.Add(consumable);
                cDropText = $"[Yellow]{consumable.Quantity} Monster Energy[/Yellow] and ";
            }
            else
            {
                if (Utility.random.Next(10) > 6)
                {
                    cDrop = true;
                    consumable = Get.Potions()[Utility.random.Next(Get.Potions().Length)];
                    consumable.Quantity = Utility.random.Next(1, 11);
                    player.Backpack.Add(consumable);
                    cDropText = $"[Yellow]{consumable.Quantity} {consumable.Name}[/Yellow] and ";

                }
            }
            string separator = eDrop ? " and " : eDrop && cDrop ? ", " : "";
            ColorConsole.WriteEmbedded($"\t {Name} drops {eDropText}{separator}{cDropText}[Yellow]{Ryō}[/Yellow] ryō!");
            player.Ryō += Ryō;
            Display.Blinking("\t [Press enter to continue]");
        }
    }
}
