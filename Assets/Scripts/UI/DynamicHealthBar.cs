﻿using System.Collections;
using Fight;
using GameLoop;
using GolemEntity.ExtraStats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DynamicHealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject fill;
        [SerializeField] private Slider sliderHealth;
        [SerializeField] private Slider sliderBack;
        [SerializeField] private DamageViewer damageViewer;
        [SerializeField] private Color fullHpColor;
        [SerializeField] private Color lowHpColor;
        [SerializeField] private Image currentColor;
        
        private GameCharacterState _characterState;
        private const int TimeToDestroy = 1;
        private Camera _mainCamera;
        private bool _isDead;
        
        private void Start()
        {
            transform.SetParent(GameObject.Find("Canvas").transform);
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            fill.SetActive(true);
            damageViewer.gameObject.SetActive(true);
            _isDead = false;

            if (_characterState)
            {
                AddListeners();
                SetStartValues();
            }
        }
        
        private void Update()
        {
            if (_characterState)
                SetRequiredPosition();
            
        }

        public void SetCharacterState(GameCharacterState state)
        {
            _characterState = state;
            SetStartValues();
            AddListeners();
            damageViewer.State = state;
        }

        private void SetStartValues()
        {
            sliderHealth.maxValue = _characterState.MaxHealth;
            sliderHealth.value = _characterState.CurrentHealth;
            SetCurrentColor();

            sliderBack.maxValue = sliderHealth.maxValue;
            sliderBack.value = sliderHealth.value;
        }

        private void AddListeners()
        {
            _characterState.StatsChanged += SetMaxValues;
            _characterState.CurrentHealthChanged += SetCurrentHealth;
            EventContainer.GolemDied += DisableOnDeath;
        }

        private void RemoveListeners()
        {
            _characterState.StatsChanged -= SetMaxValues;
            _characterState.CurrentHealthChanged -= SetCurrentHealth;
            EventContainer.GolemDied -= DisableOnDeath;
        }

        private void SetCurrentColor()
        {
            currentColor.color = Color.Lerp(fullHpColor, lowHpColor, GetCombineValue());
        }

        private float GetCombineValue()
        {
            var difference = sliderHealth.maxValue - sliderHealth.value;
            return difference / sliderHealth.maxValue;
        }

        private void SetMaxValues(GolemExtraStats stats)
        {
            sliderHealth.maxValue = stats.Health;
            SetCurrentColor();
            
        }

        private void SetCurrentHealth(float health)
        {
            var roundedValue = health;
            sliderHealth.value = roundedValue;
            SetCurrentColor();

            StartCoroutine(SetCurrentBackAfterDelay());
        }

        private IEnumerator SetCurrentBackAfterDelay()
        {
            yield return new WaitForSeconds(1);
            
            sliderBack.value = sliderHealth.value;

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
                _isDead = true;
                fill.SetActive(false);
                RemoveListeners();
            }

            bool NeedToDisable()
            {
                return _characterState.IsDead && !_isDead;
            }
        }

        private IEnumerator WaitForSecondsToDisable(int sec)
        {
            yield return new WaitForSeconds(sec); 
            gameObject.SetActive(false);
            damageViewer.gameObject.SetActive(false);
        }
    }
}