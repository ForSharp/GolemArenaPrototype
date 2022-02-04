using System;
using System.Collections;
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

        public void SetCharacterState(CharacterState state)
        {
            _characterState = state;
            AddListeners();
        }

        public void StopEffect(string effectId)
        {
            foreach (var effect in effects.Where(effect => effect.gameObject.activeSelf))
            {
                if (effect.currentEffectId == effectId)
                    effect.StopShowEffect();
            }
        }

        private void AddEffect(Sprite effectImage, float effectDuration, bool isPositive, string effectId)
        {
            var stateEffect = effects.First(effect => !effect.gameObject.activeSelf);
            stateEffect.StartShowEffect(effectImage, effectDuration, isPositive, effectId);
        }

        private void RemoveAllEffects()
        {
            foreach (var effect in effects)
            {
                effect.StopShowEffect();
            }
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
            RemoveListeners();
            gameObject.SetActive(false);
        }
    }
}