using GolemEntity.ExtraStats;

namespace GolemEntity
{
    public class ExtraStatsRate
    {
        private static float Strength { get; set; }
        private static float Agility { get; set; }
        private static float Intelligence { get; set; }

        public float AttackRangeArg;

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

        public float RegenerationHealthArgSt = Strength;
        public float RegenerationHealthArgAg = Agility;

        public float RegenerationManaIn = Intelligence;

        public float RegenerationStaminaAg = Agility;

        public float StaminaArgSt = Strength;
        public float StaminaArgAg = Agility;

        public ExtraStatsRate(float strength, float agility, float intelligence)
        {
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
        }
        
        public static GolemExtraStats InitializeExtraStats(ExtraStatsRate typeExtraArgs)
        {
            return new GolemExtraStats()
            {
                AttackRange = ExtraStatsCalculator.GetAttackRange(),
                AttackSpeed = ExtraStatsCalculator.GetAttackSpeed(typeExtraArgs.AttackSpeedArgAg),
                AvoidChance = ExtraStatsCalculator.GetAvoidChance(typeExtraArgs.AvoidChanceArgSt, typeExtraArgs.AvoidChanceArgAg),
                DamagePerHeat = ExtraStatsCalculator.GetDamagePerHeat(typeExtraArgs.DamagePerHeatArgSt, typeExtraArgs.DamagePerHeatArgAg, typeExtraArgs.DamagePerHeatArgIn),
                Defence = ExtraStatsCalculator.GetDefence(typeExtraArgs.DefenceArgSt, typeExtraArgs.DefenceArgAg),
                DodgeChance = ExtraStatsCalculator.GetDodgeChance(typeExtraArgs.DodgeChanceArgAg, typeExtraArgs.DodgeChanceArgIn),
                Health = ExtraStatsCalculator.GetHealth(typeExtraArgs.HealthArgSt),
                HitAccuracy = ExtraStatsCalculator.GetHitAccuracy(typeExtraArgs.HitAccuracyArgSt, typeExtraArgs.HitAccuracyArgAg),
                MagicAccuracy = ExtraStatsCalculator.GetMagicAccuracy(typeExtraArgs.MagicAccuracyArgSt, typeExtraArgs.MagicAccuracyArgIn),
                MagicDamage = ExtraStatsCalculator.GetMagicDamage(typeExtraArgs.MagicDamageArgIn),
                MagicResistance = ExtraStatsCalculator.GetMagicResistance(typeExtraArgs.MagicResistanceArgSt, typeExtraArgs.MagicResistanceArgIn),
                ManaPool = ExtraStatsCalculator.GetManaPool(typeExtraArgs.ManaPoolArgIn),
                MoveSpeed = ExtraStatsCalculator.GetMoveSpeed(typeExtraArgs.MoveSpeedArgSt, typeExtraArgs.MoveSpeedArgAg),
                RegenerationHealth = ExtraStatsCalculator.GetRegenerationHealth(typeExtraArgs.RegenerationHealthArgSt, typeExtraArgs.RegenerationHealthArgAg),
                RegenerationMana = ExtraStatsCalculator.GetRegenerationMana(typeExtraArgs.RegenerationManaIn),
                RegenerationStamina = ExtraStatsCalculator.GetRegenerationStamina(typeExtraArgs.RegenerationStaminaAg),
                Stamina = ExtraStatsCalculator.GetStamina(typeExtraArgs.StaminaArgSt, typeExtraArgs.StaminaArgAg)
            };
        }
    }
}