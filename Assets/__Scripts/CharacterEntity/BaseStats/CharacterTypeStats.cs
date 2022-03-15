using __Scripts.CharacterEntity;

namespace CharacterEntity.BaseStats
{
    public class CharacterTypeStats : IStatsProvider
    {
        private readonly CharacterType _characterType;

        public CharacterTypeStats(CharacterType characterType)
        {
            _characterType = characterType;
        }

        public CharacterBaseStats GetBaseStats()
        {
            return CharacterStatsService.GetBaseStats(_characterType);
        }
    }
}
