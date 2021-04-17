using System;
using UnityEngine;

namespace __Scripts.ExtraStats
{
    public class SpecializationExtraStats : ExtraStatsDecorator
    {
        private Specialization _specialization;

        public SpecializationExtraStats(IExtraStatsProvider wrappedEntity, Specialization specialization) : base(wrappedEntity)
        {
            _specialization = specialization;
        }

        protected override GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats)
        {
            return _wrappedEntity.GetExtraStats(baseStats) + GetSpecExtraStats(baseStats, _specialization);
        }

        private GolemExtraStats GetSpecExtraStats(GolemBaseStats baseStats, Specialization specialization)
        {
            var strength = baseStats.Strength;
            var agility = baseStats.Agility;
            var intelligence = baseStats.Intelligence;
            
            switch (specialization)
            {
                case Specialization.Warrior:
                    return new GolemExtraStats()
                    {
                        AttackSpeed = ExtraStatsCalculator.GetAttackSpeed(),
                        AvoidChance = ExtraStatsCalculator.GetAvoidChance(),
                        DamagePerHeat = ExtraStatsCalculator.GetDamagePerHeat(strength, agility, intelligence),
                        Defence = ExtraStatsCalculator.GetDefence(),
                        DodgeChance = ExtraStatsCalculator.GetDodgeChance(),
                        Health = ExtraStatsCalculator.GetHealth(strength),
                        HitAccuracy = ExtraStatsCalculator.GetHitAccuracy(),
                        MagicAccuracy = ExtraStatsCalculator.GetMagicAccuracy(),
                        MagicDamage = ExtraStatsCalculator.GetMagicDamage(),
                        MagicResistance = ExtraStatsCalculator.GetMagicResistance(),
                        ManaPool = ExtraStatsCalculator.GetManaPool(),
                        MoveSpeed = ExtraStatsCalculator.GetMoveSpeed(),
                        RegenerationRate = ExtraStatsCalculator.GetRegenerationRate(),
                        Stamina = ExtraStatsCalculator.GetStamina(strength, agility)
                    };
                case Specialization.Rogue:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Wizard:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.BattleMage:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Priest:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Paladin:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Bard:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Fighter:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Ranger:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.SpecialistWizard:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Illusionist:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Cleric:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Druid:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Thief:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Barbarian:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Sorcerer:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Monk:
                    return new GolemExtraStats()
                    {

                    };
                case Specialization.Tank:
                    return new GolemExtraStats()
                    {

                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(specialization), specialization, null);
            }
        }
    }
}
