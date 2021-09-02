using System;
using GameLoop;

namespace Fight
{
    public class AttackHitEventArgs : EventArgs
    {
        public readonly float DamagePerHit;
        public readonly float HitAccuracy;
        public readonly RoundStatistics Statistics;
        public readonly float AttackerRotationY;

        public AttackHitEventArgs(float damagePerHit, float hitAccuracy, RoundStatistics statistics, float attackerRotationY)
        {
            DamagePerHit = damagePerHit;
            HitAccuracy = hitAccuracy;
            Statistics = statistics;
            AttackerRotationY = attackerRotationY;
        }
    }
}