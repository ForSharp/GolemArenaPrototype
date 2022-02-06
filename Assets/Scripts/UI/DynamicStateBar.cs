using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CharacterEntity.State;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DynamicStateBar : MonoBehaviour
    {
        [SerializeField] private StateEffect[] effects;
        private CharacterState _characterState;
        private Camera _mainCamera;
        private bool _isDead;
        private float _boundsSizeY;
        private readonly Dictionary<string,Coroutine> _delays = new Dictionary<string,Coroutine>();

        private void Start()
        {
            _mainCamera = Camera.main;
            _boundsSizeY = _characterState.GetComponent<Collider>().bounds.size.y;
        }

        private void Update()
        {
            if (_characterState)
                SetRequiredPosition(2f);
        }

        private void OnEnable()
        {
            if (_characterState)
            {
                AddListeners();
                _isDead = false;
            }
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        public void SetCharacterState(CharacterState state)
        {
            _characterState = state;
            AddListeners();
        }

        public void StopEffect(string effectId)
        {
            foreach (var effect in effects.Where(effect => effect.gameObject.activeSelf))
            {
                if (effect.CurrentEffectId == effectId)
                {
                    effect.gameObject.SetActive(false);
                }

                _delays.TryGetValue(effectId, out var coroutine);
                StopCoroutine(coroutine);
                _delays.Remove(effectId);
            }
        }

        private void AddEffect(Sprite effectImage, float effectDuration, bool isPositive, bool canStack, string effectId)
        {
            if (!_delays.ContainsKey(effectId))
            {
                Add(effectId);
            }
            else if (canStack && _delays.ContainsKey(effectId))
            {
                Add(effectId + Guid.NewGuid());
            }
            else if (!canStack && _delays.ContainsKey(effectId))
            {
                StopEffect(effectId);
                Add(effectId);
            }
            
            void Add(string id)
            {
                var stateEffect = effects.First(effect => !effect.gameObject.activeSelf);
                stateEffect.gameObject.SetActive(true);
                stateEffect.StartShowEffect(effectImage, effectDuration, isPositive, id);
                _delays.Add(id, StartCoroutine(DisableEffectAfterDelay(effectDuration, stateEffect, id)));
            }
        }

        private IEnumerator DisableEffectAfterDelay(float delay, StateEffect stateEffect, string id)
        {
            yield return new WaitForSeconds(delay);
            stateEffect.gameObject.SetActive(false);
            _delays.Remove(id);
        }
        
        private void RemoveAllEffects()
        {
            foreach (var effect in effects)
            {
                effect.gameObject.SetActive(false);
            }

            foreach (var coroutine in _delays.Values)
            {
                StopCoroutine(coroutine);
            }
            _delays.Clear();
        }
        
        private void SetRequiredPosition(float multiplier = 1)
        {
            var requirePos = new Vector3(_characterState.transform.position.x,
                _characterState.transform.position.y + _boundsSizeY * multiplier,
                _characterState.transform.position.z);

            var position = _mainCamera.WorldToScreenPoint(requirePos);

            transform.position = position;
        }
        
        private void AddListeners()
        {
            _characterState.StateEffectAdded += AddEffect;
            Game.StartBattle += RemoveAllEffects;
            EventContainer.CharacterDied += DisableOnDeath;
        }

        private void RemoveListeners()
        {
            _characterState.StateEffectAdded -= AddEffect;
            Game.StartBattle -= RemoveAllEffects;
            EventContainer.CharacterDied -= DisableOnDeath;
        }
        
        private void DisableOnDeath(RoundStatistics statistics)
        {
            if (NeedToDisable())
            {
                StartCoroutine(WaitForSecondsToDisable(1));
                _isDead = true;
            }

            bool NeedToDisable()
            {
                return _characterState.IsDead && !_isDead;
            }
        }

        private IEnumerator WaitForSecondsToDisable(int sec)
        {
            yield return new WaitForSeconds(sec);
            RemoveAllEffects();
            gameObject.SetActive(false);
        }
    }
}