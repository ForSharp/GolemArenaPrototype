using System.Collections;
using System.Linq;
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
    
    private void Start()
    {
        _thisState = GetComponent<GameCharacterState>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _moveable = new NoMoveBehaviour();
        _attackable = new NoAttackBehaviour();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            _isAIControlAllowed = true;
        
        if (!_isAIControlAllowed)
            return;
        
    }

    private GameCharacterState[] GetEnemies()
    {
        return FindObjectsOfType<GameCharacterState>().Where(p => p.IsDead == false)
            .Where(p => p.Group != _thisState.Group).ToArray();
    }

    private IEnumerator Timer()
    {
        var enemies = GetEnemies();
        
        if (enemies.Length == 0)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(Timer());
        }
        
        if (enemies.Length > 0)
        {
            _targetState = enemies[Random.Range(0, enemies.Length)];
            yield return new WaitForSeconds(5);
            StartCoroutine(Timer());
        }
    }
    
}
