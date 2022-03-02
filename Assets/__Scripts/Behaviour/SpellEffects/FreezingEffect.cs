using System;
using System.Collections;
using CharacterEntity.State;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour.SpellEffects
{
    public class FreezingEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip freezeSound;

        private CharacterState _owner;
        private CharacterState _target;
        private FreezingItem _info;
        private Animator _targetAnimator;
        private AudioSource _audioSource;
        private float _targetAnimatorSpeed;

        public void Initialize(CharacterState owner, CharacterState target, FreezingItem info)
        {
            _owner = owner;
            _target = target;
            _info = info;
            _targetAnimator = _target.GetComponent<Animator>();
            _audioSource = _target.GetComponent<AudioSource>();
            _targetAnimatorSpeed = _targetAnimator.speed;
            
            AddEffect(GetDuration(_owner.Stats.magicPower, _target.Stats.magicResistance));
        }

        private void AddEffect(float duration)
        {
            _audioSource.PlayOneShot(freezeSound);

            _targetAnimator.speed = 0;
            var stunId = Guid.NewGuid().ToString();
            _target.OnStunCharacter(stunId);

            var image = _info.Info.SpriteIcon;
            var id = _info.Info.Id;
            _target.OnStateEffectAdded(image, duration,false, true, id);

            StartCoroutine(RemoveEffect(duration, stunId));
        }

        private IEnumerator RemoveEffect(float effectDuration, string stunId)
        {
            yield return new WaitForSeconds(effectDuration);
            
            _targetAnimator.speed = _targetAnimatorSpeed;
            _target.OnEndStunCharacter(stunId);
            Destroy(gameObject);
        }
        
        private float GetDuration(float magicPower, float magicResistance)
        {
            var duration = _info.PolymorphSpellInfo.PolymorphDuration;
            duration += (magicPower - magicResistance);
            if (duration > 5)
                duration = 5;
            if (duration < 1)
                duration = 1;
            return duration;
        }
    }
}