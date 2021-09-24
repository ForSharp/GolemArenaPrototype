using System;
using GameLoop;
using GolemEntity;
using GolemEntity.BaseStats;
using GolemEntity.ExtraStats;
using UI;
using UnityEngine;

namespace Fight
{
    public sealed class GameCharacterState : MonoBehaviour
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
        public readonly RoundStatistics RoundStatistics = new RoundStatistics();
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
        }

        private void OnEnable()
        {
            EventContainer.GolemStatsChanged += UpdateStats;
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
        
            if (CurrentHealth <= 0 && !IsDead)
            {
                _lastEnemyAttacked.Kills += 1;
                _lastEnemyAttacked.RoundKills += 1;
                IsDead = true;
                EventContainer.OnGolemDied(_lastEnemyAttacked);
                return;
            }
        }

        private void UpdateStats(GameCharacterState state)
        {
            if (state != this) return;
            BaseStats = Golem.GetBaseStats();
            Stats = Golem.GetExtraStats();
            SetProportionallyCurrentHealth(Golem.GetExtraStats().Health);
            SetProportionallyCurrentStamina(Golem.GetExtraStats().Stamina);
            SetProportionallyCurrentMana(Golem.GetExtraStats().ManaPool);
            MaxHealth = Stats.Health;
            MaxStamina = Stats.Stamina;
            MaxMana = Stats.ManaPool;
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
            MaxHealth = Stats.Health;
            CurrentHealth = MaxHealth;
            MaxStamina = Stats.Stamina;
            CurrentStamina = MaxStamina;
            MaxMana = Stats.ManaPool;
            CurrentMana = MaxMana;
        
            CreateHealthBar();
            _isReady = true;
        }

        private void CreateHealthBar()
        {
            if (!isDynamicHealthBarCreate) return;
            _healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            _healthBar.GetComponent<UIHealthBar>().SetCharacterState(this);
        }

        public void TakeDamage(float damage, float defence = 0, RoundStatistics statistics = default)
        {
            CurrentHealth -= damage;
            if (statistics == null) return;
            statistics.Damage += damage;
            statistics.RoundDamage += damage;
            _lastEnemyAttacked = statistics;
            OnCurrentHealthChanged(CurrentHealth);
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
