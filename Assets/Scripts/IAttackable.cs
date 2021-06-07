using UnityEngine;

public interface IAttackable
{
    void Attack(float damage, float delayBetweenHits, Vector3 attackerPosition);
}