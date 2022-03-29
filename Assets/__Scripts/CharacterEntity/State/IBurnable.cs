namespace __Scripts.CharacterEntity.State
{
    public interface IBurnable
    {
        void Burn(float duration, float periodicDamage, int intensity);
    }
}