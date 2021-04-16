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
                        Strength = 0.8f,
                        Agility = 0.2f,
                        Intelligence = 0.1f
                    };
                case GolemType.StoneGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                case GolemType.FleshGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                case GolemType.GlassGolem:
                    return new GolemBaseStats()
                    {
                        
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
                        
                    };
                case GolemType.CoralGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                case GolemType.SandGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                case GolemType.ChitinGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                case GolemType.WoodenGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                case GolemType.DemonFleshGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                case GolemType.IceGolem:
                    return new GolemBaseStats()
                    {
                        
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
