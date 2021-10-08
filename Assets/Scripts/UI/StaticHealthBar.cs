using System;
using FightState;
using GameLoop;
using GolemEntity.ExtraStats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StaticHealthBar : MonoBehaviour
    {
        [SerializeField] private Text maxHealthText;
        [SerializeField] private Text currentHealthText;
        [SerializeField] private Text maxStaminaText;
        [SerializeField] private Text currentStaminaText;
        [SerializeField] private Text maxManaText;
        [SerializeField] private Text currentManaText;
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderStamina;
        [SerializeField] private Slider sliderMana;
        
        private GameCharacterState _characterState;
        private bool _isDead;
        
        private void OnEnable()
        {
            
            _isDead = false;

            if (_characterState)
            {
                AddListeners();
                SetStartValues();
            }
        }

        private void OnDisable()
        {
            if (_characterState)
            {
                RemoveListeners();
            }
        }

        public void SetCharacterState(GameCharacterState state)
        {
            _characterState = state;
            SetStartValues();
            AddListeners();
        }

        private void SetStartValues()
        {
            sliderHealth.maxValue = _characterState.MaxHealth;
            maxHealthText.text = sliderHealth.maxValue.ToString("#.");
            sliderHealth.value = _characterState.CurrentHealth;
            currentHealthText.text = sliderHealth.value.ToString("#.");
                
            sliderStamina.maxValue = _characterState.MaxStamina;
            maxStaminaText.text = sliderStamina.maxValue.ToString("#.");
            sliderStamina.value = _characterState.CurrentStamina;
            currentStaminaText.text = sliderStamina.value.ToString("#.");
                
            sliderMana.maxValue = _characterState.MaxMana;
            maxManaText.text = sliderMana.maxValue.ToString("#.");
            sliderMana.value = _characterState.CurrentMana;
            currentManaText.text = sliderMana.value.ToString("#.");
        }

        private void AddListeners()
        {
            _characterState.StatsChanged += SetMaxValues;
            _characterState.CurrentHealthChanged += SetCurrentHealth;
            _characterState.CurrentStaminaChanged += SetCurrentStamina;
            _characterState.CurrentManaChanged += SetCurrentMana;
            EventContainer.GolemDied += DisableOnDeath;
        }

        private void RemoveListeners()
        {
            _characterState.StatsChanged -= SetMaxValues;
            _characterState.CurrentHealthChanged -= SetCurrentHealth;
            _characterState.CurrentStaminaChanged -= SetCurrentStamina;
            _characterState.CurrentManaChanged -= SetCurrentMana;
            EventContainer.GolemDied -= DisableOnDeath;
        }

        private void SetMaxValues(GolemExtraStats stats)
        {
            sliderHealth.maxValue = stats.Health;
            maxHealthText.text = sliderHealth.maxValue.ToString("#.");
                
            sliderStamina.maxValue = stats.Stamina;
            maxStaminaText.text = sliderStamina.maxValue.ToString("#.");
                
            sliderMana.maxValue = stats.ManaPool;
            maxManaText.text = sliderMana.maxValue.ToString("#.");
        }

        private void SetCurrentHealth(float health)
        {
            var roundedValue = health;
            currentHealthText.text = roundedValue.ToString("#.");
            sliderHealth.value = roundedValue;
        }
        
        private void SetCurrentStamina(float stamina)
        {
            var roundedValue = stamina;
            currentStaminaText.text = roundedValue.ToString("#.");
            sliderStamina.value = roundedValue;
        }
        
        private void SetCurrentMana(float mana)
        {
            var roundedValue = mana;
            currentManaText.text = roundedValue.ToString("#.");
            sliderMana.value = roundedValue;
        }
        
        private void DisableOnDeath(RoundStatistics statistics)
        {
            if (_characterState.IsDead && !_isDead)
            {
                _isDead = true;
            }
        }
    }
}
