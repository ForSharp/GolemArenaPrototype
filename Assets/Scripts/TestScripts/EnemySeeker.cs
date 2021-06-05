using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySeeker : MonoBehaviour
{
    private CurrentGameCharacterState _thisState;
    private CurrentGameCharacterState _target;
    private AlternativeAttackSystem _attack;
    
    //public float attackRange = 1.55f; //const in alternative attack/ must be in secondary stats
    public bool inAttackPos = false;
    
    private void Start()
    {
        _attack = GetComponent<AlternativeAttackSystem>();
        _thisState = GetComponent<CurrentGameCharacterState>();
        StartCoroutine(Timer());
    }

    private void Update()
    {
        if (!_target || _thisState.isDead)
            return;

        if (_attack.attackRange < Vector3.Distance(transform.position, _target.transform.position))
        {
            StartCoroutine(RotateWithDelay());
            inAttackPos = false;
        }
        
        if (_attack.attackRange > Vector3.Distance(transform.position, _target.transform.position))
        {
            transform.LookAt(_target.transform.position);
            if (!_target.isDead)
                _attack.StartAttack();
            
            inAttackPos = true;
        }
    }

    private IEnumerator RotateWithDelay()
    {
        if (_target == null) yield break;
        yield return new WaitForSeconds(2);
        transform.LookAt(_target.transform.position);

    }

    private IEnumerator Timer() 
    {
        CurrentGameCharacterState[] targets = FindObjectsOfType<CurrentGameCharacterState>().Where(p => p.isDead == false)
            .Where(p => p.group != _thisState.group).ToArray();

        if (targets.Length == 0)
        {
            yield return new WaitForSeconds(1);

            StartCoroutine(Timer());
        }

        if (targets.Length > 0)
        {
            _target = targets[Random.Range(0, targets.Length)];
            yield return new WaitForSeconds(5);
            StartCoroutine(Timer());
        }
        
    }
    
}
