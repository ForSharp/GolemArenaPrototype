using System;
using System.Collections;
using CharacterEntity.BaseStats;
using CharacterEntity.CharacterState;
using CharacterEntity.ExtraStats;
using GameLoop;
using Inventory;
using SpellSystem;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace CharacterEntity.State
{
    public sealed class CharacterState : MonoBehaviour, IDestructible
    {
        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private GameObject stateBarPrefab;
        private GameObject _healthBar;
        public DynamicStateBar StateBar { get; private set; }
        public CharacterEffectsContainer characterEffectsContainer;

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
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
        public SpellPanelHelper SpellPanelHelper { get; private set; }

        private RoundStatistics _lastEnemyAttacked;
        public RoundStatistics roundStatistics;
        public event EventHandler AttackReceived;
        public event Action<float> CurrentHealthChanged;
        public event Action<float> CurrentManaChanged;
        public event Action<CharacterExtraStats> StatsChanged;
        public event Action<Sprite, float, bool, string> StateEffectAdded;
        public event Action StartSpellCast;
        public event Action CancelSpellCast;

        public void OnAttackReceived(object sender, AttackHitEventArgs args)
        {
            AttackReceived?.Invoke(sender, args);
        }

        private void Start()
        {
            SoundsController = GetComponent<SoundsController>();
            roundStatistics = new RoundStatistics(this);
            InventoryHelper = GetComponent<InventoryHelper>();
            SpellPanelHelper = GetComponent<SpellPanelHelper>();
            SpellManager = new SpellManager(GetComponent<Animator>(), this, GetComponent<SpellContainer>());
            var unused = new ExtraStatsEditorWithItems(this);
            var dummy = new ConsumablesEater(this);
        }

        private void OnEnable()
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
            MaxMana = Stats.manaPool;
            CurrentMana = MaxMana;

            CreateHealthBar();
            CreateStateBar();
        }

        private void CreateHealthBar()
        {
            _healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity,
                GameObject.Find("HealthBarContainer").transform);
            _healthBar.GetComponent<DynamicHealthBar>().SetCharacterState(this);
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
                _lastEnemyAttacked = statistics;
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
                _lastEnemyAttacked.Kills += 1;
                _lastEnemyAttacked.RoundKills += 1;
                IsDead = true;
                EventContainer.OnCharacterDied(_lastEnemyAttacked);
                _lastEnemyAttacked.Owner.LvlUpCharacter(1);
            }
        }

        public void PrepareAfterNewRound()
        {
            IsDead = false;
            LvlUpCharacter(7);
            HealAllParameters();
            ShowHealthBar();
            NullRoundStatistics();
        }

        public void LvlUpCharacter(int amount)
        {
            LvlUpper.LvlUp(this, amount);
            characterEffectsContainer.PlayLvlUpEffect();
        }

        private void NullRoundStatistics()
        {
            roundStatistics.RoundDamage = 0;
            roundStatistics.RoundKills = 0;
            roundStatistics.WinLastRound = false;
            roundStatistics.RoundRate = 0;
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
            _healthBar.gameObject.SetActive(true);
            StateBar.gameObject.SetActive(true);
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

        private void OnCurrentHealthChanged(float health)
        {
            CurrentHealthChanged?.Invoke(health);
        }

        private void OnCurrentManaChanged(float mana)
        {
            CurrentManaChanged?.Invoke(mana);
        }

        private void OnStatsChanged(CharacterExtraStats stats)
        {
            StatsChanged?.Invoke(stats);
        }

        public void OnStateEffectAdded(Sprite effectImage, float effectDuration, bool effectIsPositive, string effectId)
        {
            StateEffectAdded?.Invoke(effectImage, effectDuration, effectIsPositive, effectId);
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