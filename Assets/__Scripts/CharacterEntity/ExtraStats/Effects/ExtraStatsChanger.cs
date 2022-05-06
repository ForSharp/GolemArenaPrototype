using System;
using __Scripts.CharacterEntity.BaseStats;

namespace __Scripts.CharacterEntity.ExtraStats.Effects
{
    public class ExtraStatsChanger : ExtraStatsDecorator
    {
        private readonly ExtraStatsParameter[] _extraStatsParameters;

        public ExtraStatsChanger(IExtraStatsProvider wrappedEntity, CharacterBaseStats baseStats, 
            params ExtraStatsParameter[] extraStatsParameters) : base(wrappedEntity, baseStats)
        {
            _extraStatsParameters = extraStatsParameters;
        }

        protected override CharacterExtraStats GetExtraStatsInternal(CharacterBaseStats baseStats)
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

        private CharacterExtraStats GetExtraStatsFlat(ExtraStats type, float changingValue)
        {
            var tempStats = new CharacterExtraStats();
            
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return tempStats;
        }
        
        private CharacterExtraStats GetExtraStatsMultiplier(CharacterExtraStats characterExtraStats, ExtraStats type,
            float changingValue)
        {
            switch (type)
            {
                case ExtraStats.AttackRange:
                    characterExtraStats.attackRange *= changingValue;
                    break;
                case ExtraStats.AttackSpeed:
                    characterExtraStats.attackSpeed *= changingValue;
                    break;
                case ExtraStats.AvoidChance:
                    characterExtraStats.avoidChance *= changingValue;
                    break;
                case ExtraStats.DamagePerHeat:
                    characterExtraStats.damagePerHeat *= changingValue;
                    break;
                case ExtraStats.Defence:
                    characterExtraStats.defence *= changingValue;
                    break;
                case ExtraStats.DodgeChance:
                    characterExtraStats.dodgeChance *= changingValue;
                    break;
                case ExtraStats.Health:
                    characterExtraStats.health *= changingValue;
                    break;
                case ExtraStats.HitAccuracy:
                    characterExtraStats.hitAccuracy *= changingValue;
                    break;
                case ExtraStats.MagicAccuracy:
                    characterExtraStats.magicAccuracy *= changingValue;
                    break;
                case ExtraStats.MagicPower:
                    characterExtraStats.magicPower *= changingValue;
                    break;
                case ExtraStats.MagicResistance:
                    characterExtraStats.magicResistance *= changingValue;
                    break;
                case ExtraStats.ManaPool:
                    characterExtraStats.manaPool *= changingValue;
                    break;
                case ExtraStats.MoveSpeed:
                    characterExtraStats.moveSpeed *= changingValue;
                    break;
                case ExtraStats.RegenerationHealth:
                    characterExtraStats.regenerationHealth *= changingValue;
                    break;
                case ExtraStats.RegenerationMana:
                    characterExtraStats.regenerationMana *= changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return characterExtraStats;
        }
    }
}