using System;
using GolemEntity;
using GolemEntity.BaseStats;
using GolemEntity.ExtraStats;
using UnityEngine;

public class GameCharacterState : MonoBehaviour
{
    //TODO: создать производный от GameCharacterState класс, содержащий все, что есть сейчас, но применимо только к голему.
    //TODO: GameCharacterState сделать простым и универсальным, чтобы использовать для всех персонажей кроме големов.
    
    [SerializeField] private bool isDynamicHealthBarCreate = true;
    [SerializeField] private GameObject healthBarPrefab;
    
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public int Group { get; private set; } 
    public Color ColorGroup { get; private set; } 
    public int Lvl { get; private set; }
    public bool IsDead { get; private set; }
    public GolemBaseStats BaseStats { get; private set; }
    public GolemExtraStats Stats { get; private set; }
    public string Type { get; private set; }
    public string Spec { get; private set; }
    private Golem Golem { get; set; }
    private bool _isReady;
    public RoundStatistics LastEnemyAttacked;
    public RoundStatistics RoundStatistics = new RoundStatistics() {Damage = 0, Kills = 0, Wins = 0};

    private void Start()
    {
        EventContainer.GolemStatsChanged += UpdateStats;
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
        float difference = MaxHealth - newMaxHealth;
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
        if (isDynamicHealthBarCreate)
        {
            GameObject healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar.GetComponent<UIHealthBar>().characterState = this;
        }
    }

    public void TakeDamage(float damage, int defence = 0, RoundStatistics statistics = default)
    {
        CurrentHealth -= damage;
        statistics.Damage += damage;
        LastEnemyAttacked = statistics;
    }

    public void LvlUp()
    {
        Golem.ChangeBaseStatsProportionally(10);
        EventContainer.OnGolemStatsChanged();
        Lvl++;
    }
    
    public void LvlDown()
    {
        if (Lvl > 1)
        {
            Golem.ChangeBaseStatsProportionally(-10);
            EventContainer.OnGolemStatsChanged();
            Lvl--;
        }
    }
    
    public void SpendStamina(float energy)
    {
        
    }
}
