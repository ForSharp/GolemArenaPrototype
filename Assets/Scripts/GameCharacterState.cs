using GolemEntity;
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
    public int Group { get; set; } //set in spawner
    public int Lvl { get; set; } //set in user input
    public bool IsDead { get; private set; }
    public GolemExtraStats Stats { get; private set; }
    public Golem Golem;

    private void Start()
    {
        IsDead = false;
    }

    public void InitProps()
    {
        Stats = Golem.GetExtraStats();
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
