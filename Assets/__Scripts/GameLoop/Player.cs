using __Scripts.CharacterEntity.State;
using CharacterEntity.State;
using GameLoop;

namespace __Scripts.GameLoop
{
    public static class Player
    {
        public static ChampionState PlayerCharacter { get; private set; }

        public static void SetPlayerCharacter(ChampionState character)
        {
            PlayerCharacter = character;
            EventContainer.OnPlayerCharacterCreated();
            
        }
    }
}