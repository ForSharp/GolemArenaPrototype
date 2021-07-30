using System;
using System.Collections.Generic;
using GolemEntity.BaseStats;
using GolemEntity.ExtraStats;

namespace GolemEntity
{
    public static class CharacterStatsService
    {
        public static GolemBaseStats GetBaseStats(GolemType type)
        {
            switch (type)
            {
                case GolemType.Cleopatra:
                    return new GolemBaseStats()
                    {
                        Strength = 0.14f,
                        Agility = 0.54f,
                        Intelligence = 0.82f
                    };
                case GolemType.Ramzes:
                    return new GolemBaseStats()
                    {
                        Strength = 0.37f,
                        Agility = 0.86f,
                        Intelligence = 0.27f
                    };
                case GolemType.Garruk:
                    return new GolemBaseStats()
                    {
                        Strength = 0.82f,
                        Agility = 0.22f,
                        Intelligence = 0.46f
                    };
                case GolemType.Bagrak:
                    return new GolemBaseStats()
                    {
                        Strength = 0.84f,
                        Agility = 0.21f,
                        Intelligence = 0.45f
                    };
                case GolemType.Gloin:
                    return new GolemBaseStats()
                    {
                        Strength = 0.54f,
                        Agility = 0.86f,
                        Intelligence = 0.1f
                    };
                case GolemType.Rock:
                    return new GolemBaseStats()
                    {
                        Strength = 0.95f,
                        Agility = 0.14f,
                        Intelligence = 0.41f
                    };
                case GolemType.Fort:
                    return new GolemBaseStats()
                    {
                        Strength = 0.92f,
                        Agility = 0.11f,
                        Intelligence = 0.47f
                    };
                case GolemType.Mario:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.65f,
                        Intelligence = 0.25f
                    };
                case GolemType.Freak:
                    return new GolemBaseStats()
                    {
                        Strength = 0.57f,
                        Agility = 0.39f,
                        Intelligence = 0.54f
                    };
                case GolemType.Peppa:
                    return new GolemBaseStats()
                    {
                        Strength = 0.83f,
                        Agility = 0.14f,
                        Intelligence = 0.53f
                    };
                case GolemType.Satyr:
                    return new GolemBaseStats()
                    {
                        Strength = 0.29f,
                        Agility = 0.56f,
                        Intelligence = 0.65f
                    };
                case GolemType.Maron:
                    return new GolemBaseStats()
                    {
                        Strength = 0.65f,
                        Agility = 0.75f,
                        Intelligence = 0.1f
                    };
                case GolemType.Cudgel:
                    return new GolemBaseStats()
                    {
                        Strength = 0.67f,
                        Agility = 0.27f,
                        Intelligence = 0.56f
                    };
                case GolemType.Belesar:
                    return new GolemBaseStats()
                    {
                        Strength = 0.56f,
                        Agility = 0.61f,
                        Intelligence = 0.33f
                    };
                case GolemType.Yama:
                    return new GolemBaseStats()
                    {
                        Strength = 0.34f,
                        Agility = 0.44f,
                        Intelligence = 0.72f
                    };
                case GolemType.Shanti:
                    return new GolemBaseStats()
                    {
                        Strength = 0.34f,
                        Agility = 0.46f,
                        Intelligence = 0.7f
                    };
                case GolemType.Aine:
                    return new GolemBaseStats()
                    {
                        Strength = 0.43f,
                        Agility = 0.5f,
                        Intelligence = 0.57f
                    };
                case GolemType.Medusa:
                    return new GolemBaseStats()
                    {
                        Strength = 0.22f,
                        Agility = 0.38f,
                        Intelligence = 0.9f
                    };
                case GolemType.Sazum:
                    return new GolemBaseStats()
                    {
                        Strength = 0.37f,
                        Agility = 0.5f,
                        Intelligence = 0.63f
                    };
                case GolemType.Nissa:
                    return new GolemBaseStats()
                    {
                        Strength = 0.54f,
                        Agility = 0.65f,
                        Intelligence = 0.31f
                    };
                case GolemType.Raven:
                    return new GolemBaseStats()
                    {
                        Strength = 0.61f,
                        Agility = 0.65f,
                        Intelligence = 0.24f
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static GolemBaseStats GetBaseStats(Specialization spec)
        {
            switch (spec)
            {
                case Specialization.Barbarian:
                    return new GolemBaseStats()
                    {
                        Strength = 0.82f,
                        Agility = 0.4f,
                        Intelligence = 0.28f
                    };
                case Specialization.Bard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.67f,
                        Intelligence = 0.63f
                    };
                case Specialization.BattleMage:
                    return new GolemBaseStats()
                    {
                        Strength = 0.56f,
                        Agility = 0.17f,
                        Intelligence = 0.77f
                    };
                case Specialization.Cleric:
                    return new GolemBaseStats()
                    {
                        Strength = 0.61f,
                        Agility = 0.55f,
                        Intelligence = 0.34f
                    };
                case Specialization.DeathKnight:
                    return new GolemBaseStats()
                    {
                        Strength = 0.70f,
                        Agility = 0.50f,
                        Intelligence = 0.30f
                    };
                case Specialization.DemonHunter:
                    return new GolemBaseStats()
                    {
                        Strength = 0.33f,
                        Agility = 0.68f,
                        Intelligence = 0.49f
                    };
                case Specialization.Druid:
                    return new GolemBaseStats()
                    {
                        Strength = 0.40f,
                        Agility = 0.35f,
                        Intelligence = 0.75f
                    };
                case Specialization.Fighter:
                    return new GolemBaseStats()
                    {
                        Strength = 0.76f,
                        Agility = 0.69f,
                        Intelligence = 0.05f
                    };
                case Specialization.Hunter:
                    return new GolemBaseStats()
                    {
                        Strength = 0.13f,
                        Agility = 0.75f,
                        Intelligence = 0.62f
                    };
                case Specialization.Illusionist:
                    return new GolemBaseStats()
                    {
                        Strength = 0.14f,
                        Agility = 0.89f,
                        Intelligence = 0.47f
                    };
                case Specialization.Monk:
                    return new GolemBaseStats()
                    {
                        Strength = 0.50f,
                        Agility = 0.50f,
                        Intelligence = 0.50f
                    };
                case Specialization.Paladin:
                    return new GolemBaseStats()
                    {
                        Strength = 0.70f,
                        Agility = 0.63f,
                        Intelligence = 0.17f
                    };
                case Specialization.Ranger:
                    return new GolemBaseStats()
                    {
                        Strength = 0.29f,
                        Agility = 0.82f,
                        Intelligence = 0.39f
                    };
                case Specialization.Rogue:
                    return new GolemBaseStats()
                    {
                        Strength = 0.20f,
                        Agility = 0.66f,
                        Intelligence = 0.64f
                    };
                case Specialization.Shaman:
                    return new GolemBaseStats()
                    {
                        Strength = 0.57f,
                        Agility = 0.15f,
                        Intelligence = 0.78f
                    };
                case Specialization.Sorcerer:
                    return new GolemBaseStats()
                    {
                        Strength = 0.69f,
                        Agility = 0.02f,
                        Intelligence = 0.79f
                    };
                case Specialization.Tank:
                    return new GolemBaseStats()
                    {
                        Strength = 0.83f,
                        Agility = 0.57f,
                        Intelligence = 0.1f
                    };
                case Specialization.Thief:
                    return new GolemBaseStats()
                    {
                        Strength = 0.43f,
                        Agility = 0.54f,
                        Intelligence = 0.53f
                    };
                case Specialization.Warlock:
                    return new GolemBaseStats()
                    {
                        Strength = 0.61f,
                        Agility = 0.25f,
                        Intelligence = 0.64f
                    };
                case Specialization.Warrior:
                    return new GolemBaseStats()
                    {
                        Strength = 0.68f,
                        Agility = 0.56f,
                        Intelligence = 0.26f
                    };
                case Specialization.Wizard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.62f,
                        Agility = 0.06f,
                        Intelligence = 0.82f
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(spec), spec, null);
            }
        }

        public static GolemBaseStats GetBaseStats(GolemType type, Specialization spec)
        {
            return GetBaseStats(type) + GetBaseStats(spec);
        }

        public static GolemExtraStats GetExtraStats(GolemType golemType, GolemBaseStats baseStats)
        {
            var strength = baseStats.Strength;
            var agility = baseStats.Agility;
            var intelligence = baseStats.Intelligence;

            switch (golemType)
            {
                case GolemType.Cleopatra:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        AvoidChanceArgAg = agility * 1.1f,
                        ManaPoolArgIn = intelligence * 1.1f
                    });
                case GolemType.Ramzes:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgSt = strength * 1.1f,
                        MagicResistanceArgSt = strength * 1.1f
                    });
                case GolemType.Garruk:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.05f,
                        MoveSpeedArgAg = agility * 1.15f
                    });
                case GolemType.Bagrak:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgIn = intelligence * 1.15f,
                        DamagePerHeatArgAg = agility * 1.15f
                    });
                case GolemType.Gloin:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgSt = strength * 1.1f,
                        StaminaArgSt = strength * 1.1f
                    });
                case GolemType.Rock:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgAg = agility * 1.15f,
                        DodgeChanceArgIn = intelligence * 1.15f
                    });
                case GolemType.Fort:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        AvoidChanceArgSt = strength * 1.05f,
                        HitAccuracyArgSt = strength * 1.05f
                    });
                case GolemType.Mario:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        RegenerationManaIn = intelligence * 1.2f,
                        HealthArgSt = strength * 1.1f
                    });
                case GolemType.Freak:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DodgeChanceArgIn = intelligence * 1.15f,
                        HealthArgSt = strength * 1.05f
                    });
                case GolemType.Peppa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgSt = strength * 1.05f,
                        MagicDamageArgIn = intelligence * 1.15f
                    });
                case GolemType.Satyr:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HealthArgSt = strength * 1.15f,
                        MagicDamageArgIn = intelligence * 1.1f
                    });
                case GolemType.Maron:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgAg = agility * 1.05f,
                        HitAccuracyArgSt = strength * 1.1f
                    });
                case GolemType.Cudgel:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        RegenerationManaIn = intelligence * 1.15f,
                        ManaPoolArgIn = intelligence * 1.15f
                    });
                case GolemType.Belesar:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgIn = intelligence * 1.2f,
                        RegenerationHealthArgSt = strength * 1.1f
                    });
                case GolemType.Yama:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DodgeChanceArgIn = intelligence * 1.1f,
                        DefenceArgSt = strength * 1.15f
                    });
                case GolemType.Shanti:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MoveSpeedArgAg = agility * 1.1f,
                        DamagePerHeatArgAg = agility * 1.1f
                    });
                case GolemType.Aine:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        StaminaArgSt = strength * 1.15f,
                        MagicResistanceArgIn = intelligence * 1.1f
                    });
                case GolemType.Medusa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicAccuracyArgSt = strength * 1.15f,
                        AvoidChanceArgSt = strength * 1.15f
                    });
                case GolemType.Sazum:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgAg = agility * 1.1f,
                        MagicAccuracyArgSt = strength * 1.15f
                    });
                case GolemType.Nissa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        StaminaArgAg = agility * 1.05f,
                        HitAccuracyArgAg = agility * 1.05f
                    });
                case GolemType.Raven:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgSt = strength * 1.1f,
                        HealthArgSt = strength * 1.1f
                    });
                default:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence));
            }
        }

        public static GolemExtraStats GetExtraStats(Specialization spec, GolemBaseStats baseStats)
        {
            var strength = baseStats.Strength;
            var agility = baseStats.Agility;
            var intelligence = baseStats.Intelligence;

            switch (spec)
            {
                case Specialization.Barbarian:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgSt = strength * 1.05f,
                        MagicAccuracyArgSt = strength * 1.05f,
                        DamagePerHeatArgSt = strength * 0.95f
                    });
                case Specialization.Bard:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgAg = agility * 1.05f,
                        MoveSpeedArgSt = strength * 0.85f,
                        DefenceArgSt = strength * 0.85f
                    });
                case Specialization.BattleMage:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        RegenerationStaminaAg = agility * 1.15f,
                        HitAccuracyArgAg = agility * 1.15f,
                        DamagePerHeatArgAg = agility * 0.85f
                    });
                case Specialization.Cleric:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgSt = strength * 1.05f,
                        AvoidChanceArgSt = strength * 1.05f,
                        MoveSpeedArgSt = strength * 1.05f
                    });
                case Specialization.DeathKnight:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.05f,
                        HealthArgSt = strength * 1.05f,
                        MagicAccuracyArgIn = intelligence * 0.8f
                    });
                case Specialization.DemonHunter:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgIn = intelligence * 1.15f,
                        AvoidChanceArgAg = agility * 1.05f,
                        AttackSpeedArgAg = agility * 0.95f
                    });
                case Specialization.Druid:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DodgeChanceArgIn = intelligence * 1.1f,
                        DefenceArgAg = agility * 1.15f,
                        MoveSpeedArgAg = agility * 0.85f
                    });
                case Specialization.Fighter:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MoveSpeedArgAg = agility * 1.1f,
                        HealthArgSt = strength * 1.05f,
                        MagicAccuracyArgIn = intelligence * 0.8f
                    });
                case Specialization.Hunter:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        StaminaArgAg = agility * 1.05f,
                        DamagePerHeatArgSt = strength * 1.15f,
                        MagicResistanceArgSt = strength * 0.85f
                    });
                case Specialization.Illusionist:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DodgeChanceArgAg = agility * 1.05f
                    });
                case Specialization.Monk:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgIn = intelligence * 1.1f,
                        DefenceArgAg = agility * 1.15f,
                        MoveSpeedArgSt = strength * 1.1f
                    });
                case Specialization.Paladin:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        ManaPoolArgIn = intelligence * 1.2f,
                        DamagePerHeatArgIn = intelligence * 1.2f,
                        MagicAccuracyArgSt = strength * 0.95f
                    });
                case Specialization.Ranger:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgIn = intelligence * 1.15f,
                        AttackSpeedArgAg = agility * 1.05f,
                        DefenceArgSt = strength * 0.85f
                    });
                case Specialization.Rogue:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MoveSpeedArgAg = agility * 1.05f
                    });
                case Specialization.Shaman:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        RegenerationManaIn = intelligence * 1.1f,
                        HitAccuracyArgAg = agility * 1.15f,
                        DamagePerHeatArgSt = strength * 0.9f
                    });
                case Specialization.Sorcerer:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgSt = strength * 1.1f,
                        RegenerationHealthArgAg = agility * 1.15f,
                        DodgeChanceArgAg = agility * 0.85f
                    });
                case Specialization.Tank:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.05f,
                        RegenerationHealthArgSt = strength * 1.05f,
                        MagicResistanceArgSt = strength * 0.95f
                    });
                case Specialization.Thief:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicAccuracyArgSt = strength * 1.15f
                    });
                case Specialization.Warlock:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.1f,
                        RegenerationHealthArgAg = agility * 1.15f,
                        DamagePerHeatArgSt = strength * 0.9f
                    });
                case Specialization.Warrior:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgAg = agility * 1.1f
                    });
                case Specialization.Wizard:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgSt = strength * 1.2f,
                        ManaPoolArgIn = intelligence * 0.9f
                    });
                default:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence));
            }
        }

        public static GolemExtraStats GetExtraStats(GolemType type, Specialization spec, GolemBaseStats baseStats)
        {
            return GetExtraStats(type, baseStats) + GetExtraStats(spec, baseStats);
        }

        public static MainCharacterParameter GetMainCharacterParameter(GolemType type)
        {
            switch (type)
            {
                case GolemType.Cleopatra:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Ramzes:
                    return MainCharacterParameter.Agility;
                case GolemType.Garruk:
                    return MainCharacterParameter.Strength;
                case GolemType.Bagrak:
                    return MainCharacterParameter.Strength;
                case GolemType.Gloin:
                    return MainCharacterParameter.Agility;
                case GolemType.Rock:
                    return MainCharacterParameter.Strength;
                case GolemType.Fort:
                    return MainCharacterParameter.Strength;
                case GolemType.Mario:
                    return MainCharacterParameter.Agility;
                case GolemType.Freak:
                    return MainCharacterParameter.Strength;
                case GolemType.Peppa:
                    return MainCharacterParameter.Strength;
                case GolemType.Satyr:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Maron:
                    return MainCharacterParameter.Agility;
                case GolemType.Cudgel:
                    return MainCharacterParameter.Strength;
                case GolemType.Belesar:
                    return MainCharacterParameter.Agility;
                case GolemType.Yama:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Shanti:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Aine:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Medusa:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Sazum:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Nissa:
                    return MainCharacterParameter.Agility;
                case GolemType.Raven:
                    return MainCharacterParameter.Agility;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static MainCharacterParameter GetMainCharacterParameter(Specialization spec)
        {
            switch (spec)
            {
                case Specialization.Barbarian:
                    return MainCharacterParameter.Strength;
                case Specialization.Bard:
                    return MainCharacterParameter.Agility;
                case Specialization.BattleMage:
                    return MainCharacterParameter.Intelligence;
                case Specialization.Cleric:
                    return MainCharacterParameter.Strength;
                case Specialization.DeathKnight:
                    return MainCharacterParameter.Strength;
                case Specialization.DemonHunter:
                    return MainCharacterParameter.Agility;
                case Specialization.Druid:
                    return MainCharacterParameter.Intelligence;
                case Specialization.Fighter:
                    return MainCharacterParameter.Strength;
                case Specialization.Hunter:
                    return MainCharacterParameter.Agility;
                case Specialization.Illusionist:
                    return MainCharacterParameter.Agility;
                case Specialization.Monk:
                    return MainCharacterParameter.Intelligence;
                case Specialization.Paladin:
                    return MainCharacterParameter.Strength;
                case Specialization.Ranger:
                    return MainCharacterParameter.Agility;
                case Specialization.Rogue:
                    return MainCharacterParameter.Agility;
                case Specialization.Shaman:
                    return MainCharacterParameter.Intelligence;
                case Specialization.Sorcerer:
                    return MainCharacterParameter.Intelligence;
                case Specialization.Tank:
                    return MainCharacterParameter.Strength;
                case Specialization.Thief:
                    return MainCharacterParameter.Agility;
                case Specialization.Warlock:
                    return MainCharacterParameter.Intelligence;
                case Specialization.Warrior:
                    return MainCharacterParameter.Strength;
                case Specialization.Wizard:
                    return MainCharacterParameter.Intelligence;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spec), spec, null);
            }
        }

        public static float[] GetStatsCollection(GolemExtraStats stats)
        {
            return new[]
            {
                stats.AttackRange,
                stats.AttackSpeed,
                stats.AvoidChance,
                stats.DamagePerHeat,
                stats.Defence,
                stats.DodgeChance,
                stats.Health,
                stats.HitAccuracy,
                stats.MagicAccuracy,
                stats.MagicDamage,
                stats.MagicResistance,
                stats.ManaPool,
                stats.MoveSpeed,
                stats.RegenerationHealth,
                stats.RegenerationMana,
                stats.RegenerationStamina,
                stats.Stamina
            };
        }

        private static readonly GolemBaseStats BaseStats = new GolemBaseStats
            {Strength = 150, Agility = 150, Intelligence = 150};

        private const int NonExistentEnumValue = 2222222;

        public static List<string> GetCharacterFeatures(GolemType type)
        {
            var typeStats = GetExtraStats(type, BaseStats);
            typeStats = GetExtraStats(type, BaseStats);
            var defaultStats = GetExtraStats((GolemType) NonExistentEnumValue, BaseStats);
            var typeStatsColl = GetStatsCollection(typeStats);
            var defaultStatsColl = GetStatsCollection(defaultStats);

            return GetFeaturesCollection(typeStatsColl, defaultStatsColl);
        }

        public static List<string> GetCharacterFeatures(Specialization spec)
        {
            var specStats = GetExtraStats(spec, BaseStats);
            specStats = GetExtraStats(spec, BaseStats);
            var defaultStats = GetExtraStats((Specialization) NonExistentEnumValue, BaseStats);
            var specStatsColl = GetStatsCollection(specStats);
            var defaultStatsColl = GetStatsCollection(defaultStats);

            return GetFeaturesCollection(specStatsColl, defaultStatsColl);
        }

        public static List<string> GetCharacterFeatures(GolemType type, Specialization spec)
        {
            var currentStats = GetExtraStats(type, spec, BaseStats);
            currentStats = GetExtraStats(type, spec, BaseStats);
            var defaultStats = GetExtraStats((GolemType) NonExistentEnumValue, (Specialization) NonExistentEnumValue,
                BaseStats);
            var typeStatsColl = GetStatsCollection(currentStats);
            var defaultStatsColl = GetStatsCollection(defaultStats);

            return GetFeaturesCollection(typeStatsColl, defaultStatsColl);
        }

        private static List<string> GetFeaturesCollection(float[] currentStatsColl, float[] defaultStatsColl)
        {
            var features = new List<string>();
            for (var i = 0; i < currentStatsColl.Length; i++)
            {
                if (Math.Abs(currentStatsColl[i] - defaultStatsColl[i]) > 0.01f)
                {
                    var result = currentStatsColl[i] - defaultStatsColl[i] > 0
                        ? $"<color=green>+ {(-(100 - (int) (currentStatsColl[i] / defaultStatsColl[i] * 100)) != 0 ? -(100 - (int) (currentStatsColl[i] / defaultStatsColl[i] * 100)) : 1)} %</color>"
                        : $"<color=red>- {(-(100 - (int) (defaultStatsColl[i] / currentStatsColl[i] * 100)) != 0 ? -(100 - (int) (defaultStatsColl[i] / currentStatsColl[i] * 100)) : 1)} %</color>";
                    features.Add($"{GetStatsStringCollection()[i]} " + result);
                }
            }

            return features;
        }

        private static string[] GetStatsStringCollection()
        {
            return new[]
            {
                "AttackRange",
                "AttackSpeed",
                "AvoidChance",
                "DamagePerHeat",
                "Defence",
                "DodgeChance",
                "Health",
                "HitAccuracy",
                "MagicAccuracy",
                "MagicDamage",
                "MagicResistance",
                "ManaPool",
                "MoveSpeed",
                "RegenerationHealth",
                "RegenerationMana",
                "RegenerationStamina",
                "Stamina"
            };
        }
    }
}