using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    private Transform _npc;
    private RectTransform _rectTransform;
    private Slider _slider;
    private HealthBar _healthBar;
    
    public Transform NPC
    {
        get { return _npc;}
        set
        {
            _npc = value; 
            _healthBar = NPC.GetComponent<HealthBar>();
            _slider = GetComponent<Slider>();
            _slider.maxValue = _healthBar.maxHealth;
        }
    }
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        
    }

    private void Update()
    {
        if (!NPC)
            return;
        
        //Vector3 posNps = new Vector3(NPC.position.x, NPC.position.y + 2, NPC.position.z);
        //_rectTransform.position = Camera.main.WorldToScreenPoint(posNps);
        //GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(posNps);

        _slider.value = _healthBar.currentHealth;
        
        Vector2 position = Camera.main.WorldToScreenPoint(NPC.transform.position);
        position.y += 2f;

        transform.position = position;
    }
}
