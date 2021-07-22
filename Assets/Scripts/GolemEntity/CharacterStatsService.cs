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
                case GolemType.AncientQueen:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.AncientWarrior:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.BarbarianGiant:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.BigOrk:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Dwarf:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.ElementalGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.FortGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.MechanicalGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.MutantGuy:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.PigButcher:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.RedDemon:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Slayer:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.Troll:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.DarkElf:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.EvilGod:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.ForestGuardian:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.ForestWitch:
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
                case GolemType.Mystic:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.SpiritDemon:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.UndeadKnight:
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
        
        public static GolemExtraStats GetExtraStats(GolemType golemType, GolemBaseStats baseStats)
        {
            var strength = baseStats.Strength;
            var agility = baseStats.Agility;
            var intelligence = baseStats.Intelligence;

            switch (golemType)
            {
                case GolemType.AncientQueen:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.AncientWarrior:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.BarbarianGiant:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.BigOrk:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Dwarf:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.ElementalGolem:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.FortGolem:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.MechanicalGolem:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.MutantGuy:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.PigButcher:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.RedDemon:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Slayer:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Troll:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.DarkElf:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.EvilGod:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.ForestGuardian:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.ForestWitch:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Medusa:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.Mystic:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.SpiritDemon:
                    return ExtraStatsRate.InitializeExtraStats(new ExtraStatsRate(strength, agility, intelligence)
                    {
                        DefenceArgAg = strength * 1.1f
                    });
                case GolemType.UndeadKnight:
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
        
        public static MainCharacterParameter GetMainCharacterParameter(GolemType type)
        {
            switch (type)
            {
                case GolemType.AncientQueen:
                    return MainCharacterParameter.Intelligence;
                case GolemType.AncientWarrior:
                    return MainCharacterParameter.Stamina;
                case GolemType.BarbarianGiant:
                    return MainCharacterParameter.Strength;
                case GolemType.BigOrk:
                    return MainCharacterParameter.Strength;
                case GolemType.Dwarf:
                    return MainCharacterParameter.Stamina;
                case GolemType.ElementalGolem:
                    return MainCharacterParameter.Strength;
                case GolemType.FortGolem:
                    return MainCharacterParameter.Strength;
                case GolemType.MechanicalGolem:
                    return MainCharacterParameter.Stamina;
                case GolemType.MutantGuy:
                    return MainCharacterParameter.Strength;
                case GolemType.PigButcher:
                    return MainCharacterParameter.Strength;
                case GolemType.RedDemon:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Slayer:
                    return MainCharacterParameter.Stamina;
                case GolemType.Troll:
                    return MainCharacterParameter.Strength;
                case GolemType.DarkElf:
                    return MainCharacterParameter.Stamina;
                case GolemType.EvilGod:
                    return MainCharacterParameter.Intelligence;
                case GolemType.ForestGuardian:
                    return MainCharacterParameter.Intelligence;
                case GolemType.ForestWitch:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Medusa:
                    return MainCharacterParameter.Intelligence;
                case GolemType.Mystic:
                    return MainCharacterParameter.Intelligence;
                case GolemType.SpiritDemon:
                    return MainCharacterParameter.Stamina;
                case GolemType.UndeadKnight:
                    return MainCharacterParameter.Stamina;
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

        private static List<string> GetFeaturesCollection(float[] currentStatsColl, float[] defaultStatsColl)
        {
            var features = new List<string>();
            for (var i = 0; i < currentStatsColl.Length; i++)
            {
                if (Math.Abs(currentStatsColl[i] - defaultStatsColl[i]) > 0.01f)
                {
                    var result = currentStatsColl[i] - defaultStatsColl[i] > 0
                        ? $"increased by {currentStatsColl[i] / defaultStatsColl[i] * 100} %"
                        : $"decreased by {defaultStatsColl[i] / currentStatsColl[i] * 100} %";
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