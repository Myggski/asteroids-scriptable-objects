using UnityEngine;

namespace Asteroids
{
    public class AsteroidDestroyer : MonoBehaviour
    {
        [SerializeField]
        private AsteroidSet _asteroidSet;

        
        public void OnAsteroidHitByLaser(int asteroidId)
        {
            // Get the asteroid
            
            // Check if big or small
            
            // if small enough, we Destoy
            
            // if it's big, we split it up.
        }

        public void RegisterAsteroid(Asteroid asteroid)
        {
            
        }

        private void DestroyAsteroid(Asteroid asteroid) {

        }
    }
}
