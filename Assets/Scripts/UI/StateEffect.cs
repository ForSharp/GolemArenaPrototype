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
        public string CurrentEffectId { get; private set; }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_currentDuration > 0)
            {
                _currentDuration -= Time.deltaTime;
                durationImage.fillAmount = _currentDuration / _effectDuration;
            }
        }

        public void StartShowEffect(Sprite effect, float effectDuration, bool isPositive, string effectId)
        {

            effectImage.sprite = effect;
            _effectDuration = effectDuration;
            _currentDuration = _effectDuration;

            durationImage.color = isPositive ? positiveColor : negativeColor;

            CurrentEffectId = effectId;
        }
    }
}