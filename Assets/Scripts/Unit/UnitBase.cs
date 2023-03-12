using Enemy;
using UI;
using UnityEngine;
using Weapon;

namespace Unit
{
    public abstract class UnitBase : MonoBehaviour
    {
        [SerializeField] protected float delayBetweenAttacks = 2f;
        [SerializeField] protected float armor = 20f;
        
        protected float _currentHealth;
        protected bool _isActive;
        
        protected WeaponBase _weapon;
        protected HealthBar _healthBar;

        public bool IsActive => _isActive;
        protected void Awake()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
            _weapon = GetComponentInChildren<WeaponBase>();
        }

        protected void Start()
        {
            _currentHealth = 1f;
            _isActive = true;
            
            _healthBar.Show(_currentHealth).Forget();
        }
        
        protected abstract void Attack();
        protected abstract void TakeDamage(int damage);
        protected abstract void Rip();
    }
}
