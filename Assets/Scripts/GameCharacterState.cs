using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameCharacterState : MonoBehaviour
{
    public int Group { get; set; }
    private int _defence;
    
    public void TakeDamage(int damage, int defence = 0)
    {
        
    }
}
