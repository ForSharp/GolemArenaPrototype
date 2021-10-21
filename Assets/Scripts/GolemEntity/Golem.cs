﻿using System.Collections.Generic;
using GolemEntity.BaseStats;
using GolemEntity.ExtraStats;

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

        public Golem(GolemType golemType, Specialization specialization)
        {
            _golemType = golemType;
            _specialization = specialization;
            
            InitProvider();
            SetRate();
            SetDefaultStatsByRate();
            InitExtraProvider();
        }
        
        public void ChangeBaseStatsProportionallyPermanent(float value)
        {
            var changing = new BaseStatsEditor(value, ParseIStatsToGolemBaseStats(Rate), _provider);
            _provider = changing;
            
            AddPermanentBaseStats(changing);
            
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

        private List<IStatsProvider> _permanentBaseStats = new List<IStatsProvider>();

        public void AddPermanentBaseStats(IStatsProvider provider)
        {
            _permanentBaseStats.Add(provider);
        }

        private List<IExtraStatsProvider> _tempExtraStats = new List<IExtraStatsProvider>();
        
        public void AddTempExtraStats(IExtraStatsProvider provider)
        {
            _tempExtraStats.Add(provider);
        }
        
        public void RemoveTempExtraStats(IExtraStatsProvider provider)
        {
            _tempExtraStats.Remove(provider);
            
            RecalculateExtraStats();
        }

        public void RemoveAllTempExtraStats()
        {
            _tempExtraStats.Clear();
            
            RecalculateExtraStats();
        }
        
        private List<IExtraStatsProvider> _extraStatsByItems = new List<IExtraStatsProvider>();

        public void AddExtraStatsByItems(IExtraStatsProvider provider)
        {
            _extraStatsByItems.Add(provider);
        }
        
        public void RemoveExtraStatsByItems(IExtraStatsProvider provider)
        {
            _extraStatsByItems.Remove(provider);
            
            RecalculateExtraStats();
        }
        
        private void RecalculateExtraStats()
        {
            _extra = new TypeExtraStats(_golemType, GetBaseStats());
            _extra = new SpecializationExtraStats( _specialization, _extra, GetBaseStats());

            foreach (var t in _tempExtraStats)
            {
                _extra = t;
            }

            foreach (var t in _extraStatsByItems)
            {
                _extra = t;
            }
        }

        private void InitProvider()
        {
            IStatsProvider changing = new GolemTypeStats(_golemType);
            _provider = changing;
            AddPermanentBaseStats(changing);
            changing = new SpecializationStats(_provider, _specialization);
            _provider = changing;
            AddPermanentBaseStats(changing);
        }

        private void SetRate()
        {
            Rate = _provider;
        }

        private void SetDefaultStatsByRate()
        {
            var changing = new DefaultStats(MINBaseStats, Rate);
            _provider = changing;
            AddPermanentBaseStats(changing);
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
                AttackRange = extra.GetExtraStats().AttackRange, 
                AttackSpeed = extra.GetExtraStats().AttackSpeed,
                AvoidChance = extra.GetExtraStats().AvoidChance,
                DamagePerHeat = extra.GetExtraStats().DamagePerHeat,
                Defence = extra.GetExtraStats().Defence,
                DodgeChance = extra.GetExtraStats().DodgeChance,
                Health = extra.GetExtraStats().Health,
                HitAccuracy = extra.GetExtraStats().HitAccuracy,
                MagicAccuracy = extra.GetExtraStats().MagicAccuracy,
                MagicPower = extra.GetExtraStats().MagicPower,
                MagicResistance = extra.GetExtraStats().MagicResistance,
                ManaPool = extra.GetExtraStats().ManaPool,
                MoveSpeed = extra.GetExtraStats().MoveSpeed,
                RegenerationHealth = extra.GetExtraStats().RegenerationHealth,
                RegenerationMana = extra.GetExtraStats().RegenerationMana,
                RegenerationStamina = extra.GetExtraStats().RegenerationStamina,
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
