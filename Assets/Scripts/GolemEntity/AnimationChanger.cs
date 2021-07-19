﻿using UnityEngine;
using Random = UnityEngine.Random;

namespace GolemEntity
{
    public static class AnimationChanger 
    {
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Kick = Animator.StringToHash("Kick");
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int SuperAttack = Animator.StringToHash("SuperAttack");
        private static readonly int AvoidHit = Animator.StringToHash("AvoidHit");
        private static readonly int GetHit = Animator.StringToHash("GetHit");
        private static readonly int TestAttack = Animator.StringToHash("TestAttack");
        private static readonly int SwordAttack = Animator.StringToHash("SwordAttack");

        private const int KickAnimationAmount = 22; //total 29
        private const int HitAnimationAmount = 34; //total 35
        private const int DeathAnimationAmount = 11;
        private const int FightIdleAnimationAmount = 4; //5 in base animator, 4 in sword
        private const int SuperAttackAnimationAmount = 15; //without hurricane kick
        private const int AvoidHitAnimationAmount = 10; 
        private const int GetHitAnimationAmount = 18; 
        
        public static void SetWalkingFight(Animator animator)
        {
            animator.Play("Walking");
        }

        public static void SetTestAttack(Animator animator)
        {
            animator.SetTrigger(TestAttack);
        }

        public static void SetSwordAttack(Animator animator)
        {
            // var blendTreeStages = GetBlendTreeStages(9);
            // animator.SetTrigger(SwordAttack);
            // animator.SetFloat(Animator.StringToHash("SwordAttackVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);

            var numb = Random.Range(1, 10);
            animator.SetTrigger("SwordAttack" + numb);
        }
        
        public static void SetAvoidHit(Animator animator)
        {
            var blendTreeStages = GetBlendTreeStages(AvoidHitAnimationAmount);
            animator.SetTrigger(AvoidHit);
            animator.SetFloat(Animator.StringToHash("AvoidHitVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
        }
        
        public static void SetGetHit(Animator animator)
        {
            var blendTreeStages = GetBlendTreeStages(GetHitAnimationAmount);
            animator.SetTrigger(GetHit);
            animator.SetFloat(Animator.StringToHash("GetHitVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
        }
        
        public static void SetHitAttack(Animator animator)
        {
            var blendTreeStages = GetBlendTreeStages(KickAnimationAmount);
            animator.SetTrigger(Hit);
            animator.SetFloat(Animator.StringToHash("HitVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
        }

        public static void SetKickAttack(Animator animator)
        {
            var blendTreeStages = GetBlendTreeStages(HitAnimationAmount);
            animator.SetTrigger(Kick);
            animator.SetFloat(Animator.StringToHash("KickVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
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
                var blendTreeStages = GetBlendTreeStages(FightIdleAnimationAmount);
                animator.SetFloat(Animator.StringToHash("FightIdleVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
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
            var blendTreeStages = GetBlendTreeStages(DeathAnimationAmount);
            animator.SetFloat(Animator.StringToHash("DeathVariation"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);
        }
    }
}
