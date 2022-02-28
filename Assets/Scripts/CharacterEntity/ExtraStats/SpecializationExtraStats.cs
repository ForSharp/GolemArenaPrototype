using CharacterEntity.BaseStats;

namespace CharacterEntity.ExtraStats
{
    public class SpecializationExtraStats : ExtraStatsDecorator
    {
        private readonly Specialization _specialization;

        public SpecializationExtraStats(Specialization specialization, IExtraStatsProvider wrappedEntity, CharacterBaseStats baseStats) : base(
             wrappedEntity, baseStats)
        {
            _specialization = specialization;
        }

        protected override CharacterExtraStats GetExtraStatsInternal(CharacterBaseStats baseStats)
        {
            return _wrappedEntity.GetExtraStats() + GetSpecExtraStats(baseStats);
        }

        private CharacterExtraStats GetSpecExtraStats(CharacterBaseStats baseStats)
        {
            return CharacterStatsService.GetExtraStats(_specialization, baseStats);
        }
        
    }
}
