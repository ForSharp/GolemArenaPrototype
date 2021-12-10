using CharacterEntity.CharacterState;
using CharacterEntity.State;

namespace GameLoop
{
    public static class Player
    {
        public static CharacterState PlayerCharacter { get; private set; }

        public static void SetPlayerCharacter(CharacterState character)
        {
            PlayerCharacter = character;
            EventContainer.OnPlayerCharacterCreated();
            
        }
    }
}