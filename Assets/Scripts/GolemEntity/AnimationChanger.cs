using System;
using UnityEngine;

namespace GolemEntity
{
    public static class AnimationChanger 
    {

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
            animator.SetTrigger("LeftPunch");
        }
        
        public static void SetGolemRightHit(Animator animator)
        {
            animator.SetTrigger("RightPunch");
        }

        public static void SetGolemDoubleHit(Animator animator)
        {
            animator.SetTrigger("DoublePunch");
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
