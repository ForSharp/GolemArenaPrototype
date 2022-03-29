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

        private CharacterExtraStats GetExtraStatsFlat(__Scripts.CharacterEntity.ExtraStats.ExtraStats type, float changingValue)
        {
            var tempStats = new CharacterExtraStats();
            
            switch (type)
            {
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.AttackRange:
                    tempStats.attackRange = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.AttackSpeed:
                    tempStats.attackSpeed = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.AvoidChance:
                    tempStats.avoidChance = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.DamagePerHeat:
                    tempStats.damagePerHeat = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.Defence:
                    tempStats.defence = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.DodgeChance:
                    tempStats.dodgeChance = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.Health:
                    tempStats.health = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.HitAccuracy:
                    tempStats.hitAccuracy = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MagicAccuracy:
                    tempStats.magicAccuracy = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MagicPower:
                    tempStats.magicPower = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MagicResistance:
                    tempStats.magicResistance = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.ManaPool:
                    tempStats.manaPool = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MoveSpeed:
                    tempStats.moveSpeed = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.RegenerationHealth:
                    tempStats.regenerationHealth = changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.RegenerationMana:
                    tempStats.regenerationMana = changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return tempStats;
        }
        
        private CharacterExtraStats GetExtraStatsMultiplier(CharacterExtraStats characterExtraStats, __Scripts.CharacterEntity.ExtraStats.ExtraStats type,
            float changingValue)
        {
            switch (type)
            {
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.AttackRange:
                    characterExtraStats.attackRange *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.AttackSpeed:
                    characterExtraStats.attackSpeed *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.AvoidChance:
                    characterExtraStats.avoidChance *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.DamagePerHeat:
                    characterExtraStats.damagePerHeat *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.Defence:
                    characterExtraStats.defence *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.DodgeChance:
                    characterExtraStats.dodgeChance *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.Health:
                    characterExtraStats.health *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.HitAccuracy:
                    characterExtraStats.hitAccuracy *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MagicAccuracy:
                    characterExtraStats.magicAccuracy *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MagicPower:
                    characterExtraStats.magicPower *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MagicResistance:
                    characterExtraStats.magicResistance *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.ManaPool:
                    characterExtraStats.manaPool *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.MoveSpeed:
                    characterExtraStats.moveSpeed *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.RegenerationHealth:
                    characterExtraStats.regenerationHealth *= changingValue;
                    break;
                case __Scripts.CharacterEntity.ExtraStats.ExtraStats.RegenerationMana:
                    characterExtraStats.regenerationMana *= changingValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return characterExtraStats;
        }
    }
}