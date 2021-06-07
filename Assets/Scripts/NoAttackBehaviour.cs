using UnityEngine;

public class NoAttackBehaviour : IAttackable
{
    public void Attack(float damage = default, float delayBetweenHits = default, Vector3 attackerPosition = default)
    {
        //do nothing
    }
}