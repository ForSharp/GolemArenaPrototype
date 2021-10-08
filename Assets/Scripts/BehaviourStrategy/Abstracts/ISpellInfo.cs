namespace BehaviourStrategy.Abstracts
{
    public interface ISpellInfo
    {
        SpellType SpellType { get; }
        float ManaCost { get; }
        float Cooldown { get; }
        int SpellLvl { get; }
        float Damage { get; }
        float PeriodicDamage { get; }
        int EffectDuration { get; }
        float Hill { get; }
        
    }
}