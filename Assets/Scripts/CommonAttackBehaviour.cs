using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CommonAttackBehaviour : MonoBehaviour, IAttackable
{
    private readonly float _hitHeight;
    private readonly float _attackRange;
    private readonly float _destructionRadius;
    private readonly Action<Animator>[] _hitAnimationSetters;
    private Animator _animator;
    //private readonly Action _setAttackSound;
    private int _group;
    private bool _isFriendlyFire;
    private float _timer;
    
    public CommonAttackBehaviour(float hitHeight, float attackRange, float destructionRadius, Animator animator, int group, 
        bool isFriendlyFire = false, params Action<Animator>[] hitAnimationSetters)
    {
        _hitHeight = hitHeight;
        _attackRange = attackRange;
        _destructionRadius = destructionRadius;
        _hitAnimationSetters = hitAnimationSetters;
        _animator = animator;
        _group = group;
        _isFriendlyFire = isFriendlyFire;
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
        if (_timer >= delayBetweenHits && Time.timeScale != 0)
        {
            _timer = 0;
            _hitAnimationSetters[Random.Range(0, _hitAnimationSetters.Length)].Invoke(_animator); 
            Vector3 spherePosition = attackerPosition + transform.forward * _attackRange;
            spherePosition.y += _hitHeight;
            Collider[] colliders = Physics.OverlapSphere(spherePosition, _destructionRadius);
            foreach (var item in colliders)
            {
                AttackGameCharacter(item, damage);
                AttackDestructibleObjects(item, damage);
            }
        }
    }

    private void AttackGameCharacter(Collider item, float damage)
    {
        if (item.GetComponent<GameCharacterState>())
        {
            var state = GetComponent<GameCharacterState>();
            if (!_isFriendlyFire)
            {
                if (state.Group != _group)
                {
                    state.TakeDamage(damage);
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
        if (item.GetComponent<DestructibleObject>())
        {
            item.GetComponent<DestructibleObject>().TakeDamage(damage);
        }
    }
}
