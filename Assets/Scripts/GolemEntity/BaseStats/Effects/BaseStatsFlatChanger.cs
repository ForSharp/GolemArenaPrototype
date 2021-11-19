namespace GolemEntity.BaseStats.Effects
{
    public class BaseStatsFlatChanger : StatsDecorator
    {
        private readonly GolemBaseStats _changingStats;
        public BaseStatsFlatChanger(GolemBaseStats changingStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _changingStats = changingStats;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + _changingStats;
        }
    }
}