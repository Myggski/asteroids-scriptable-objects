using Lasers;
using UnityEngine;

namespace Ship
{
    public class Gun : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Laser _laserPrefab;

        private Transform _transform;

        private void Start() {
            _transform = transform;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }
        
        private void Shoot()
        {
            Instantiate(_laserPrefab, _transform.position, _transform.rotation);
        }
    }
}
