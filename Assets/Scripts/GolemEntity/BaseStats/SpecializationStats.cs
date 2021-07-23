namespace GolemEntity.BaseStats
{
    public class SpecializationStats : StatsDecorator
    {
        
        private readonly Specialization _specialization;
        
        public SpecializationStats(IStatsProvider wrappedEntity, Specialization specialization) : base(wrappedEntity)
        {
            _specialization = specialization;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + GetSpecStats();
        }

        private GolemBaseStats GetSpecStats()
        {
            return CharacterStatsService.GetBaseStats(_specialization);
        }
    }
}
