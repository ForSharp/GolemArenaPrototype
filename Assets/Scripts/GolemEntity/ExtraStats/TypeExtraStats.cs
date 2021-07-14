﻿using System;
using GolemEntity.BaseStats;

namespace GolemEntity.ExtraStats
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
                    TypeExtraArgs typeWaterGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f
                    };
                    return InitializeExtraStats(typeWaterGolem);
                case GolemType.AirGolem:
                    TypeExtraArgs typeAirGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeAirGolem);
                case GolemType.CrystalGolem:
                    TypeExtraArgs typeCristalGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeCristalGolem);
                case GolemType.FireGolem:
                    TypeExtraArgs typeFireGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeFireGolem);
                case GolemType.PlasmaGolem:
                    TypeExtraArgs typePlasmaGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typePlasmaGolem);
                case GolemType.SteamGolem:
                    TypeExtraArgs typeSteamGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeSteamGolem);
                case GolemType.DarkGolem:
                    TypeExtraArgs typeDarkGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeDarkGolem);
                case GolemType.NatureGolem:
                    TypeExtraArgs typeNatureGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeNatureGolem);
                case GolemType.FogGolem:
                    TypeExtraArgs typeFogGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeFogGolem);
                case GolemType.ObsidianGolem:
                    TypeExtraArgs typeObsidianGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeObsidianGolem);
                case GolemType.InsectGolem:
                    TypeExtraArgs typeInsectGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeInsectGolem);
                case GolemType.StalagmiteGolem:
                    TypeExtraArgs typeStalagmiteGolem = new TypeExtraArgs(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f 
                    };
                    return InitializeExtraStats(typeStalagmiteGolem);
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
            }
        }
        
        private GolemExtraStats InitializeExtraStats(TypeExtraArgs typeExtraArgs)
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
        
        private class TypeExtraArgs
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

            public TypeExtraArgs(float strength, float agility, float intelligence)
            {
                Strength = strength;
                Agility = agility;
                Intelligence = intelligence;
            }
        }
    }
}
