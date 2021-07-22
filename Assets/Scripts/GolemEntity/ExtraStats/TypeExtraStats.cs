using System;
using GolemEntity.BaseStats;

namespace GolemEntity.ExtraStats
{
    public class TypeExtraStats : IExtraStatsProvider
    {
        private GolemType _type;
        private GolemBaseStats _baseStats;

        public TypeExtraStats(GolemType type, GolemBaseStats baseStats)
        {
            _type = type;
            _baseStats = baseStats;
        }

        public GolemExtraStats GetExtraStats()
        {
            return CharacterStatsService.GetExtraStats(_type, _baseStats);
        }
    }
}
