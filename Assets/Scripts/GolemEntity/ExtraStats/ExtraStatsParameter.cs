using System;

namespace GolemEntity.ExtraStats
{
    [Serializable]
    public struct ExtraStatsParameter
    {
        public float ChangingValue;
        public ExtraStats StatsType;

        public ExtraStatsParameter(float changingValue, ExtraStats statsType)
        {
            ChangingValue = changingValue;
            StatsType = statsType;
        }
    }
}