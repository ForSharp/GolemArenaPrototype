using System;
using System.Linq;
using BehaviourStrategy.Abstracts;
using FightState;
using GameLoop;
using UnityEngine;

namespace BehaviourStrategy.SpellEffects
{
    public class FireballEffect : MonoBehaviour
    {
        [SerializeField] private GameObject beforeExplosion;
        [SerializeField] private GameObject afterExplosion;
        [SerializeField] private GameObject flame;

        private GameCharacterState _state;
        private float _magicPower;
        private float _magicAccuracy;
        private int _ownerGroupNumber;
        private bool _isExplosionStarted;
        private ISpellInfo _info;
        
        private void Start()
        {
            beforeExplosion.SetActive(true);
            afterExplosion.SetActive(false);
            
            Destroy(gameObject, 10);
        }

        public void CustomConstructor(GameCharacterState ownerState, ISpellInfo info)
        {
            _magicPower = ownerState.Stats.MagicPower;
            _magicAccuracy = ownerState.Stats.MagicAccuracy;
            _state = ownerState;
            _ownerGroupNumber = ownerState.Group;
            _info = info;
        }
        
        private void Update()
        {
            if (!_isExplosionStarted)
            {
                transform.position += Vector3.forward * Time.deltaTime * 5;
                
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
                        if (item.TryGetComponent(out GameCharacterState state))
                        {
                            if (state.Group != _ownerGroupNumber)
                            {
                                
                                var flameObj = Instantiate(flame, state.gameObject.transform);
                                var flameEffect = flameObj.GetComponent<FlameEffect>();
                                flameEffect.BurnTarget(_state, state, _info.PeriodicDamage, _info.EffectDuration);
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
            if (item.TryGetComponent(out GameCharacterState state))
            {
                if (state.Group != _ownerGroupNumber)
                {
                    //takedamage
                    state.TakeDamage(_info.Damage, _state.RoundStatistics);
                    EventContainer.OnMagicDamageReceived(_state, state, _info.Damage, false);
                }
            }
        }

        private Collider[] FilterCollidersArray(Collider[] colliders)
        {
            
            // var filteredDestructibleObjects = colliders.Where(c =>
            //         c.GetComponentInParent<DestructibleObject>())
            //     .Where(c => c.GetComponentInParent<DestructibleObject>().IsDead == false);
            //
            //
            // var destructibleObjects =
            //     filteredDestructibleObjects as Collider[] ?? filteredDestructibleObjects.ToArray();
            //
            //
            //
            // return destructibleObjects;
            
            var filteredGameCharacterColliders = colliders.Where(c =>
                    c.GetComponentInParent<GameCharacterState>())
                .Where(c => c.GetComponentInParent<GameCharacterState>().IsDead == false);
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