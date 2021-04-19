using System;

namespace __Scripts
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
                case GolemType.IronGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.6f,
                        Agility = 0.4f,
                        Intelligence = 0.1f
                    };
                case GolemType.StoneGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.8f,
                        Agility = 0.1f,
                        Intelligence = 0.2f
                    };
                case GolemType.FleshGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.3f,
                        Intelligence = 0.4f
                    };
                case GolemType.GlassGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.2f,
                        Agility = 0.7f,
                        Intelligence = 0.5f
                    };
                case GolemType.MithrilGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.1f,
                        Agility = 0.7f,
                        Intelligence = 0.6f
                    };
                case GolemType.BoneGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.4f,
                        Agility = 0.6f,
                        Intelligence = 0.4f
                    };
                case GolemType.CoralGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.4f,
                        Agility = 0.4f,
                        Intelligence = 0.6f
                    };
                case GolemType.SandGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.6f,
                        Intelligence = 0.2f
                    };
                case GolemType.ChitinGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.7f,
                        Agility = 0.2f,
                        Intelligence = 0.2f
                    };
                case GolemType.WoodenGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.1f,
                        Intelligence = 0.8f
                    };
                case GolemType.DemonFleshGolem:
                    return new GolemBaseStats()
                    {
                        Strength = 0.5f,
                        Agility = 0.8f,
                        Intelligence = 0.3f
                    };
                case GolemType.IceGolem:
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
