using System.Collections;
using FightState;
using GameLoop;
using UnityEngine;

namespace BehaviourStrategy.SpellEffects
{
    public class FlameEffect : MonoBehaviour
    {
        [SerializeField] private GameObject flame;

        public void BurnTarget(GameCharacterState attacker, GameCharacterState target, float periodicDamage,
            float timeDuration)
        {
            flame.SetActive(true);
            StartCoroutine(SetPeriodicDamage(attacker, target, periodicDamage));

            Destroy(gameObject, timeDuration);
        }
        
        public void BurnTarget(DestructibleObject target, float periodicDamage, float timeDuration)
        {
            
        }

        private IEnumerator SetPeriodicDamage(GameCharacterState attacker, GameCharacterState target, float periodicDamage)
        {
            yield return new WaitForSeconds(1);
            
            target.TakeDamage(periodicDamage, attacker.roundStatistics);
            EventContainer.OnMagicDamageReceived(attacker, target, periodicDamage, true);

            StartCoroutine(SetPeriodicDamage(attacker, target, periodicDamage));
        }
        
        private IEnumerator SetPeriodicDamage(DestructibleObject target, float periodicDamage)
        {
            yield return new WaitForSeconds(1);

            StartCoroutine(SetPeriodicDamage(target, periodicDamage));
        }
    }
}