namespace CharacterEntity.BaseStats.Effects
{
    public class BaseStatsUltimateChanger : StatsDecorator
    {
        private readonly CharacterBaseStats _changingStats;
        public BaseStatsUltimateChanger(CharacterBaseStats changingStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _changingStats = changingStats;
        }

        protected override CharacterBaseStats GetStatsInternal()
        {
            var baseStats = WrappedEntity.GetBaseStats();
            baseStats.strength *= _changingStats.strength;
            baseStats.agility *= _changingStats.agility;
            baseStats.intelligence *= _changingStats.intelligence;

            return baseStats;
        }
    }
}