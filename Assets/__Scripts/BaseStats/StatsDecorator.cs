using UnityEngine;

namespace __Scripts
{
    public abstract class StatsDecorator : IStatsProvider
    {
        protected readonly IStatsProvider _wrappedEntity;

        protected StatsDecorator(IStatsProvider wrappedEntity)
        {
            _wrappedEntity = wrappedEntity;
        }
        
        public GolemBaseStats GetBaseStats()
        {
            return GetStatsInternal();
        }

        protected abstract GolemBaseStats GetStatsInternal();
    }
}
