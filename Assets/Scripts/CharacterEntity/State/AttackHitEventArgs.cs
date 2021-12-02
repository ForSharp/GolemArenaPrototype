using System;
using GameLoop;

namespace CharacterEntity.CharacterState
{
    public class AttackHitEventArgs : EventArgs
    {
        public float DamagePerHit;
        public readonly float HitAccuracy;
        public readonly RoundStatistics Statistics;
        public readonly float AttackerRotationY;
        public readonly string AttackerName;

        public AttackHitEventArgs(float damagePerHit, float hitAccuracy, RoundStatistics statistics, float attackerRotationY, string attackerName)
        {
            DamagePerHit = damagePerHit;
            HitAccuracy = hitAccuracy;
            Statistics = statistics;
            AttackerRotationY = attackerRotationY;
            AttackerName = attackerName;
        }
    }
}