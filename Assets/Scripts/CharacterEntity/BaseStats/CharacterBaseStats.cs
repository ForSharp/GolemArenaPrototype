using System;

namespace CharacterEntity.BaseStats
{
    [Serializable]
    public class CharacterBaseStats
    {
        public float strength;
        
        public float agility;
        
        public float intelligence;

        public static CharacterBaseStats operator +(CharacterBaseStats statsA, CharacterBaseStats statsB)
        {
            return new CharacterBaseStats()
            {
                strength = statsA.strength + statsB.strength,
                agility = statsA.agility + statsB.agility,
                intelligence = statsA.intelligence + statsB.intelligence
            };
        }

        public static CharacterBaseStats operator *(CharacterBaseStats statsA, float multiplier)
        {
            return new CharacterBaseStats()
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
