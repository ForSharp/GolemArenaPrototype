using System;

namespace GolemEntity.ExtraStats
{
    [Serializable]
    public struct ExtraStatsParameter
    {
        public float ChangingValue;
        public ExtraStats StatsType;
        public bool IsFlat;

        public ExtraStatsParameter(float changingValue, ExtraStats statsType, bool isFlat)
        {
            ChangingValue = changingValue;
            StatsType = statsType;
            IsFlat = isFlat;
        }
    }
}