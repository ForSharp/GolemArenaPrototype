using System;
using UnityEngine;

public class CommonAttack : MonoBehaviour
{
    private int _damagePerHit;
    private readonly float _delayBetweenHits;
    private readonly float _hitHeight;
    private readonly float _attackRange;
    private readonly float _destructionRadius;
    private readonly Action _setHitAnimation;
    //private readonly Action _setAttackSound;
    private Vector3 _attackerPosition;
    private int _group;
    private bool _isFriendlyFire;
    private float _timer;

    public CommonAttack(int damagePerHit, float delayBetweenHits, float hitHeight, float attackRange, 
        float destructionRadius, Action setHitAnimation, Vector3 attackerPosition, int group, 
        bool isFriendlyFire = false)
    {
        _damagePerHit = damagePerHit;
        _delayBetweenHits = delayBetweenHits;
        _hitHeight = hitHeight;
        _attackRange = attackRange;
        _destructionRadius = destructionRadius;
        _setHitAnimation = setHitAnimation;
        _attackerPosition = attackerPosition;
        _group = group;
        _isFriendlyFire = isFriendlyFire;
    }

    private void Start()
    {
        _timer = _delayBetweenHits - 0.1f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void Attack()
    {
        if (_timer >= _delayBetweenHits && Time.timeScale != 0)
        {
            _timer = 0;
            _setHitAnimation.Invoke();
            Vector3 spherePosition = _attackerPosition + transform.forward * _attackRange;
            spherePosition.y += _hitHeight;
            Collider[] colliders = Physics.OverlapSphere(spherePosition, _destructionRadius);
            foreach (var item in colliders)
            {
                AttackGameCharacter(item);
                AttackDestructibleObjects(item);
            }
        }
    }

    private void AttackGameCharacter(Collider item)
    {
        if (item.GetComponent<GameCharacterState>())
        {
            var state = GetComponent<GameCharacterState>();

            if (!_isFriendlyFire)
            {
                if (state.Group != _group)
                {
                    state.TakeDamage(_damagePerHit);
                }
            }
            else if (_isFriendlyFire)
            {
                state.TakeDamage(_damagePerHit);
            }
        }
    }

    private void AttackDestructibleObjects(Collider item)
    {
        if (item.GetComponent<DestructibleObject>())
        {
            item.GetComponent<DestructibleObject>().TakeDamage(_damagePerHit);
        }
    }
}
