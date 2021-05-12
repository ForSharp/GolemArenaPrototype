using System.Collections;
using GolemEntity;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControllerTester : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotateSpeed = 3.0f;
    private CharacterController _character;
    private Animator _animator;
    private bool inAttack;
    public bool InAttack
    {
        get { return inAttack; }
        set
        {
            inAttack = value;
            if (value != false) AnimationChanger.SetGolemRightHit(_animator);
        }
    }
    
    private void Start()
    {
        _character = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }

        if (InAttack) return;

        Move();
    }

    

    private IEnumerator Attack()
    {
        InAttack = true;
        yield return new WaitForSeconds(0.2F);

        Vector3 spherePosition = transform.position + transform.forward * 0.45F;
        spherePosition.y += 0.75F;
        
        //проверка на коллизии либо эдфорс, либо все вместе в зависимости от ситуации
        
        yield return new WaitForSeconds(0.45F);
        InAttack = false;
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
