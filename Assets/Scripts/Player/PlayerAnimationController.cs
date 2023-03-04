using System;
using UnityEngine;

namespace Scripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Walk = Animator.StringToHash("Walk");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetAnimationMove(float value)
        {
            _animator.SetFloat(Walk, value);
        }
    }
}