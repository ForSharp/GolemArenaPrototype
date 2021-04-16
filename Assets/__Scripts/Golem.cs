using UnityEngine;

namespace __Scripts
{
    public class Golem : MonoBehaviour
    {
        private GolemType _golemType = GolemType.MithrilGolem;
        private Specialization _specialization = Specialization.Wizard;

        private IStatsProvider _provider;
        private static float _minBaseStats = 1000;
        
        public IStatsProvider Rate { get; private set; }

        public Golem()
        {
            Rate = SetRate();
            
            _provider = new DefaultStats(_minBaseStats, Rate);
            //_provider = new WeaknessTremor(_provider);
        }

        private IStatsProvider SetRate()
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

    public class DefaultStats : StatsDecorator
    {
        private readonly float _minBaseStats;
        
        protected override GolemBaseStats GetStatsInternal()
        {
            return _wrappedEntity.GetBaseStats() * _minBaseStats;
        }

        public DefaultStats(float minBaseStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _minBaseStats = minBaseStats;
        }
    }
}
