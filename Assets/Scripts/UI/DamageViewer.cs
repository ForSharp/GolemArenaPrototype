using System;
using CharacterEntity.CharacterState;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DamageViewer : MonoBehaviour
    {
        [SerializeField] private Text[] attack;
        [SerializeField] private Animator[] animatorAttack;
        
        private int _attackInfoQueueNumber = 0;
        
        private static readonly int HitReceived = Animator.StringToHash("HitReceived");
        private static readonly int CriticalHitReceived = Animator.StringToHash("CriticalHitReceived");
        public CharacterState State { get; set; }

        private void OnEnable()
        {
            EventContainer.FightEvent += HandleFightEvent;
            EventContainer.MagicDamageReceived += HandleMagicDamageReceiving;
        }

        private void OnDisable()
        {
            EventContainer.FightEvent -= HandleFightEvent;
            EventContainer.MagicDamageReceived -= HandleMagicDamageReceiving;
        }

        private void HandleMagicDamageReceiving(CharacterState sender, CharacterState target, float damage,
            bool isPeriodic)
        {
            if (target == State)
            {
                animatorAttack[_attackInfoQueueNumber].SetTrigger(HitReceived);
                attack[_attackInfoQueueNumber].text = $"-{damage:#.}";
            
                _attackInfoQueueNumber++;
                if (_attackInfoQueueNumber >= 3)
                {
                    _attackInfoQueueNumber = 0;
                }
            }
        }
        
        
        private void HandleFightEvent(object sender, EventArgs args)
        {
            if ((CharacterState)sender == State)
            {
                var fightArgs = (FightEventArgs)args;
                if (fightArgs.IsAvoiding)
                {
                    animatorAttack[_attackInfoQueueNumber].SetTrigger(HitReceived);
                    attack[_attackInfoQueueNumber].text = "AVOID";
                }
                else if (fightArgs.IsAttackFromBehind)
                {
                    animatorAttack[_attackInfoQueueNumber].SetTrigger(CriticalHitReceived);
                    attack[_attackInfoQueueNumber].text = $"-{fightArgs.AttackHitEventArgs.DamagePerHit:#.}!";
                }
                else
                {
                    animatorAttack[_attackInfoQueueNumber].SetTrigger(HitReceived);
                    attack[_attackInfoQueueNumber].text = $"-{fightArgs.AttackHitEventArgs.DamagePerHit:#.}";
                }

                _attackInfoQueueNumber++;
                if (_attackInfoQueueNumber >= 3)
                {
                    _attackInfoQueueNumber = 0;
                }
            }
        }
    }
}