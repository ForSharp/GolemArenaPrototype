using System;
using System.Collections;
using System.Collections.Generic;
using GameLoop;
using GolemEntity;
using GolemEntity.BaseStats;
using GolemEntity.ExtraStats;
using Inventory.Abstracts;
using UI;
using UnityEngine;

namespace FightState
{
    public sealed class GameCharacterState : MonoBehaviour, IDestructible
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
        public GolemBaseStats BaseStats { get; private set; }
        public GolemExtraStats Stats { get; private set; }
        public string Type { get; private set; }
        public string Spec { get; private set; }
        public Golem Golem { get; private set; }
        public SoundsController SoundsController { get; private set; }

        private bool _isReady;
        private RoundStatistics _lastEnemyAttacked;
        public RoundStatistics RoundStatistics;
        public List<IInventoryItem> Items = new List<IInventoryItem>();

        public event EventHandler AttackReceived;
        public event Action<float> CurrentHealthChanged;
        public event Action<float> CurrentStaminaChanged;
        public event Action<float> CurrentManaChanged;
        public event Action<GolemExtraStats> StatsChanged;

        public void OnAttackReceived(object sender, AttackHitEventArgs args)
        {
            AttackReceived?.Invoke(sender, args);
        }

        private void Start()
        {
            SoundsController = GetComponent<SoundsController>();
            RoundStatistics = new RoundStatistics(this);
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

        private void Update()
        {
            if (!_isReady)
            {
                return;
            }
        }

        private void UpdateStats(GameCharacterState state)
        {
            if (state != this) return;
            BaseStats = Golem.GetBaseStats();
            Stats = Golem.GetExtraStats();
            SetProportionallyCurrentHealth(Golem.GetExtraStats().health);
            SetProportionallyCurrentStamina(Golem.GetExtraStats().stamina);
            SetProportionallyCurrentMana(Golem.GetExtraStats().manaPool);
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
                OnCurrentHealthChanged(CurrentHealth);
            }
        }

        private void SetProportionallyCurrentStamina(float newMaxStamina)
        {
            var difference = MaxStamina - newMaxStamina;
            CurrentStamina -= difference;
            if (CurrentStamina > newMaxStamina)
            {
                CurrentStamina = newMaxStamina;
                OnCurrentStaminaChanged(CurrentStamina);
            }
        }

        private void SetProportionallyCurrentMana(float newMaxMana)
        {
            var difference = MaxMana - newMaxMana;
            CurrentMana -= difference;
            if (CurrentMana > newMaxMana)
            {
                CurrentMana = newMaxMana;
                OnCurrentManaChanged(CurrentMana);
            }
        }

        public void InitializeState(Golem golem, int group, Color colorGroup, string type, string spec)
        {
            Golem = golem;
            Group = group;
            ColorGroup = colorGroup;
            Type = type;
            Spec = spec;

            SetStartState();
        }

        private void SetStartState()
        {
            if (Golem == null) return;

            IsDead = false;
            Lvl = 1;
            Stats = Golem.GetExtraStats();
            BaseStats = Golem.GetBaseStats();
            MaxHealth = Stats.health;
            CurrentHealth = MaxHealth;
            MaxStamina = Stats.stamina;
            CurrentStamina = MaxStamina;
            MaxMana = Stats.manaPool;
            CurrentMana = MaxMana;

            CreateHealthBar();
            _isReady = true;
        }

        private void CreateHealthBar()
        {
            if (!isDynamicHealthBarCreate) return;
            _healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
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
            UpgradeSystem.LvlUp(this, 7);
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

        private void OnStatsChanged(GolemExtraStats stats)
        {
            StatsChanged?.Invoke(stats);
        }
    }
}