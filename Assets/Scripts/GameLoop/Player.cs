using FightState;

namespace GameLoop
{
    public static class Player
    {
        public static GameCharacterState PlayerCharacter { get; private set; }

        public static void SetPlayerCharacter(GameCharacterState character)
        {
            PlayerCharacter = character;
            EventContainer.OnPlayerCharacterCreated();
            
        }
    }
}