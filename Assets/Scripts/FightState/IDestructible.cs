using GameLoop;

namespace FightState
{
    public interface IDestructible
    {
        void TakeDamage(float damage, RoundStatistics statistics);
    }
}