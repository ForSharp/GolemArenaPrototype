using UnityEngine;

namespace __Scripts.Behaviour.Abstracts
{
    public interface IMoveable
    {
        void Move(float moveSpeed, Vector3 targetPos);

    }
}