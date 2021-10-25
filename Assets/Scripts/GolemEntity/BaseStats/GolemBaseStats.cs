namespace GolemEntity.BaseStats
{
    public struct GolemBaseStats 
    {
        public float Strength { get; set; }
        
        public float Agility { get; set; }
        
        public float Intelligence { get; set; }

        public static GolemBaseStats operator +(GolemBaseStats statsA, GolemBaseStats statsB)
        {
            return new GolemBaseStats()
            {
                Strength = statsA.Strength + statsB.Strength,
                Agility = statsA.Agility + statsB.Agility,
                Intelligence = statsA.Intelligence + statsB.Intelligence
            };
        }

        public static GolemBaseStats operator *(GolemBaseStats statsA, float multiplier)
        {
            return new GolemBaseStats()
            {
                Strength = statsA.Strength * multiplier,
                Agility = statsA.Agility * multiplier,
                Intelligence = statsA.Intelligence * multiplier
            };
        }

        

        public override string ToString()
        {
            return $"Strength = {Strength}, Agility = {Agility}, Intelligence = {Intelligence}";
        }

        
    }
}
