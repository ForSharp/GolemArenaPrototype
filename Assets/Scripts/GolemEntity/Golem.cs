using System;
using System.Threading;
using System.Threading.Tasks;
using __Scripts.ExtraStats;
using __Scripts.GolemEntity.ExtraStats;
using UnityEngine;

namespace __Scripts.GolemEntity
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
        
        public async void ChangeBaseStatsProportionally(float value)
        {
            _provider = new BaseStatsEditor(value, ParseIStatsToGolemBaseStats(Rate), _provider);
            
            await RecalculateExtraStats();
        }

        private GolemBaseStats GetBaseStats()
        {
            return ParseIStatsToGolemBaseStats(_provider);
        }

        private GolemExtraStats GetExtraStats()
        {
            return ParseIExtraStatsToGolemExtraStats(_extra);
        }

        // private async Task<GolemExtraStats> GetExtraStats()
        // {
        //     GolemExtraStats result = default;
        //     await Task.Run( () => result = ParseIExtraStatsToGolemExtraStats(_extra));
        //     return result;
        // }

        private async Task RecalculateExtraStats()
        {
            await Task.Run(() =>
            {
                //mustn't nullify because there may be other modifiers
                //_extra = null;
                
                //Thread.Sleep(20);
                
                //_extra = new TypeExtraStats(_golemType, GetBaseStats() * 0);
                //_extra = new SpecializationExtraStats( _specialization, _extra, GetBaseStats() * 0); 
                _extra = new TypeExtraStats(_golemType, GetBaseStats());
                _extra = new SpecializationExtraStats( _specialization, _extra, GetBaseStats()); 
            });
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
            //Thread.Sleep(10);
            
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
            //GolemExtraStats temp = new GolemExtraStats();

            // temp.AttackSpeed = extra.GetExtraStats().AttackSpeed;
            // temp.AvoidChance = extra.GetExtraStats().AvoidChance;
            // temp.DamagePerHeat = extra.GetExtraStats().DamagePerHeat;
            // temp.Defence = extra.GetExtraStats().Defence;
            // temp.DodgeChance = extra.GetExtraStats().DodgeChance;
            // temp.Health = extra.GetExtraStats().Health;
            // temp.HitAccuracy = extra.GetExtraStats().HitAccuracy;
            // temp.MagicAccuracy = extra.GetExtraStats().MagicAccuracy;
            // temp.MagicDamage = extra.GetExtraStats().MagicDamage;
            // temp.MagicResistance = extra.GetExtraStats().MagicResistance;
            // temp.ManaPool = extra.GetExtraStats().ManaPool;
            // temp.MoveSpeed = extra.GetExtraStats().MoveSpeed;
            // temp.RegenerationRate = extra.GetExtraStats().RegenerationRate;
            // temp.Stamina = extra.GetExtraStats().Stamina;
            // return temp;
            
            return new GolemExtraStats()
            {
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

        public async void ShowGolemBaseStats()
        {
            //Thread.Sleep(20);
            await Task.Run(() =>
            {
                Debug.Log($"Async: {GetBaseStats().ToString()}");
            });
        }
        
        public async void ShowGolemExtraStats()
        {
            //Thread.Sleep(20);
            await Task.Run(() =>
            {
                Debug.Log($"Async: {GetExtraStats().ToString()}");
            });
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
