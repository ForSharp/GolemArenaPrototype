using System;
using System.Collections;
using __Scripts.CharacterEntity.BaseStats;
using __Scripts.CharacterEntity.ExtraStats;
using __Scripts.GameLoop;
using __Scripts.UI;
using UnityEngine;

namespace __Scripts.CharacterEntity.State
{
    public abstract class CharacterState : MonoBehaviour, IDestructible
    {
        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private GameObject stateBarPrefab;
        protected GameObject healthBar;
        public DynamicStateBar StateBar { get; protected set; }
        public CharacterEffectsContainer characterEffectsContainer;

        public string Type { get; protected set; }
        public float MaxHealth { get; protected set; }
        public float CurrentHealth { get; protected set; }
        public float MaxMana { get; protected set; }
        public float CurrentMana { get; protected set; }
        public int Group { get; protected set; }
        public Color ColorGroup { get; protected set; }
        public int Lvl { get; set; }
        public bool IsDead { get; protected set; }
        public CharacterBaseStats BaseStats { get; protected set; }
        public CharacterExtraStats Stats { get; protected set; }
        public RoundStatistics RoundStatistics { get; protected set; }
        public Character Character { get; protected set; }
        public ConsumablesEater ConsumablesEater { get; protected set; }
        public RoundStatistics LastEnemyAttacked { get; protected set; }
        public event Action<CharacterExtraStats> StatsChanged;
        public event EventHandler AttackReceived;
        public event Action<float> CurrentHealthChanged;
        public event Action<float> CurrentManaChanged;
        public event Action<Sprite, float, bool, bool, string> StateEffectAdded;
        public event Action<string> StunCharacter;
        public event Action<string> EndStunCharacter;

        public void OnAttackReceived(object sender, AttackHitEventArgs args)
        {
            AttackReceived?.Invoke(sender, args);
        }

        protected void OnEnable()
        {
            EventContainer.CharacterStatsChanged += UpdateStats;
            StartCoroutine(RegenerateCurrents());
        }
        
        private void OnDestroy()
        {
            EventContainer.CharacterStatsChanged -= UpdateStats;
        }

        private void UpdateStats(CharacterState state)
        {
            if (state != this) return;
            BaseStats = Character.GetBaseStats();
            Stats = Character.GetExtraStats();
            SetProportionallyCurrentHealth(Character.GetExtraStats().health);
            SetProportionallyCurrentMana(Character.GetExtraStats().manaPool);
            MaxHealth = Stats.health;
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

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }

            OnCurrentHealthChanged(CurrentHealth);
        }

        private void SetProportionallyCurrentMana(float newMaxMana)
        {
            var difference = MaxMana - newMaxMana;
            CurrentMana -= difference;
            if (CurrentMana > newMaxMana)
            {
                CurrentMana = newMaxMana;
            }

            if (CurrentMana < 0)
            {
                CurrentMana = 0;
            }

            OnCurrentManaChanged(CurrentMana);
        }

        protected void SetStartState()
        {
            if (Character == null) return;

            IsDead = false;
            Lvl = 1;
            Stats = Character.GetExtraStats();
            BaseStats = Character.GetBaseStats();
            MaxHealth = Stats.health;
            CurrentHealth = MaxHealth;
            MaxMana = Stats.manaPool;
            CurrentMana = MaxMana;

            CreateHealthBar();
            CreateStateBar();
        }

        private void CreateHealthBar()
        {
            healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity,
                GameObject.Find("HealthBarContainer").transform);
            healthBar.GetComponent<DynamicHealthBar>().SetCharacterState(this);
        }

        private void CreateStateBar()
        {
            var bar = Instantiate(stateBarPrefab, transform.position, Quaternion.identity,
                GameObject.Find("HealthBarContainer").transform);
            StateBar = bar.GetComponent<DynamicStateBar>();
            StateBar.SetCharacterState(this);
        }

        public void TakeDamage(float damage, RoundStatistics statistics)
        {
            CurrentHealth -= damage;

            if (statistics != null)
            {
                LastEnemyAttacked = statistics;
            }

            if (CurrentHealth <= 0)
            {
                damage += CurrentHealth;
                PrepareToDie();
            }

            if (statistics != null)
            {
                statistics.Damage += damage;
                statistics.RoundDamage += damage;
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
                
                IsDead = true;

                if (LastEnemyAttacked != null)
                {
                    LastEnemyAttacked.Kills += 1;
                    LastEnemyAttacked.RoundKills += 1;
                    LastEnemyAttacked.Owner.LvlUpCharacter(1);
                    
                    EventContainer.OnCharacterDied(LastEnemyAttacked);
                }
                else
                {
                    EventContainer.OnCharacterDied(LastEnemyAttacked);
                }
            }
        }

        public void LvlUpCharacter(int amount)
        {
            LvlUpper.LvlUp(this, amount);
            characterEffectsContainer.PlayLvlUpEffect();
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

        protected void OnCurrentHealthChanged(float health)
        {
            CurrentHealthChanged?.Invoke(health);
        }

        protected void OnCurrentManaChanged(float mana)
        {
            CurrentManaChanged?.Invoke(mana);
        }

        private void OnStatsChanged(CharacterExtraStats stats)
        {
            StatsChanged?.Invoke(stats);
        }

        public void OnStateEffectAdded(Sprite effectImage, float effectDuration, bool effectIsPositive, bool canStack, string effectId)
        {
            StateEffectAdded?.Invoke(effectImage, effectDuration, effectIsPositive, canStack, effectId);
        }

        public void OnStunCharacter(string stunId)
        {
            StunCharacter?.Invoke(stunId);
        }
        
        public void OnEndStunCharacter(string stunId)
        {
            EndStunCharacter?.Invoke(stunId);
        }
    }
}