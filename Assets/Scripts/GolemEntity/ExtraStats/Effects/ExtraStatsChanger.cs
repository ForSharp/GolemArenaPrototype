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
            var stats = _wrappedEntity.GetExtraStats();
            
            foreach (var t in _extraStatsParameters)
            {
                if (t.IsFlat)
                {
                    stats += GetExtraStatsFlat(t.StatsType, t.ChangingValue);
                }
                else
                {
                    stats = GetExtraStatsMultiplier(stats, t.StatsType, t.ChangingValue);
                }
            }

            return stats;
        }

        private GolemExtraStats GetExtraStatsFlat(ExtraStats type, float changingValue)
        {
            var tempStats = new GolemExtraStats();
            
            switch (type)
            {
                case ExtraStats.AttackRange:
                    tempStats.attackRange = changingValue;
                    break;
                case ExtraStats.AttackSpeed:
                    tempStats.attackSpeed = changingValue;
                    break;
                case ExtraStats.AvoidChance:
                    tempStats.avoidChance = changingValue;
                    break;
                case ExtraStats.DamagePerHeat:
                    tempStats.damagePerHeat = changingValue;
                    break;
                case ExtraStats.Defence:
                    tempStats.defence = changingValue;
                    break;
                case ExtraStats.DodgeChance:
                    tempStats.dodgeChance = changingValue;
                    break;
                case ExtraStats.Health:
                    tempStats.health = changingValue;
                    break;
                case ExtraStats.HitAccuracy:
                    tempStats.hitAccuracy = changingValue;
                    break;
                case ExtraStats.MagicAccuracy:
                    tempStats.magicAccuracy = changingValue;
                    break;
                case ExtraStats.MagicPower:
                    tempStats.magicPower = changingValue;
                    break;
                case ExtraStats.MagicResistance:
                    tempStats.magicResistance = changingValue;
                    break;
                case ExtraStats.ManaPool:
                    tempStats.manaPool = changingValue;
                    break;
                case ExtraStats.MoveSpeed:
                    tempStats.moveSpeed = changingValue;
                    break;
                case ExtraStats.RegenerationHealth:
                    tempStats.regenerationHealth = changingValue;
                    break;
                case ExtraStats.RegenerationMana:
                    tempStats.regenerationMana = changingValue;
                    break;
                case ExtraStats.RegenerationStamina:
                    tempStats.regenerationStamina = changingValue;
                    break;
                case ExtraStats.Stamina:
                    tempStats.stamina = changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return tempStats;

            // switch (type)
            // {
            //     case ExtraStats.AttackRange:
            //         _wrappedEntity.GetExtraStats().attackRange += changingValue;
            //         break;
            //     case ExtraStats.AttackSpeed:
            //         _wrappedEntity.GetExtraStats().attackSpeed += changingValue;
            //         break;
            //     case ExtraStats.AvoidChance:
            //         _wrappedEntity.GetExtraStats().avoidChance += changingValue;
            //         break;
            //     case ExtraStats.DamagePerHeat:
            //         _wrappedEntity.GetExtraStats().damagePerHeat += changingValue;
            //         break;
            //     case ExtraStats.Defence:
            //         _wrappedEntity.GetExtraStats().defence += changingValue;
            //         break;
            //     case ExtraStats.DodgeChance:
            //         _wrappedEntity.GetExtraStats().dodgeChance += changingValue;
            //         break;
            //     case ExtraStats.Health:
            //         _wrappedEntity.GetExtraStats().health += changingValue;
            //         break;
            //     case ExtraStats.HitAccuracy:
            //         _wrappedEntity.GetExtraStats().hitAccuracy += changingValue;
            //         break;
            //     case ExtraStats.MagicAccuracy:
            //         _wrappedEntity.GetExtraStats().magicAccuracy += changingValue;
            //         break;
            //     case ExtraStats.MagicPower:
            //         _wrappedEntity.GetExtraStats().magicPower += changingValue;
            //         break;
            //     case ExtraStats.MagicResistance:
            //         _wrappedEntity.GetExtraStats().magicResistance += changingValue;
            //         break;
            //     case ExtraStats.ManaPool:
            //         _wrappedEntity.GetExtraStats().manaPool += changingValue;
            //         break;
            //     case ExtraStats.MoveSpeed:
            //         _wrappedEntity.GetExtraStats().moveSpeed += changingValue;
            //         break;
            //     case ExtraStats.RegenerationHealth:
            //         _wrappedEntity.GetExtraStats().regenerationHealth += changingValue;
            //         break;
            //     case ExtraStats.RegenerationMana:
            //         _wrappedEntity.GetExtraStats().regenerationMana += changingValue;
            //         break;
            //     case ExtraStats.RegenerationStamina:
            //         _wrappedEntity.GetExtraStats().regenerationStamina += changingValue;
            //         break;
            //     case ExtraStats.Stamina:
            //         _wrappedEntity.GetExtraStats().stamina += changingValue;
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException(nameof(type), type, null);
            // }
        }
        
        private GolemExtraStats GetExtraStatsMultiplier(GolemExtraStats golemExtraStats, ExtraStats type,
            float changingValue)
        {
            switch (type)
            {
                case ExtraStats.AttackRange:
                    golemExtraStats.attackRange *= changingValue;
                    break;
                case ExtraStats.AttackSpeed:
                    golemExtraStats.attackSpeed *= changingValue;
                    break;
                case ExtraStats.AvoidChance:
                    golemExtraStats.avoidChance *= changingValue;
                    break;
                case ExtraStats.DamagePerHeat:
                    golemExtraStats.damagePerHeat *= changingValue;
                    break;
                case ExtraStats.Defence:
                    golemExtraStats.defence *= changingValue;
                    break;
                case ExtraStats.DodgeChance:
                    golemExtraStats.dodgeChance *= changingValue;
                    break;
                case ExtraStats.Health:
                    golemExtraStats.health *= changingValue;
                    break;
                case ExtraStats.HitAccuracy:
                    golemExtraStats.hitAccuracy *= changingValue;
                    break;
                case ExtraStats.MagicAccuracy:
                    golemExtraStats.magicAccuracy *= changingValue;
                    break;
                case ExtraStats.MagicPower:
                    golemExtraStats.magicPower *= changingValue;
                    break;
                case ExtraStats.MagicResistance:
                    golemExtraStats.magicResistance *= changingValue;
                    break;
                case ExtraStats.ManaPool:
                    golemExtraStats.manaPool *= changingValue;
                    break;
                case ExtraStats.MoveSpeed:
                    golemExtraStats.moveSpeed *= changingValue;
                    break;
                case ExtraStats.RegenerationHealth:
                    golemExtraStats.regenerationHealth *= changingValue;
                    break;
                case ExtraStats.RegenerationMana:
                    golemExtraStats.regenerationMana *= changingValue;
                    break;
                case ExtraStats.RegenerationStamina:
                    golemExtraStats.regenerationStamina *= changingValue;
                    break;
                case ExtraStats.Stamina:
                    golemExtraStats.stamina *= changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return golemExtraStats;
        }
    }
}