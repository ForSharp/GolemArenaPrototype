using System;
using System.Collections;
using System.Collections.Generic;
using GolemEntity;
using UnityEngine;

public class AlternativeAttackSystem : MonoBehaviour
{
    public float damagePerHit = 20.0f;
    public float timeBetweenHits = 2.15f;
    public float range = 1f;

    private float _timer;
    private Ray _hitRay;
    private RaycastHit _targetHit;
    private int _hittableMask;
    private Animator _animator;
    
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

    private void Attack()
    {
        _timer = 0;
        
        //attack sound play
        
        AnimationChanger.SetGolemRightHit(_animator);
        
        Vector3 spherePosition = transform.position + transform.forward * 0.45F;
        spherePosition.y += 1.75F;
        
        Collider[] colliders = Physics.OverlapSphere(spherePosition, 0.45F);

        foreach (Collider item in colliders)
        {
            if (item.GetComponent<Optimization>())
            {
                Debug.Log("work");
                StartCoroutine(item.GetComponent<Optimization>().ShowDamage());
            }
        }
    }
}
