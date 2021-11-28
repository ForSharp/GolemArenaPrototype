namespace CharacterEntity.CharacterState
{
    public interface IBurnable
    {
        void Burn(float duration, float periodicDamage, int intensity);
    }
}