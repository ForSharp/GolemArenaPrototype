using System;
using UnityEngine;

public class WalkWithoutNavMeshBehaviour : MonoBehaviour, IMoveable
{
    private Transform _navMeshObject;

    public void FactoryMethod()
    {
        
    }
    
    private void Update()
    {
        
    }

    public void Move(float moveSpeed, Vector3 targetPos)
    {
        transform.position = targetPos;
    }
}