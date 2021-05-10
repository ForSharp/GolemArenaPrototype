using System;
using UnityEngine;

namespace GolemEntity
{
    public static class AnimationChanger 
    {

        public static void SetIdle(Animator animator)
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

        public static void SetGolemLeftHit()
        {
            
        }
        
        public static void SetGolemRightHit()
        {
            
        }

        public static void SetGolemDoubleHit()
        {
            
        }
        
        public static void SetGolemBlock()
        {
            
        }
        
    }
}
