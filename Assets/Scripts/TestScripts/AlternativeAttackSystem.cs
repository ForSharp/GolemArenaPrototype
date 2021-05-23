using GolemEntity;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlternativeAttackSystem : MonoBehaviour
{
    public int damagePerHit = 20;
    public float timeBetweenHits = 2.15f;

    [SerializeField] private bool _autoAttack = true;
    private float _timer;
    private Animator _animator;
    private CurrentGameCharacterState _characterState;

    private const float HeightHit = 1.75f;
    private const float ArmLenght = 1.55f;
    private const float DestructionRadius = 1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterState = GetComponent<CurrentGameCharacterState>();
    }

    private void Update()
    {
        if (_characterState.isDead)
            return;
        
        _timer += Time.deltaTime;
        if (_autoAttack)
            StartAttack();
    }

    public void StartAttack()
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
            item.GetComponent<CurrentGameCharacterState>().TakeDamage(damagePerHit);
        }
    }
    
    private void AttackDestructibleObjects(Collider item)
    {
        if (item.GetComponent<Optimization>())
        {
            StartCoroutine(item.GetComponent<Optimization>().ShowDamage());

            if (item.GetComponentInParent<CurrentGameCharacterState>())
            {
                item.GetComponentInParent<CurrentGameCharacterState>().TakeDamage(damagePerHit);
                if (item.GetComponentInParent<CurrentGameCharacterState>().currentHealth <= 0)
                {
                    item.GetComponent<Optimization>().GetComponent<Rigidbody>().AddForce(transform.forward * 300);
                }
            }
        }
    }
}