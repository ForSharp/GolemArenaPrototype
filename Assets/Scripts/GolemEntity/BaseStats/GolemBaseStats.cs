using System;

namespace GolemEntity.BaseStats
{
    [Serializable]
    public class GolemBaseStats
    {
        public float strength;
        
        public float agility;
        
        public float intelligence;

        public static GolemBaseStats operator +(GolemBaseStats statsA, GolemBaseStats statsB)
        {
            return new GolemBaseStats()
            {
                strength = statsA.strength + statsB.strength,
                agility = statsA.agility + statsB.agility,
                intelligence = statsA.intelligence + statsB.intelligence
            };
        }

        public static GolemBaseStats operator *(GolemBaseStats statsA, float multiplier)
        {
            return new GolemBaseStats()
            {
                strength = statsA.strength * multiplier,
                agility = statsA.agility * multiplier,
                intelligence = statsA.intelligence * multiplier
            };
        }

        public override string ToString()
        {
            return $"Strength = {strength}, Agility = {agility}, Intelligence = {intelligence}";
        }
        
    }
}
