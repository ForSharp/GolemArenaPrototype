using System;
using GolemEntity.BaseStats;

namespace GolemEntity.ExtraStats.Effects
{
    public class ExtraStatsFlatAddition : ExtraStatsDecorator
    {
        private ExtraStatsParameter[] _extraStatsParameters;

        public ExtraStatsFlatAddition(IExtraStatsProvider wrappedEntity, GolemBaseStats baseStats, 
            params ExtraStatsParameter[] extraStatsParameters) : base(wrappedEntity, baseStats)
        {
            _extraStatsParameters = extraStatsParameters;
        }

        protected override GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats)
        {
            foreach (var t in _extraStatsParameters)
            {
                SwitchExtraStats(t.StatsType, t.ChangingValue);
            }

            return _wrappedEntity.GetExtraStats();
        }

        private void SwitchExtraStats(ExtraStats type, float changingValue)
        {
            switch (type)
            {
                case ExtraStats.AttackRange:
                    _wrappedEntity.GetExtraStats().AttackRange += changingValue;
                    break;
                case ExtraStats.AttackSpeed:
                    _wrappedEntity.GetExtraStats().AttackSpeed += changingValue;
                    break;
                case ExtraStats.AvoidChance:
                    _wrappedEntity.GetExtraStats().AvoidChance += changingValue;
                    break;
                case ExtraStats.DamagePerHeat:
                    _wrappedEntity.GetExtraStats().DamagePerHeat += changingValue;
                    break;
                case ExtraStats.Defence:
                    _wrappedEntity.GetExtraStats().Defence += changingValue;
                    break;
                case ExtraStats.DodgeChance:
                    _wrappedEntity.GetExtraStats().DodgeChance += changingValue;
                    break;
                case ExtraStats.Health:
                    _wrappedEntity.GetExtraStats().Health += changingValue;
                    break;
                case ExtraStats.HitAccuracy:
                    _wrappedEntity.GetExtraStats().HitAccuracy += changingValue;
                    break;
                case ExtraStats.MagicAccuracy:
                    _wrappedEntity.GetExtraStats().MagicAccuracy += changingValue;
                    break;
                case ExtraStats.MagicPower:
                    _wrappedEntity.GetExtraStats().MagicPower += changingValue;
                    break;
                case ExtraStats.MagicResistance:
                    _wrappedEntity.GetExtraStats().MagicResistance += changingValue;
                    break;
                case ExtraStats.ManaPool:
                    _wrappedEntity.GetExtraStats().ManaPool += changingValue;
                    break;
                case ExtraStats.MoveSpeed:
                    _wrappedEntity.GetExtraStats().MoveSpeed += changingValue;
                    break;
                case ExtraStats.RegenerationHealth:
                    _wrappedEntity.GetExtraStats().RegenerationHealth += changingValue;
                    break;
                case ExtraStats.RegenerationMana:
                    _wrappedEntity.GetExtraStats().RegenerationMana += changingValue;
                    break;
                case ExtraStats.RegenerationStamina:
                    _wrappedEntity.GetExtraStats().RegenerationStamina += changingValue;
                    break;
                case ExtraStats.Stamina:
                    _wrappedEntity.GetExtraStats().Stamina += changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}