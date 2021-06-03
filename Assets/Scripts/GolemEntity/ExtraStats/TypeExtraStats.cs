using System;
using __Scripts.ExtraStats;

namespace __Scripts.GolemEntity.ExtraStats
{
    public class TypeExtraStats : IExtraStatsProvider
    {
        private GolemType _type;
        private GolemBaseStats _baseStats;

        public TypeExtraStats(GolemType type, GolemBaseStats baseStats)
        {
            _type = type;
            _baseStats = baseStats;
        }

        public GolemExtraStats GetExtraStats()
        {
            var strength = _baseStats.Strength;
            var agility = _baseStats.Agility;
            var intelligence = _baseStats.Intelligence;

            switch (_type)
            {
                case GolemType.WaterGolem:
                    TypeExtraArgs typeIron = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeIron);
                case GolemType.AirGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.CristalGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.FireGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.PlasmaGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.SteamGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.DarkGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.NatureGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.FogGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.ObsidianGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.InsectGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.StalagmiteGolem:
                    return new GolemExtraStats()
                    {

                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
            }
        }
        
        private GolemExtraStats InitializeExtraStats(TypeExtraArgs typeExtraArgs)
        {
            return new GolemExtraStats()
            {
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
                RegenerationRate = ExtraStatsCalculator.GetRegenerationRate(typeExtraArgs.RegenerationRateArgSt, typeExtraArgs.RegenerationRateArgAg),
                Stamina = ExtraStatsCalculator.GetStamina(typeExtraArgs.StaminaArgSt, typeExtraArgs.StaminaArgAg)
            };
        }
        
        private class TypeExtraArgs
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

            public TypeExtraArgs(float strength, float agility, float intelligence)
            {
                Strength = strength;
                Agility = agility;
                Intelligence = intelligence;
            }
        }
    }
}
