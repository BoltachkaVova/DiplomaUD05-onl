using UnityEngine;

namespace Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Reloading = Animator.StringToHash("Reloading");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int Walk = Animator.StringToHash("Walk");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Rip()
        {
            _animator.SetTrigger(Death);
        }
        
        public void Attack()
        {
            _animator.SetTrigger(Attack1);
        }

        public void Move(float speed)
        {
            _animator.SetFloat(Walk, speed);
        }
    }
}