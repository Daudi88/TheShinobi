using System;
using System.Collections.Generic;
using System.Linq;
using TheShinobi.Abilities;
using TheShinobi.Characters;
using TheShinobi.Items;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Consumables;
using TheShinobi.Items.Weapons;
using TheShinobi.Structures;

namespace TheShinobi.HelperMethods
{
    static class Get
    {
        /* This class contains the following methods:
         * GetEnemies()         - Returns an array of Enemies the player can 
         *                        encountered in the game.
         * GetArmors()          - Returns an array of Armors common enemies use 
         *                        in the game and that can be purchased at the  
         *                        Ninja Tool Shop.
         * GetWeapons()         - Returns an array of Weapons common enemies use
         *                        in the game that can be purchased at the  
         *                        Ninja Tool Shop.
         * GetSevenSwords()     - Returns an array of the seven legendary swords 
         *                        that can be found at the graveyard.
         * Getpotions()         - Returns an array of common potions used in the game.
         * GetMeals()           - Returns an array of meals for Lightning Burger Shop.
         * GetAbuHassanItems()  - Returns an array of Abu Hassan's special items.
         */

        public static int ContentLength(List<string> content)
        {
            List<int> lengths = new List<int>();
            foreach (var text in content)
            {
                int length = text.Length;
                lengths.Add(length -= ColorLength(text));
            }
            return lengths.OrderByDescending(i => i).First();
        }

        public static int ColorLength(string text)
        {
            if (text.Contains("["))
            {
                int at = text.IndexOf("[");
                int at2 = text.IndexOf("]");
                return text.Substring(at, at2 - at + 1).Length * 2 + 1;
            }
            return 0;
        }

        public static string Status(Ability ability, string color)
        {
            if (ability.Current < ability.Max / 5)
            {
                return $"[Red]{ability.Current}[/Red]";
            }
            else
            {
                return $"[{color}]{ability.Current}[/{color}]";
            }
        }

        public static string DamageDice(int level)
        {
            return level switch
            {
                1 => "1d8",
                2 => "1d10",
                3 => "1d12",
                4 => "2d8",
                5 => "2d10",
                6 => "2d12",
                7 => "3d10",
                8 => "3d12",
                9 => "4d10",
                10 => "4d12",
                _ => ""
            };
        }

        public static List<Func<Player, bool>> Options(Player player, out List<string> content)
        {
            var positions = Positions();
            string choices = positions[player.Pos].Item3;
            int ctr = 1;
            content = new List<string>();
            var options = new List<Func<Player, bool>>();
            foreach (var letter in choices)
            {
                switch (letter)
                {
                    case 'N':
                        content.Add($"{ctr++}. Go North");
                        options.Add(Adventure.North);
                        break;
                    case 'E':
                        content.Add($"{ctr++}. Go East");
                        options.Add(Adventure.East);
                        break;
                    case 'W':
                        content.Add($"{ctr++}. Go West");
                        options.Add(Adventure.West);
                        break;
                    case 'S':
                        content.Add($"{ctr++}. Go South");
                        options.Add(Adventure.South);
                        break;
                    case 'V':
                        content.Add($"{ctr++}. Go back home");
                        options.Add(Adventure.ToVillage);
                        break;
                    case 'H':
                        content.Add($"{ctr++}. Go West");
                        options.Add(Adventure.ToHiruzen);
                        break;
                    case 'T':
                        content.Add($"{ctr++}. Go North");
                        options.Add(Adventure.ToTreasure);
                        break;
                    case 'G':
                        content.Add($"{ctr++}. Go East");
                        options.Add(Adventure.ToGraveyard);
                        break;
                    case 'A':
                        content.Add($"{ctr++}. Go West");
                        options.Add(Adventure.ToAbuHassan);
                        break;
                    case 'B':
                        content.Add($"{ctr++}. Go North");
                        options.Add(Adventure.BossEncounter);
                        break;
                    default:
                        break;
                }
            }
            return options;
        }

        public static Dictionary<double, Tuple<int, int, string>> Positions()
        {
            var positions = new Dictionary<double, Tuple<int, int, string>>();
            positions.Add(0.2, new Tuple<int, int, string>(15, 15, "TE"));
            positions.Add(0.3, new Tuple<int, int, string>(13, 10, "S"));
            positions.Add(0.4, new Tuple<int, int, string>(12, 4, "E"));
            positions.Add(1.2, new Tuple<int, int, string>(25, 15, "NEW"));
            positions.Add(1.3, new Tuple<int, int, string>(25, 9, "NES"));
            positions.Add(1.4, new Tuple<int, int, string>(21, 4, "AS"));
            positions.Add(2.0, new Tuple<int, int, string>(38, 19, ""));
            positions.Add(2.1, new Tuple<int, int, string>(38, 17, "NV"));
            positions.Add(2.2, new Tuple<int, int, string>(38, 15, "NEWS"));
            positions.Add(2.3, new Tuple<int, int, string>(36, 9, "EWS"));
            positions.Add(2.4, new Tuple<int, int, string>(38, 5, "BE"));
            positions.Add(3.2, new Tuple<int, int, string>(49, 15, "NEW"));
            positions.Add(3.3, new Tuple<int, int, string>(48, 9, "NWS"));
            positions.Add(3.4, new Tuple<int, int, string>(48, 5, "EW"));
            positions.Add(4.1, new Tuple<int, int, string>(72, 21, "NG"));
            positions.Add(4.2, new Tuple<int, int, string>(69, 15, "EWS"));
            positions.Add(4.3, new Tuple<int, int, string>(70, 10, "E"));
            positions.Add(4.4, new Tuple<int, int, string>(65, 5, "EW"));
            positions.Add(5.1, new Tuple<int, int, string>(85, 21, "W"));
            positions.Add(5.2, new Tuple<int, int, string>(82, 15, "NW"));
            positions.Add(5.3, new Tuple<int, int, string>(82, 10, "NHS"));
            positions.Add(5.4, new Tuple<int, int, string>(80, 5, "WS"));
            return positions;
        }

        public static Enemy[] Enemies()
        {
            Enemy[] enemies = new Enemy[]
            {
                new Enemy("Shinobi", "Sound Four", 1, new Shirt(), new Fists()),
                new Enemy("Sakon", "Sound Five", 2, new FlakJacket(), new Kiba()),
                new Enemy("Tayuya","Sound Four", 2, new Shirt(), new Fists()),
                new Enemy("Ukon", "Sound Five", 2, new Shirt(), new Kusarigama()),
                new Enemy("Sasori", "Akatsuki", 2, new Shirt(), new TekagiShuko()),
                new Enemy("Konan", "Amegakure", 3, new FlakJacket(), new Kubikiribōchō()),
                new Enemy("Nagato", "Uzumaki", 3, new SteamArmor(), new Kunai()),
                new Enemy("Haku", "Yuki", 4, new FlakJacket(), new Shuriken()),
                new Enemy("Obito", "Uchiha", 4, new ChakraArmor(), new Gunbai()),
                new Enemy("Kaguya", "Kaguya", 5, new FlakJacket(), new FistsOfBones()),
                new Enemy("Ginkaku", "Kinkaku Force", 5, new SteamArmor(), new Shichiseiken()),
                new Enemy("Madara", "Uchiha", 6, new InfiniteArmor(), new Gunbai()),
                new Enemy("Hanzō", "Amegakure", 6, new InfiniteArmor(), new Kusarigama()),
                new Enemy("Deidara", "Akatsuki", 7, new SteamArmor(), new Shuriken()),
                new Enemy("Kimimaro", "Kaguya", 7, new ChakraArmor(), new FistsOfBones()),
                new Enemy("Kabuto", "Konohagakure", 7, new SteamArmor(), new Shichiseiken()),
                new Enemy("Kisame", "Hoshigaki", 8, new FlakJacket(), new Kunai()),
                new Enemy("Kakuzu", "Akatsuki", 8, new InfiniteArmor(), new Kusarigama()),
                new Enemy("Hocke", "1337", 8, new BulletproofVest(), new AK47()),
                new Enemy("Daudi", "1337", 9, new BulletproofVest(), new AK47()),

            };
            return enemies;
        }

        public static Armor[] Armors()
        {
            Armor[] armors = new Armor[]
            {
                new FlakJacket(),
                new SteamArmor(),
                new ShinobiBattleArmor(),
                new ChakraArmor(),
                new InfiniteArmor()
            };
            return armors;
        }

        public static Weapon[] Weapons()
        {
            Weapon[] weapons = new Weapon[]
            {
                new Kunai(),
                new Shuriken(),
                new TekagiShuko(),
                new ChakraBlade(),
                new Gunbai(),
                new Kusarigama(),
                new Shichiseiken()

            };
            return weapons;
        }

        public static Weapon[] SevenSwords(Player player)
        {
            List<Weapon> weapons = new List<Weapon>()
            {
                new Kabutowari(),
                new Hiramekarei(),
                new Samehada(),
                new Kiba(),
                new Nuibari(),
                new Shibuki()
            };

            if (player.Name != "Kakashi Hatake")
            {
                weapons.Add(new Kubikiribōchō());
            }
            return weapons.ToArray();
        }

        public static Consumable[] Potions()
        {
            Consumable[] potions = new Consumable[]
            {
                new Consumable("Lesser Healing Potion", 15, 5),
                new Consumable("Healing Potion", 20, 8),
                new Consumable("Greater Healing Potion", 25, 10),
                new Consumable("Superior Healing Potion", 30, 12)
            };
            return potions;
        }

        public static Consumable[] Meals()
        {
            string text = "You eat a bowl of";
            Consumable[] meals = new Consumable[]
            {
                new Consumable("Fried Chicken Ramen", 45, 25, text),
                new Consumable("Shabu Beef Ramen", 45, 25, text),
                new Consumable("Naruto Chashu Ramen", 60, 30, text),
                new Consumable("Stew Pork Ramen", 45, 25, text),
                new Consumable("Seafood Ramen", 85, 40, text),
                new Consumable("Vegetable Ramen", 45, 25, text)
            };
            return meals;
        }

        public static Item[] AbuHassanItems()
        {
            Item[] items = new Item[]
            {
                new BulletproofVest(), new AK47(),
                new EnergyDrink("Red Bull", 50, 10, "You get wings")
            };
            return items;
        }

        public static string[] NoFightStories()
        {
            string[] stories = new string[]
            {
                 "\n\t As you walk up the mountain trail you sense a group of ninjas from the Uchicha Clan ahead." +
                 "\n\t You easily sneak past them on your way to rescue Hanare!",

                 "\n\t You hear something in the distance and use your Sharingan to see a group of Ninjas lying in ambush for travelers." +
                 "\n\t If it wasn't for that you are in a rush to rescue Hanare you would have made minced meat of them all!",

                 "\n\t You use your senses and feel that no enemy is near, you set up camp and light a fire" +
                 "\n\t You eat a delicious Green Chilli Burger while watching the Sunset over The Hidden Leaf Village.",

                 "\n\t As the sun sets over the mountains you see a clan who has setup camp on your land!" +
                 "\n\t There is no time to engage them now, first you must rescue Hanare!",

                 "\n\t As you walk through The Shikkotsu Forest you sense Hanare is further North!" +
                 "\n\t You start running towards the montains!",

                 "\n\t You aproach a empty camp, the fireplace is still smoldering a little and there are signs of someone who has been tied up!" +
                 "\n\t In the soil you see one of Hanare's black gloves! You follow their trail...",

                 "\n\t You sense that you are all alone at this place, the sun is shining and everything is calm" +
                 "\n\t You set up camp for the night and eat some Ramen from Ichiraku..."
            };
            return stories;
        }

        public static string[] FightStories(Enemy enemy)
        {
            string[] stories = new string[]
            {
                "\t  You walk down the bushy trail and sence there is trouble ahead!" +
                $"\n\t  You can smell that dirty {enemy.Name} from the {enemy.Clan} clan miles away and you attack instantly!",

                $"\t  While climbing up the mountains in search of Hanare you get ambushed by some filthyKLIPPHÄR {enemy.Clan} villains!" +
                "\n\t  You attack, maybe they know more about Hanare's kidnapping!",

                "\t  You have walked for hours when you decide to set up camp at a lake." +
                $"\n\t  Suddenly some low life from the {enemy.Clan} Clan attacks you!" +
                $"\n\t  Four of them run away when they see that it is you but {enemy.Name} stays behind to fight!",

                $"\t  Ahead you hear fighting going on! It is some bastards from the {enemy.Clan} robbing a villager!" +
                $"\n\t  You draw your weapon and attack!",

                "\t  The sky is full of dark clouds and when lightning strikes the ground vibrates!" +
                $"\n\t  As you set up camp in the mountains to escape the bad weather some villains from the {enemy.Clan} Clan attacks!",

                "\t  The sky is blue and the sun is shining. As you walk past a shrubbery you hear a noice!" +
                $"\n\t  A lowlife from the {enemy.Clan} Clan attacks you!",

                "\t  Walking on a narrow trail in search of tracks from the kidnappers" +
                $"\n\t  The {enemy.Clan} Clan suddenly attacks from all sides!",

                $"\t  A group of {enemy.Clan} clan are blocking your path ahead!" +
                "\n\t  As you approach they attack you!",

                $"\t  Some enemies from {enemy.Clan} clan are blocking your path" +
                "\n\t  There is no time to loose Hanare must be found quickly, you attack!",

                $"\t  You sence there is trouble ahead! The {enemy.Clan} clan is trying to hide around the corner" +
                $"\n\t  You can both smell and sense them miles away... You attack!",

                "\t  You have followed Hanares kidnappers trail for hours when you hear enemies further ahead." +
                $"\n\t  You draw your weapon and attack! the enemies from the {enemy.Clan} ",
            };
            return stories;
        }

        public static Ninjutsu Ninjutsu(string rank)
        {
            switch (rank)
            {
                case "Genin":
                    return new Ninjutsu("Paper Shuriken", "1d6", 5);
                case "Chūnin":
                    return new Ninjutsu("Flame Bullet", "3d8", 20);
                case "Jōnin":
                    return new Ninjutsu("Rasengan", "4d12", 50);
                case "Hokage":
                    return new Ninjutsu("Tailed Beast Ball", "5d20", 150);
                default:
                    return new Ninjutsu("", "", 0);
            }
        }

        public static bool NewRank(int level, out string rank)
        {
            switch (level)
            {
                case 1:
                    rank = "Genin";
                    return true;
                case 2:
                case 3:
                    rank = "Genin";
                    return false;
                case 4:
                    rank = "Chūnin";
                    return true;
                case 5:
                case 6:
                case 7:
                case 8:
                    rank = "Chūnin";
                    return false;
                case 9:
                    rank = "Jōnin";
                    return true;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    rank = "Jōnin";
                    return false;
                case 16:
                    rank = "Hokage";
                    return true;
                default:
                    rank = "Hokage";
                    return false;
            }
        }

        internal static int ExpLimit(string rank)
        {
            return rank switch
            {
                "Genin" => 1500,
                "Chūnin" => 9000,
                "Jōnin" => 30000,
                _ => 0
            };
        }
        public static string[] EndStory(Player player)
        {
            string[] stories = new string[]
                {
                "\t After fighting Orochimaru for hours you finally wear him down and kill him. \n" +
                $"\t As you dry Orochimaru's blood from your weapon on his Flack jacket.",

                "\n\t This fight is far from over, Orochimura did not do this on his own..." +
                "\n\t And you both sense and hear more enemies deeper inside tha cave system.",

                "\n\t You hear faint sounds deeper into the cave and you sense it is Hanare!\n " +
                "\n\t As you walked further into the huge cave system Hanares cry for help \n" +
                "\t get's clearer. You are closing in on her!",

                "\t Suddenly you sense danger from behinde \n" +
                "\t you instinctivly make a Shinobi backflip! \n" +
                "\n\t You dodge the sword that was beeing aimed at your back with full force!" +
                "\n\t You knew it! It is the filty backstabbing Akatsuku Clan! \n" +
                "\n\t Nobody else would ever try to kill a Shinobi from behinde." +
                "\n\t Three Akatsuku are attacking and they have you cornered. \n",

                "\t Beeing a true ninjutsu hero you are draw your Weapon and counter attack. \n" +
                "\t The closest attacker throws a Kunai aimed at your heart! \n" +
                "\n\t You use your chakra and redirect the knife into on of the other dirty" +
                "\n\t Akatsu scumbag's! \n",

                "\t This fight did not last longer then a few minutes. \n" +
                "n\t They where weak and in your way. \n" +
                "\t Now they are all very very dead...\n",

                "\t You sense Hanare is close and you walk into one of the dark side tunnels...\n" +
                "\t Your senses tells you that Hanare is not alone! \n" +
                "\t Someone with the ability to hide his true powers are also present.\n",

                "\t Who is this, and how can he block your senses? \n" +
                "\t You feel that the enemy and Hanare is just around the next corner. \n" +
                "\t As you draw your weapon and walk around the corner you se Hanare and him, HIM...\n\n",

                "\t This is impossible... He is dead!?! \n" +
                "\t you know you killed him and every last member of their Clan. \n" +
                "\t this cant be!?! Kakuzu's alive? \n",

                "\t Kakuzu laughs like a mad man when he sees your confusion “HA HA HA HA“ \n" +
                "\n\t “Did you not know that I teamed up with Orihime Inoue years ago? YOU FOOL!“ \n" +
                "\t “Orihime brought me back from the dead not more then 30 minutes after you left! \n" +
                "\t “She truly is special my dear Orihime...“",

                "\t “You should have killed her when you had the chance!“ \n" +
                "\n\t “Instead I will now kill someone near and dear to you! \n" +
                "\t “I want you to watch me kill Hanare before I come back and kill you to!” \n" +
                "\t “GRAB HIM!!!“\n",

                "\t You are suddenly overwhelmed by enemies pulling you down towards the ground, \n" +
                "\t where did they come from!? \n" +
                "\t Kakuzu must have used his {ability} to block your senses! \n",

                "\t Well, if this how he wants to play, fine by me! \n" +
                "\t “You shall meet your own powers you fool!“ \n\n" +
                "\t You remove your forhead protector and use your sharingan! \n" +
                "\n\t Your insight in Kakuzu's powers makes you block his senses \n" +
                "\t and he has no idea that you are approaching him. \n\n " +
                "\t Until its to late... \n",

                "\t “The rage {players.name} unleashed upon Kakuzu \n" +
                "\t has never been seen from any Shinobi ever before.\n" +
                "\t A true Shinobi are known to keep their cool even when angry! \n" +
                "\t The tale of this fight will live on as a tale forever...“\n",

                "\t As you come back to your senses you rush to Hanare \n" +
                "\t and untie the ropes around her hands and neck \n" +
                "\t she is still unconscious and very very weak. \n " +
                "\t without a second of rest you pick Hanare up and \n" +
                "\t carry her all the way back to The Hidden Leaf Village.\n",

                "\n\t As you approach the village, you call for help.\n" +
                "\t Tsunade meets you infront of Konoha Hospital \n" +
                "\t and instantly brings Hanare in for care... \n " +
                "\n\t You have waited hours for the medical-nin to heal Hanare. \n" +
                "\t Suddenly you hear Hanares voice...\n\n",

                "\t “Thank you {player.name}! You rescued me“ \n" +
                "\t “I knew you would find me, THANK YOU HERO!” \n\n",

                "\t You rescued Hanare and the peace is restored in The Hidden Leaf Village \n " +
                "\n\t THE END... \n\n",

                "\t CREDITS: \n" +
                "\t Kakashi Hatake - Robin Kamo\n" +
                "\t Campus Mölndal\n" +
                "\t Every cool person who has shared 8bit music\n" +
                "\t Youtube fabvl – Kakashi Rap Song Athousan.\n" +
                "\t Youtube suferas - Naruto - Sadness and Sorrow 8 Bit\n" +
                "\t Youtube Otaku Bits - Naruto Shippuden opening 3 - Blue Bird (8bit)\n" +
                "\t Youtube Musikage - Naruto Opening 2 - Haruka Kanata 8-bit NES Remix\n" +
                "\t Youtube 8 Bit Music Worl - Naruto - The Raising Fighting Spirit (8 bit)\n\n" +
                "\t CREATORS\n" +
                "\t Daudi - David Ström \n" +
                "\t Hocke - Håkan Eriksson"
                };
            return stories;
        }
    }
}
