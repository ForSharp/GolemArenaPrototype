﻿using System;
using System.Linq;
using __Scripts.Inventory.Abstracts;
using Behaviour.SpellEffects;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using GameLoop;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Behaviour.SpellEffects
{
    public class FireballEffect : MonoBehaviour
    {
        [SerializeField] private GameObject beforeExplosion;
        [SerializeField] private GameObject afterExplosion;
        [SerializeField] private GameObject flame;

        private ChampionState _state;
        private CharacterState _target;
        private float _magicPower;
        private float _magicAccuracy;
        private int _ownerGroupNumber;
        private bool _isExplosionStarted;
        private FireBallItem _info;
        private Vector3 _startPos;
        private Vector3 _endPos;
        private float _movementProgress;
        
        private void Start()
        {
            beforeExplosion.SetActive(true);
            afterExplosion.SetActive(false);
            
            Destroy(gameObject, 10);
        }

        public void Initialize(ChampionState ownerState, CharacterState target, FireBallItem info)
        {
            _magicPower = ownerState.Stats.magicPower;
            _magicAccuracy = ownerState.Stats.magicAccuracy;
            _state = ownerState;
            _ownerGroupNumber = ownerState.Group;
            _info = info;
            _target = target;
            _startPos = transform.position;
            _endPos = target.transform.position;
        }
        
        private void Update()
        {
            if (!_isExplosionStarted)
            {
                transform.position = Vector3.Lerp(_startPos, _endPos, _movementProgress);
                _movementProgress += 0.1f;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
                if (FilterCollidersArray(colliders).Length > 0)
                {
                    _isExplosionStarted = true;
                    beforeExplosion.SetActive(false);
                    foreach (var item in colliders)
                    {
                        TakeExplosionDamage(item);
                        //нанести урон тому в кого попали
                        //урон увеличивается от магической мощи нападающего, уменьшается от магического сопротивления жертвы
                    }

                    afterExplosion.SetActive(true);
                    
                    Collider[] explosionColliders = Physics.OverlapSphere(transform.position, 0.6f);
                    //увеличить сферу, вызвать горение у того, в кого попали. Шанс не 100%,
                    //он высчитывается в зависимости от разности магической точности и магического уклонения
                    foreach (var item in FilterCollidersArray(explosionColliders))
                    {
                        if (item.TryGetComponent(out CharacterState state))
                        {
                            if (state.Group != _ownerGroupNumber)
                            {
                                
                                var flameObj = Instantiate(flame, state.gameObject.transform);
                                var flameEffect = flameObj.GetComponent<FlameEffect>();
                                flameEffect.BurnTarget(_state, state, _info.PeriodicDamageSpellInfo.PeriodicDamagingValue, _info.PeriodicDamageSpellInfo.PeriodicDamageDuration);

                                var inventoryItem = (IInventoryItem)_info;
                                state.OnStateEffectAdded(inventoryItem.Info.SpriteIcon, _info.PeriodicDamageSpellInfo.PeriodicDamageDuration, false, true, inventoryItem.Info.Id);
                                
                                //при поджоге наносить переодический урон
                                //при поджоге добавить эффект горения
                                //урон увеличивается от магической мощи нападающего, уменьшается от магического сопротивления жертвы
                                //поджог должен прекратиться по истечении определенного времени
                            }
                        }
                    }
                    
                }
            }
        }

        private void TakeExplosionDamage(Collider item)
        {
            if (item.TryGetComponent(out CharacterState state))
            {
                if (state.Group != _ownerGroupNumber)
                {
                    //takedamage
                    state.TakeDamage(_info.DamageSpellInfo.DamagingValue, _state.RoundStatistics);
                    EventContainer.OnMagicDamageReceived(_state, state, _info.DamageSpellInfo.DamagingValue, false);
                }
            }
        }

        private Collider[] FilterCollidersArray(Collider[] colliders)
        {

            var filteredGameCharacterColliders = colliders.Where(c =>
                    c.GetComponentInParent<CharacterState>())
                .Where(c => c.GetComponentInParent<CharacterState>().IsDead == false);
            var filteredDestructibleObjects = colliders.Where(c =>
                    c.GetComponentInParent<DestructibleObject>())
                .Where(c => c.GetComponentInParent<DestructibleObject>().IsDead == false);

            var gameCharacterColliders =
                filteredGameCharacterColliders as Collider[] ?? filteredGameCharacterColliders.ToArray();
            var destructibleObjects =
                filteredDestructibleObjects as Collider[] ?? filteredDestructibleObjects.ToArray();

            if (gameCharacterColliders.Length == 0 && destructibleObjects.Length == 0)
            {
                return Array.Empty<Collider>();
            }
            if (gameCharacterColliders.Length == 0)
            {
                return destructibleObjects;
            }
            if (destructibleObjects.Length == 0)
            {
                return gameCharacterColliders;
            }

            var filteredArray = new Collider[gameCharacterColliders.Length +
                                             destructibleObjects.Length];

            gameCharacterColliders.CopyTo(filteredArray, 0);
            destructibleObjects.CopyTo(filteredArray, destructibleObjects.Length);

            return filteredArray;
        }
        
    }
}