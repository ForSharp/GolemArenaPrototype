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
        [SerializeField] private DamageViewer damageViewer;
        
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
        
        private void SetMaxValues(GolemExtraStats stats)
        {
            sliderHealth.maxValue = stats.Health;
        }

        private void SetCurrentHealth(float health)
        {
            var roundedValue = health;
            sliderHealth.value = roundedValue;
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