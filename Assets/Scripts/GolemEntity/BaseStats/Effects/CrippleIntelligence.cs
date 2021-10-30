namespace GolemEntity.BaseStats.Effects
{
    public class CrippleIntelligence : StatsDecorator
    {
        public CrippleIntelligence(IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            var stats = WrappedEntity.GetBaseStats();
            stats.intelligence = 0;
            return stats;
        }
    }
}
