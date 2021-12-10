using System;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using Inventory.Abstracts.Spells;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Behaviour.Abstracts
{
    public interface ICastable
    {
        void CastSpell(CharacterState target);

        void SpellConstructor(ISpellItem info, CharacterState character, Animator animator);

    }
}