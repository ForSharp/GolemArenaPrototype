using __Scripts.ExtraStats;
using __Scripts.GolemEntity.ExtraStats;

namespace __Scripts.GolemEntity
{
    public class Golem 
    {
        private readonly GolemType _golemType;
        private readonly Specialization _specialization;

        private IStatsProvider _provider;
        private static float _minBaseStats = 1000;
        
        public IStatsProvider Rate { get; }

        public Golem(GolemType golemType, Specialization specialization)
        {
            _golemType = golemType;
            _specialization = specialization;
            
            Rate = GetRate();
            _provider = new DefaultStats(_minBaseStats, Rate);
        }

        public string GetExtras()
        {
            IExtraStatsProvider extra;
            extra = new TypeExtraStats(_golemType);
            extra = new SpecializationExtraStats(extra, _specialization);
            return extra.GetExtraStats(GetCurrentStats(_provider)).ToString();
        }

        public void ChangeBaseStatsProportionally(float value)
        {
            _provider = new BaseStatsEditor(value, GetCurrentStats(Rate), _provider);
        }

        public GolemBaseStats GetCurrentStats(IStatsProvider provider)
        {
            return new GolemBaseStats()
            {
                Strength = provider.GetBaseStats().Strength,
                Agility = provider.GetBaseStats().Agility,
                Intelligence = provider.GetBaseStats().Intelligence
            };
        }
        
        private IStatsProvider GetRate()
        {
            _provider = new GolemTypeStats(_golemType); 
            _provider = new SpecializationStats(_provider, _specialization);
            
            var rate = _provider;
            return rate;
        }
        
        public string GetGolemStats()
        {
            return _provider.GetBaseStats().ToString();
        }
    }

    public class BaseStatsEditor : StatsDecorator
    {
        private readonly float _value;
        private readonly GolemBaseStats _multiplier;
        public BaseStatsEditor(float value, GolemBaseStats multiplier, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _value = value;
            _multiplier = multiplier;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return _wrappedEntity.GetBaseStats() + (_multiplier * _value);
        }
    }

    public class DefaultStats : StatsDecorator
    {
        private readonly float _minBaseStats;

        public DefaultStats(float minBaseStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _minBaseStats = minBaseStats;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return _wrappedEntity.GetBaseStats() * _minBaseStats;
        }
    }
}
