﻿using System;
using System.Collections;
using System.Linq;
using GolemEntity;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class GolemAI : MonoBehaviour
{
    private bool _isAIControlAllowed = false;

    private IMoveable _moveable;
    private IAttackable _attackable;

    private GameCharacterState _thisState;
    private GameCharacterState _targetState;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private const float CloseDistance = 10;
    private const float HitHeight = 1.75f;
    private const float DestructionRadius = 1f;
    private const float MoveSpeed = 5f;
    private const float DelayBetweenHits = 3f;
    

    private void Start()
    {
        _thisState = GetComponent<GameCharacterState>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        SetDefaultBehaviour();
        AnimationChanger.SetFightIdle(_animator, true);
        StartCoroutine(FindEnemies());

        EventContainer.GolemDied += KillGolem;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //user input class
            _isAIControlAllowed = true;

        if (_thisState.IsDead)
        {
            return;
        }

        if (!_isAIControlAllowed || !_targetState)
        {
            SetDefaultBehaviour();
            return;
        }
        
        SetFightBehaviour();
    }

    private void KillGolem()
    {
        if (_thisState.IsDead)
        {
            AnimationChanger.SetGolemDie(_animator);
            
            _thisState.LastEnemyAttacked.Kills += 1;
            EventContainer.GolemDied -= KillGolem;
            
            StartCoroutine(WaitForSecondsToDisable(4));
        }
    }

    private IEnumerator WaitForSecondsToDisable(int sec)
    {
        yield return new WaitForSeconds(sec); 
        gameObject.SetActive(false);
    }

    private void SetDefaultBehaviour()
    {
        _moveable = new NoMoveBehaviour(_animator, AnimationChanger.SetFightIdle);
        _attackable = new NoAttackBehaviour(_animator, AnimationChanger.SetFightIdle);
    }

    private void SetFightBehaviour()
    {
        var distanceToTarget = Vector3.Distance(transform.position, _targetState.transform.position);

        if (_thisState.Stats.AttackRange * 1.5 >= distanceToTarget)
        {
            _animator.applyRootMotion = true;
            
            TurnSmoothly();
            
            SetMoveBehaviour(new NoMoveBehaviour(_animator, AnimationChanger.SetFightIdle));
            _moveable.Move(default, default);
            
            var attack = gameObject.GetComponent<CommonMeleeAttackBehaviour>();
            SetAttackBehaviour(attack);
            attack.FactoryMethod(HitHeight, _thisState.Stats.AttackRange, DestructionRadius,
                _animator, _thisState.Group, _thisState.RoundStatistics, false,
                AnimationChanger.SetHitAttack, AnimationChanger.SetKickAttack, 
                AnimationChanger.SetSuperAttack);
            _attackable.Attack(_thisState.Stats.DamagePerHeat, DelayBetweenHits, transform.position);

            //TurnSmoothly();
        }
        else if (CloseDistance >= distanceToTarget)
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new WalkBehaviour(_thisState.Stats.AttackRange, _animator, _navMeshAgent,
                AnimationChanger.SetGolemWalk));
            _moveable.Move(MoveSpeed, _targetState.transform.position); 
        }
        else
        {
            _animator.applyRootMotion = false;
            SetMoveBehaviour(new RunBehaviour(_thisState, _thisState.Stats.AttackRange, _animator, _navMeshAgent, false,
                AnimationChanger.SetGolemRun));
            _moveable.Move(MoveSpeed * 2, _targetState.transform.position);
        }
    }

    private void TurnSmoothly()
    {
        Vector3 direction = _targetState.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, MoveSpeed * Time.deltaTime);
    }

    private void SetMoveBehaviour(IMoveable moveable)
    {
        _moveable = moveable;
    }

    private void SetAttackBehaviour(IAttackable attackable)
    {
        _attackable = attackable;
    }

    private GameCharacterState[] GetEnemies()
    {
        return FindObjectsOfType<GameCharacterState>().Where(p => p.IsDead == false)
            .Where(p => p.Group != _thisState.Group).ToArray();
    }

    private IEnumerator FindEnemies()
    {
        var enemies = GetEnemies();

        if (enemies.Length == 0)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(FindEnemies());
        }

        if (enemies.Length > 0)
        {
            _targetState = enemies[Random.Range(0, enemies.Length)];
            yield return new WaitForSeconds(5);
            StartCoroutine(FindEnemies());
        }
    }
}