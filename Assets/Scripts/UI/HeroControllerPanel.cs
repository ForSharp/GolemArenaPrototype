using Fight;
using GameLoop;
using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeroControllerPanel : MonoBehaviour
    {
        [SerializeField] private HeroPortraitController portrait;
        [SerializeField] private Text heroType;
        [SerializeField] private Text heroSpec;
        [SerializeField] private Text heroLvl;
        
        private GameCharacterState _state;

        private void OnEnable()
        {
            SetCharacter();
            EventContainer.GolemStatsChanged += UpdateLvl;
        }

        private void OnDisable()
        {
            EventContainer.GolemStatsChanged -= UpdateLvl;
        }

        private void SetCharacter()
        {
            _state = Player.PlayerCharacter;
            SetPortrait();

            heroType.text = _state.Type;
            heroSpec.text = _state.Spec;
            heroLvl.text = _state.Lvl.ToString();
        }

        private void UpdateLvl(GameCharacterState state)
        {
            if (_state == state)
                heroLvl.text = _state.Lvl.ToString();
        }

        private void SetPortrait()
        {
            portrait.SetTexture((GolemType)Game.ToEnum(_state.Type, typeof(GolemType)));
        }
    }
}
