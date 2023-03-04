using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace Scripts
{
    public class Planet : MonoBehaviour
    {
        private Player _object;
        private Rigidbody _rigidbody;
        
        public float AccelerationOfGravity => _accelerationOfGravity; 

        private float _accelerationOfGravity = -9.8f;

        private void Awake()
        {
            _object = FindObjectOfType<Player>();
            _rigidbody = _object.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            GravityPlanet();
        }

        private void GravityPlanet()
        {
            var directionToPlanet = (_object.transform.position - transform.position).normalized;
            _rigidbody.AddForce(directionToPlanet * _accelerationOfGravity);
        }
        
    }
}