﻿using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Potions;
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

        //public void DropItems(Player player)
        //{
        //    Potion potion;
        //    if (Name == "Hocke")
        //    {
        //        potion = new Potion("Monster Energy", 100, player.MaxHp, "You unleashed the beast");
        //    }
        //    else if (Name == "Daudi")
        //    {
        //        potion = new Potion("Coke Zero", 100, 1000, "You taste the feeling");
        //    }
        //    else
        //    {
        //        Potion[] potions = Utility.GetPotions();
        //        potion = potions[Utility.random.Next(potions.Length)];
        //    }
        //    potion.Quantity = Utility.random.Next(1, 11);
        //    player.Backpack.Add(potion);
        //}
    }
}
