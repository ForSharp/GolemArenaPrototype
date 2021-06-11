using GolemEntity;
using GolemEntity.ExtraStats;
using UnityEngine;

public class GameCharacterState : MonoBehaviour
{
    [SerializeField] private bool isDynamicHealthBarCreate = true;
    [SerializeField] private GameObject healthBarPrefab;
    
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public int Group { get; set; }
    
    public bool IsDead { get; private set; }
    public GolemExtraStats Stats { get; private set; }
    public Golem Golem;

    private void Start()
    {
        Stats = Golem.GetExtraStats();
        MaxHealth = Stats.Health;
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (IsDead)
            return;
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
        
    }

    public void SpendStamina(float energy)
    {
        
    }
}
