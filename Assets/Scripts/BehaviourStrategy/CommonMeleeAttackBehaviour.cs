﻿using System;
using System.Collections;
using System.Linq;
using Fight;
using GameLoop;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace BehaviourStrategy
{
    public class CommonMeleeAttackBehaviour : MonoBehaviour, IAttackable
    {
        private float _hitHeight;
        private float _attackRange;
        private float _destructionRadius;
        private Action<Animator>[] _hitAnimationSetters;
        private Animator _animator;
        private int _group;
        private float _timer;
        private float _timeToResetJump;
        private float _timeToEndAttack;
        private RoundStatistics _statistics;
        private float _damage;
        private float _delayBetweenHits;
        private GameObject _target;
        private NavMeshAgent _agent;
        private bool _isReady;
        private bool _isLastHitEnd = true;
        private bool _isJumpUp;

        public void CustomConstructor(float hitHeight, float attackRange, float destructionRadius, Animator animator,
            int group, float damage, float delayBetweenHits, GameObject target, NavMeshAgent agent,
            RoundStatistics statistics = default,
            params Action<Animator>[] hitAnimationSetters)
        {
            _hitHeight = hitHeight;
            _attackRange = attackRange;
            _destructionRadius = destructionRadius;
            _hitAnimationSetters = hitAnimationSetters;
            _animator = animator;
            _group = group;
            _statistics = statistics;
            _damage = damage;
            _delayBetweenHits = delayBetweenHits;
            _target = target;
            _agent = agent;
            _isReady = true;
        }

        private void Start()
        {
            _timer = 100f;
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_isJumpUp)
            {
                _timeToResetJump += Time.deltaTime;
                ForceResetJumping();
            }
            EndAttackIfNeed();
        }

        #region AnimationEvents

        private void OnAttackStarted()
        {
            _isLastHitEnd = false;
            _timeToEndAttack = 0;
            _agent.baseOffset = 0;
        }

        private void OnAttack()
        {
            AttackEnemy();
        }

        private void OnAttackEnded()
        {
            _isLastHitEnd = true;
            if (_agent)
            {
                _agent.baseOffset = 0;
            }
            _isJumpUp = false;
        }

        private void OnStartJump()
        {
            _isJumpUp = true;
            while (_isJumpUp && _agent.baseOffset <= 0.85f)
            {
                _agent.baseOffset += Time.deltaTime * 1;
            }
        }

        private void OnStartLanding()
        {
            LandHero();
        }

        #endregion

        private void LandHero()
        {
            _isJumpUp = false;
            while (_agent.baseOffset > 0 && !_isJumpUp)
            {
                _agent.baseOffset -= Time.deltaTime * 1;
            }
        }

        private void ForceResetJumping()
        {
            if (_timeToResetJump >= 1.5f)
            {
                LandHero();
            }
        }
        
        public void Attack()
        {
            if (!_isReady)
            {
                Debug.Log("Before using Attack You must init fields by CustomConstructor");
                return;
            }

            if (CanAttack())
            {
                _timer = 0;
                _hitAnimationSetters[Random.Range(0, _hitAnimationSetters.Length)].Invoke(_animator);
            }

            bool CanAttack()
            {
                return _timer >= _delayBetweenHits && Time.timeScale != 0 && _isLastHitEnd;
            }
        }

        private void AttackEnemy()
        {
            if (_target)
            {
                transform.LookAt(_target.transform.position);
            }

            if (!TryFindEnemiesInSpecifiedArea(GetDamageArea(_attackRange / 3)))
            {
                TryFindEnemiesInSpecifiedArea(GetDamageArea(_attackRange));
            }
        }

        private Vector3 GetDamageArea(float attackRange)
        {
            Vector3 spherePosition = transform.position + transform.forward * attackRange;
            spherePosition.y += _hitHeight;
            return spherePosition;
        }

        private bool TryFindEnemiesInSpecifiedArea(Vector3 area)
        {
            Collider[] colliders = Physics.OverlapSphere(area, _destructionRadius);
            
            foreach (var item in FilterCollidersArray(colliders))
            {
                AttackDestructibleObjects(item, _damage);
                if (AttackGameCharacter(item, _damage))
                    return true;
            }
            return false;
        }

        private bool AttackGameCharacter(Collider item, float damage)
        {
            if (item.GetComponentInParent<GameCharacterState>())
            {
                var state = item.GetComponentInParent<GameCharacterState>();

                if (state.Group != _group)
                {
                    state.TakeDamage(damage, statistics: _statistics);
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
        
        private void EndAttackIfNeed()
        {
            if (!_isLastHitEnd)
            {
                _timeToEndAttack += Time.deltaTime;
                if (_timeToEndAttack >= _delayBetweenHits * 1.5f)
                {
                    ForceEndAttack();
                }
            }
        }

        private void ForceEndAttack()
        {
            _timeToEndAttack = 0;
            _isLastHitEnd = true;
        }
    }
}