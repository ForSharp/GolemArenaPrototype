using GameLoop;

namespace CharacterEntity.CharacterState
{
    public interface IDestructible
    {
        void TakeDamage(float damage, RoundStatistics statistics);
    }
}