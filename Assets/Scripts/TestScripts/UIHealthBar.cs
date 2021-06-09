using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [HideInInspector] public CurrentGameCharacterState characterState;
    [SerializeField] private GameObject fill;
    private Slider _slider;
    private const int TimeToDestroy = 5;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        transform.SetParent(GameObject.Find("Canvas").transform);
        if (characterState)
            _slider.maxValue = characterState.maxHealth;
    }

    private void Update()
    {
        if (!characterState)
            return;
        
        UpdateMaxValue();
        UpdateSliderValue();
        SetRequiredPosition();
        DestroyOnDeath();
        
    }

    private void SetRequiredPosition(float multiplier = 1)
    {
        var requirePos = new Vector3(characterState.transform.position.x,
            characterState.transform.position.y + characterState.GetComponent<Collider>().bounds.size.y * multiplier,
            characterState.transform.position.z);
        
        var position = Camera.main.WorldToScreenPoint(requirePos);

        transform.position = position;
    }

    private void UpdateSliderValue()
    {
        _slider.value = characterState.currentHealth;
    }
    
    private void DestroyOnDeath()
    {
        if (characterState.isDead)
        {
            fill.SetActive(false);
            Destroy(gameObject, TimeToDestroy);
        }
    }
    
    private void ChangeMaxValue()
    {
        //if an event occurs, in which the maximum health value changes in runtime, this method should be executed
        
        _slider.maxValue = characterState.maxHealth;
    }

    private void UpdateMaxValue()
    {
        if (Math.Abs(characterState.maxHealth - _slider.maxValue) > 1)
        {
            _slider.maxValue = characterState.maxHealth;
        }
    }
}
