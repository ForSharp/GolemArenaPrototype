using UnityEngine;

namespace CharacterEntity.State
{
    public class CreepState : CharacterState
    {
        private ChampionState _creepOwner;
        private new void Start()
        {
            base.Start();
            ConsumablesEater = new ConsumablesEater(this);
        }

        public void InitializeState(Character character, int group, Color colorGroup, string type)
        {
            Character = character;
            Group = group;
            ColorGroup = colorGroup;
            Type = type;
            SetStartState();
        }
    }
}