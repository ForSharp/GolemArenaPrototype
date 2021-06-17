using System;
using Scripts;

namespace GolemEntity.BaseStats
{
    public class GolemTypeStats : IStatsProvider
    {
        private readonly GolemType _golemType;

        public GolemTypeStats(GolemType golemType)
        {
            _golemType = golemType;
        }
        
        public GolemBaseStats GetBaseStats()
        {
            switch (_golemType)
            {
                case GolemType.WaterGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.AirGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.8f,
                        Agility = 0.1f,
                        Intelligence = 0.2f
                    };
                case GolemType.CrystalGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.3f,
                        Intelligence = 0.4f
                    };
                case GolemType.FireGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.7f,
                        Intelligence = 0.5f
                    };
                case GolemType.PlasmaGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.1f,
                        Agility = 0.7f,
                        Intelligence = 0.6f
                    };
                case GolemType.SteamGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.4f,
                        Agility = 0.6f,
                        Intelligence = 0.4f
                    };
                case GolemType.DarkGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.4f,
                        Agility = 0.4f,
                        Intelligence = 0.6f
                    };
                case GolemType.NatureGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.6f,
                        Intelligence = 0.2f
                    };
                case GolemType.FogGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.7f,
                        Agility = 0.2f,
                        Intelligence = 0.2f
                    };
                case GolemType.ObsidianGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.1f,
                        Intelligence = 0.8f
                    };
                case GolemType.InsectGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.8f,
                        Intelligence = 0.3f
                    };
                case GolemType.StalagmiteGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.3f,
                        Intelligence = 0.2f
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
