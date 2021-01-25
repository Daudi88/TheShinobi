using TheShinobi.HelperMethods;
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
            Consumable consumable;
            if (Name == "Hocke")
            {
                consumable = new EnergyDrink("Monster Energy", 100, player.MaxHp, "You unleash the beast", 20);
            }
            else if (Name == "Daudi")
            {
                consumable = new EnergyDrink("NOCCO BCAA", 100, player.MaxHp, "You are NOCCO enough", 20);
            }
            else
            {
                Consumable[] potions = Utility.GetPotions();
                consumable = potions[Utility.random.Next(potions.Length)];
            }
            consumable.Quantity = Utility.random.Next(1, 11);
            player.Backpack.Add(consumable);
        }
    }
}
