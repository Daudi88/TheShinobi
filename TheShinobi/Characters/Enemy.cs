using TheShinobi.Abilities.Ninjutsus;
using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    class Enemy : Character
    {
        public string Pronoun { get; set; }
        public string Clan { get; set; }
        public Enemy(string name, string clan, int level, Armor armor, Weapon weapon, Ninjutsu jutsu, string pronoun = "his")
        {
            Name = name;
            Pronoun = pronoun;
            Clan = clan;
            Level = level;
            Stamina.Max = Utility.random.Next(3 * Level + 12, 3 * Level + 33);
            Stamina.Current = Stamina.Max;
            Chakra.Max = Utility.random.Next(5 * Level, 6 * Level + 5);
            Chakra.Current = Chakra.Max;
            Armor = armor;
            Weapon = weapon;
            Ninjutsus.Add(jutsu);
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Ryō = Utility.random.Next(1, 100 * Level + 1);
            Exp.Current = Utility.random.Next(10 * Level, 40 * Level + 1);
        }

        public override string Attack(Character defender)
        {
            string text = "";            
            if (Utility.RollDice("1d20") + Chakra.Current >= defender.Defence)
            {
                string weapon;
                int damage;
                Ninjutsu jutsu = Ninjutsus[Utility.random.Next(Ninjutsus.Count)];
                if (Utility.random.Next(101) > 50 && Chakra.Current - jutsu.Cost > 0)
                {
                    damage = Utility.RollDice(jutsu.Damage);
                    weapon = jutsu.Name;
                    Chakra.Current -= jutsu.Cost;
                }
                else
                {
                    damage = Utility.RollDice(Weapon.Damage);
                    weapon = Weapon.Name;
                }
                text = $"{Name} hits you with {Pronoun} {weapon} dealing [Red]{damage}[/Red] damage!";
                defender.Stamina.Current -= damage;
                if (defender.Stamina.Current < 0)
                {
                    defender.Stamina.Current = 0;
                }
            }
            else
            {
                text = $"{Name} miss!";
            }
            return text;
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
