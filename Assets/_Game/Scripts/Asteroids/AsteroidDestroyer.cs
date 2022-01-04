using DefaultNamespace.ScriptableEvents;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidDestroyer : MonoBehaviour {
        [Header("References:")]
        [SerializeField] private AsteroidSet _asteroidSet;
        [SerializeField] private ScriptableEventInt _onAsteroidSplit;

        /// <summary>
        /// Gets triggered by onAsteroidOnHitEvent (Scriptable event)
        /// Checks if the asteroid is splittable, else it gets destroyed
        /// </summary>
        /// <param name="instanceId">InstanceId of the asteroid</param>
        public void OnAsteroidHitByLaser(int instanceId) {
            Asteroid hittedAsteroid = _asteroidSet.Get(instanceId);

            if (ReferenceEquals(hittedAsteroid, null)) {
                return;
            }

            if (hittedAsteroid.IsSplittable) {
                _onAsteroidSplit.Raise(instanceId);
                return;
            }
            
            Asteroid asteroidToDestroy = _asteroidSet.Get(instanceId);

            if (!ReferenceEquals(asteroidToDestroy, null)) {
                Destroy(asteroidToDestroy.gameObject);
            }
        }
    }
}
