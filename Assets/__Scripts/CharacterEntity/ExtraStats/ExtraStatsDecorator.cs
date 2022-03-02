using CharacterEntity.BaseStats;

namespace CharacterEntity.ExtraStats
{
    public abstract class ExtraStatsDecorator: IExtraStatsProvider
    {
        protected readonly IExtraStatsProvider _wrappedEntity;
        protected CharacterBaseStats _baseStats;

        protected ExtraStatsDecorator(IExtraStatsProvider wrappedEntity, CharacterBaseStats baseStats)
        {
            _wrappedEntity = wrappedEntity;
            _baseStats = baseStats;
        }
        
        public CharacterExtraStats GetExtraStats()
        {
            return GetExtraStatsInternal(_baseStats);
        }
        
        protected abstract CharacterExtraStats GetExtraStatsInternal(CharacterBaseStats baseStats);
    }
}
