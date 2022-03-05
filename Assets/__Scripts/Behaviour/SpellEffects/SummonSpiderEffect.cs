using __Scripts.GameLoop;
using CharacterEntity;
using CharacterEntity.State;
using GameLoop;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour.SpellEffects
{
    public class SummonSpiderEffect : MonoBehaviour
    {
        [SerializeField] private GameObject spiderPrefab;
        private Spawner _spawner;

        private SummonSpiderItem _info;
        private ChampionState _owner;
        private CharacterState _target;

        public void Initialize(SummonSpiderItem info, ChampionState owner, CharacterState target)
        {
            _info = info;
            _owner = owner;
            _target = target;
            _spawner = GameObject.Find("Tester").GetComponent<Spawner>();
            
            SummonSpider();
        }

        private void SummonSpider()
        {
            _spawner.SpawnCreep(spiderPrefab, CreepType.Spider, _info.SummonSpellInfo.SummonStats, _owner,
                _target.transform.position, _info.SummonSpellInfo.SummonDuration);
            
            Destroy(gameObject, 2);
        }
    }
}