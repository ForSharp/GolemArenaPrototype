namespace CharacterEntity.BaseStats.Effects
{
    public class BaseStatsFlatChanger : StatsDecorator
    {
        private readonly CharacterBaseStats _changingStats;
        public BaseStatsFlatChanger(CharacterBaseStats changingStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _changingStats = changingStats;
        }

        protected override CharacterBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + _changingStats;
        }
    }
}