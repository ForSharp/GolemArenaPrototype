using System;

namespace __Scripts
{
    
    public class SpecializationStats : StatsDecorator
    {
        
        private Specialization _specialization;
        
        public SpecializationStats(IStatsProvider wrappedEntity, Specialization specialization) : base(wrappedEntity)
        {
            _specialization = specialization;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + GetSpecStats(_specialization);
        }

        private GolemBaseStats GetSpecStats(Specialization specialization)
        {
            switch (specialization)
            {
                case Specialization.Warrior:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.3f,
                        Intelligence = 0.2f
                    };
                case Specialization.Rogue:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.7f,
                        Intelligence = 0.3f
                    };
                case Specialization.Wizard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.3f,
                        Agility = 0.2f,
                        Intelligence = 0.8f
                    };
                case Specialization.BattleMage:
                    return new GolemBaseStats()
                    {
                        Strength = 0.4f,
                        Agility = 0.2f,
                        Intelligence = 0.6f
                    };
                case Specialization.Priest:
                    return new GolemBaseStats()
                    {
                        Strength = 0.3f,
                        Agility = 0.4f,
                        Intelligence = 0.7f
                    };
                case Specialization.Paladin:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.2f,
                        Intelligence = 0.3f
                    };
                case Specialization.Bard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.6f,
                        Intelligence = 0.5f
                    };
                case Specialization.Fighter:
                    return new GolemBaseStats()
                    {
                        Strength = 0.4f,
                        Agility = 0.6f,
                        Intelligence = 0.1f
                    };
                case Specialization.Ranger:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.8f,
                        Intelligence = 0.3f
                    };
                case Specialization.SpecialistWizard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.1f,
                        Agility = 0.1f,
                        Intelligence = 1.3f
                    };
                case Specialization.Illusionist:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.4f,
                        Intelligence = 0.7f
                    };
                case Specialization.Cleric:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.4f,
                        Intelligence = 0.3f
                    };
                case Specialization.Druid:
                    return new GolemBaseStats()
                    {
                        Strength = 0.4f,
                        Agility = 0.3f,
                        Intelligence = 0.5f
                    };
                case Specialization.Thief:
                    return new GolemBaseStats()
                    {
                        Strength = 0.3f,
                        Agility = 0.6f,
                        Intelligence = 0.4f
                    };
                case Specialization.Barbarian:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.6f,
                        Intelligence = 0.1f
                    };
                case Specialization.Sorcerer:
                    return new GolemBaseStats()
                    {
                        Strength = 0.3f,
                        Agility = 0.1f,
                        Intelligence = 0.9f
                    };
                case Specialization.Monk:
                    return new GolemBaseStats()
                    {
                        Strength = 0.3f,
                        Agility = 0.3f,
                        Intelligence = 0.6f
                    };
                case Specialization.Tank:
                    return new GolemBaseStats()
                    {
                        Strength = 1.2f,
                        Agility = 0.2f,
                        Intelligence = 0.1f
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(specialization), specialization, null);
            }
        }
    }
}
