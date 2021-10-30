using System;
using GolemEntity.BaseStats;

namespace GolemEntity.ExtraStats.Effects
{
    public class ExtraStatsChanger : ExtraStatsDecorator
    {
        private ExtraStatsParameter[] _extraStatsParameters;

        public ExtraStatsChanger(IExtraStatsProvider wrappedEntity, GolemBaseStats baseStats, 
            params ExtraStatsParameter[] extraStatsParameters) : base(wrappedEntity, baseStats)
        {
            _extraStatsParameters = extraStatsParameters;
        }

        protected override GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats)
        {
            foreach (var t in _extraStatsParameters)
            {
                if (t.IsFlat)
                {
                    SwitchExtraStatsFlat(t.StatsType, t.ChangingValue);
                }
                else
                {
                    SwitchExtraStatsMultiplier(t.StatsType, t.ChangingValue);
                }
            }

            return _wrappedEntity.GetExtraStats();
        }

        private void SwitchExtraStatsFlat(ExtraStats type, float changingValue)
        {
            switch (type)
            {
                case ExtraStats.AttackRange:
                    _wrappedEntity.GetExtraStats().attackRange += changingValue;
                    break;
                case ExtraStats.AttackSpeed:
                    _wrappedEntity.GetExtraStats().attackSpeed += changingValue;
                    break;
                case ExtraStats.AvoidChance:
                    _wrappedEntity.GetExtraStats().avoidChance += changingValue;
                    break;
                case ExtraStats.DamagePerHeat:
                    _wrappedEntity.GetExtraStats().damagePerHeat += changingValue;
                    break;
                case ExtraStats.Defence:
                    _wrappedEntity.GetExtraStats().defence += changingValue;
                    break;
                case ExtraStats.DodgeChance:
                    _wrappedEntity.GetExtraStats().dodgeChance += changingValue;
                    break;
                case ExtraStats.Health:
                    _wrappedEntity.GetExtraStats().health += changingValue;
                    break;
                case ExtraStats.HitAccuracy:
                    _wrappedEntity.GetExtraStats().hitAccuracy += changingValue;
                    break;
                case ExtraStats.MagicAccuracy:
                    _wrappedEntity.GetExtraStats().magicAccuracy += changingValue;
                    break;
                case ExtraStats.MagicPower:
                    _wrappedEntity.GetExtraStats().magicPower += changingValue;
                    break;
                case ExtraStats.MagicResistance:
                    _wrappedEntity.GetExtraStats().magicResistance += changingValue;
                    break;
                case ExtraStats.ManaPool:
                    _wrappedEntity.GetExtraStats().manaPool += changingValue;
                    break;
                case ExtraStats.MoveSpeed:
                    _wrappedEntity.GetExtraStats().moveSpeed += changingValue;
                    break;
                case ExtraStats.RegenerationHealth:
                    _wrappedEntity.GetExtraStats().regenerationHealth += changingValue;
                    break;
                case ExtraStats.RegenerationMana:
                    _wrappedEntity.GetExtraStats().regenerationMana += changingValue;
                    break;
                case ExtraStats.RegenerationStamina:
                    _wrappedEntity.GetExtraStats().regenerationStamina += changingValue;
                    break;
                case ExtraStats.Stamina:
                    _wrappedEntity.GetExtraStats().stamina += changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        private void SwitchExtraStatsMultiplier(ExtraStats type, float changingValue)
        {
            switch (type)
            {
                case ExtraStats.AttackRange:
                    _wrappedEntity.GetExtraStats().attackRange *= changingValue;
                    break;
                case ExtraStats.AttackSpeed:
                    _wrappedEntity.GetExtraStats().attackSpeed *= changingValue;
                    break;
                case ExtraStats.AvoidChance:
                    _wrappedEntity.GetExtraStats().avoidChance *= changingValue;
                    break;
                case ExtraStats.DamagePerHeat:
                    _wrappedEntity.GetExtraStats().damagePerHeat *= changingValue;
                    break;
                case ExtraStats.Defence:
                    _wrappedEntity.GetExtraStats().defence *= changingValue;
                    break;
                case ExtraStats.DodgeChance:
                    _wrappedEntity.GetExtraStats().dodgeChance *= changingValue;
                    break;
                case ExtraStats.Health:
                    _wrappedEntity.GetExtraStats().health *= changingValue;
                    break;
                case ExtraStats.HitAccuracy:
                    _wrappedEntity.GetExtraStats().hitAccuracy *= changingValue;
                    break;
                case ExtraStats.MagicAccuracy:
                    _wrappedEntity.GetExtraStats().magicAccuracy *= changingValue;
                    break;
                case ExtraStats.MagicPower:
                    _wrappedEntity.GetExtraStats().magicPower *= changingValue;
                    break;
                case ExtraStats.MagicResistance:
                    _wrappedEntity.GetExtraStats().magicResistance *= changingValue;
                    break;
                case ExtraStats.ManaPool:
                    _wrappedEntity.GetExtraStats().manaPool *= changingValue;
                    break;
                case ExtraStats.MoveSpeed:
                    _wrappedEntity.GetExtraStats().moveSpeed *= changingValue;
                    break;
                case ExtraStats.RegenerationHealth:
                    _wrappedEntity.GetExtraStats().regenerationHealth *= changingValue;
                    break;
                case ExtraStats.RegenerationMana:
                    _wrappedEntity.GetExtraStats().regenerationMana *= changingValue;
                    break;
                case ExtraStats.RegenerationStamina:
                    _wrappedEntity.GetExtraStats().regenerationStamina *= changingValue;
                    break;
                case ExtraStats.Stamina:
                    _wrappedEntity.GetExtraStats().stamina *= changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}