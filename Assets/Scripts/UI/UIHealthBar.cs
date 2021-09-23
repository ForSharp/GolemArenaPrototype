using System;
using System.Collections;
using System.Globalization;
using Fight;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealthBar : MonoBehaviour
    {
        [HideInInspector] public GameCharacterState characterState;
        [SerializeField] private GameObject fill;
        [SerializeField] private Text maxHealthText;
        [SerializeField] private Text currentHealthText;
        [SerializeField] private Text maxStaminaText;
        [SerializeField] private Text currentStaminaText;
        [SerializeField] private Text maxManaText;
        [SerializeField] private Text currentManaText;
        [SerializeField] private Text attack;
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderStamina;
        [SerializeField] private Slider sliderMana;
        private const int TimeToDestroy = 1;
        private Camera _mainCamera;

        private void Start()
        {
            transform.SetParent(GameObject.Find("Canvas").transform);
            if (characterState)
            {
                sliderHealth.maxValue = (int)characterState.MaxHealth;
                maxHealthText.text = sliderHealth.maxValue.ToString(CultureInfo.InvariantCulture);
                
                sliderStamina.maxValue = (int)characterState.MaxStamina;
                maxStaminaText.text = sliderStamina.maxValue.ToString(CultureInfo.InvariantCulture);
                
                sliderMana.maxValue = (int)characterState.MaxMana;
                maxManaText.text = sliderMana.maxValue.ToString(CultureInfo.InvariantCulture);
            }

            _mainCamera = Camera.main;
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

            var position = _mainCamera.WorldToScreenPoint(requirePos);

            transform.position = position;
        }

        private void UpdateSliderValue()
        {
            sliderHealth.value = (int)characterState.CurrentHealth;
            currentHealthText.text = sliderHealth.value.ToString(CultureInfo.InvariantCulture);
            
            sliderStamina.value = (int)characterState.CurrentStamina;
            currentStaminaText.text = sliderStamina.value.ToString(CultureInfo.InvariantCulture);
                
            sliderMana.value = (int)characterState.CurrentMana;
            currentManaText.text = sliderMana.value.ToString(CultureInfo.InvariantCulture);
        }

        private void DestroyOnDeath()
        {
            if (characterState.IsDead)
            {
                fill.SetActive(false);
            
                StartCoroutine(WaitForSecondsToDisable(TimeToDestroy));
            }
            
        }

        private IEnumerator WaitForSecondsToDisable(int sec)
        {
            yield return new WaitForSeconds(sec); 
            gameObject.SetActive(false);
        }

        public void ChangeMaxValue()
        {
            sliderHealth.maxValue = (int)characterState.MaxHealth;
            maxHealthText.text = sliderHealth.maxValue.ToString(CultureInfo.InvariantCulture);
            
            sliderStamina.maxValue = (int)characterState.MaxStamina;
            maxStaminaText.text = sliderStamina.maxValue.ToString(CultureInfo.InvariantCulture);
                
            sliderMana.maxValue = (int)characterState.MaxMana;
            maxManaText.text = sliderMana.maxValue.ToString(CultureInfo.InvariantCulture);
            
            UpdateSliderValue();
        }

        public void ShowFill()
        {
            fill.SetActive(true);
        }

        private void UpdateMaxValue()
        {
            if (Math.Abs(characterState.MaxHealth - sliderHealth.maxValue) > 1)
            {
                sliderHealth.maxValue = (int)characterState.MaxHealth;
                maxHealthText.text = sliderHealth.maxValue.ToString(CultureInfo.InvariantCulture);
            }
            
            if (Math.Abs(characterState.MaxStamina - sliderStamina.maxValue) > 1)
            {
                sliderStamina.maxValue = (int)characterState.MaxStamina;
                maxStaminaText.text = sliderStamina.maxValue.ToString(CultureInfo.InvariantCulture);
            }
            
            if (Math.Abs(characterState.MaxMana - sliderMana.maxValue) > 1)
            {
                sliderMana.maxValue = (int)characterState.MaxMana;
                maxManaText.text = sliderMana.maxValue.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}