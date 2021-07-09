using UnityEngine;

namespace BehaviourStrategy
{
    public interface IMoveable
    {
        void Move(float moveSpeed, Vector3 targetPos);

    }
}