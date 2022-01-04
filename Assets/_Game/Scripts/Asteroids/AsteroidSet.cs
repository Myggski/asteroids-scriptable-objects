using Core;
using UnityEngine;
using Variables;

namespace Asteroids {
    [CreateAssetMenu(fileName = "New AsteroidSet", menuName = "Sets/AsteroidSet")]
    public class AsteroidSet : RuntimeSet<Asteroid> {
        [SerializeField] private IntObservable _asteroidDestroyedObservable;

        public override void Remove(int instanceId) {
            base.Remove(instanceId);
            
            _asteroidDestroyedObservable.ApplyChange(1);
        }
    }
}