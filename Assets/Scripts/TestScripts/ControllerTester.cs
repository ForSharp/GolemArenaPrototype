using System.Collections;
using GolemEntity;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControllerTester : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotateSpeed = 3.0f;
    private CharacterController _character;
    private AlternativeAttackSystem _attackSystem;
    private CurrentGameCharacterState _characterState;
    private Animator _animator;

    private void Start()
    {
        _characterState = GetComponent<CurrentGameCharacterState>();
        _attackSystem = GetComponent<AlternativeAttackSystem>();
        _character = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_characterState.isDead)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _attackSystem.StartAttack();
        }

        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            transform.Rotate(0, horizontal * rotateSpeed, 0);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float curSpeed = speed * vertical;
            _character.SimpleMove(forward * curSpeed);
            AnimationChanger.SetGolemWalk(_animator, true);
        }
        else
        {
            AnimationChanger.SetIdle(_animator, true);
        }
    }
}