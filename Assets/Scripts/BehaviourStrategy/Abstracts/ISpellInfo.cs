namespace BehaviourStrategy.Abstracts
{
    public interface ISpellInfo
    {
        SpellType SpellType { get; }
        float ManaCost { get; }
        float Cooldown { get; }
        int SpellLvl { get; }
    }
}