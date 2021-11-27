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
            var baseStats = WrappedEntity.GetBaseStats();
            baseStats.strength *= _changingStats.strength;
            baseStats.agility *= _changingStats.agility;
            baseStats.intelligence *= _changingStats.intelligence;

            return baseStats;
        }
    }
}