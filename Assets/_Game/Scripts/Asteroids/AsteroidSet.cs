using Core;
using UnityEngine;

namespace Asteroids {
    [CreateAssetMenu(fileName = "new AsteroidSet", menuName = "Sets/AsteroidSet")]
    public class AsteroidSet : RuntimeSet<int, Asteroid> {
        public void RegisterAsteroid(Asteroid asteroid) {
            Add(asteroid.InstanceId, asteroid);
        }

        public void DestroyAsteroid(Asteroid asteroid) {
            Remove(asteroid.InstanceId);
        }
    }
}