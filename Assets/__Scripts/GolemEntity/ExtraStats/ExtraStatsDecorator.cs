namespace __Scripts.ExtraStats
{
    public abstract class ExtraStatsDecorator: IExtraStatsProvider
    {
        protected readonly IExtraStatsProvider _wrappedEntity;

        protected ExtraStatsDecorator(IExtraStatsProvider wrappedEntity)
        {
            _wrappedEntity = wrappedEntity;
        }
        
        public GolemExtraStats GetExtraStats(GolemBaseStats baseStats)
        {
            return GetExtraStatsInternal(baseStats);
        }
        
        protected abstract GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats);
    }
}
