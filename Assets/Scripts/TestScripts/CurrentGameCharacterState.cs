using System.Collections;
using GolemEntity;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class CurrentGameCharacterState : MonoBehaviour
{
    public int maxHealth = 100; //Golem.GetExtraStats.Health
    public int currentHealth = 100;

    public int sliderPlacementHeight = 80;
    [HideInInspector] public bool isDead = false;

    [SerializeField] private bool idDynamicHealthBarCreate = true;
    [SerializeField] private GameObject healthBarPrefab;

    public int group = 0; //team of a golem
    
    private void Start()
    {
        if (idDynamicHealthBarCreate)
        {
            //Instantiate(healthBar.transform, canvas.transform);
            GameObject healthBar = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar.GetComponent<UIHealthBar>().characterState = this;
        }
    }

    private void Update()
    {
        if (isDead)
            return;

        if (currentHealth <= 0)
        {
            KillGameCharacter();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
    }

    private void KillGameCharacter()
    {
        isDead = true;
        if (GetComponent<Animator>())
        {
            Animator anim = GetComponent<Animator>();
            AnimationChanger.SetGolemDie(anim);
        }

        StartCoroutine(DestroyWithDelay());
    }
    
    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}