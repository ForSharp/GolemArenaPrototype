using System;
using UnityEngine;

public class GameCharacterState : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public int Group { get; private set; }

    private void Start()
    {
        
    }

    public void TakeDamage(float damage, int defence = 0)
    {
        
    }

    public void SpendStamina(float energy)
    {
        
    }
}
