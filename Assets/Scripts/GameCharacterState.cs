using GolemEntity;
using GolemEntity.BaseStats;
using GolemEntity.ExtraStats;
using UnityEngine;

public class GameCharacterState : MonoBehaviour
{
    //TODO: создать производный от GameCharacterState класс, содержащий все, что есть сейчас, но применимо только к голему.
    //TODO: GameCharacterState сделать простым и универсальным, чтобы использовать для всех персонажей кроме големов.
    //TODO: change public to privates, create accessors, init by arguments in spawner mb
    
    [SerializeField] private bool isDynamicHealthBarCreate = true;
    [SerializeField] private GameObject healthBarPrefab;
    
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public int Group { get; set; } //set in spawner
    public int Lvl { get; private set; }
    public bool IsDead { get; private set; }
    public GolemBaseStats BaseStats { get; private set; }
    public GolemExtraStats Stats { get; private set; }
    public Golem Golem;
    public string Type { get; set; }
    public string Spec { get; set; }

    private void Start()
    {
        IsDead = false;
        Lvl = 1;
    }

    public void InitProps()
    {
        Stats = Golem.GetExtraStats();
        BaseStats = Golem.GetBaseStats();
        MaxHealth = Stats.Health;
        CurrentHealth = MaxHealth;
        CreateHealthBar();
    }
    
    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            IsDead = true;
            return;
        }
    }

    private void CreateHealthBar()
    {
        if (isDynamicHealthBarCreate)
        {
            GameObject healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar.GetComponent<UIHealthBar>().characterState = this;
        }
    }

    public void TakeDamage(float damage, int defence = 0)
    {
        CurrentHealth -= damage;
    }

    public void LvlUp()
    {
        
    }
    
    public void LvlDown()
    {
        
    }
    
    public void SpendStamina(float energy)
    {
        
    }
}
