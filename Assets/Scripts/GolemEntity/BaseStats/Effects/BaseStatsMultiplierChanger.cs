namespace GolemEntity.BaseStats.Effects
{
    public class BaseStatsMultiplierChanger : StatsDecorator
    {
        private readonly GolemBaseStats _changingStats;
        private readonly GolemBaseStats _multiplier;
        public BaseStatsMultiplierChanger(GolemBaseStats changingStats, GolemBaseStats multiplier, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _changingStats = changingStats;
            _multiplier = multiplier;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            var tempStats = new GolemBaseStats
            {
                strength = _multiplier.strength * _changingStats.strength,
                agility = _multiplier.agility * _changingStats.agility,
                intelligence = _multiplier.intelligence * _changingStats.intelligence
            };

            return WrappedEntity.GetBaseStats() + tempStats;
        }
    }
}