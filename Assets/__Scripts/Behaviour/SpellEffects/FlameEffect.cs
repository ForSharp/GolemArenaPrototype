using System.Collections;
using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using UnityEngine;

namespace __Scripts.Behaviour.SpellEffects
{
    public class FlameEffect : MonoBehaviour
    {
        [SerializeField] private GameObject flame;

        public void BurnTarget(ChampionState attacker, CharacterState target, float periodicDamage,
            float timeDuration)
        {
            flame.SetActive(true);
            StartCoroutine(SetPeriodicDamage(attacker, target, periodicDamage));

            Destroy(gameObject, timeDuration);
        }

        private IEnumerator SetPeriodicDamage(ChampionState attacker, CharacterState target, float periodicDamage)
        {
            yield return new WaitForSeconds(1);
            
            target.TakeDamage(periodicDamage, attacker.RoundStatistics);
            EventContainer.OnMagicDamageReceived(attacker, target, periodicDamage, true);

            StartCoroutine(SetPeriodicDamage(attacker, target, periodicDamage));
        }
        
    }
}