using System;
using UnityEngine;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int damage;

        public int Damage => damage;

        private Vector3 _direction;

        private void Awake()
        {
            _direction = transform.forward;
        }

        private void Update()
        {
            transform.Translate(_direction * speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision other)
        {
            gameObject.SetActive(false);
        }
        
    }
}