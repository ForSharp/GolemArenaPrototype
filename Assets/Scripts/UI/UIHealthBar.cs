using System.Collections;
using System.Globalization;
using Fight;
using GameLoop;
using GolemEntity.ExtraStats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealthBar : MonoBehaviour
    {
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
        [SerializeField] private bool inPanel;
        
        private GameCharacterState _characterState;
        private const int TimeToDestroy = 1;
        private Camera _mainCamera;
        private bool _isDead;

        private void Start()
        {
            if (!inPanel)
            {
                transform.SetParent(GameObject.Find("Canvas").transform);
                _mainCamera = Camera.main;
            }
        }

        private void OnEnable()
        {
            fill.SetActive(true);
            _isDead = false;

            if (_characterState)
            {
                AddListeners();
                SetStartValues();
            }
        }
        
        private void Update()
        {
            if (_characterState &&!inPanel)
                SetRequiredPosition();
        }

        public void SetCharacterState(GameCharacterState state)
        {
            _characterState = state;
            SetStartValues();
            AddListeners();
        }

        private void SetStartValues()
        {
            sliderHealth.maxValue = (int)_characterState.MaxHealth;
            maxHealthText.text = sliderHealth.maxValue.ToString(CultureInfo.InvariantCulture);
            sliderHealth.value = (int)_characterState.CurrentHealth;
            currentHealthText.text = sliderHealth.value.ToString(CultureInfo.InvariantCulture);
                
            sliderStamina.maxValue = (int)_characterState.MaxStamina;
            maxStaminaText.text = sliderStamina.maxValue.ToString(CultureInfo.InvariantCulture);
            sliderStamina.value = (int)_characterState.CurrentStamina;
            currentStaminaText.text = sliderStamina.value.ToString(CultureInfo.InvariantCulture);
                
            sliderMana.maxValue = (int)_characterState.MaxMana;
            maxManaText.text = sliderMana.maxValue.ToString(CultureInfo.InvariantCulture);
            sliderMana.value = (int)_characterState.CurrentMana;
            currentManaText.text = sliderMana.value.ToString(CultureInfo.InvariantCulture);
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
            sliderHealth.maxValue = (int)stats.Health;
            maxHealthText.text = sliderHealth.maxValue.ToString(CultureInfo.InvariantCulture);
                
            sliderStamina.maxValue = (int)stats.Stamina;
            maxStaminaText.text = sliderStamina.maxValue.ToString(CultureInfo.InvariantCulture);
                
            sliderMana.maxValue = (int)stats.ManaPool;
            maxManaText.text = sliderMana.maxValue.ToString(CultureInfo.InvariantCulture);
        }

        private void SetCurrentHealth(float health)
        {
            var roundedValue = (int)health;
            currentHealthText.text = roundedValue.ToString(CultureInfo.InvariantCulture);
            sliderHealth.value = roundedValue;
        }
        
        private void SetCurrentStamina(float stamina)
        {
            var roundedValue = (int)stamina;
            currentStaminaText.text = roundedValue.ToString(CultureInfo.InvariantCulture);
            sliderStamina.value = roundedValue;
        }
        
        private void SetCurrentMana(float mana)
        {
            var roundedValue = (int)mana;
            currentManaText.text = roundedValue.ToString(CultureInfo.InvariantCulture);
            sliderMana.value = roundedValue;
        }
        
        private void SetRequiredPosition(float multiplier = 1)
        {
            var requirePos = new Vector3(_characterState.transform.position.x,
                _characterState.transform.position.y + _characterState.GetComponent<Collider>().bounds.size.y * multiplier,
                _characterState.transform.position.z);

            var position = _mainCamera.WorldToScreenPoint(requirePos);

            transform.position = position;
        }

        private void DisableOnDeath(RoundStatistics statistics)
        {
            if (NeedToDisable())
            {
                StartCoroutine(WaitForSecondsToDisable(TimeToDestroy));
            }

            if (_characterState.IsDead && !_isDead)
            {
                _isDead = true;
                fill.SetActive(false);
                RemoveListeners();
            }

            bool NeedToDisable()
            {
                return _characterState.IsDead && !_isDead && !inPanel;
            }
        }

        private IEnumerator WaitForSecondsToDisable(int sec)
        {
            yield return new WaitForSeconds(sec); 
            gameObject.SetActive(false);
        }
    }
}