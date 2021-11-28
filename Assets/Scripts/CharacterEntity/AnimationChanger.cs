using UnityEngine;
using Random = UnityEngine.Random;

namespace CharacterEntity
{
    public static class AnimationChanger 
    {
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Kick = Animator.StringToHash("Kick");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int SuperAttack = Animator.StringToHash("SuperAttack");
        private static readonly int AvoidHit = Animator.StringToHash("AvoidHit");
        private static readonly int GetHit = Animator.StringToHash("GetHit");
        private static readonly int SwordAttack = Animator.StringToHash("SwordAttack");

        private const int KickAnimationAmount = 31; 
        private const int SwordHitAnimationAmount = 16; 
        private const int DeathAnimationAmount = 12;
        private const int FightIdleAnimationAmount = 5;
        private const int AvoidHitAnimationAmount = 17; 
        private const int GetHitAnimationAmount = 23; 
        
        private const int SuperAttackAnimationAmount = 15; 
        
        public static void SetWalkingFight(Animator animator)
        {
            animator.Play("Walking");
        }

        public static void SetSomersault(Animator animator)
        {
            animator.Play("StandToRoll");
            animator.applyRootMotion = true;
        }

        public static void SetCastFireBall(Animator animator)
        {
            animator.SetTrigger("CastSpell");
            animator.SetFloat(Animator.StringToHash("CastSpellVariation"), 0);
        }
        
        public static void SetSwordAttack(Animator animator)
        {
            animator.SetTrigger(SwordAttack);
            animator.SetFloat(Animator.StringToHash("SwordAttackVariation"), Random.Range(0, SwordHitAnimationAmount));

        }
        
        public static void SetAvoidHit(Animator animator)
        {
            animator.SetTrigger(AvoidHit);
            animator.SetFloat(Animator.StringToHash("AvoidHitVariation"), Random.Range(0, AvoidHitAnimationAmount));
        }
        
        public static void SetGetHit(Animator animator)
        {
            animator.SetTrigger(GetHit);
            animator.SetFloat(Animator.StringToHash("GetHitVariation"), Random.Range(0, GetHitAnimationAmount));
        }

        public static void SetKickAttack(Animator animator)
        {
            animator.SetTrigger(Kick);
            animator.SetFloat(Animator.StringToHash("KickVariation"), Random.Range(0, KickAnimationAmount));
        }

        public static void SetHitAttack(Animator animator)
        {
            var blendTreeStages = GetBlendTreeStages(KickAnimationAmount);
            animator.SetTrigger(Hit);
            animator.SetFloat(Animator.StringToHash("HitVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
        }

        public static void SetSuperAttack(Animator animator)
        {
            var blendTreeStages = GetBlendTreeStages(SuperAttackAnimationAmount);
            animator.SetTrigger(SuperAttack);
            animator.SetFloat(Animator.StringToHash("SuperAttackVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
        }
        
        private static float[] GetBlendTreeStages(int nums)
        {
            float[] blendTreeStages = new float[nums];
            blendTreeStages[0] = 0;
            float step = 1.0f / (nums - 1);
            for (int i = 1; i < blendTreeStages.Length; i++)
            {
                blendTreeStages[i] = step * i;
            }

            return blendTreeStages;
        }

        public static void SetFightIdle(Animator animator, bool variation)
        {
            animator.SetFloat(Animator.StringToHash("Forward"), 0f);
            if (variation)
            {
                animator.SetFloat(Animator.StringToHash("FightIdleVariation"), Random.Range(0, FightIdleAnimationAmount));
            }
        }
        public static void SetFightIdle(Animator animator)
        {
            animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }

        public static void SetGolemWalk(Animator animator)
        {
            animator.SetFloat(Animator.StringToHash("Forward"), 0.5f);
        }

        public static void SetGolemRun(Animator animator)
        {
            animator.SetFloat(Animator.StringToHash("Forward"), 1f);
        }

        public static void SetGolemDie(Animator animator)
        {
            animator.SetTrigger(Dead);
            animator.SetFloat(Animator.StringToHash("DeathVariation"), Random.Range(0, DeathAnimationAmount));
        }
    }
}
