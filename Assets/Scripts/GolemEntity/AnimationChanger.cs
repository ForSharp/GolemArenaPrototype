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
                (SetGolemLeftHit1),
                (SetGolemLeftHit2),
                (SetGolemLeftHit3),
                (SetGolemRightHit),
                (SetGolemRightHit1),
                (SetGolemRightHit2),
                (SetGolemRightHit3),
                (SetGolemDoubleHit),
                (SetGolemLeftKick1),
                (SetGolemLeftKick2),
                (SetGolemRightKick1),
                (SetGolemRightKick2)
            };
        }
        
        public static Action[] GetAllDeathAnimations(Animator animator)
        {
            return new Action[]
            {
                (() => SetGolemDie(animator)),
                (() => SetGolemDieUnarmed(animator)),
                (() => SetGolemDieStaff(animator)),
                (() => SetGolemDieShield(animator))
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
        public static void SetGolemDieUnarmed(Animator animator)
        {
            animator.SetTrigger("Unarmed-Death1");
        }
        public static void SetGolemDieStaff(Animator animator)
        {
            animator.SetTrigger("Staff-Death1");
        }
        public static void SetGolemDieShield(Animator animator)
        {
            animator.SetTrigger("Shield-Death1");
        }
        
        public static void SetGolemLeftHit(Animator animator)
        {
            //1.5
            animator.SetTrigger("LeftPunch");
        }
        public static void SetGolemLeftHit1(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-L1");
        }
        public static void SetGolemLeftHit2(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-L2");
        }
        public static void SetGolemLeftHit3(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-L3");
        }
        public static void SetGolemRightHit(Animator animator)
        {
            //1.5
            animator.SetTrigger("RightPunch");
        }
        public static void SetGolemRightHit1(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-R1");
        }
        public static void SetGolemRightHit2(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-R2");
        }
        public static void SetGolemRightHit3(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-R3");
        }

        public static void SetGolemDoubleHit(Animator animator)
        {
            //1.867
            //animator.speed = attackSpeed;
            animator.SetTrigger("DoublePunch");
            //animator.speed = 1;
        }
        public static void SetGolemLeftKick1(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-Kick-L1");
        }
        public static void SetGolemLeftKick2(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-Kick-L2");
        }
        public static void SetGolemRightKick1(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-Kick-R1");
        }
        public static void SetGolemRightKick2(Animator animator)
        {
            animator.SetTrigger("Unarmed-Attack-Kick-R2");
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
