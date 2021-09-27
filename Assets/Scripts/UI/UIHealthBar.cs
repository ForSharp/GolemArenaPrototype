using System;
using System.Collections;
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
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderStamina;
        [SerializeField] private Slider sliderMana;
        [SerializeField] private bool inPanel;
        [SerializeField] private Text[] attack;
        [SerializeField] private Animator[] animatorAttack;

        private int _attackInfoQueueNumber = 0;
        private GameCharacterState _characterState;
        private const int TimeToDestroy = 1;
        private Camera _mainCamera;
        private bool _isDead;
        private static readonly int HitReceived = Animator.StringToHash("HitReceived");
        private static readonly int CriticalHitReceived = Animator.StringToHash("CriticalHitReceived");

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

            if (!inPanel)
            {
                EventContainer.FightEvent += HandleFightEvent;
            }
        }

        private void RemoveListeners()
        {
            _characterState.StatsChanged -= SetMaxValues;
            _characterState.CurrentHealthChanged -= SetCurrentHealth;
            _characterState.CurrentStaminaChanged -= SetCurrentStamina;
            _characterState.CurrentManaChanged -= SetCurrentMana;
            EventContainer.GolemDied -= DisableOnDeath;
            
            if (!inPanel)
            {
                EventContainer.FightEvent -= HandleFightEvent;
            }
        }
        
        private void HandleFightEvent(object sender, EventArgs args)
        {
            if ((GameCharacterState)sender == _characterState)
            {
                var fightArgs = (FightEventArgs)args;
                if (fightArgs.IsAvoiding)
                {
                    animatorAttack[_attackInfoQueueNumber].SetTrigger(HitReceived);
                    attack[_attackInfoQueueNumber].text = "AVOID";
                }
                else if (fightArgs.IsAttackFromBehind)
                {
                    animatorAttack[_attackInfoQueueNumber].SetTrigger(CriticalHitReceived);
                    attack[_attackInfoQueueNumber].text = $"-{fightArgs.AttackHitEventArgs.DamagePerHit:#.00}!";
                }
                else
                {
                    animatorAttack[_attackInfoQueueNumber].SetTrigger(HitReceived);
                    attack[_attackInfoQueueNumber].text = $"-{fightArgs.AttackHitEventArgs.DamagePerHit:#.00}";
                }

                _attackInfoQueueNumber++;
                if (_attackInfoQueueNumber >= 3)
                {
                    _attackInfoQueueNumber = 0;
                }
            }
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