using __Scripts.GameLoop;
using GameLoop;

namespace CharacterEntity.CharacterState
{
    public interface IDestructible
    {
        void TakeDamage(float damage, RoundStatistics statistics);
    }
}