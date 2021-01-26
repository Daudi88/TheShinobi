using TheShinobi.HelperMethods;
using TheShinobi.Interfaces;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
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
            Item item;
            if (Name == "Hocke")
            {
                item = new EnergyDrink("Monster Energy", 100, 15, "You unleash the beast");
            }
            else if (Name == "Daudi")
            {
                item = new EnergyDrink("NOCCO", 100, 12, "You are NOCCO enough");
            }
            else
            {
                Consumable[] potions = Get.Potions();
                item = potions[Utility.random.Next(potions.Length)];
            }
            item.Quantity = Utility.random.Next(1, 11);
            player.Backpack.Add(item);
        }
    }
}
