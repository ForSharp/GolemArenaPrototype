using System;
using UnityEngine;

namespace __Scripts.ExtraStats
{
    public class TypeExtraStats : ExtraStatsDecorator
    {
        private GolemType _golemType;
        
        public TypeExtraStats(IExtraStatsProvider wrappedEntity, GolemType golemType) : base(wrappedEntity)
        {
            _golemType = golemType;
        }

        protected override GolemExtraStats GetExtraStatsInternal(GolemBaseStats baseStats)
        {
            return _wrappedEntity.GetExtraStats(baseStats);
        }

        private GolemExtraStats GetTypeExtraStats(GolemBaseStats baseStats, GolemType type)
        {
            var strength = baseStats.Strength;
            var agility = baseStats.Agility;
            var intelligence = baseStats.Intelligence;
            
            switch (type)
            {
                case GolemType.IronGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.StoneGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.FleshGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.GlassGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.MithrilGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.BoneGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.CoralGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.SandGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.ChitinGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.WoodenGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.DemonFleshGolem:
                    return new GolemExtraStats()
                    {

                    };
                case GolemType.IceGolem:
                    return new GolemExtraStats()
                    {

                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
