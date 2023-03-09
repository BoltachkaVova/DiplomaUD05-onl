using UnityEngine;
using UI;
using Weapon;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float delayBetweenAttacks;
        [SerializeField] private float armor = 20f;
        
        private float _currentHealth;
        private float _counterTime;
        private bool _isActive;

        private Player.Player _target;
        private WeaponBase _weapon;
        private EnemyAnimations _animations;
        private HealthBar _healthBar;


        private void Awake()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            _animations = GetComponent<EnemyAnimations>();
            _weapon = GetComponentInChildren<WeaponBase>();
            _target = FindObjectOfType<Player.Player>();
        }

        private void Start()
        {
            _currentHealth = 1f;
            _healthBar.Show(_currentHealth).Forget();
            _isActive = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Bullet bullet) && _isActive)
                TakeDamage(bullet.Damage);
        }
        
        private void Update()
        {
            if (!_target.IsActive)
                return;
            
            var direction = (_target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
            
            _counterTime += Time.deltaTime;
            if (_counterTime >= delayBetweenAttacks)
                Attack();
           
        }

        private void Attack()
        {
            _counterTime = 0;
            _weapon.Fire();
        }
        
        private void TakeDamage(int damage)
        {
            var incomingDamage = damage / armor;
            _currentHealth -= incomingDamage; 
            _healthBar.Show(_currentHealth).Forget();
            
            if (_currentHealth <= 0)
                Rip();
        }

        private void Rip()
        {
            _isActive = false;
            _animations.Rip();
        }
    }
}