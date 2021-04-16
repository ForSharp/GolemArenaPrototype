using System;

namespace __Scripts
{
    public class SpecializationStats : StatsDecorator
    {
        private GolemType _golemType = GolemType.IronGolem;
        private Specialization _specialization = Specialization.Tank;
        
        public SpecializationStats(IStatsProvider wrappedEntity, Specialization specialization) : base(wrappedEntity)
        {
            _specialization = specialization;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return _wrappedEntity.GetBaseStats() + GetSpecStats(_specialization);
        }

        private GolemBaseStats GetSpecStats(Specialization specialization)
        {
            switch (specialization)
            {
                case Specialization.Warrior:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.3f,
                        Intelligence = 0.2f
                    };
                case Specialization.Rogue:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.6f,
                        Intelligence = 0.3f
                    };
                case Specialization.Wizard:
                    return new GolemBaseStats()
                    {
                        Strength = 0.3f,
                        Agility = 0.1f,
                        Intelligence = 0.8f
                    };
                case Specialization.BattleMage:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Priest:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Paladin:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Bard:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Fighter:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Ranger:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.SpecialistWizard:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Illusionist:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Cleric:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Druid:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Thief:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Barbarian:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Sorcerer:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Monk:
                    return new GolemBaseStats()
                    {
                        
                    };
                case Specialization.Tank:
                    return new GolemBaseStats()
                    {
                        
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(specialization), specialization, null);
            }
        }
    }
}
