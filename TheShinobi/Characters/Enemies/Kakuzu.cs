using TheShinobi.HelperMethods;
using TheShinobi.Items.Armors;
using TheShinobi.Items.Weapons;

namespace TheShinobi.Characters.Enemies
{
    class Kakuzu : Enemy
    {
        public Kakuzu()
        {
            Name = "Kakuzu";
            Level = 5;
            Hp = 50;
            Exp = 50;
            Armor = new FlakJacket();
            Weapon = new Sword(); // vilket är hans vapen? 
            Defence = Armor.Defence;
            Damage = Weapon.Damage;
            Gold = Utility.random.Next(1, 100 * Level);

            /*
             * Kakuzu has some very strange techniques, 
             * the weirdest of them all is that his body 
             * is made up of a string that can expand 
             * across a long distance by his chakra.
             * Kakuzu can use his string to shoot out 
             * limbs off his body to attack enemies.
             */
        }
    }
}
