using System;
using __Scripts.ExtraStats;

namespace __Scripts.GolemEntity.ExtraStats
{
    public class SpecializationExtraStats : ExtraStatsDecorator
    {
        private Specialization _specialization;

        private float[] _extraStatsParams;

        public SpecializationExtraStats(Specialization specialization, IExtraStatsProvider wrappedEntity, GolemBaseStats baseStats) : base(
             wrappedEntity, baseStats)
        {
            _specialization = specialization;
        }

        protected override GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats)
        {
            return _wrappedEntity.GetExtraStats() + GetSpecExtraStats(baseStats, _specialization);
        }

        private GolemExtraStats GetSpecExtraStats(GolemBaseStats baseStats, Specialization specialization)
        {
            var strength = baseStats.Strength;
            var agility = baseStats.Agility;
            var intelligence = baseStats.Intelligence;

            switch (specialization)
            {
                case Specialization.Warrior:
                    SpecExtraArgs specWar = new SpecExtraArgs(strength, agility, intelligence)
                    {
                        AttackSpeedArgAg = agility, //2
                        HealthArgSt = strength //1.5
                    };
                    return InitializeExtraStats(specWar);
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

        private GolemExtraStats InitializeExtraStats(SpecExtraArgs specExtraArgs)
        {
            return new GolemExtraStats()
            {
                AttackSpeed = ExtraStatsCalculator.GetAttackSpeed(specExtraArgs.AttackSpeedArgAg),
                AvoidChance = ExtraStatsCalculator.GetAvoidChance(specExtraArgs.AvoidChanceArgSt, specExtraArgs.AvoidChanceArgAg),
                DamagePerHeat = ExtraStatsCalculator.GetDamagePerHeat(specExtraArgs.DamagePerHeatArgSt, specExtraArgs.DamagePerHeatArgAg, specExtraArgs.DamagePerHeatArgIn),
                Defence = ExtraStatsCalculator.GetDefence(specExtraArgs.DefenceArgSt, specExtraArgs.DefenceArgAg),
                DodgeChance = ExtraStatsCalculator.GetDodgeChance(specExtraArgs.DodgeChanceArgAg, specExtraArgs.DodgeChanceArgIn),
                Health = ExtraStatsCalculator.GetHealth(specExtraArgs.HealthArgSt),
                HitAccuracy = ExtraStatsCalculator.GetHitAccuracy(specExtraArgs.HitAccuracyArgSt, specExtraArgs.HitAccuracyArgAg),
                MagicAccuracy = ExtraStatsCalculator.GetMagicAccuracy(specExtraArgs.MagicAccuracyArgSt, specExtraArgs.MagicAccuracyArgIn),
                MagicDamage = ExtraStatsCalculator.GetMagicDamage(specExtraArgs.MagicDamageArgIn),
                MagicResistance = ExtraStatsCalculator.GetMagicResistance(specExtraArgs.MagicResistanceArgSt, specExtraArgs.MagicResistanceArgIn),
                ManaPool = ExtraStatsCalculator.GetManaPool(specExtraArgs.ManaPoolArgIn),
                MoveSpeed = ExtraStatsCalculator.GetMoveSpeed(specExtraArgs.MoveSpeedArgSt, specExtraArgs.MoveSpeedArgAg),
                RegenerationRate = ExtraStatsCalculator.GetRegenerationRate(specExtraArgs.RegenerationRateArgSt, specExtraArgs.RegenerationRateArgAg),
                Stamina = ExtraStatsCalculator.GetStamina(specExtraArgs.StaminaArgSt, specExtraArgs.StaminaArgAg)
            };
        }
        
        private class SpecExtraArgs
        {
            private static float Strength { get; set; }
            private static float Agility { get; set; }
            private static float Intelligence { get; set; }
            
            public float AttackSpeedArgAg = Agility;
            
            public float AvoidChanceArgSt = Strength;
            public float AvoidChanceArgAg = Agility;
            
            public float DamagePerHeatArgSt = Strength;
            public float DamagePerHeatArgAg = Agility;
            public float DamagePerHeatArgIn = Intelligence;
            
            public float DefenceArgSt = Strength;
            public float DefenceArgAg = Agility;
            
            public float DodgeChanceArgAg = Agility;
            public float DodgeChanceArgIn = Intelligence;
            
            public float HealthArgSt = Strength;
            
            public float HitAccuracyArgSt = Strength;
            public float HitAccuracyArgAg = Agility;
            
            public float MagicAccuracyArgSt = Strength;
            public float MagicAccuracyArgIn = Intelligence;
            
            public float MagicDamageArgIn = Intelligence;
            
            public float MagicResistanceArgSt = Strength;
            public float MagicResistanceArgIn = Intelligence;
            
            public float ManaPoolArgIn = Intelligence;
            
            public float MoveSpeedArgSt = Strength;
            public float MoveSpeedArgAg = Agility;
            
            public float RegenerationRateArgSt = Strength;
            public float RegenerationRateArgAg = Agility;
            
            public float StaminaArgSt = Strength;
            public float StaminaArgAg = Agility;

            public SpecExtraArgs(float strength, float agility, float intelligence)
            {
                Strength = strength;
                Agility = agility;
                Intelligence = intelligence;
            }
        }
    }
}
