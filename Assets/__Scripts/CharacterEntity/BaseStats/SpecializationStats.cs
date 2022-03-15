using __Scripts.CharacterEntity;

namespace CharacterEntity.BaseStats
{
    public class SpecializationStats : StatsDecorator
    {
        
        private readonly Specialization _specialization;
        
        public SpecializationStats(IStatsProvider wrappedEntity, Specialization specialization) : base(wrappedEntity)
        {
            _specialization = specialization;
        }

        protected override CharacterBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + GetSpecStats();
        }

        private CharacterBaseStats GetSpecStats()
        {
            return CharacterStatsService.GetBaseStats(_specialization);
        }
    }
}
