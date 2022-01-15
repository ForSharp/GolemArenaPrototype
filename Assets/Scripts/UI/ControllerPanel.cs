﻿using CharacterEntity;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ControllerPanel : MonoBehaviour
    {
        [SerializeField] private HeroPortraitController portrait;
        [SerializeField] private Text heroType;
        [SerializeField] private Text heroSpec;
        [SerializeField] private Text heroLvl;
        [SerializeField] private StaticHealthBar healthBar;
        
        private CharacterState _state;

        // private void OnEnable()
        // {
        //     SetCharacter();
        //     EventContainer.CharacterStatsChanged += UpdateLvl;
        // }
        //
        // private void OnDisable()
        // {
        //     EventContainer.CharacterStatsChanged -= UpdateLvl;
        // }
        //
        // private void SetCharacter()
        // {
        //     _state = Player.PlayerCharacter;
        //     SetPortrait();
        //
        //     heroType.text = _state.Type;
        //     heroSpec.text = _state.Spec;
        //     heroLvl.text = _state.Lvl.ToString();
        //     
        //     healthBar.SetCharacterState(_state);
        // }
        //
        // private void UpdateLvl(CharacterState state)
        // {
        //     if (_state == state)
        //         heroLvl.text = _state.Lvl.ToString();
        // }
        //
        // private void SetPortrait()
        // {
        //     portrait.SetTexture((CharacterType)Game.ToEnum(_state.Type, typeof(CharacterType)));
        // }
    }
}
