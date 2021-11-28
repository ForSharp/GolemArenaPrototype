using GameLoop;

namespace CharacterEntity
{
    public static class LvlUpper
    {
        public static void LvlUp(CharacterState.CharacterState state)
        {
            state.Character.ChangeBaseStatsProportionallyPermanent(10);
            EventContainer.OnGolemStatsChanged(state);
            state.Lvl++;
        }

        public static void LvlUp(CharacterState.CharacterState state, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                state.Character.ChangeBaseStatsProportionallyPermanent(10);
                EventContainer.OnGolemStatsChanged(state);
                state.Lvl++;
            }
        }
        
        public static void LvlDown(CharacterState.CharacterState state)
        {
            if (state.Lvl <= 1) return;
            state.Character.ChangeBaseStatsProportionallyPermanent(-10);
            EventContainer.OnGolemStatsChanged(state);
            state.Lvl--;
        }
    }
}