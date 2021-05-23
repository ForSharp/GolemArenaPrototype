using System;
using System.Collections;
using System.Collections.Generic;
using GolemEntity;
using UnityEngine;
using Random = UnityEngine.Random;


public class AlternativeAttackSystem : MonoBehaviour
{
    public float damagePerHit = 20.0f;
    public float timeBetweenHits = 2.15f;


    private float _timer;
    private Ray _hitRay;
    private RaycastHit _targetHit;
    private int _hittableMask;
    private Animator _animator;

    private const float HeightHit = 1.75f;
    private const float ArmLenght = 1.55f;
    private const float DestructionRadius = 1f;

    private void Awake()
    {
        _hittableMask = LayerMask.GetMask("Hittable");
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        StartAttack();
    }

    private void StartAttack()
    {
        if (_timer >= timeBetweenHits && Time.timeScale != 0)
        {
            Attack();
        }
    }

    private void SetHitAnimation()
    {
        float hitChance = Random.Range(0, 4);
        if (hitChance < 1.1)
        {
            AnimationChanger.SetGolemRightHit(_animator);
        }
        else if (hitChance < 2.2)
        {
            AnimationChanger.SetGolemLeftHit(_animator);
        }
        else
        {
            AnimationChanger.SetGolemDoubleHit(_animator);
        }
    }

    private void Attack()
    {
        _timer = 0;

        //attack sound play

        SetHitAnimation();

        Vector3 spherePosition = transform.position + transform.forward * ArmLenght;
        spherePosition.y += HeightHit;

        Collider[] colliders = Physics.OverlapSphere(spherePosition, DestructionRadius);

        foreach (Collider item in colliders)
        {
            AttackDestructibleObjects(item);
            
            AttackGameCharacters(item);
        }
    }

    private void AttackGameCharacters(Collider item)
    {
        if (item.GetComponent<CurrentGameCharacterState>())
        {
            item.GetComponent<CurrentGameCharacterState>().TakeDamage(10);
            
        }
    }
    
    private void AttackDestructibleObjects(Collider item)
    {
        if (item.GetComponent<Optimization>())
        {
            StartCoroutine(item.GetComponent<Optimization>().ShowDamage());

            if (item.GetComponentInParent<CurrentGameCharacterState>())
            {
                item.GetComponentInParent<CurrentGameCharacterState>().TakeDamage(10);
                if (item.GetComponentInParent<CurrentGameCharacterState>().currentHealth <= 0)
                {
                    item.GetComponent<Optimization>().GetComponent<Rigidbody>().AddForce(transform.forward * 300);
                }
            }
        }
    }
}