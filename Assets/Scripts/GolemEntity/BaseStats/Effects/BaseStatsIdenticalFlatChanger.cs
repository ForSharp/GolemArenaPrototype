namespace GolemEntity.BaseStats.Effects
{
    public class BaseStatsIdenticalFlatChanger : StatsDecorator
    {
        private readonly float _changingValue;
        public BaseStatsIdenticalFlatChanger(float changingValue, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _changingValue = changingValue;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            var tempStats = new GolemBaseStats()
            {
                strength = _changingValue,
                agility = _changingValue,
                intelligence = _changingValue
            };
            
            return WrappedEntity.GetBaseStats() + tempStats;
        }
    }
}