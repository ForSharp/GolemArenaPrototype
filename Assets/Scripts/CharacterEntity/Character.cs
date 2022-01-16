using System.Collections.Generic;
using CharacterEntity.BaseStats;
using CharacterEntity.BaseStats.Effects;
using CharacterEntity.ExtraStats;
using CharacterEntity.ExtraStats.Effects;

namespace CharacterEntity
{
    public class Character 
    {
        private readonly CharacterType _characterType;
        private readonly Specialization _specialization;

        private IStatsProvider _provider;
        private IExtraStatsProvider _extra;
        private const float MINBaseStats = 100;

        private IStatsProvider Rate { get; set; }

        public Character(CharacterType characterType, Specialization specialization)
        {
            _characterType = characterType;
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

            RecalculateExtraStats();
        }

        public void ChangeBaseStatsProportionallyPermanent(CharacterBaseStats stats)
        {
            var changing = new BaseStatsMultiplierChanger(stats, ParseIStatsToGolemBaseStats(Rate), _provider);
            _provider = changing;
            
            RecalculateExtraStats();
        }

        public void ChangeBaseStatsFlatPermanent(CharacterBaseStats stats)
        {
            var changing = new BaseStatsFlatChanger(stats, _provider);
            _provider = changing;
            
            RecalculateExtraStats();
        }
        
        public void ChangeBaseStatsUltimatePermanent(CharacterBaseStats stats)
        {
            var changing = new BaseStatsUltimateChanger(stats, _provider);
            _provider = changing;
            
            RecalculateExtraStats();
        }

        public CharacterBaseStats GetBaseStats()
        {
            return ParseIStatsToGolemBaseStats(_provider);
        }

        public CharacterExtraStats GetExtraStats()
        {
            return ParseIExtraStatsToGolemExtraStats(_extra);
        }
        

        private readonly List<ExtraStatsParameter[]> _tempExtraStats = new List<ExtraStatsParameter[]>();
        
        public void AddTempExtraStats(ExtraStatsParameter[] parameters)
        {
            _tempExtraStats.Add(parameters);
            
            RecalculateExtraStats();
        }
        
        public void RemoveTempExtraStats(ExtraStatsParameter[] parameters)
        {
            _tempExtraStats.Remove(parameters);
            
            RecalculateExtraStats();
        }

        public void RemoveAllTempExtraStats()
        {
            _tempExtraStats.Clear();
            
            RecalculateExtraStats();
        }
        
        private readonly List<ExtraStatsParameter[]> _extraStatsByItems = new List<ExtraStatsParameter[]>();

        public void AddExtraStatsByItems(ExtraStatsParameter[] parameters)
        {
            _extraStatsByItems.Add(parameters);

            RecalculateExtraStats();
        }
        
        public void RemoveExtraStatsByItems(ExtraStatsParameter[] parameters)
        {
            _extraStatsByItems.Remove(parameters);
            
            RecalculateExtraStats();
        }
        
        private void RecalculateExtraStats()
        {
            _extra = new TypeExtraStats(_characterType, GetBaseStats());
            _extra = new SpecializationExtraStats( _specialization, _extra, GetBaseStats());

            foreach (var t in _extraStatsByItems)
            {
                _extra = new ExtraStatsChanger(_extra, GetBaseStats(), t);
            }

            foreach (var t in _tempExtraStats)
            {
                
                _extra = new ExtraStatsChanger(_extra, GetBaseStats(), t);
            }
        }

        private void InitProvider()
        {
            IStatsProvider changing = new CharacterTypeStats(_characterType);
            _provider = changing;
            changing = new SpecializationStats(_provider, _specialization);
            _provider = changing;
        }

        private void SetRate()
        {
            Rate = _provider;
        }

        private void SetDefaultStatsByRate()
        {
            var changing = new DefaultStats(MINBaseStats, Rate);
            _provider = changing;
        }

        private void InitExtraProvider()
        {
            _extra = new TypeExtraStats(_characterType, GetBaseStats());
            _extra = new SpecializationExtraStats( _specialization, _extra, GetBaseStats());
        }

        private static CharacterBaseStats ParseIStatsToGolemBaseStats(IStatsProvider provider)
        {
            return new CharacterBaseStats()
            {
                strength = provider.GetBaseStats().strength,
                agility = provider.GetBaseStats().agility,
                intelligence = provider.GetBaseStats().intelligence
            };
        }

        private static CharacterExtraStats ParseIExtraStatsToGolemExtraStats(IExtraStatsProvider extra)
        {
            return new CharacterExtraStats()
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
                regenerationMana = extra.GetExtraStats().regenerationMana
            };
        }

    }

    public class DefaultStats : StatsDecorator
    {
        private readonly float _minBaseStats;

        public DefaultStats(float minBaseStats, IStatsProvider wrappedEntity) : base(wrappedEntity)
        {
            _minBaseStats = minBaseStats;
        }

        protected override CharacterBaseStats GetStatsInternal()
        {
            return WrappedEntity.GetBaseStats() * _minBaseStats;
        }
    }
}
