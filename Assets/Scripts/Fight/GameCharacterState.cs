using System;
using GameLoop;
using GolemEntity;
using GolemEntity.BaseStats;
using GolemEntity.ExtraStats;
using UI;
using UnityEngine;
using UserInterface;

namespace Fight
{
    public sealed class GameCharacterState : MonoBehaviour
    {
        [SerializeField] private bool isDynamicHealthBarCreate = true;
        [SerializeField] private GameObject healthBarPrefab;
        
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
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
        public RoundStatistics LastEnemyAttacked;
        public readonly RoundStatistics RoundStatistics = new RoundStatistics();
        public event EventHandler AttackReceived;

        public void OnAttackReceived(AttackHitEventArgs args)
        {
            AttackReceived?.Invoke(this, args);
        }
        
        private void Start()
        {
            EventContainer.GolemStatsChanged += UpdateStats;
            SoundsController = GetComponent<SoundsController>();
        }

        private void Update()
        {
            if (!_isReady)
            {
                return;
            }
        
            if (CurrentHealth <= 0)
            {
                IsDead = true;
                EventContainer.OnGolemDied();
                return;
            }
        }

        private void UpdateStats()
        {
            BaseStats = Golem.GetBaseStats();
            Stats = Golem.GetExtraStats();
            SetProportionallyCurrentHealth(Golem.GetExtraStats().Health);
            MaxHealth = Stats.Health;
        }

        private void SetProportionallyCurrentHealth(float newMaxHealth)
        {
            var difference = MaxHealth - newMaxHealth;
            CurrentHealth -= difference;
            if (CurrentHealth > newMaxHealth)
            {
                CurrentHealth = newMaxHealth;
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
        
            CreateHealthBar();
            _isReady = true;
        }

        private void CreateHealthBar()
        {
            if (!isDynamicHealthBarCreate) return;
            var healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar.GetComponent<UIHealthBar>().characterState = this;
        }

        public void TakeDamage(float damage, float defence = 0, RoundStatistics statistics = default)
        {
            CurrentHealth -= damage;
            if (statistics == null) return;
            statistics.Damage += damage;
            LastEnemyAttacked = statistics;
        }

        public void SpendStamina(float energy)
        {
        
        }

        
    }
}
