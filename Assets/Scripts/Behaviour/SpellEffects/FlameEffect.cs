using System.Collections;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using GameLoop;
using UnityEngine;

namespace Behaviour.SpellEffects
{
    public class FlameEffect : MonoBehaviour
    {
        [SerializeField] private GameObject flame;

        public void BurnTarget(CharacterState attacker, CharacterState target, float periodicDamage,
            float timeDuration)
        {
            flame.SetActive(true);
            StartCoroutine(SetPeriodicDamage(attacker, target, periodicDamage));

            Destroy(gameObject, timeDuration);
        }
        
        public void BurnTarget(DestructibleObject target, float periodicDamage, float timeDuration)
        {
            
        }

        private IEnumerator SetPeriodicDamage(CharacterState attacker, CharacterState target, float periodicDamage)
        {
            yield return new WaitForSeconds(1);
            
            target.TakeDamage(periodicDamage, attacker.RoundStatistics);
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