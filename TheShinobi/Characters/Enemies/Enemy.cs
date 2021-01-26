using System;
using TheShinobi.HelperMethods;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Enemy : Character
    {
        public Enemy(string name, int level, int hp, Armor armor, Weapon weapon)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Armor = armor;
            Weapon = weapon;
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level + 1);
            Exp = Utility.random.Next(10 * Level, 40 * Level + 1);
        }

        public override int Attack(Character defender)
        {
            if (Utility.random.Next(100) >= defender.Defence)
            {
                return Utility.RollDice(Damage);
            }
            else
            {
                return 0;
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
                eDropText = $"{equipable.IndefiniteArticle} {equipable.Name}";
            }
            Item consumable;
            if (Name == "Hocke")
            {
                cDrop = true;
                consumable = new EnergyDrink("Monster Energy", 100, 15, "You unleash the beast");
                consumable.Quantity = Utility.random.Next(1, 11);
                player.Backpack.Add(consumable);
                cDropText = $"{consumable.Quantity} Monster Energy and ";
            }
            else if (Name == "Daudi")
            {
                cDrop = true;
                consumable = new EnergyDrink("NOCCO", 100, 12, "You are NOCCO enough");
                consumable.Quantity = Utility.random.Next(1, 11);
                player.Backpack.Add(consumable);
                cDropText = $"{consumable.Quantity} NOCCO and ";
            }
            else
            {
                if (Utility.random.Next(10) > 6)
                {
                    cDrop = true;
                    consumable = Get.Potions()[Utility.random.Next(Get.Potions().Length)];
                    consumable.Quantity = Utility.random.Next(1, 11);
                    player.Backpack.Add(consumable);
                    cDropText = $"{consumable.Quantity} {consumable.Name} and ";

                }
            }
            string separator = eDrop ? " and " : eDrop && cDrop ? ", " : "";
            ColorConsole.Write($"\t {Name} drops {eDropText}{separator}{cDropText}{Gold} gold!", ConsoleColor.Yellow);
            player.Gold += Gold;
        }
    }
}
