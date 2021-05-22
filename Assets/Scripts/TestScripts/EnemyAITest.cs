using System.Collections;
using System.Collections.Generic;
using GolemEntity;
using UnityEngine;

public class EnemyAITest : MonoBehaviour
{
    public int damage = 1;
    public float speed;

    private Animator animator;
    private Transform target;
    
    private int healthPoint = 100;
    public int HealthPoint
    {
        get { return healthPoint; }
        set
        {
            if (value <= 0) { healthPoint = 0; Death(); }
            else healthPoint = value;
        }
    }
    
    private bool inAttack;
    public bool InAttack
    {
        get { return inAttack; }
        set
        {
            inAttack = value;
            if (value) AnimationChanger.SetGolemRightHit(animator); //реализована 1 анимация из 3 доступных на данный момент
        }
    }
    
    void Death()
    {
        StopAllCoroutines();
        int random =Mathf.RoundToInt(Random.Range(0.51F, 3.49F));
        
        AnimationChanger.SetGolemDie(animator);
        animator.applyRootMotion = true;
        speed = 0.0F;
        target = null;
        inAttack = false;
        Destroy(gameObject, 8.0F);

        DestroyImmediate(GetComponent<Rigidbody>());
        DestroyImmediate(GetComponent<Collider>());
    }
    
    void Move()
    {
        if (!target) return;
        
        if (Vector3.Distance(transform.position, target.position) > 1.12F)
        {
            Vector3 goal = target.position;
            goal.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            //State = defaultState;
        }
        else
        {
            if (!inAttack) StartCoroutine(Attack());
        }
    }
    
    IEnumerator Attack()
    {
        InAttack = true;
        yield return new WaitForSeconds(0.4F); //задержка между ударами. должна высчитываться на основе текущей скорости атаки

        Vector3 spherePosition = transform.position + transform.forward * 0.45F; //радиус зоны поражения
        spherePosition.y += 0.75F; //высота. 

        Collider[] colliders = Physics.OverlapSphere(spherePosition, 0.6F); 

        foreach (Collider item in colliders)
        {
            // if (item.GetComponent<PlayerHelper>()) //здесь проверяем, коснулись ли игрока
            // {
            //     Debug.Log("SHOOT!");
            //     PlayerHelper enemy = item.GetComponent<PlayerHelper>(); //обращаемся к классу игрока, где есть жизни
            //     enemy.HealthPoint -= damage;//уменьшаем отсюда хп
            // }
        }

        yield return new WaitForSeconds(0.25F);
        InAttack = false;
    }
}
