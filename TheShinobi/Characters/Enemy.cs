using TheShinobi.Abilities.Ninjutsus;
using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters
{
    /// <summary>
    /// Class for all enemies in the game. A lot of base stats are randomized.
    /// </summary>
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

        /// <summary>
        /// <see cref="Enemy"/> performes attempt on attacking 
        /// <see cref="Player"/> character. Choice of weapon is randomized.
        /// </summary>
        /// <param name="defender"></param>
        /// <returns></returns>
        public override string Attack(Character defender)
        {
            string text = "";            
            if (Utility.RollDice("1d20") >= defender.Defence)
            {
                string weapon;
                int damage;
                Ninjutsu jutsu = Ninjutsus[Utility.random.Next(Ninjutsus.Count)];
                if (Utility.random.Next(200) > 30 && Chakra.Current - jutsu.Cost > 0)
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
                text = $"{Name} hits you with {Pronoun} {weapon} dealing [Yellow]{damage}[/Yellow] damage!";
                defender.Stamina.Current -= damage;
                if (defender.Stamina.Current < 0)
                {
                    defender.Stamina.Current = 0;
                }
            }
            else
            {
                text = $"{Name} misses you with {Pronoun} attack!";
            }
            return text;
        }

        /// <summary>
        /// When defeated the <see cref="Enemy"/> drops ryō and give 
        /// experience point to <see cref="Player"/>. May also drop equipment.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="top"></param>
        public void DropItems(Player player, int top)
        {
            int rnd = Utility.random.Next(100);
            bool eDrop = false;
            bool cDrop = false;
            string eDropText = "";
            string cDropText = "";
            if (rnd > 60)
            {
                eDrop = true;
                Item equipable;
                if (rnd > 80 && !Weapon.Name.Contains("Fists"))
                {
                    equipable = Weapon;
                }
                else
                {
                    equipable = Armor;
                }
                eDropText = $"{equipable.IndefiniteArticle} [Yellow]{equipable.Name}[/Yellow]";
                Utility.AddToBackpack(player, equipable);
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
                    int quantity = Utility.random.Next(1, 11);
                    Utility.AddToBackpack(player, consumable, quantity);
                    player.Backpack.Add(consumable);
                    cDropText = $"[Yellow]{consumable.Quantity} {consumable.Name}[/Yellow] and ";

                }
            }
            string separator = eDrop ? " and " : eDrop && cDrop ? ", " : "";
            string text = $"{Name} drops {eDropText}{separator}{cDropText}[Yellow]{Ryō}[/Yellow] ryō!";
            ColorConsole.WriteEmbeddedSetDelayed(text, top, blink: false, delay: 0);
            top++;
            ColorConsole.WriteEmbeddedSetDelayed($"You gain [Yellow]{Exp.Current}[/Yellow] exp from defeating {Name}!", top);
            player.Ryō += Ryō;
            player.Exp.Current += Exp.Current;
        }
    }
}
