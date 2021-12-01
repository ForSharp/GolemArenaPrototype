using System.Collections.Generic;
using Behaviour.Abstracts;
using Inventory;
using Inventory.Abstracts.Spells;
using UnityEngine;

namespace CharacterEntity
{
    public class SpellManager
    {
        private HashSet<ISpellItem> _learnedSpells = new HashSet<ISpellItem>();
        private ICastable _spellFirst;
        private ICastable _spellSecond;
        private ICastable _spellThird;
        private readonly Animator _animator;
        private readonly CharacterState.CharacterState _character;
        
        public bool LearnSpell(ISpellItem spell)//должен сработать при клике на предмет спелл
        {
            if (_learnedSpells.Contains(spell))
            {
                if (spell.SpellInfo.SpellLvl < 3)
                {
                    //spell.SpellInfo.SpellLvl++;
                    return true;
                }

                return false;
            }

            _learnedSpells.Add(spell);
            return true;
        }

        public SpellManager(Animator animator, CharacterState.CharacterState characterState)
        {
            _animator = animator;
            _character = characterState;
        }
        
        public void CastSpellFirst(CharacterState.CharacterState targetState)
        {
            _spellFirst = SpellContainer.Instance.fireballSpell;
            //в конструкторе спелла вместо трансформа цели надо отправлять кэрактерстейт цели
            SpellContainer.Instance.fireballSpell.CustomConstructor(ItemContainer.Instance.GetFireBallLvl1(), targetState.transform,
                _animator, AnimationChanger.SetCastFireBall, _character);
            _spellFirst.CastSpell();
        }

        public void SetupSpellFirst(ISpellItem spell) //экипировать спелл
        {
            _spellFirst = SpellContainer.Instance.GetSpell(spell);
        }

        private void SetSpellFields(ICastable spell)
        {
            //у интерфейса нет метода кастом конструктор
            
        }
    }
}