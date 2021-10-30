namespace GolemEntity.BaseStats.Effects
{
    public class WeaknessTremor : StatsDecorator
    {
        public WeaknessTremor(IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            var stats = WrappedEntity.GetBaseStats();
            stats.strength *= 0.9f;
            stats.agility *= 0.9f;
            return stats;
        }
    }
}
