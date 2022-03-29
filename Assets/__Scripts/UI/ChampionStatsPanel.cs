using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.ExtraStats;
using __Scripts.CharacterEntity.State;
using __Scripts.Controller;
using __Scripts.GameLoop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace __Scripts.UI
{
    public sealed class ChampionStatsPanel : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private HeroPortraitController portrait;

        [SerializeField] private GameObject extraPanel;

        [SerializeField] private Text characterType;
        [SerializeField] private Text characterSpec;
        [SerializeField] private Text strength;
        [SerializeField] private Text agility;
        [SerializeField] private Text intelligence;
        [SerializeField] private Text characterLvl;

        [SerializeField] private Text attack;
        [SerializeField] private Text moveSpeed;
        [SerializeField] private Text defence;
        [SerializeField] private Text hpRegen;
        [SerializeField] private Text manaRegen;

        [SerializeField] private Text attackRange;
        [SerializeField] private Text attackSpeed;
        [SerializeField] private Text avoidChance;
        [SerializeField] private Text dodgeChance;
        [SerializeField] private Text hitAccuracy;
        [SerializeField] private Text magicAccuracy;
        [SerializeField] private Text magicPower;
        [SerializeField] private Text magicResistance;

        [SerializeField] private StaticHealthBar healthBar;
        public bool InPanel { get; private set; }

        private ChampionState _state;

        private CharacterExtraStats _stats;

        private void OnEnable()
        {
            SetPlayerCharacter();
            EventContainer.CharacterStatsChanged += SetStatsValues;
        }

        private void OnDisable()
        {
            EventContainer.CharacterStatsChanged -= SetStatsValues;
        }


        public void SetPlayerCharacter()
        {
            _state = Player.PlayerCharacter;
            _stats = Player.PlayerCharacter.Stats;
            SetStatsValues(Player.PlayerCharacter);
            healthBar.SetCharacterState(_state);
        }
        
        public void ChangeExtraStatsPanelState()
        {
            if (extraPanel.activeSelf)
                extraPanel.SetActive(false);
            else
            {
                extraPanel.SetActive(true);
                FillExtraInfo();
                CloseNonEquippingInventory();
                CloseLearnedSpells();
            }
        }

        public void CloseExtraStatsPanel()
        {
            extraPanel.SetActive(false);
        }

        public void HandleClick(ChampionState state)
        {
            if (Game.Stage == Game.GameStage.MainMenu)
                return;

            _state = state;
            _stats = _state.Stats;
            FillMainInfo();
            SetPortrait();
            FillExtraInfo();
            panel.SetActive(true);
            CameraMovement.Instance.SetTarget(state);
            _state.SoundsController.PlayClickAndVictorySound();

            healthBar.SetCharacterState(state);

            HideAllInventoryPanels();
            CloseSpellPanels();
            CloseExtraStatsPanel();
            state.InventoryHelper.InventoryOrganization.ShowInventory();
            state.SpellPanelHelper.SpellsPanel.ShowActiveSpells();
        }

        private void CloseLearnedSpells()
        {
            foreach (var character in Game.AllChampionsInSession)
            {
                character.SpellPanelHelper.SpellsPanel.HideLearnedSpellsPanel();
            }
        }

        private void CloseSpellPanels()
        {
            foreach (var character in Game.AllChampionsInSession)
            {
                character.SpellPanelHelper.SpellsPanel.HideAll();
            }
        }

        private void CloseNonEquippingInventory()
        {
            foreach (var character in Game.AllChampionsInSession)
            {
                character.InventoryHelper.InventoryOrganization.HideNonEquippingSlots();
            }
        }

        private void HideAllInventoryPanels()
        {
            foreach (var character in Game.AllChampionsInSession)
            {
                character.InventoryHelper.InventoryOrganization.HideAllInventory();
            }
        }

        public void SetStatsValues(CharacterState state)
        {
            if (_state.Stats != null)
            {
                _stats = _state.Stats;
                FillMainInfo();
                SetPortrait();
                FillExtraInfo();
            }
        }

        private void SetPortrait()
        {
            portrait.SetTexture((CharacterType)Game.ToEnum(_state.Type, typeof(CharacterType)));
        }

        private void FillMainInfo()
        {
            characterType.text = _state.Type;
            characterSpec.text = _state.Spec;
            characterLvl.text = _state.Lvl.ToString();
            strength.text = _state.BaseStats.strength >= 100
                ? _state.BaseStats.strength.ToString("#.")
                : " " + _state.BaseStats.strength.ToString("#.");
            agility.text = _state.BaseStats.agility >= 100
                ? _state.BaseStats.agility.ToString("#.")
                : " " + _state.BaseStats.agility.ToString("#.");
            intelligence.text = _state.BaseStats.intelligence >= 100
                ? _state.BaseStats.intelligence.ToString("#.")
                : " " + _state.BaseStats.intelligence.ToString("#.");

            attack.text = _stats.damagePerHeat.ToString("#.0");
            moveSpeed.text = _stats.moveSpeed.ToString("#.00");
            defence.text = _stats.defence.ToString("#.0");

            hpRegen.text = "+" + _stats.regenerationHealth.ToString("#.00");
            manaRegen.text = "+" + _stats.regenerationMana.ToString("#.00");
        }

        private void FillExtraInfo()
        {
            if (extraPanel.activeSelf)
            {
                attackRange.text = _stats.attackRange.ToString("#.0");
                attackSpeed.text = _stats.attackSpeed.ToString("#.0");
                avoidChance.text = _stats.avoidChance.ToString("#.0");
                dodgeChance.text = _stats.dodgeChance.ToString("#.0");
                hitAccuracy.text = _stats.hitAccuracy.ToString("#.0");
                magicAccuracy.text = _stats.magicAccuracy.ToString("#.0");
                magicPower.text = _stats.magicPower.ToString("#.0");
                magicResistance.text = _stats.magicResistance.ToString("#.0");
            }
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