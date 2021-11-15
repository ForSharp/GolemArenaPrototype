using System.Collections.Generic;
using GolemEntity.BaseStats;
using GolemEntity.BaseStats.Effects;
using GolemEntity.ExtraStats;
using GolemEntity.ExtraStats.Effects;

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
            var changing = new BaseStatsIdenticalMultiplierChanger(value, ParseIStatsToGolemBaseStats(Rate), _provider);
            _provider = changing;
            
            AddPermanentBaseStats(changing);
            
            RecalculateExtraStats();
        }

        public void ChangeBaseStatsProportionallyPermanent(GolemBaseStats stats)
        {
            
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

        public void AddExtraStatsByItems(ExtraStatsParameter[] parameters)
        {
            ExtraStatsChanger changer = new ExtraStatsChanger(_extra, GetBaseStats(), parameters);

            _extraStatsByItems.Add(changer);
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
                strength = provider.GetBaseStats().strength,
                agility = provider.GetBaseStats().agility,
                intelligence = provider.GetBaseStats().intelligence
            };
        }

        private static GolemExtraStats ParseIExtraStatsToGolemExtraStats(IExtraStatsProvider extra)
        {
            return new GolemExtraStats()
            {
                attackRange = extra.GetExtraStats().attackRange, 
                attackSpeed = extra.GetExtraStats().attackSpeed,
                avoidChance = extra.GetExtraStats().avoidChance,
                damagePerHeat = extra.GetExtraStats().damagePerHeat,
                defence = extra.GetExtraStats().defence,
                dodgeChance = extra.GetExtraStats().dodgeChance,
                health = extra.GetExtraStats().health,
                hitAccuracy = extra.GetExtraStats().hitAccuracy,
                magicAccuracy = extra.GetExtraStats().magicAccuracy,
                magicPower = extra.GetExtraStats().magicPower,
                magicResistance = extra.GetExtraStats().magicResistance,
                manaPool = extra.GetExtraStats().manaPool,
                moveSpeed = extra.GetExtraStats().moveSpeed,
                regenerationHealth = extra.GetExtraStats().regenerationHealth,
                regenerationMana = extra.GetExtraStats().regenerationMana,
                regenerationStamina = extra.GetExtraStats().regenerationStamina,
                stamina = extra.GetExtraStats().stamina
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
