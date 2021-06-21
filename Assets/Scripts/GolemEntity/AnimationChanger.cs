using UnityEngine;
using System;

namespace GolemEntity
{
    public static class AnimationChanger 
    {

        public static Action<Animator>[] GetAllAttackAnimations(Animator animator)
        {
            return new Action<Animator>[]
            {
                (SetGolemLeftHit),
                (SetGolemRightHit),
                (SetGolemDoubleHit)
            };
        }
        
        public static Action[] GetAllDeathAnimations(Animator animator)
        {
            return new Action[]
            {
                (() => SetGolemDie(animator))
            };
        }
        
        public static void SetIdle(Animator animator, bool dampTime = false)
        {
            if (dampTime)
            {
                animator.SetFloat(Animator.StringToHash("Forward"), 0f,0.5f, Time.deltaTime);
            }
            
            animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }

        public static void SetGolemWalk(Animator animator, bool dampTime = false)
        {
            if (dampTime)
            {
                animator.SetFloat(Animator.StringToHash("Forward"), 0.5f,0.5f, Time.deltaTime);
            }
            
            animator.SetFloat(Animator.StringToHash("Forward"), 0.5f);
        }

        public static void SetGolemRun(Animator animator)
        {
            animator.SetFloat(Animator.StringToHash("Forward"), 1f);
        }

        public static void SetGolemDie(Animator animator)
        {
            animator.SetTrigger("Die");
        }

        public static void SetGolemLeftHit(Animator animator)
        {
            //1.5
            animator.SetTrigger("LeftPunch");
        }
        
        public static void SetGolemRightHit(Animator animator)
        {
            //1.5
            animator.SetTrigger("RightPunch");
        }

        public static void SetGolemDoubleHit(Animator animator)
        {
            //1.867
            //animator.speed = attackSpeed;
            animator.SetTrigger("DoublePunch");
            //animator.speed = 1;
        }
        
        public static void SetGolemBlock(Animator animator)
        {
            animator.SetBool("Block", true);
        }
        public static void ResetGolemBlock(Animator animator)
        {
            animator.SetBool("Block", false);
        }
    }
}
