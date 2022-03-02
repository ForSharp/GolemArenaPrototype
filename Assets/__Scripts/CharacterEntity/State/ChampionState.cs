using System;
using __Scripts.Inventory;
using GameLoop;
using Inventory;
using SpellSystem;
using UnityEngine;

namespace CharacterEntity.State
{
    public class ChampionState : CharacterState
    {
        public string Spec { get; private set; }
        public InventoryHelper InventoryHelper { get; private set; }
        public SpellManager SpellManager { get; private set; }
        public SpellPanelHelper SpellPanelHelper { get; private set; }
        public RoundStatistics RoundStatistics { get; private set; }
        public SoundsController SoundsController { get; private set; }
        public event Action StartSpellCast;
        public event Action CancelSpellCast;

        private void Start()
        {
            RoundStatistics = new RoundStatistics(this);
            
            InventoryHelper = GetComponent<InventoryHelper>();
            SpellPanelHelper = GetComponent<SpellPanelHelper>();
            SpellManager = new SpellManager(GetComponent<Animator>(), this, GetComponent<SpellContainer>());
            
            var unused = new ExtraStatsEditorWithItems(this);
            
            ConsumablesEater = new ConsumablesEater(this);
            
            SoundsController = GetComponent<SoundsController>();
        }
        
        public void InitializeState(Character character, int group, Color colorGroup, string type, string spec)
        {
            Character = character;
            Group = group;
            ColorGroup = colorGroup;
            Type = type;
            Spec = spec;

            SetStartState();
        }
        
        public void PrepareAfterNewRound()
        {
            IsDead = false;
            LvlUpCharacter(7);
            HealAllParameters();
            ShowHealthBar();
            NullRoundStatistics();
        }

        public void PrepareAfterNewRoundForException()
        {
            IsDead = false;
            HealAllParameters();
            ShowHealthBar();
            NullRoundStatistics();
        }
        
        private void NullRoundStatistics()
        {
            RoundStatistics.RoundDamage = 0;
            RoundStatistics.RoundKills = 0;
            RoundStatistics.WinLastRound = false;
            RoundStatistics.RoundRate = 0;
        }

        private void HealAllParameters()
        {
            CurrentHealth = MaxHealth;
            CurrentMana = MaxMana;

            OnCurrentHealthChanged(CurrentHealth);
            OnCurrentManaChanged(CurrentMana);
        }

        private void ShowHealthBar()
        {
            healthBar.gameObject.SetActive(true);
            StateBar.gameObject.SetActive(true);
        }
        
        public void OnStartSpellCast()
        {
            StartSpellCast?.Invoke();
        }

        public void OnCancelSpellCast()
        {
            CancelSpellCast?.Invoke();
        }
    }
}