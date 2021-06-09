using System;
using __Scripts.GolemEntity.ExtraStats;
using GolemEntity;
using UnityEngine;

public class GameCharacterState : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public int Group { get; private set; }
    
    public bool IsDead { get; private set; }

    public ExtraStats Stats { get; private set; }

    public Golem golem;
    
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
