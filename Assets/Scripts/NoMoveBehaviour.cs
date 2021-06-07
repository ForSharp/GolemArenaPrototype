using UnityEngine;

public class NoMoveBehaviour: IMoveable
{
    public void Move(float moveSpeed = default, Vector3 targetPos = default)
    {
        //do nothing
    }
}