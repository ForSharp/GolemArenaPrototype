using System.Globalization;
using Controller;
using Fight;
using GameLoop;
using GolemEntity;
using GolemEntity.ExtraStats;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public sealed class GolemStatsPanel : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private HeroPortraitController portrait;
        [SerializeField] private Text[] mainInfo;
        [SerializeField] private Text[] mainInfoExtraPanel;
        [SerializeField] private Text[] extraStatsUI;
        [SerializeField] private GameObject openedPanel;
        [SerializeField] private GameObject closedPanel;

        [HideInInspector] public bool inPanel;

        private GameCharacterState _state;
        private GolemExtraStats _stats;
        private bool _allowUpd;

        private void OnEnable()
        {
            EventContainer.GolemStatsChanged += AllowUpdateStatsValues;
        }

        private void OnDisable()
        {
            EventContainer.GolemStatsChanged -= AllowUpdateStatsValues;
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

        public void HandleClick(GameCharacterState state)
        {
            _state = state;
            _stats = _state.Stats;
            FillMainInfo();
            SetPortrait();
            FillTexts();
            panel.SetActive(true);
            CameraMovement.Instance.SetTarget(state);
            _state.SoundsController.PlayClickAndVictorySound();
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

        private void AllowUpdateStatsValues(GameCharacterState state)
        {
            _allowUpd = true;
        }

        private void SetLvl()
        {
            if (_state)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    UpgradeSystem.LvlUp(_state);
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    UpgradeSystem.LvlDown(_state);
                }
            }
        }

        private void SetPortrait()
        {
            portrait.SetTexture((GolemType)Game.ToEnum(_state.Type, typeof(GolemType)));
        }

        private void FillMainInfo()
        {
            mainInfo[0].text = _state.Type;
            mainInfo[1].text = _state.Spec;
            mainInfo[2].text = _state.Lvl.ToString();
            mainInfo[3].text = _state.BaseStats.Strength.ToString(CultureInfo.InvariantCulture);
            mainInfo[4].text = _state.BaseStats.Agility.ToString(CultureInfo.InvariantCulture);
            mainInfo[5].text = _state.BaseStats.Intelligence.ToString(CultureInfo.InvariantCulture);

            mainInfoExtraPanel[0].text = _state.Type;
            mainInfoExtraPanel[1].text = _state.Spec;
            mainInfoExtraPanel[2].text = _state.Lvl.ToString();
            mainInfoExtraPanel[3].text = _state.BaseStats.Strength.ToString(CultureInfo.InvariantCulture);
            mainInfoExtraPanel[4].text = _state.BaseStats.Agility.ToString(CultureInfo.InvariantCulture);
            mainInfoExtraPanel[5].text = _state.BaseStats.Intelligence.ToString(CultureInfo.InvariantCulture);
        }

        private void FillTexts()
        {
            var currentStats = GetExtraStatsArray();

            for (var i = 0; i < extraStatsUI.Length; i++)
            {
                extraStatsUI[i].text = currentStats[i].ToString(CultureInfo.InvariantCulture);
            }
        }

        private float[] GetExtraStatsArray()
        {
            if (_state)
            {
                float[] extraStats =
                {
                    _stats.AttackRange, _stats.AttackSpeed, _stats.AvoidChance, _stats.DamagePerHeat, _stats.Defence,
                    _stats.DodgeChance, _stats.Health, _stats.HitAccuracy,
                    _stats.MagicAccuracy, _stats.MagicDamage, _stats.MagicResistance, _stats.ManaPool, _stats.MoveSpeed,
                    _stats.RegenerationHealth, _stats.RegenerationMana,
                    _stats.RegenerationStamina, _stats.Stamina
                };
                return extraStats;
            }

            return default;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            inPanel = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            inPanel = true;
        }
    }
}