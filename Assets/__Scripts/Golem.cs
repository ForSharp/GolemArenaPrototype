using UnityEngine;

namespace __Scripts
{
    public class Golem : MonoBehaviour
    {
        private GolemType _golemType = GolemType.MithrilGolem;
        private Specialization _specialization = Specialization.Wizard;

        private IStatsProvider _provider;
        private static float _minBaseStats = 1000;

        public Golem()
        {
            _provider = new GolemTypeStats(_golemType); 
            _provider = new SpecializationStats(_provider, _specialization);
            
            var rate = _provider;
            _provider = new DefaultStats(_minBaseStats, rate);

            _provider = new WeaknessTremor(_provider);
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
