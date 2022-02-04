using System;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StateEffect : MonoBehaviour
    {
        [SerializeField] private Image effectImage;
        [SerializeField] private Image durationImage;
        [SerializeField] private Color positiveColor;
        [SerializeField] private Color negativeColor;

        private float _effectDuration;
        private float _currentDuration;
        public string currentEffectId { get; private set; }

        private void Start()
        {
            StopShowEffect();
        }

        private void Update()
        {
            if (_currentDuration > 0)
            {
                _currentDuration -= Time.deltaTime;
                durationImage.fillAmount = _currentDuration / _effectDuration;
            }
            else
            {
                StopShowEffect();
            }
        }

        public void StartShowEffect(Sprite effect, float effectDuration, bool isPositive, string effectId)
        {
            gameObject.SetActive(true);
            effectImage.sprite = effect;
            _effectDuration = effectDuration;
            _currentDuration = _effectDuration;

            durationImage.color = isPositive ? positiveColor : negativeColor;

            currentEffectId = effectId;
        }

        public void StopShowEffect()
        {
            gameObject.SetActive(false);
        }
    }
}