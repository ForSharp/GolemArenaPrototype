using System.Collections;
using __Scripts.GameLoop;
using UnityEngine;

namespace __Scripts.CharacterEntity.State
{
    public class CreepState : CharacterState
    {
        public ChampionState CreepOwner { get; private set; }
        public float CreepLiveDuration { get; private set; }
        private  void Start()
        {
            RoundStatistics = new RoundStatistics(this);
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
            
            AddRoundStatsToOwner();
            
            Game.RemoveDeadCreep(this);
        }

        private void AddRoundStatsToOwner()
        {
            CreepOwner.RoundStatistics.RoundDamage += RoundStatistics.RoundDamage;
            CreepOwner.RoundStatistics.RoundKills += RoundStatistics.RoundKills;
        }
    }
}