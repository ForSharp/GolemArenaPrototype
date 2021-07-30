using GolemEntity;

namespace GameLoop
{
    public static class Player
    {
        public static Golem PlayerCharacter { get; private set; }

        public static void SetPlayerCharacter(Golem character)
        {
            PlayerCharacter = character;
            EventContainer.OnPlayerCharacterCreated();
            
        }
    }
}