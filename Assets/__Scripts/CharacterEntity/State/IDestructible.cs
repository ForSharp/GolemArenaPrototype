using __Scripts.GameLoop;

namespace __Scripts.CharacterEntity.State
{
    public interface IDestructible
    {
        void TakeDamage(float damage, RoundStatistics statistics);
    }
}