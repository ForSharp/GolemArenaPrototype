namespace __Scripts.ExtraStats
{
    public abstract class ExtraStatsDecorator: IExtraStatsProvider
    {
        protected readonly IExtraStatsProvider _wrappedEntity;
        protected GolemBaseStats _baseStats;

        protected ExtraStatsDecorator(IExtraStatsProvider wrappedEntity, GolemBaseStats baseStats)
        {
            _wrappedEntity = wrappedEntity;
            _baseStats = baseStats;
        }
        
        public GolemExtraStats GetExtraStats()
        {
            return GetExtraStatsInternal(_baseStats);
        }
        
        protected abstract GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats);
    }
}
