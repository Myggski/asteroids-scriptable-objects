using Core;
using UnityEngine;

namespace Lasers {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Laser : MonoBehaviour, IRuntimeSetObject {
        [Header("References:")]
        [SerializeField] private LaserSet _laserSet;

        [Header("Config:")]
        [SerializeField] private float _speed = 0.2f;

        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private int _instanceId;

        public int InstanceId => _instanceId;

        private void Awake() {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();

            RegisterLaser();
        }

        private void OnDestroy() {
            _laserSet.Remove(_instanceId);
        }

        private void FixedUpdate() {
            _rigidbody.MovePosition(_transform.position + _transform.up * _speed);
        }

        /// <summary>
        /// Adds laser to LaserSet
        /// </summary>
        private void RegisterLaser() {
            _laserSet.Add(this);
        }
    }
}
