using System;
using CharacterEntity.CharacterState;
using CharacterEntity.ExtraStats;
using CharacterEntity.State;
using GameLoop;
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
        
        private CharacterState _characterState;
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

        public void SetCharacterState(CharacterState state)
        {
            _characterState = state;
            SetStartValues();
            RemoveListeners();
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

        private void SetMaxValues(CharacterExtraStats stats)
        {
            sliderHealth.maxValue = stats.health;
            maxHealthText.text = sliderHealth.maxValue.ToString("#.");

            sliderStamina.maxValue = stats.stamina;
            maxStaminaText.text = sliderStamina.maxValue.ToString("#.");
                
            sliderMana.maxValue = stats.manaPool;
            maxManaText.text = sliderMana.maxValue.ToString("#.");
            
        }

        private void SetCurrentHealth(float health)
        {
            var roundedValue = health;
            sliderHealth.value = roundedValue;
            currentHealthText.text = roundedValue.ToString("#.");
        }
        
        private void SetCurrentStamina(float stamina)
        {
            var roundedValue = stamina;
            sliderStamina.value = roundedValue;
            currentStaminaText.text = roundedValue.ToString("#.");
        }
        
        private void SetCurrentMana(float mana)
        {
            var roundedValue = mana;
            sliderMana.value = roundedValue;
            currentManaText.text = roundedValue.ToString("#.");
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
