namespace GolemEntity.BaseStats
{
    public class GolemTypeStats : IStatsProvider
    {
        private readonly GolemType _golemType;

        public GolemTypeStats(GolemType golemType)
        {
            _golemType = golemType;
        }

        public GolemBaseStats GetBaseStats()
        {
            return CharacterStatsService.GetBaseStats(_golemType);
        }
    }
}
