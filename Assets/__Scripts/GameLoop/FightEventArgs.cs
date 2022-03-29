using System;
using __Scripts.CharacterEntity.State;

namespace __Scripts.GameLoop
{
    public class FightEventArgs : EventArgs
    {
        public readonly AttackHitEventArgs AttackHitEventArgs;
        public readonly string Target;
        public readonly bool IsAttackFromBehind;
        public readonly bool IsAvoiding;

        public FightEventArgs(AttackHitEventArgs attackHitEventArgs, string target, bool isAttackFromBehind, bool isAvoiding = false)
        {
            AttackHitEventArgs = attackHitEventArgs;
            Target = target;
            IsAttackFromBehind = isAttackFromBehind;
            IsAvoiding = isAvoiding;
        }
    }
}