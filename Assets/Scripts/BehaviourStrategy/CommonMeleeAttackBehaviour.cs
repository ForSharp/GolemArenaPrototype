using System;
using System.Collections;
using System.Linq;
using Fight;
using GameLoop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BehaviourStrategy
{
    public class CommonMeleeAttackBehaviour : MonoBehaviour, IAttackable
    {
        [SerializeField] private GameObject spherePrefab;

        private float _hitHeight;
        private float _attackRange;
        private float _destructionRadius;
        private Action<Animator>[] _hitAnimationSetters;

        private Animator _animator;

        //private readonly Action _setAttackSound;
        private int _group;
        private bool _isFriendlyFire;
        private float _timer;
        private RoundStatistics _statistics;

        private bool _isReady = false;


        public void CustomConstructor(float hitHeight, float attackRange, float destructionRadius, Animator animator,
            int group,
            RoundStatistics statistics = default, bool isFriendlyFire = false,
            params Action<Animator>[] hitAnimationSetters)
        {
            _hitHeight = hitHeight;
            _attackRange = attackRange;
            _destructionRadius = destructionRadius;
            _hitAnimationSetters = hitAnimationSetters;
            _animator = animator;
            _group = group;
            _isFriendlyFire = isFriendlyFire;
            _statistics = statistics;
            _isReady = true;
        }

        private void Start()
        {
            _timer = 100f; //on the first hit, the attack will start without delay from the attack speed
        }

        private void OnAttack()
        {
        }

        private void OnAttackEnded()
        {
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        public void Attack(float damage, float delayBetweenHits, Vector3 attackerPosition)
        {
            if (!_isReady)
            {
                Debug.Log("Before using Attack You must init fields by CustomConstructor");
                return;
            }

            if (_timer >= delayBetweenHits && Time.timeScale != 0)
            {
                _timer = 0;
                _hitAnimationSetters[Random.Range(0, _hitAnimationSetters.Length)].Invoke(_animator);

                StartCoroutine(AttackCoroutine(damage, attackerPosition));
            }
        }

        private IEnumerator AttackCoroutine(float damage, Vector3 attackerPosition)
        {
            //yield return new WaitForSeconds(delayBetweenHits / 2);

            yield return new WaitForSeconds(0.75f);

            Vector3 spherePosition = attackerPosition + transform.forward * _attackRange;
            spherePosition.y += _hitHeight;
            Debug.DrawLine(new Vector3(transform.position.x, spherePosition.y, transform.position.z), spherePosition,
                Color.magenta, 5);
            Collider[] colliders = Physics.OverlapSphere(spherePosition, _destructionRadius);


            var sphere = Instantiate(spherePrefab, spherePosition, Quaternion.identity);
            Destroy(sphere, 1);

            foreach (var item in FilterCollidersArray(colliders))
            {
                AttackDestructibleObjects(item, damage);
                if (AttackGameCharacter(item, damage))
                    break;
            }
        }

        private Collider[] FilterCollidersArray(Collider[] colliders)
        {
            var filteredGameCharacterColliders = colliders.Where(c =>
                    c.GetComponentInParent<GameCharacterState>())
                .Where(c => c.GetComponentInParent<GameCharacterState>().IsDead == false);
            var filteredDestructibleObjects = colliders.Where(c =>
                    c.GetComponentInParent<DestructibleObject>())
                .Where(c => c.GetComponentInParent<DestructibleObject>().IsDead == false);

            var gameCharacterColliders =
                filteredGameCharacterColliders as Collider[] ?? filteredGameCharacterColliders.ToArray();
            var destructibleObjects =
                filteredDestructibleObjects as Collider[] ?? filteredDestructibleObjects.ToArray();

            var filteredArray = new Collider[gameCharacterColliders.Length +
                                             destructibleObjects.Length];

            gameCharacterColliders.CopyTo(filteredArray, 0);
            destructibleObjects.CopyTo(filteredArray, destructibleObjects.Length);

            return filteredArray;
        }

        private bool AttackGameCharacter(Collider item, float damage)
        {
            if (item.GetComponentInParent<GameCharacterState>())
            {
                var state = item.GetComponentInParent<GameCharacterState>();
                if (!_isFriendlyFire)
                {
                    if (state.Group != _group)
                    {
                        state.TakeDamage(damage, statistics: _statistics);
                        return true;
                    }
                }
                else if (_isFriendlyFire)
                {
                    if (state == GetComponent<GameCharacterState>())
                        return false;
                    
                    state.TakeDamage(damage);
                    return true;
                }
            }

            return false;
        }

        private void AttackDestructibleObjects(Collider item, float damage)
        {
            if (item.GetComponentInParent<DestructibleObject>())
            {
                item.GetComponentInParent<DestructibleObject>().TakeDamage(damage);
            }
        }
    }
}