using __Scripts.CharacterEntity.ExtraStats;
using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.UI
{
    public class StaticHealthBar : MonoBehaviour
    {
        [SerializeField] private Text maxHealthText;
        [SerializeField] private Text currentHealthText;
        [SerializeField] private Text maxManaText;
        [SerializeField] private Text currentManaText;
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderMana;
        
        private CharacterState _characterState;
        private bool _isDead;
        
        private void OnEnable()
        {
            
            _isDead = false;

            if (_characterState)
            {
                SetStartValues();
                AddListeners();
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
            if (_characterState)
            {
                RemoveListeners();
            }
            
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

            sliderMana.maxValue = _characterState.MaxMana;
            maxManaText.text = sliderMana.maxValue.ToString("#.");
            sliderMana.value = _characterState.CurrentMana;
            currentManaText.text = sliderMana.value.ToString("#.");
        }

        private void AddListeners()
        {
            _characterState.StatsChanged += SetMaxValues;
            _characterState.CurrentHealthChanged += SetCurrentHealth;
            _characterState.CurrentManaChanged += SetCurrentMana;
            EventContainer.CharacterDied += DisableOnDeath;
        }

        private void RemoveListeners()
        {
            _characterState.StatsChanged -= SetMaxValues;
            _characterState.CurrentHealthChanged -= SetCurrentHealth;
            _characterState.CurrentManaChanged -= SetCurrentMana;
            EventContainer.CharacterDied -= DisableOnDeath;
        }

        private void SetMaxValues(CharacterExtraStats stats)
        {
            sliderHealth.maxValue = stats.health;
            sliderHealth.value = _characterState.CurrentHealth;
            maxHealthText.text = sliderHealth.maxValue.ToString("#.");

            sliderMana.maxValue = stats.manaPool;
            sliderMana.value = _characterState.CurrentMana;
            maxManaText.text = sliderMana.maxValue.ToString("#.");
            
        }

        private void SetCurrentHealth(float health)
        {
            var roundedValue = health;
            sliderHealth.value = roundedValue;
            currentHealthText.text = roundedValue.ToString("#.");
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
