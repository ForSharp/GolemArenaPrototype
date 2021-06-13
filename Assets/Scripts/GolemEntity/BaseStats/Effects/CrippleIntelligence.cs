using GolemEntity.BaseStats;

namespace Scripts.BaseStats.Effects
{
    public class CrippleIntelligence : StatsDecorator
    {
        public CrippleIntelligence(IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            var stats = WrappedEntity.GetBaseStats();
            stats.Intelligence = 0;
            return stats;
        }
    }
}
