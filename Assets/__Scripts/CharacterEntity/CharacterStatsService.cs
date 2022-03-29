using System;
using System.Collections.Generic;
using __Scripts.CharacterEntity.BaseStats;
using __Scripts.CharacterEntity.ExtraStats;

namespace __Scripts.CharacterEntity
{
    public static class CharacterStatsService
    {
        public static CharacterBaseStats GetBaseStats(CharacterType type)
        {
            switch (type)
            {
                case CharacterType.Cleopatra:
                    return new CharacterBaseStats
                    {
                        strength = 0.14f,
                        agility = 0.54f,
                        intelligence = 0.82f
                    };
                case CharacterType.Ramzes:
                    return new CharacterBaseStats
                    {
                        strength = 0.37f,
                        agility = 0.86f,
                        intelligence = 0.27f
                    };
                case CharacterType.Garruk:
                    return new CharacterBaseStats
                    {
                        strength = 0.82f,
                        agility = 0.22f,
                        intelligence = 0.46f
                    };
                case CharacterType.Bagrak:
                    return new CharacterBaseStats
                    {
                        strength = 0.84f,
                        agility = 0.21f,
                        intelligence = 0.45f
                    };
                case CharacterType.Gloin:
                    return new CharacterBaseStats
                    {
                        strength = 0.54f,
                        agility = 0.86f,
                        intelligence = 0.1f
                    };
                case CharacterType.Rock:
                    return new CharacterBaseStats
                    {
                        strength = 0.95f,
                        agility = 0.14f,
                        intelligence = 0.41f
                    };
                case CharacterType.Fort:
                    return new CharacterBaseStats
                    {
                        strength = 0.92f,
                        agility = 0.11f,
                        intelligence = 0.47f
                    };
                case CharacterType.Mario:
                    return new CharacterBaseStats
                    {
                        strength = 0.6f,
                        agility = 0.65f,
                        intelligence = 0.25f
                    };
                case CharacterType.Freak:
                    return new CharacterBaseStats
                    {
                        strength = 0.57f,
                        agility = 0.39f,
                        intelligence = 0.54f
                    };
                case CharacterType.Peppa:
                    return new CharacterBaseStats
                    {
                        strength = 0.83f,
                        agility = 0.14f,
                        intelligence = 0.53f
                    };
                case CharacterType.Satyr:
                    return new CharacterBaseStats
                    {
                        strength = 0.29f,
                        agility = 0.56f,
                        intelligence = 0.65f
                    };
                case CharacterType.Maron:
                    return new CharacterBaseStats
                    {
                        strength = 0.65f,
                        agility = 0.75f,
                        intelligence = 0.1f
                    };
                case CharacterType.Cudgel:
                    return new CharacterBaseStats
                    {
                        strength = 0.67f,
                        agility = 0.27f,
                        intelligence = 0.56f
                    };
                case CharacterType.Belesar:
                    return new CharacterBaseStats
                    {
                        strength = 0.56f,
                        agility = 0.61f,
                        intelligence = 0.33f
                    };
                case CharacterType.Yama:
                    return new CharacterBaseStats
                    {
                        strength = 0.34f,
                        agility = 0.44f,
                        intelligence = 0.72f
                    };
                case CharacterType.Shanti:
                    return new CharacterBaseStats
                    {
                        strength = 0.34f,
                        agility = 0.46f,
                        intelligence = 0.7f
                    };
                case CharacterType.Aine:
                    return new CharacterBaseStats
                    {
                        strength = 0.43f,
                        agility = 0.5f,
                        intelligence = 0.57f
                    };
                case CharacterType.Medusa:
                    return new CharacterBaseStats
                    {
                        strength = 0.22f,
                        agility = 0.38f,
                        intelligence = 0.9f
                    };
                case CharacterType.Sazum:
                    return new CharacterBaseStats
                    {
                        strength = 0.37f,
                        agility = 0.5f,
                        intelligence = 0.63f
                    };
                case CharacterType.Nissa:
                    return new CharacterBaseStats
                    {
                        strength = 0.54f,
                        agility = 0.65f,
                        intelligence = 0.31f
                    };
                case CharacterType.Raven:
                    return new CharacterBaseStats
                    {
                        strength = 0.61f,
                        agility = 0.65f,
                        intelligence = 0.24f
                    };
                case CharacterType.Default:
                    return new CharacterBaseStats
                    {
                        strength = 0.5f,
                        agility = 0.5f,
                        intelligence = 0.5f
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static CharacterBaseStats GetBaseStats(Specialization spec)
        {
            switch (spec)
            {
                case Specialization.Barbarian:
                    return new CharacterBaseStats
                    {
                        strength = 0.82f,
                        agility = 0.4f,
                        intelligence = 0.28f
                    };
                case Specialization.Bard:
                    return new CharacterBaseStats
                    {
                        strength = 0.2f,
                        agility = 0.67f,
                        intelligence = 0.63f
                    };
                case Specialization.BattleMage:
                    return new CharacterBaseStats
                    {
                        strength = 0.56f,
                        agility = 0.17f,
                        intelligence = 0.77f
                    };
                case Specialization.Cleric:
                    return new CharacterBaseStats
                    {
                        strength = 0.61f,
                        agility = 0.55f,
                        intelligence = 0.34f
                    };
                case Specialization.DeathKnight:
                    return new CharacterBaseStats
                    {
                        strength = 0.70f,
                        agility = 0.50f,
                        intelligence = 0.30f
                    };
                case Specialization.DemonHunter:
                    return new CharacterBaseStats
                    {
                        strength = 0.33f,
                        agility = 0.68f,
                        intelligence = 0.49f
                    };
                case Specialization.Druid:
                    return new CharacterBaseStats
                    {
                        strength = 0.40f,
                        agility = 0.35f,
                        intelligence = 0.75f
                    };
                case Specialization.Fighter:
                    return new CharacterBaseStats
                    {
                        strength = 0.76f,
                        agility = 0.69f,
                        intelligence = 0.05f
                    };
                case Specialization.Hunter:
                    return new CharacterBaseStats
                    {
                        strength = 0.13f,
                        agility = 0.75f,
                        intelligence = 0.62f
                    };
                case Specialization.Illusionist:
                    return new CharacterBaseStats
                    {
                        strength = 0.14f,
                        agility = 0.89f,
                        intelligence = 0.47f
                    };
                case Specialization.Monk:
                    return new CharacterBaseStats
                    {
                        strength = 0.50f,
                        agility = 0.50f,
                        intelligence = 0.50f
                    };
                case Specialization.Paladin:
                    return new CharacterBaseStats
                    {
                        strength = 0.70f,
                        agility = 0.63f,
                        intelligence = 0.17f
                    };
                case Specialization.Ranger:
                    return new CharacterBaseStats
                    {
                        strength = 0.29f,
                        agility = 0.82f,
                        intelligence = 0.39f
                    };
                case Specialization.Rogue:
                    return new CharacterBaseStats
                    {
                        strength = 0.20f,
                        agility = 0.66f,
                        intelligence = 0.64f
                    };
                case Specialization.Shaman:
                    return new CharacterBaseStats
                    {
                        strength = 0.57f,
                        agility = 0.15f,
                        intelligence = 0.78f
                    };
                case Specialization.Sorcerer:
                    return new CharacterBaseStats
                    {
                        strength = 0.69f,
                        agility = 0.02f,
                        intelligence = 0.79f
                    };
                case Specialization.Tank:
                    return new CharacterBaseStats
                    {
                        strength = 0.83f,
                        agility = 0.57f,
                        intelligence = 0.1f
                    };
                case Specialization.Thief:
                    return new CharacterBaseStats
                    {
                        strength = 0.43f,
                        agility = 0.54f,
                        intelligence = 0.53f
                    };
                case Specialization.Warlock:
                    return new CharacterBaseStats
                    {
                        strength = 0.61f,
                        agility = 0.25f,
                        intelligence = 0.64f
                    };
                case Specialization.Warrior:
                    return new CharacterBaseStats
                    {
                        strength = 0.68f,
                        agility = 0.56f,
                        intelligence = 0.26f
                    };
                case Specialization.Wizard:
                    return new CharacterBaseStats
                    {
                        strength = 0.62f,
                        agility = 0.06f,
                        intelligence = 0.82f
                    };
                case Specialization.Default:
                    return new CharacterBaseStats
                    {
                        strength = 0.5f,
                        agility = 0.5f,
                        intelligence = 0.5f
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(spec), spec, null);
            }
        }

        public static CharacterBaseStats GetBaseStats(CharacterType type, Specialization spec)
        {
            return GetBaseStats(type) + GetBaseStats(spec);
        }

        public static CharacterExtraStats GetExtraStats(CharacterType characterType, CharacterBaseStats baseStats)
        {
            var strength = baseStats.strength;
            var agility = baseStats.agility;
            var intelligence = baseStats.intelligence;

            switch (characterType)
            {
                case CharacterType.Cleopatra:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        AvoidChanceArgAg = agility * 1.1f,
                        ManaPoolArgIn = intelligence * 1.1f
                    });
                case CharacterType.Ramzes:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgSt = strength * 1.1f,
                        MagicResistanceArgSt = strength * 1.1f
                    });
                case CharacterType.Garruk:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgSt = strength * 1.05f,
                        MoveSpeedArgAg = agility * 1.15f
                    });
                case CharacterType.Bagrak:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgIn = intelligence * 1.15f,
                        DamagePerHeatArgAg = agility * 1.15f
                    });
                case CharacterType.Gloin:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgSt = strength * 1.1f,
                        DefenceArgSt = strength * 1.1f
                    });
                case CharacterType.Rock:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgAg = agility * 1.15f,
                        DodgeChanceArgIn = intelligence * 1.15f
                    });
                case CharacterType.Fort:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        AvoidChanceArgSt = strength * 1.05f,
                        HitAccuracyArgSt = strength * 1.05f
                    });
                case CharacterType.Mario:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        RegenerationManaIn = intelligence * 1.2f,
                        HealthArgSt = strength * 1.1f
                    });
                case CharacterType.Freak:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DodgeChanceArgIn = intelligence * 1.15f,
                        HealthArgSt = strength * 1.05f
                    });
                case CharacterType.Peppa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgSt = strength * 1.05f,
                        MagicPowerArgIn = intelligence * 1.15f
                    });
                case CharacterType.Satyr:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HealthArgSt = strength * 1.15f,
                        MagicPowerArgIn = intelligence * 1.1f
                    });
                case CharacterType.Maron:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgAg = agility * 1.05f,
                        HitAccuracyArgSt = strength * 1.1f
                    });
                case CharacterType.Cudgel:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        RegenerationManaIn = intelligence * 1.15f,
                        ManaPoolArgIn = intelligence * 1.15f
                    });
                case CharacterType.Belesar:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicResistanceArgIn = intelligence * 1.2f,
                        RegenerationHealthArgSt = strength * 1.1f
                    });
                case CharacterType.Yama:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DodgeChanceArgIn = intelligence * 1.1f,
                        DefenceArgSt = strength * 1.15f
                    });
                case CharacterType.Shanti:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MoveSpeedArgAg = agility * 1.1f,
                        DamagePerHeatArgAg = agility * 1.1f
                    });
                case CharacterType.Aine:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicAccuracyArgSt = strength * 1.15f,
                        MagicResistanceArgIn = intelligence * 1.1f
                    });
                case CharacterType.Medusa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        MagicAccuracyArgSt = strength * 1.15f,
                        AvoidChanceArgSt = strength * 1.15f
                    });
                case CharacterType.Sazum:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        HitAccuracyArgAg = agility * 1.1f,
                        MagicAccuracyArgSt = strength * 1.15f
                    });
                case CharacterType.Nissa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgAg = agility * 1.05f,
                        HitAccuracyArgAg = agility * 1.05f
                    });
                case CharacterType.Raven:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DamagePerHeatArgSt = strength * 1.1f,
                        HealthArgSt = strength * 1.1f
                    });
                case CharacterType.Default:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence));
                default:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence));
            }
        }

        public static CharacterExtraStats GetExtraStats(Specialization spec, CharacterBaseStats baseStats)
        {
            var strength = baseStats.strength;
            var agility = baseStats.agility;
            var intelligence = baseStats.intelligence;

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
                        AvoidChanceArgAg = agility * 1.15f,
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
                        MoveSpeedArgAg = agility * 1.05f,
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
                case Specialization.Default:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence));
                default:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence));
            }
        }

        public static CharacterExtraStats GetExtraStats(CharacterType type, Specialization spec, CharacterBaseStats baseStats)
        {
            return GetExtraStats(type, baseStats) + GetExtraStats(spec, baseStats);
        }

        public static MainCharacterParameter GetMainCharacterParameter(CharacterType type)
        {
            switch (type)
            {
                case CharacterType.Cleopatra:
                    return MainCharacterParameter.Intelligence;
                case CharacterType.Ramzes:
                    return MainCharacterParameter.Agility;
                case CharacterType.Garruk:
                    return MainCharacterParameter.Strength;
                case CharacterType.Bagrak:
                    return MainCharacterParameter.Strength;
                case CharacterType.Gloin:
                    return MainCharacterParameter.Agility;
                case CharacterType.Rock:
                    return MainCharacterParameter.Strength;
                case CharacterType.Fort:
                    return MainCharacterParameter.Strength;
                case CharacterType.Mario:
                    return MainCharacterParameter.Agility;
                case CharacterType.Freak:
                    return MainCharacterParameter.Strength;
                case CharacterType.Peppa:
                    return MainCharacterParameter.Strength;
                case CharacterType.Satyr:
                    return MainCharacterParameter.Intelligence;
                case CharacterType.Maron:
                    return MainCharacterParameter.Agility;
                case CharacterType.Cudgel:
                    return MainCharacterParameter.Strength;
                case CharacterType.Belesar:
                    return MainCharacterParameter.Agility;
                case CharacterType.Yama:
                    return MainCharacterParameter.Intelligence;
                case CharacterType.Shanti:
                    return MainCharacterParameter.Intelligence;
                case CharacterType.Aine:
                    return MainCharacterParameter.Intelligence;
                case CharacterType.Medusa:
                    return MainCharacterParameter.Intelligence;
                case CharacterType.Sazum:
                    return MainCharacterParameter.Intelligence;
                case CharacterType.Nissa:
                    return MainCharacterParameter.Agility;
                case CharacterType.Raven:
                    return MainCharacterParameter.Agility;
                case CharacterType.Default:
                    return MainCharacterParameter.Strength;
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
                case Specialization.Default:
                    return MainCharacterParameter.Strength;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spec), spec, null);
            }
        }

        public static float[] GetStatsCollection(CharacterExtraStats stats)
        {
            return new[]
            {
                stats.attackRange,
                stats.attackSpeed,
                stats.avoidChance,
                stats.damagePerHeat,
                stats.defence,
                stats.dodgeChance,
                stats.health,
                stats.hitAccuracy,
                stats.magicAccuracy,
                stats.magicPower,
                stats.magicResistance,
                stats.manaPool,
                stats.moveSpeed,
                stats.regenerationHealth,
                stats.regenerationMana
            };
        }

        private static readonly CharacterBaseStats BaseStats = new CharacterBaseStats
            {strength = 10, agility = 10, intelligence = 10};

        

        public static List<string> GetCharacterFeatures(CharacterType type)
        {
            var typeStats = GetExtraStats(type, BaseStats); //must be there
            typeStats = GetExtraStats(type, BaseStats);
            var defaultStats = GetExtraStats(CharacterType.Default, BaseStats);
            var typeStatsColl = GetStatsCollection(typeStats);
            var defaultStatsColl = GetStatsCollection(defaultStats);

            return GetFeaturesCollection(typeStatsColl, defaultStatsColl);
        }

        public static List<string> GetCharacterFeatures(Specialization spec)
        {
            var specStats = GetExtraStats(spec, BaseStats); //must be there
            specStats = GetExtraStats(spec, BaseStats);
            var defaultStats = GetExtraStats(Specialization.Default, BaseStats);
            var specStatsColl = GetStatsCollection(specStats);
            var defaultStatsColl = GetStatsCollection(defaultStats);

            return GetFeaturesCollection(specStatsColl, defaultStatsColl);
        }

        public static List<string> GetCharacterFeatures(CharacterType type, Specialization spec)
        {
            var currentStats = GetExtraStats(type, spec, BaseStats); //must be there
            currentStats = GetExtraStats(type, spec, BaseStats);
            var defaultStats = GetExtraStats(CharacterType.Default, Specialization.Default,
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
                "MagicPower",
                "MagicResistance",
                "ManaPool",
                "MoveSpeed",
                "RegenerationHealth",
                "RegenerationMana"
            };
        }
    }
}