﻿using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;
using TheShinobi.HelperMethods;
using System.Collections.Generic;

namespace TheShinobi.Characters
{
    class Player : Character
    {
        public double Pos { get; set; }
        public int AttackBonus { get; set; }
        public int MaxHp { get; set; }
        public int MaxExp { get; set; }
        public List<Item> Backpack { get; set; } = new List<Item>();
        public Player(string name)
        {
            Name = name;
            Level = 1;
            Hp = 30;
            MaxHp = 30;
            MaxExp = 200;
            Armor = new Shirt();
            Defence = Armor.Defence;
            Weapon = new Fists();
            Damage = Weapon.Damage;
        }

        public void LevelUp()
        {
            Level++;
            MaxExp += 200 * Level;
            MaxHp += Level;
            if (MaxHp > 1000)
            {
                MaxHp = 1000;
            }
            Hp = MaxHp;
        }

        public override int Attack(Character defender)
        {
            if (Utility.random.Next(100 + AttackBonus) >= defender.Defence)
            {
                return Utility.RollDice(Damage);
            }
            else
            {
                return 0; // När den här metoden returnerar 0 ska det skrivas ut att attacken missade/misslyckades :)
            }
        }
    }
}
