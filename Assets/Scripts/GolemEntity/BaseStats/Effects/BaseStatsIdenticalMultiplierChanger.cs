namespace GolemEntity.BaseStats.Effects
{
    public class BaseStatsIdenticalMultiplierChanger : StatsDecorator
    {
        private readonly float _value;
        private readonly GolemBaseStats _multiplier;
        public BaseStatsIdenticalMultiplierChanger(float value, GolemBaseStats multiplier, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _value = value;
            _multiplier = multiplier;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + (_multiplier * _value);
        }
    }
}