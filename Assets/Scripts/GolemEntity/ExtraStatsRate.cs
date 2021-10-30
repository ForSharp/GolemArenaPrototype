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

        public float MagicPowerArgIn = Intelligence;

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
                attackRange = ExtraStatsCalculator.GetAttackRange(),
                attackSpeed = ExtraStatsCalculator.GetAttackSpeed(typeExtraArgs.AttackSpeedArgAg),
                avoidChance = ExtraStatsCalculator.GetAvoidChance(typeExtraArgs.AvoidChanceArgSt, typeExtraArgs.AvoidChanceArgAg),
                damagePerHeat = ExtraStatsCalculator.GetDamagePerHeat(typeExtraArgs.DamagePerHeatArgSt, typeExtraArgs.DamagePerHeatArgAg, typeExtraArgs.DamagePerHeatArgIn),
                defence = ExtraStatsCalculator.GetDefence(typeExtraArgs.DefenceArgSt, typeExtraArgs.DefenceArgAg),
                dodgeChance = ExtraStatsCalculator.GetDodgeChance(typeExtraArgs.DodgeChanceArgAg, typeExtraArgs.DodgeChanceArgIn),
                health = ExtraStatsCalculator.GetHealth(typeExtraArgs.HealthArgSt),
                hitAccuracy = ExtraStatsCalculator.GetHitAccuracy(typeExtraArgs.HitAccuracyArgSt, typeExtraArgs.HitAccuracyArgAg),
                magicAccuracy = ExtraStatsCalculator.GetMagicAccuracy(typeExtraArgs.MagicAccuracyArgSt, typeExtraArgs.MagicAccuracyArgIn),
                magicPower = ExtraStatsCalculator.GetMagicPower(typeExtraArgs.MagicPowerArgIn),
                magicResistance = ExtraStatsCalculator.GetMagicResistance(typeExtraArgs.MagicResistanceArgSt, typeExtraArgs.MagicResistanceArgIn),
                manaPool = ExtraStatsCalculator.GetManaPool(typeExtraArgs.ManaPoolArgIn),
                moveSpeed = ExtraStatsCalculator.GetMoveSpeed(typeExtraArgs.MoveSpeedArgSt, typeExtraArgs.MoveSpeedArgAg),
                regenerationHealth = ExtraStatsCalculator.GetRegenerationHealth(typeExtraArgs.RegenerationHealthArgSt, typeExtraArgs.RegenerationHealthArgAg),
                regenerationMana = ExtraStatsCalculator.GetRegenerationMana(typeExtraArgs.RegenerationManaIn),
                regenerationStamina = ExtraStatsCalculator.GetRegenerationStamina(typeExtraArgs.RegenerationStaminaAg),
                stamina = ExtraStatsCalculator.GetStamina(typeExtraArgs.StaminaArgSt, typeExtraArgs.StaminaArgAg)
            };
        }
    }
}