using System.Collections;
using GameLoop;
using UnityEngine;

namespace CharacterEntity.State
{
    public class CreepState : CharacterState
    {
        public ChampionState CreepOwner { get; private set; }
        public float CreepLiveDuration { get; private set; }
        private  void Start()
        {
            ConsumablesEater = new ConsumablesEater(this);
        }

        public void InitializeState(Character character, ChampionState owner, string type, float duration)
        {
            Character = character;
            CreepOwner = owner;
            Group = owner.Group;
            ColorGroup = owner.ColorGroup;
            Type = type;
            SetStartState();
            CreepLiveDuration = duration;

            StartCoroutine(KillCreep(CreepLiveDuration));
        }

        private IEnumerator KillCreep(float duration)
        {
            yield return new WaitForSeconds(duration);
            
            GetDeadlyDamage();
        }

        private void GetDeadlyDamage()
        {
            TakeDamage(100000000, null);
            Game.RemoveDeadCreep(this);
        }
    }
}