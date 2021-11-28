namespace CharacterEntity.BaseStats
{
    public abstract class StatsDecorator : IStatsProvider
    {
        protected readonly IStatsProvider WrappedEntity;

        protected StatsDecorator(IStatsProvider wrappedEntity)
        {
            WrappedEntity = wrappedEntity;
        }
        
        public CharacterBaseStats GetBaseStats()
        {
            return GetStatsInternal();
        }

        protected abstract CharacterBaseStats GetStatsInternal();
    }
}
