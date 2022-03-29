namespace __Scripts.CharacterEntity.BaseStats.Effects
{
    public class BaseStatsIdenticalMultiplierChanger : StatsDecorator
    {
        private readonly float _value;
        private readonly CharacterBaseStats _multiplier;
        public BaseStatsIdenticalMultiplierChanger(float value, CharacterBaseStats multiplier, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _value = value;
            _multiplier = multiplier;
        }

        protected override CharacterBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + (_multiplier * _value);
        }
    }
}