using CharacterEntity.BaseStats;

namespace CharacterEntity.ExtraStats
{
    public class TypeExtraStats : IExtraStatsProvider
    {
        private CharacterType _type;
        private CharacterBaseStats _baseStats;

        public TypeExtraStats(CharacterType type, CharacterBaseStats baseStats)
        {
            _type = type;
            _baseStats = baseStats;
        }

        public CharacterExtraStats GetExtraStats()
        {
            return CharacterStatsService.GetExtraStats(_type, _baseStats);
        }
    }
}
