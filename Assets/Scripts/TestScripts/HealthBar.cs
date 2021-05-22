using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    
    public bool idDynamicHealthBarCreate = true;

    public GameObject healthBar;
    public Transform canvas;

    private void Start()
    {
        if (idDynamicHealthBarCreate)
        {
            Instantiate(healthBar.transform, canvas.transform);
            //healthBar.gameObject.transform.SetParent(GameObject.Find("Canvas").transform);
            healthBar.GetComponent<UIHealthBar>().NPC = transform;
            
        }
        
    }
}
