using UnityEngine;

namespace __Scripts.CharacterEntity
{
    public static class AnimationChangerCreep
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");

        public static void SetMoving(Animator animator)
        {
            animator.SetBool(IsMoving, true);
        }
        
        public static void SetIdle(Animator animator)
        {
            animator.SetBool(IsMoving, false);
        }
        
        public static void SetAttack(Animator animator)
        {
            animator.SetTrigger(Attack);
        }

        public static void SetDeath(Animator animator)
        {
            animator.SetTrigger(Death);
        }
    }
}