using UnityEngine;

public class InverseKinematicsController : MonoBehaviour
{
    public bool isIkActive;
    
    private Animator _animator;
    [SerializeField] private Transform _target;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!_animator)
            return;

        if (isIkActive)
        {
            Debug.Log("OnAnimatorIK");
            
            TurnHeadToTarget();
        }
    }

    private void TurnHeadToTarget()
    {
        _animator.SetLookAtWeight(1);
        _animator.SetLookAtPosition(_target.position);
    }

}
