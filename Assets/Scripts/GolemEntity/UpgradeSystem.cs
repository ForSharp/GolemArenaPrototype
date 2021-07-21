using Fight;
using GameLoop;

namespace GolemEntity
{
    public static class UpgradeSystem
    {
        public static void LvlUp(GameCharacterState state)
        {
            state.Golem.ChangeBaseStatsProportionally(10);
            EventContainer.OnGolemStatsChanged();
            state.Lvl++;
        }

        public static void LvlDown(GameCharacterState state)
        {
            if (state.Lvl <= 1) return;
            state.Golem.ChangeBaseStatsProportionally(-10);
            EventContainer.OnGolemStatsChanged();
            state.Lvl--;
        }
    }
}