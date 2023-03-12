using UnityEngine;

namespace Weapon

{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected Bullet bulletPrefab;
        
        private ShootPoint _shootPoint;
        private ParticleSystem _particle;
        private void Awake()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
            _particle.Stop();
            
            _shootPoint = GetComponentInChildren<ShootPoint>();
        }
        
        public virtual void Fire(Vector3 direction)
        {
            var bullet = Instantiate(bulletPrefab.gameObject, _shootPoint.transform.position, Quaternion.identity).GetComponent<Bullet>();
           bullet._direction = direction;
           
           _particle.Play();
        }
    }
}