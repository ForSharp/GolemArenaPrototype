namespace __Scripts.CharacterEntity.BaseStats.Effects
{
    public class BaseStatsMultiplierChanger : StatsDecorator
    {
        private readonly CharacterBaseStats _changingStats;
        private readonly CharacterBaseStats _multiplier;
        public BaseStatsMultiplierChanger(CharacterBaseStats changingStats, CharacterBaseStats multiplier, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _changingStats = changingStats;
            _multiplier = multiplier;
        }

        protected override CharacterBaseStats GetStatsInternal()
        {
            var tempStats = new CharacterBaseStats
            {
                strength = _multiplier.strength * _changingStats.strength,
                agility = _multiplier.agility * _changingStats.agility,
                intelligence = _multiplier.intelligence * _changingStats.intelligence
            };

            return WrappedEntity.GetBaseStats() + tempStats;
        }
    }
}