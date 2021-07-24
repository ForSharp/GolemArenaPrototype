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
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Ramzes:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Garruk:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Bagrak:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Gloin:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Rock:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Fort:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Mario:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Freak:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Peppa:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Satyr:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Maron:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Cudgel:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Belesar:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Yama:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Shanti:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Aine:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Medusa:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Sazum:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Nissa:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Raven:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
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
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Bard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.BattleMage:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Cleric:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.DeathKnight:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.DemonHunter:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Druid:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Fighter:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Hunter:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Illusionist:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Monk:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Paladin:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Ranger:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Rogue:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Shaman:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Sorcerer:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Tank:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Thief:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Warlock:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Warrior:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case Specialization.Wizard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
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
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Ramzes:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Garruk:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Bagrak:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Gloin:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Rock:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Fort:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Mario:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Freak:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Peppa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Satyr:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Maron:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Cudgel:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Belesar:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Yama:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Shanti:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Aine:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Medusa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Sazum:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Nissa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Raven:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
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
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Bard:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.BattleMage:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Cleric:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.DeathKnight:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.DemonHunter:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Druid:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Fighter:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Hunter:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Illusionist:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Monk:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Paladin:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Ranger:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Rogue:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Shaman:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Sorcerer:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Tank:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Thief:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Warlock:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Warrior:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case Specialization.Wizard:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
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

        public static List<string> GetCharacterFeatures(GolemType type)
        {
            var baseStats = new GolemBaseStats() {Strength = 10, Agility = 10, Intelligence = 10};
            var typeStats = GetExtraStats(type, baseStats);
            var defaultStats = GetExtraStats(golemType: default, baseStats);
            var typeStatsColl = GetStatsCollection(typeStats);
            var defaultStatsColl = GetStatsCollection(defaultStats);

            return GetFeaturesCollection(typeStatsColl, defaultStatsColl);
        }

        public static List<string> GetCharacterFeatures(Specialization spec)
        {
            var baseStats = new GolemBaseStats() {Strength = 10, Agility = 10, Intelligence = 10};
            var specStats = GetExtraStats(spec, baseStats);
            var defaultStats = GetExtraStats(spec: default, baseStats);
            var specStatsColl = GetStatsCollection(specStats);
            var defaultStatsColl = GetStatsCollection(defaultStats);
            
            return GetFeaturesCollection(specStatsColl, defaultStatsColl);
        }

        public static List<string> GetCharacterFeatures(GolemType type, Specialization spec)
        {
            var baseStats = new GolemBaseStats() {Strength = 10, Agility = 10, Intelligence = 10};
            var currentStats = GetExtraStats(type, spec, baseStats);
            var defaultStats = GetExtraStats(default, default, baseStats);
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
                        ? $"increased by {(int)(currentStatsColl[i] / defaultStatsColl[i] * 100)} %"
                        : $"decreased by {(int)(defaultStatsColl[i] / currentStatsColl[i] * 100)} %";
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