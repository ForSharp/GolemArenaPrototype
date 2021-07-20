using System;
using GameLoop;

namespace Fight
{
    public class AttackHitEventArgs : EventArgs
    {
        public float damagePerHit;
        public float hitAccuracy;
        public RoundStatistics statistics;

        public AttackHitEventArgs(float damagePerHit, float hitAccuracy, RoundStatistics statistics)
        {
            this.damagePerHit = damagePerHit;
            this.hitAccuracy = hitAccuracy;
            this.statistics = statistics;
        }
    }
}