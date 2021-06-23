using System;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

namespace GolemEntity
{
    public static class AnimationChanger 
    {
        private static readonly int InFightPos = Animator.StringToHash("InFightPos");
        
        
        public static Action<Animator>[] GetAllAttackAnimations(Animator animator)
        {
            return new Action<Animator>[]
            {
                (SetGolemLeftHit),
                (SetGolemRightHit),
                (SetGolemDoubleHit)
            };
        }

        public static void SetFightPos(Animator animator)
        {
            animator.SetBool(InFightPos, true);
        }
        
        public static void ResetFightPos(Animator animator)
        {
            animator.SetBool(InFightPos, false);
        }

        public static void SetNewAttack(Animator animator)
        {
            // animator.SetBool(InFightPos, true);
            //
            // float[] blendTreeStages = new float[32];
            // blendTreeStages[0] = 0;
            // float step = 1.0f / 31;
            // for (int i = 1; i < blendTreeStages.Length; i++)
            // {
            //     blendTreeStages[i] = step * i;
            // }
            //
            // animator.SetFloat(Animator.StringToHash("NewAttack"), blendTreeStages[Random.Range(0, blendTreeStages.Length)]);

            string[] newTestTags = {"NewAt1", "NewAt2", "NewAt3", "NewAt4", "NewAt5", "NewAt6"};
            
            string[] newTestTags1 = {"Kick1", "Kick2", "Kick3"};
            
            string[] newTestTags2 = {"Kick1", "Kick2", "Kick3", "NewAt1", "NewAt2", "NewAt3", "NewAt4", "NewAt5", "NewAt6"};
            
            string[] newTestTags3 = {"Kick2", "Kick3"};

            var currentAttack = newTestTags2[Random.Range(0, newTestTags2.Length)];
            
            animator.SetTrigger(currentAttack);

            animator.GetComponent<GameCharacterState>().testStringAttack = currentAttack;
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
