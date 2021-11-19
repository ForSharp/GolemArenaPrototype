namespace GolemEntity.BaseStats.Effects
{
    public class BaseStatsUltimateChanger : StatsDecorator
    {
        private readonly GolemBaseStats _changingStats;
        public BaseStatsUltimateChanger(GolemBaseStats changingStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _changingStats = changingStats;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            WrappedEntity.GetBaseStats().strength *= _changingStats.strength;
            WrappedEntity.GetBaseStats().agility *= _changingStats.agility;
            WrappedEntity.GetBaseStats().intelligence *= _changingStats.intelligence;

            return WrappedEntity.GetBaseStats();
        }
    }
}