using System;
using UnityEngine;

namespace GolemEntity
{
    public class AnimationChanger : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        
        public void SetIdle()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGTHR1()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0.5f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGTHR3()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 1f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGTHL1()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), -0.5f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGTHL3()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), -1f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGolemNWalk()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0.5f);
        }
        
        public void SetGRSR1()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 1f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0.05f);
        }
        
        public void SetGRR1()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), -1f);
        }
        
        public void SetGRSL1()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGRL1()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetRSR1()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGolemRun()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGRSL1Run()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGRRRun()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }
        
        public void SetGRL1Run()
        {
            _animator.SetFloat(Animator.StringToHash("Turn"), 0f);
            _animator.SetFloat(Animator.StringToHash("Forward"), 0f);
        }

    }
}
