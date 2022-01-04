using Core;
using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IRuntimeSetObject
    {
        [Header("References:")]
        [SerializeField] private Transform _shape;
        [SerializeField] private ScriptableEventInt _onAsteroidHit;
        [SerializeField] private AsteroidSet _asteroidSet;

        [Header("Config:")]
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _minTorque;
        [SerializeField] private float _maxTorque;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;
        
        public int InstanceId => _instanceId;
        /// <summary>
        /// Returns true if the asteroid is big enough
        /// </summary>
        public bool IsSplittable => _shape.localScale.x > (_maxSize + _minSize) / 2;

        /// <summary>
        /// Gets the local scale size of the asteroid
        /// </summary>
        public Vector3 LocalScaleSize => _shape.localScale;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();
            
            SetDirection();
            AddForce();
            AddTorque();
            SetRandomSize();
            RegisterAsteroid();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.Equals(other.tag, "Laser"))
            {
               HitByLaser(other.gameObject);
            }
        }

        private void OnDestroy() {
            _asteroidSet.Remove(_instanceId);
        }

        private void HitByLaser(GameObject laserGameObject)
        {
            Destroy(laserGameObject);
            _onAsteroidHit.Raise(_instanceId);
        }
        
        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            var force = Random.Range(_minForce, _maxForce);
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            var torque = Random.Range(_minTorque, _maxTorque);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }
        
        /// <summary>
        /// Sets size of asteroid
        /// </summary>
        /// <param name="localScaleSize">Size in local scale</param>
        public void SetSize(Vector3 localScaleSize) {
            _shape.localScale = localScaleSize;
        }

        private void SetRandomSize()
        {
            var size = Random.Range(_minSize, _maxSize);
            _shape.localScale = new Vector3(size, size, 0f);
        }

        /// <summary>
        /// Adds the asteroid to the AsteroidSet-list
        /// </summary>
        private void RegisterAsteroid() {
            _asteroidSet.Add(this);
        }
    }
}
