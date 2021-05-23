using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [HideInInspector] public CurrentGameCharacterState characterState;
    [SerializeField] private GameObject _fill;
    private Slider _slider;
    
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
        
        _slider.value = characterState.currentHealth;
        if (characterState.currentHealth <= 0)
        {
            _fill.SetActive(false); //костыль. при 0 там небольшое значение хп видно (зеленый цвет)
        }
        
        Vector2 position = Camera.main.WorldToScreenPoint(characterState.transform.position);
        position.y += characterState.sliderPlacementHeight; 

        transform.position = position;
    }
}
