namespace TheShinobi.Abilities
{
    class Ninjutsu
    {
        // E-Rank

        // D-Rank
        // Paper Shuriken   =>  Harden and shape paper into shuriken.

        // C-Rank
        // Earth Release: Tearing Earth Turning Palm    =>  By placing the palm of their hand on the ground, the user breaks up and shifts the local earth.
        // Fire Release: Dragon Fire Technique  => The user spits flames from their mouth that, like a dragon's, quickly travel forward in a straight direction.
        // Fire Release: Flame Bullet   =>  This technique simply shoots a flame bullet at the enemy.
        // Fire Release: Great Fireball Technique   =>  Against living targets, the fireball can cause extensive burns.
        // Fire Release: Phoenix Sage Fire Technique    =>  The user spits a volley of small fireballs into the air; at a glance, it can appear as though there was only one fireball that then burst into many. 
        // Lightning Release: Lightning Beast Tracking Fang =>  The user forms lightning in their hand and launches it at the opponent in the form a hound to attack them.
        // Water Release: Gunshot   =>  The user creates water in their body using chakra. The user then spits the water from their mouth as orb-shaped projectiles
        // Water Release: Wild Water Wave   =>  The user spews water from their mouth in a waterfall-like fashion to wash away the target
        // 


        public string Name { get; set; }
        public string Damage { get; set; }
        public int Cost { get; set; }
        public Ninjutsu(string name, string damage, int cost)
        {
            Name = name;
            Damage = damage;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"{Name} ({Damage} Damage, -{Cost} Chakra)"; 
        }
    }
}
