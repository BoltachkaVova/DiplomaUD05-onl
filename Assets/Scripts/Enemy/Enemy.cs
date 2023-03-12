using Rewards;
using UnityEngine;
using UI;
using Unit;
using Weapon;

namespace Enemy
{
    public class Enemy : UnitBase
    {
        private float _counterTime;
        
        private Player.Player _target;
        private EnemyAnimations _animations;
        private Coin[] _coins;

        private Vector3 _direction;

        private void Awake()
        {
            base.Awake();
            _animations = GetComponent<EnemyAnimations>();
            _target = FindObjectOfType<Player.Player>();
            _coins = GetComponentsInChildren<Coin>(true);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet) && _isActive)
                TakeDamage(bullet.Damage);
        }
        
        private void Update()
        {
            if (!_target.IsActive || !_isActive)
                return;
            
            _direction = (_target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(_direction, transform.up);
            
            _counterTime += Time.deltaTime;
            if (_counterTime >= delayBetweenAttacks)
                Attack();
           
        }

        protected override void Attack()
        {
            _counterTime = 0;
            _weapon.Fire(_direction);
        }
        
        protected override void TakeDamage(int damage)
        {
            var incomingDamage = damage / armor;
            _currentHealth -= incomingDamage; 
            _healthBar.Show(_currentHealth).Forget();
            
            if (_currentHealth <= 0)
                Rip();
        }

        protected override void Rip()
        {
            _isActive = false;
            
            foreach (var coin in _coins)
                coin.gameObject.SetActive(true);
            
            _animations.Rip();
        }
    }
}