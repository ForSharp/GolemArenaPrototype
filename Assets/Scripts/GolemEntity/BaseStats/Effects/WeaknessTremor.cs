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
            stats.Strength *= 0.9f;
            stats.Agility *= 0.9f;
            return stats;
        }
    }
}
