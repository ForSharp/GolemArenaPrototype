using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommonMeleeAttackBehaviour : MonoBehaviour, IAttackable
{
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
    
    public CommonMeleeAttackBehaviour(float hitHeight, float attackRange, float destructionRadius, Animator animator, int group, 
        RoundStatistics statistics, bool isFriendlyFire = false, params Action<Animator>[] hitAnimationSetters)
    {
        _hitHeight = hitHeight;
        _attackRange = attackRange;
        _destructionRadius = destructionRadius;
        _hitAnimationSetters = hitAnimationSetters;
        _animator = animator;
        _group = group;
        _isFriendlyFire = isFriendlyFire;
    }

    public void FactoryMethod(float hitHeight, float attackRange, float destructionRadius, Animator animator, int group,
        RoundStatistics statistics, bool isFriendlyFire = false, params Action<Animator>[] hitAnimationSetters)
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

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void Attack(float damage, float delayBetweenHits, Vector3 attackerPosition)
    {
        if (!_isReady)
        {
            Debug.Log("Before using Attack() You must Invoke FactoryMethod");
            return;
        }
        
        if (_timer >= delayBetweenHits && Time.timeScale != 0)
        {
            _timer = 0;
            _hitAnimationSetters[Random.Range(0, _hitAnimationSetters.Length)].Invoke(_animator);

            StartCoroutine(AttackCoroutine(damage, delayBetweenHits, attackerPosition));
        }
    }

    private IEnumerator AttackCoroutine(float damage, float delayBetweenHits, Vector3 attackerPosition)
    {
        yield return new WaitForSeconds(delayBetweenHits / 2);
        
        Vector3 spherePosition = attackerPosition + transform.forward * _attackRange;
        spherePosition.y += _hitHeight;
        Collider[] colliders = Physics.OverlapSphere(spherePosition, _destructionRadius);
        foreach (var item in FilterCollidersArray(colliders))
        {
            AttackGameCharacter(item, damage);
            AttackDestructibleObjects(item, damage);
            break; 
        }
    }

    private Collider[] FilterCollidersArray(Collider[] colliders)
    {
        var filteredGameCharacterColliders = colliders.Where(c =>
            c.GetComponentInParent<GameCharacterState>()).Where(c => c.GetComponentInParent<GameCharacterState>().IsDead == false);
        var filteredDestructibleObjects = colliders.Where(c =>
            c.GetComponentInParent<DestructibleObject>()).Where(c => c.GetComponentInParent<DestructibleObject>().IsDead == false);

        var gameCharacterColliders = filteredGameCharacterColliders as Collider[] ?? filteredGameCharacterColliders.ToArray();
        var destructibleObjects = filteredDestructibleObjects as Collider[] ?? filteredDestructibleObjects.ToArray();
        
        var filteredArray = new Collider[gameCharacterColliders.Length +
                                         destructibleObjects.Length];
        
        gameCharacterColliders.CopyTo(filteredArray, 0);
        destructibleObjects.CopyTo(filteredArray, destructibleObjects.Length);

        return filteredArray;
    }
    
    private void AttackGameCharacter(Collider item, float damage)
    {
        if (item.GetComponentInParent<GameCharacterState>())
        {
            var state = item.GetComponentInParent<GameCharacterState>();
            if (!_isFriendlyFire)
            {
                if (state.Group != _group)
                {
                    state.TakeDamage(damage, statistics: _statistics);
                }
            }
            else if (_isFriendlyFire)
            {
                state.TakeDamage(damage);
            }
        }
    }

    private void AttackDestructibleObjects(Collider item, float damage)
    {
        if (item.GetComponentInParent<DestructibleObject>())
        {
            item.GetComponentInParent<DestructibleObject>().TakeDamage(damage);
        }
    }
}
