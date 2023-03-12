using System;
using Rewards;
using Unit;
using UnityEngine;
using Weapon;


namespace Player
{
    public class Player : UnitBase
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float rotateSpeed = 6f;

        private Vector3 _direction;

        private int _coins;
        private float _counterTime;
        
        
        
        private Joystick _joystick;
        private PlayerAnimations _animations;
        private Enemy.Enemy _target;
        

        private void Awake()
        {
            base.Awake();
            _animations = GetComponent<PlayerAnimations>();
            _joystick = FindObjectOfType<Joystick>();
            _target = FindObjectOfType<Enemy.Enemy>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Coin coin))
            {
                _coins++;
                coin.gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet) && _isActive)
                TakeDamage(bullet.Damage);
        }
        
        private void Update()
        {
            if(!_isActive)
                return;
            
            _counterTime += Time.deltaTime;
            
            if (_joystick.Direction != Vector2.zero)
            {
                Move();
                return;
            }
            _animations.Move(0);
            
            if(_target.IsActive)
                SearchClosestEnemy();
            
            if (delayBetweenAttacks <= _counterTime && _target.IsActive)
            {
                _counterTime = 0;
                Attack();
            }
        }

        private void SearchClosestEnemy()
        {
            _direction = (_target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), rotateSpeed * Time.deltaTime); 
        }

        private void Move()
        {
            _direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y); 
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), rotateSpeed * Time.deltaTime); 
            
            var magnitude = _joystick.Direction.magnitude; 
            transform.position += transform.forward * (speed * Time.deltaTime * magnitude); 
            _animations.Move(magnitude);
        }


        protected override void Attack()
        {
            _weapon.Fire(_direction);
            _animations.Attack();
        }
        
        protected override void TakeDamage(int damage)
        {
            var incomingDamage = damage / armor;
            _currentHealth -= incomingDamage;
            
            _healthBar.Show(_currentHealth).Forget();
            
            if(_currentHealth <= 0)
                Rip();
        }

        protected override void Rip()
        {
            _animations.Rip();
            _isActive = false;
        }
    }
}