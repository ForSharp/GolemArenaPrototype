using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CurrentGameCharacterState : MonoBehaviour
{
    public int maxHealth = 100; //Golem.GetExtraStats.Health
    public int currentHealth = 100;
    
    public int sliderPlacementHeight = 80;
    
    [SerializeField]private bool idDynamicHealthBarCreate = true;
    [SerializeField]private GameObject healthBarPrefab;

    private void Start()
    {
        if (idDynamicHealthBarCreate)
        {
            //Instantiate(healthBar.transform, canvas.transform);
            GameObject healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar.GetComponent<UIHealthBar>().characterState = this;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }

    private void KillGameCharacter()
    {
        
    }
}
