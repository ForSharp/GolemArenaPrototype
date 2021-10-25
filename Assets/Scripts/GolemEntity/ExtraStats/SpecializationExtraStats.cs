using System;
using GolemEntity.BaseStats;

namespace GolemEntity.ExtraStats
{
    public class SpecializationExtraStats : ExtraStatsDecorator
    {
        private readonly Specialization _specialization;

        public SpecializationExtraStats(Specialization specialization, IExtraStatsProvider wrappedEntity, GolemBaseStats baseStats) : base(
             wrappedEntity, baseStats)
        {
            _specialization = specialization;
        }

        protected override GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats)
        {
            return _wrappedEntity.GetExtraStats() + GetSpecExtraStats(baseStats);
        }

        private GolemExtraStats GetSpecExtraStats(GolemBaseStats baseStats)
        {
            return CharacterStatsService.GetExtraStats(_specialization, baseStats);
        }
        
    }
}
