﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameCharacterState : MonoBehaviour
{
    public int Group { get; set; }
    private int _defence;
    
    public void TakeDamage(float damage, int defence = 0)
    {
        
    }

    public void SpendStamina(float energy)
    {
        
    }
}
