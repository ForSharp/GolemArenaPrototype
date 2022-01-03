﻿using System.Collections.Generic;
using CharacterEntity;
using CharacterEntity.CharacterState;
using CharacterEntity.ExtraStats;
using CharacterEntity.State;
using Controller;
using GameLoop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public sealed class CharacterStatsPanel : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private HeroPortraitController portrait;
        [SerializeField] private Text[] mainInfo;
        [SerializeField] private Text[] mainInfoExtraPanel;
        [SerializeField] private Text[] extraStatsUI;
        [SerializeField] private GameObject openedPanel;
        [SerializeField] private GameObject closedPanel;
        [SerializeField] private StaticHealthBar healthBar;

        public bool InPanel { get; private set; }

        private CharacterState _state;
        private CharacterExtraStats _stats;
        private bool _allowUpd;

        private void OnEnable()
        {
            EventContainer.CharacterStatsChanged += AllowUpdateStatsValues;
        }

        private void OnDisable()
        {
            EventContainer.CharacterStatsChanged -= AllowUpdateStatsValues;
        }

        private void Update()
        {
            SetLvl();

            if (_allowUpd && _state)
            {
                UpdateStats();
            }
        }

        public void OpenExtraStatsPanel()
        {
            openedPanel.SetActive(true);
            closedPanel.SetActive(false);
        }

        public void CloseExtraStatsPanel()
        {
            openedPanel.SetActive(false);
            closedPanel.SetActive(true);
        }

        public void HandleClick(CharacterState state)
        {
            if (Game.Stage == Game.GameStage.MainMenu)
                return;

            _state = state;
            _stats = _state.Stats;
            FillMainInfo();
            SetPortrait();
            FillTexts();
            panel.SetActive(true);
            CameraMovement.Instance.SetTarget(state);
            _state.SoundsController.PlayClickAndVictorySound();

            healthBar.SetCharacterState(state);

            HideAllInventoryPanels();
            state.InventoryHelper.InventoryOrganization.ShowInventory();
        }

        private void HideAllInventoryPanels()
        {
            foreach (var character in Game.AllCharactersInSession)
            {
                character.InventoryHelper.InventoryOrganization.HideAllInventory();
            }
        }

        private void UpdateStats()
        {
            if (_state.Stats != null)
            {
                _stats = _state.Stats;
                FillMainInfo();
                SetPortrait();
                FillTexts();
                _allowUpd = false;
            }
        }

        private void AllowUpdateStatsValues(CharacterState state)
        {
            _allowUpd = true;
        }

        private void SetLvl()
        {
            if (_state)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    LvlUpper.LvlUp(_state);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    LvlUpper.LvlDown(_state);
                }
            }
        }

        private void SetPortrait()
        {
            portrait.SetTexture((CharacterType)Game.ToEnum(_state.Type, typeof(CharacterType)));
        }

        private void FillMainInfo()
        {
            mainInfo[0].text = _state.Type;
            mainInfo[1].text = _state.Spec;
            mainInfo[2].text = _state.Lvl.ToString();
            mainInfo[3].text = _state.BaseStats.strength >= 100
                ? _state.BaseStats.strength.ToString("#.")
                : " " + _state.BaseStats.strength.ToString("#.");
            mainInfo[4].text = _state.BaseStats.agility >= 100
                ? _state.BaseStats.agility.ToString("#.")
                : " " + _state.BaseStats.agility.ToString("#.");
            mainInfo[5].text = _state.BaseStats.intelligence >= 100
                ? _state.BaseStats.intelligence.ToString("#.")
                : " " + _state.BaseStats.intelligence.ToString("#.");

            mainInfoExtraPanel[0].text = _state.Type;
            mainInfoExtraPanel[1].text = _state.Spec;
            mainInfoExtraPanel[2].text = _state.Lvl.ToString();
            mainInfoExtraPanel[3].text = mainInfo[3].text;
            mainInfoExtraPanel[4].text = mainInfo[4].text;
            mainInfoExtraPanel[5].text = mainInfo[5].text;
        }

        private void FillTexts()
        {
            var currentStats = GetExtraStatsArray();
            var needToDemoWithSinglePrecision = new List<int> { 0, 12, 13, 14, 15 };
            for (var i = 0; i < extraStatsUI.Length; i++)
            {
                if (needToDemoWithSinglePrecision.Contains(i))
                {
                    extraStatsUI[i].text = currentStats[i].ToString("#.00");
                }
                else
                {
                    extraStatsUI[i].text = currentStats[i].ToString("#.");
                }
            }
        }

        private float[] GetExtraStatsArray()
        {
            if (_state)
            {
                float[] extraStats =
                {
                    _stats.attackRange, _stats.attackSpeed, _stats.avoidChance, _stats.damagePerHeat, _stats.defence,
                    _stats.dodgeChance, _stats.health, _stats.hitAccuracy,
                    _stats.magicAccuracy, _stats.magicPower, _stats.magicResistance, _stats.manaPool, _stats.moveSpeed,
                    _stats.regenerationHealth, _stats.regenerationMana,
                    _stats.regenerationStamina, _stats.stamina
                };
                return extraStats;
            }

            return default;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InPanel = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            InPanel = true;
        }
    }
}