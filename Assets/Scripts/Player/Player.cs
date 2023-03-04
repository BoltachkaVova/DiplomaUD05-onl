using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 0.75f;
        [SerializeField] private float rotateSpeed = 7.5f;
        
        private Planet _planet;
        private Joystick _joystick;
        private Rigidbody _rigidbody;
        private PlayerAnimationController _animation;

        private bool _isActive; // для остановки расчетов физики(будет)
        
        private void Awake()
        {
            _animation = GetComponent<PlayerAnimationController>();
            _rigidbody = GetComponent<Rigidbody>();
            _planet = FindObjectOfType<Planet>();
            _joystick = FindObjectOfType<Joystick>();
        }
        
        private void FixedUpdate()
        {
            _animation.SetAnimationMove(_joystick.Direction.magnitude);
            if (_joystick.Direction != Vector2.zero)
                Move();

            var upwards = _planet.transform.position - transform.position;
            var rotation = Quaternion.FromToRotation(-transform.up, upwards);
            _rigidbody.rotation = rotation * _rigidbody.rotation;
        }

        private void Move()
        {
            var direction = new Vector3(_joystick.Direction.x, 0f, _joystick.Direction.y).normalized;
            _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime));

            var magnitude = _joystick.Direction.magnitude;
            _rigidbody.MovePosition(transform.position + transform.forward * (magnitude * speed * Time.deltaTime));
        }
        
    }
}