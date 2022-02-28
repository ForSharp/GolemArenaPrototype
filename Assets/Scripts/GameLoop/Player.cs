﻿using CharacterEntity.CharacterState;
using CharacterEntity.State;

namespace GameLoop
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