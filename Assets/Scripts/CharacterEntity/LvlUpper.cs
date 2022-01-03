using GameLoop;

namespace CharacterEntity
{
    public static class LvlUpper
    {
        public static void LvlUp(State.CharacterState state)
        {
            state.Character.ChangeBaseStatsProportionallyPermanent(10);
            EventContainer.OnCharacterStatsChanged(state);
            state.Lvl++;
        }

        public static void LvlUp(State.CharacterState state, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                state.Character.ChangeBaseStatsProportionallyPermanent(10);
                EventContainer.OnCharacterStatsChanged(state);
                state.Lvl++;
            }
        }
        
        public static void LvlDown(State.CharacterState state)
        {
            if (state.Lvl <= 1) return;
            state.Character.ChangeBaseStatsProportionallyPermanent(-10);
            EventContainer.OnCharacterStatsChanged(state);
            state.Lvl--;
        }
    }
}