namespace GolemEntity.BaseStats
{
    public abstract class StatsDecorator : IStatsProvider
    {
        protected readonly IStatsProvider WrappedEntity;

        protected StatsDecorator(IStatsProvider wrappedEntity)
        {
            WrappedEntity = wrappedEntity;
        }
        
        public GolemBaseStats GetBaseStats()
        {
            return GetStatsInternal();
        }

        protected abstract GolemBaseStats GetStatsInternal();
    }
}
