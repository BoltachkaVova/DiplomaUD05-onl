using UI;
using UnityEngine;
using Weapon;


namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField] private float rotateSpeed = 6f;
        [SerializeField] private float armor;

        private float _currentHealth;
        private bool _isActive; 
        
        private Vector3 _direction;
        
        private Joystick _joystick;
        private PlayerAnimations _animations;
        private WeaponBase _weapon;
        private HealthBar _healthBar;


        public bool IsActive => _isActive;

        private void Awake()
        {
            _animations = GetComponent<PlayerAnimations>();
            _joystick = FindObjectOfType<Joystick>();
            _weapon = GetComponentInChildren<WeaponBase>();
            _healthBar = GetComponentInChildren<HealthBar>();
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
            if(!_isActive)
                return;
            
            if (_joystick.Direction != Vector2.zero)
            {
                Move();
                return;
            }
            
            _animations.Move(0);
            if (Input.GetKeyDown(KeyCode.Space))
                Attack();
        }
        
        private void Move()
        {
            _direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y); 
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), rotateSpeed * Time.deltaTime); 
            
            var magnitude = _joystick.Direction.magnitude; 
            transform.position += transform.forward * (speed * Time.deltaTime * magnitude); 
            _animations.Move(magnitude);
        }


        private void Attack()
        {
            _weapon.Fire();
            _animations.Attack();
        }
        
        private void TakeDamage(int damage)
        {
            var incomingDamage = damage / armor;
            _currentHealth -= incomingDamage;
            
            _healthBar.Show(_currentHealth).Forget();
            
            if(_currentHealth <= 0)
                Rip();
        }

        private void Rip()
        {
            _animations.Rip();
            _isActive = false;
        }
    }
}