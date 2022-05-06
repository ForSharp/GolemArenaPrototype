﻿using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Behaviour.SpellEffects
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
            _spawner = FindObjectOfType<Spawner>();
            
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