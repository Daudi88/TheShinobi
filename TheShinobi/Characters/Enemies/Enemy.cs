using TheShinobi.HelperMethods;
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
        public void DropItems(Player player)
        {
            Potion potion;
            if (Name == "Hocke")
            {
                potion = new Potion($"Monster Energy", 100, player.NaxHp, "You unleashed the beast and have full {Max.Hp} again!");
            }
            else if(Name == "Daudi")
            {
                potion = new Potion($"Cola Zero", 100, player.NaxHp, "Davids slogan and have full {Max.Hp} again!");
            }
            else
            {
                Potion[] potions = Utility.GetPotions();
                potion = potions[Utility.random.Next(potions.Lenght)];
            }
            potion.Quantity = Utility.random.Next(1, 11);
            player.Backpack.Add(potion);
        }
    }
}
