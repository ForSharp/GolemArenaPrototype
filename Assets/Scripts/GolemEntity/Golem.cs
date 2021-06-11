using System;
using __Scripts;
using __Scripts.ExtraStats;
using __Scripts.GolemEntity.ExtraStats;
using GolemEntity.ExtraStats;
using UnityEngine;

namespace GolemEntity
{
    public class Golem 
    {
        private readonly GolemType _golemType;
        private readonly Specialization _specialization;

        private IStatsProvider _provider;
        private IExtraStatsProvider _extra;
        private const float MINBaseStats = 100;

        private IStatsProvider Rate { get; set; }

        private void Start()
        {
            
        }

        public Golem(GolemType golemType, Specialization specialization)
        {
            _golemType = golemType;
            _specialization = specialization;
            
            InitProvider();
            SetRate();
            SetDefaultStatsByRate();
            InitExtraProvider();
        }
        
        public void ChangeBaseStatsProportionally(float value)
        {
            _provider = new BaseStatsEditor(value, ParseIStatsToGolemBaseStats(Rate), _provider);
            
            RecalculateExtraStats();
        }

        public GolemBaseStats GetBaseStats()
        {
            return ParseIStatsToGolemBaseStats(_provider);
        }

        public GolemExtraStats GetExtraStats()
        {
            return ParseIExtraStatsToGolemExtraStats(_extra);
        }
        
        private void RecalculateExtraStats()
        {
            _extra = new TypeExtraStats(_golemType, GetBaseStats());
            _extra = new SpecializationExtraStats( _specialization, _extra, GetBaseStats()); 
            
        }

        private void InitProvider()
        {
            _provider = new GolemTypeStats(_golemType); 
            _provider = new SpecializationStats(_provider, _specialization);
        }

        private void SetRate()
        {
            Rate = _provider;
        }

        private void SetDefaultStatsByRate()
        {
            _provider = new DefaultStats(MINBaseStats, Rate);
        }

        private void InitExtraProvider()
        {
            
            _extra = new TypeExtraStats(_golemType, GetBaseStats());
            _extra = new SpecializationExtraStats( _specialization, _extra, GetBaseStats());
        }

        private static GolemBaseStats ParseIStatsToGolemBaseStats(IStatsProvider provider)
        {
            return new GolemBaseStats()
            {
                Strength = provider.GetBaseStats().Strength,
                Agility = provider.GetBaseStats().Agility,
                Intelligence = provider.GetBaseStats().Intelligence
            };
        }

        private static GolemExtraStats ParseIExtraStatsToGolemExtraStats(IExtraStatsProvider extra)
        {
            return new GolemExtraStats()
            {
                AttackRange = extra.GetExtraStats().AttackRange + 1.55f, //NOTE! must be 1st here. Must be immutable
                AttackSpeed = extra.GetExtraStats().AttackSpeed,
                AvoidChance = extra.GetExtraStats().AvoidChance,
                DamagePerHeat = extra.GetExtraStats().DamagePerHeat,
                Defence = extra.GetExtraStats().Defence,
                DodgeChance = extra.GetExtraStats().DodgeChance,
                Health = extra.GetExtraStats().Health,
                HitAccuracy = extra.GetExtraStats().HitAccuracy,
                MagicAccuracy = extra.GetExtraStats().MagicAccuracy,
                MagicDamage = extra.GetExtraStats().MagicDamage,
                MagicResistance = extra.GetExtraStats().MagicResistance,
                ManaPool = extra.GetExtraStats().ManaPool,
                MoveSpeed = extra.GetExtraStats().MoveSpeed,
                RegenerationRate = extra.GetExtraStats().RegenerationRate,
                Stamina = extra.GetExtraStats().Stamina
            };
        }

        public string GetGolemBaseStats()
        {
            return GetBaseStats().ToString();
        }
        
        public string GetGolemExtraStats()
        {
            return GetExtraStats().ToString();
        }
    }

    public class BaseStatsEditor : StatsDecorator
    {
        private readonly float _value;
        private readonly GolemBaseStats _multiplier;
        public BaseStatsEditor(float value, GolemBaseStats multiplier, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _value = value;
            _multiplier = multiplier;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() + (_multiplier * _value);
        }
    }

    public class DefaultStats : StatsDecorator
    {
        private readonly float _minBaseStats;

        public DefaultStats(float minBaseStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _minBaseStats = minBaseStats;
        }

        protected override GolemBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() * _minBaseStats;
        }
    }
}
