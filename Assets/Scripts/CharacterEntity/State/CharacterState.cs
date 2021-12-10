﻿using System;
using System.Collections;
using CharacterEntity.BaseStats;
using CharacterEntity.CharacterState;
using CharacterEntity.ExtraStats;
using GameLoop;
using Inventory;
using UI;
using UnityEngine;

namespace CharacterEntity.State
{
    public sealed class CharacterState : MonoBehaviour, IDestructible
    {
        [SerializeField] private bool isDynamicHealthBarCreate = true;
        [SerializeField] private GameObject healthBarPrefab;
        private GameObject _healthBar;
        
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public float MaxStamina { get; private set; }
        public float CurrentStamina { get; private set; }
        public float MaxMana { get; private set; }
        public float CurrentMana { get; private set; }
        public int Group { get; private set; }
        public Color ColorGroup { get; private set; }
        public int Lvl { get; set; }
        public bool IsDead { get; private set; }
        public CharacterBaseStats BaseStats { get; private set; }
        public CharacterExtraStats Stats { get; private set; }
        public string Type { get; private set; }
        public string Spec { get; private set; }
        public Character Character { get; private set; }
        public SoundsController SoundsController { get; private set; }
        public InventoryHelper InventoryHelper { get; private set; }
        public SpellManager SpellManager { get; private set; }
        //public SpellsPanel SpellsPanel { get; private set; }
        
        private RoundStatistics _lastEnemyAttacked;
        public RoundStatistics RoundStatistics;
        public event EventHandler AttackReceived;
        public event Action<float> CurrentHealthChanged;
        public event Action<float> CurrentStaminaChanged;
        public event Action<float> CurrentManaChanged;
        public event Action<CharacterExtraStats> StatsChanged;

        public void OnAttackReceived(object sender, AttackHitEventArgs args)
        {
            AttackReceived?.Invoke(sender, args);
        }

        private void Start()
        {
            SoundsController = GetComponent<SoundsController>();
            RoundStatistics = new RoundStatistics(this);
            InventoryHelper = GetComponent<InventoryHelper>();
            SpellManager = new SpellManager(GetComponent<Animator>(), this, GetComponent<SpellContainer>());
            //SpellPanel
            var unused = new ExtraStatsEditorWithItems(this);
            var dummy = new ConsumablesEater(this);
        }

        private void OnEnable()
        {
            EventContainer.GolemStatsChanged += UpdateStats;

            StartCoroutine(RegenerateCurrents());
        }

        private void OnDestroy()
        {
            EventContainer.GolemStatsChanged -= UpdateStats;
        }

        private void UpdateStats(CharacterState state)
        {
            if (state != this) return;
            BaseStats = Character.GetBaseStats();
            Stats = Character.GetExtraStats();
            SetProportionallyCurrentHealth(Character.GetExtraStats().health);
            SetProportionallyCurrentStamina(Character.GetExtraStats().stamina);
            SetProportionallyCurrentMana(Character.GetExtraStats().manaPool);
            MaxHealth = Stats.health;
            MaxStamina = Stats.stamina;
            MaxMana = Stats.manaPool;
            OnStatsChanged(Stats);
        }

        private void SetProportionallyCurrentHealth(float newMaxHealth)
        {
            var difference = MaxHealth - newMaxHealth;
            CurrentHealth -= difference;
            if (CurrentHealth > newMaxHealth)
            {
                CurrentHealth = newMaxHealth;
            }
            OnCurrentHealthChanged(CurrentHealth);
        }

        private void SetProportionallyCurrentStamina(float newMaxStamina)
        {
            var difference = MaxStamina - newMaxStamina;
            CurrentStamina -= difference;
            if (CurrentStamina > newMaxStamina)
            {
                CurrentStamina = newMaxStamina;
            }
            OnCurrentStaminaChanged(CurrentStamina);
        }

        private void SetProportionallyCurrentMana(float newMaxMana)
        {
            var difference = MaxMana - newMaxMana;
            CurrentMana -= difference;
            if (CurrentMana > newMaxMana)
            {
                CurrentMana = newMaxMana;
            }
            OnCurrentManaChanged(CurrentMana);
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

        private void SetStartState()
        {
            if (Character == null) return;

            IsDead = false;
            Lvl = 1;
            Stats = Character.GetExtraStats();
            BaseStats = Character.GetBaseStats();
            MaxHealth = Stats.health;
            CurrentHealth = MaxHealth;
            MaxStamina = Stats.stamina;
            CurrentStamina = MaxStamina;
            MaxMana = Stats.manaPool;
            CurrentMana = MaxMana;

            CreateHealthBar();
        }

        private void CreateHealthBar()
        {
            if (!isDynamicHealthBarCreate) return;
            _healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, GameObject.Find("HealthBarContainer").transform);
            _healthBar.GetComponent<DynamicHealthBar>().SetCharacterState(this);
        }

        public void TakeDamage(float damage, RoundStatistics statistics)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                damage += CurrentHealth;
                PrepareToDie();
            }

            if (statistics != null)
            {
                statistics.Damage += damage;
                statistics.RoundDamage += damage;
                _lastEnemyAttacked = statistics;
            }

            OnCurrentHealthChanged(CurrentHealth);
        }

        public bool TrySpendMana(float mana)
        {
            if (CurrentMana < mana)
                return false;

            CurrentMana -= mana;
            OnCurrentManaChanged(CurrentMana);
            return true;
        }
        private void PrepareToDie()
        {
            if (!IsDead)
            {
                CurrentHealth = 0;
                _lastEnemyAttacked.Kills += 1;
                _lastEnemyAttacked.RoundKills += 1;
                IsDead = true;
                EventContainer.OnGolemDied(_lastEnemyAttacked);
            }
        }

        public void PrepareAfterNewRound()
        {
            IsDead = false;
            LvlUpper.LvlUp(this, 7);
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
            CurrentStamina = MaxStamina;
            CurrentMana = MaxMana;

            OnCurrentHealthChanged(CurrentHealth);
            OnCurrentStaminaChanged(CurrentStamina);
            OnCurrentManaChanged(CurrentMana);
        }

        private void ShowHealthBar()
        {
            _healthBar.gameObject.SetActive(true);
        }

        public void SpendStamina(float energy)
        {
        }

        public void HealCurrentsFlat(MainCharacterParameter parameter, float healingValue)
        {
            switch (parameter)
            {
                case MainCharacterParameter.Strength:
                    CurrentHealth += healingValue;
                    if (CurrentHealth > MaxHealth)
                    {
                        CurrentHealth = MaxHealth;
                    }
                    OnCurrentHealthChanged(CurrentHealth);
                    break;
                case MainCharacterParameter.Agility:
                    CurrentStamina += healingValue;
                    if (CurrentStamina > MaxStamina)
                    {
                        CurrentStamina = MaxStamina;
                    }
                    OnCurrentStaminaChanged(CurrentStamina);
                    break;
                case MainCharacterParameter.Intelligence:
                    CurrentMana += healingValue;
                    if (CurrentMana > MaxMana)
                    {
                        CurrentMana = MaxMana;
                    }
                    OnCurrentManaChanged(CurrentMana);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
            }
        }
        
        public void HealCurrentsMultiply(MainCharacterParameter parameter, float healingValue)
        {
            switch (parameter)
            {
                case MainCharacterParameter.Strength:
                    CurrentHealth *= healingValue;
                    if (CurrentHealth > MaxHealth)
                    {
                        CurrentHealth = MaxHealth;
                    }
                    OnCurrentHealthChanged(CurrentHealth);
                    break;
                case MainCharacterParameter.Agility:
                    CurrentStamina *= healingValue;
                    if (CurrentStamina > MaxStamina)
                    {
                        CurrentStamina = MaxStamina;
                    }
                    OnCurrentStaminaChanged(CurrentStamina);
                    break;
                case MainCharacterParameter.Intelligence:
                    CurrentMana *= healingValue;
                    if (CurrentMana > MaxMana)
                    {
                        CurrentMana = MaxMana;
                    }
                    OnCurrentManaChanged(CurrentMana);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(parameter), parameter, null);
            }
        }
        
        private IEnumerator RegenerateCurrents()
        {
            yield return new WaitForSeconds(1);

            if (!IsDead)
            {
                if (CurrentHealth < MaxHealth)
                {
                    CurrentHealth += Stats.regenerationHealth;
                    if (CurrentHealth > MaxHealth)
                    {
                        CurrentHealth = MaxHealth;
                    }
                    OnCurrentHealthChanged(CurrentHealth);
                }

                if (CurrentStamina < MaxStamina)
                {
                    CurrentStamina += Stats.regenerationStamina;
                    if (CurrentStamina > MaxStamina)
                    {
                        CurrentStamina = MaxStamina;
                    }
                    OnCurrentStaminaChanged(CurrentStamina);
                }

                if (CurrentMana < MaxMana)
                {
                    CurrentMana += Stats.regenerationMana;
                    if (CurrentMana > MaxMana)
                    {
                        CurrentMana = MaxMana;
                    }
                    OnCurrentManaChanged(CurrentMana);
                }

            
                StartCoroutine(RegenerateCurrents());
            }

        }

        private void OnCurrentHealthChanged(float health)
        {
            CurrentHealthChanged?.Invoke(health);
        }

        private void OnCurrentStaminaChanged(float stamina)
        {
            CurrentStaminaChanged?.Invoke(stamina);
        }

        private void OnCurrentManaChanged(float mana)
        {
            CurrentManaChanged?.Invoke(mana);
        }

        private void OnStatsChanged(CharacterExtraStats stats)
        {
            StatsChanged?.Invoke(stats);
        }
    }
}