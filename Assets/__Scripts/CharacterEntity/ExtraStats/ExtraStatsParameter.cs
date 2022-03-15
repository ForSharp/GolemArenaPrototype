using System;

namespace CharacterEntity.ExtraStats
{
    [Serializable]
    public struct ExtraStatsParameter
    {
        public float ChangingValue;
        public __Scripts.CharacterEntity.ExtraStats.ExtraStats StatsType;
        public bool IsFlat;

        public ExtraStatsParameter(float changingValue, __Scripts.CharacterEntity.ExtraStats.ExtraStats statsType, bool isFlat)
        {
            ChangingValue = changingValue;
            StatsType = statsType;
            IsFlat = isFlat;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ExtraStatsParameter other)) return false;
            return Math.Abs(ChangingValue - other.ChangingValue) < 0.01f && StatsType == other.StatsType &&
                   IsFlat == other.IsFlat;
        }

        public override int GetHashCode()
        {
            return (int)ChangingValue * 1000 + (int)StatsType * 10 + (IsFlat ? 1 : 0);
        }
    }
}