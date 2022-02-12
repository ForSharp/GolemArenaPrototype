using UnityEngine;

namespace CharacterEntity.State
{
    public class CreepState : CharacterState
    {
        public ChampionState CreepOwner { get; private set; }
        private new void Start()
        {
            base.Start();
            ConsumablesEater = new ConsumablesEater(this);
        }

        public void InitializeState(Character character, ChampionState owner, string type)
        {
            Character = character;
            CreepOwner = owner;
            Group = owner.Group;
            ColorGroup = owner.ColorGroup;
            Type = type;
            SetStartState();
        }
    }
}