using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private AsteroidSet _asteroidSet;

        [Header("Config:")]
        [SerializeField] private float _minSpawnTime;
        [SerializeField] private float _maxSpawnTime;
        [SerializeField] private int _minAmount;
        [SerializeField] private int _maxAmount;
        
        private float _timer;
        private float _nextSpawnTime;
        private Camera _camera;

        private enum SpawnLocation
        {
            Top,
            Bottom,
            Left,
            Right
        }

        private void Start()
        {
            _camera = Camera.main;
            SpawnRandomAsteroids();
            UpdateNextSpawnTime();
        }

        private void Update()
        {
            UpdateTimer();

            if (!ShouldSpawn())
                return;

            SpawnRandomAsteroids();
            UpdateNextSpawnTime();
            _timer = 0f;
        }

        private void UpdateNextSpawnTime()
        {
            _nextSpawnTime = Random.Range(_minSpawnTime, _maxSpawnTime);
        }

        private void UpdateTimer()
        {
            _timer += Time.deltaTime;
        }

        private bool ShouldSpawn()
        {
            return _timer >= _nextSpawnTime;
        }

        public void SplitAsteroid(int instanceId) {
            Asteroid asteroidToSplit = _asteroidSet.Get(instanceId);
            Asteroid spawnedAsteroid = SpawnAsteroid(asteroidToSplit.transform.position);
            Vector3 splittedSize = asteroidToSplit.LocalScaleSize / 2f;
            
            asteroidToSplit.SetSize(splittedSize);
            spawnedAsteroid.SetSize(splittedSize);
        }

        private void SpawnRandomAsteroids() {
            int  numberOfAsteroidsToSpawn = Random.Range(_minAmount, _maxAmount + 1);
            SpawnLocation randomLocation = GetRandomSpawnLocation();
            Vector3 spawnPosition = GetStartPosition(randomLocation);

            SpawnAsteroids(spawnPosition, numberOfAsteroidsToSpawn);
        }

        private Asteroid[] SpawnAsteroids(Vector3 startPosition, int numberOfAsteroids)
        {
            Asteroid[] astroids = new Asteroid[numberOfAsteroids]; 
            
            for (int i = 0; i < numberOfAsteroids; i++) {
                astroids[i] = SpawnAsteroid(startPosition);
            }

            return astroids;
        }

        private Asteroid SpawnAsteroid(Vector3 startPosition) {
            return Instantiate(_asteroidPrefab, startPosition, Quaternion.identity);
        }

        private static SpawnLocation GetRandomSpawnLocation()
        {
            int roll = Random.Range(0, 4);

            return roll switch
            {
                1 => SpawnLocation.Bottom,
                2 => SpawnLocation.Left,
                3 => SpawnLocation.Right,
                _ => SpawnLocation.Top
            };
        }

        private Vector3 GetStartPosition(SpawnLocation spawnLocation)
        {
            Vector3 startPosition = new Vector3 { z = Mathf.Abs(_camera.transform.position.z) };
            
            const float padding = 5f;
            switch (spawnLocation)
            {
                case SpawnLocation.Top:
                    startPosition.x = Random.Range(0f, Screen.width);
                    startPosition.y = Screen.height + padding;
                    break;
                case SpawnLocation.Bottom:
                    startPosition.x = Random.Range(0f, Screen.width);
                    startPosition.y = 0f - padding;
                    break;
                case SpawnLocation.Left:
                    startPosition.x = 0f - padding;
                    startPosition.y = Random.Range(0f, Screen.height);
                    break;
                case SpawnLocation.Right:
                    startPosition.x = Screen.width - padding;
                    startPosition.y = Random.Range(0f, Screen.height);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spawnLocation), spawnLocation, null);
            }
            
            return _camera.ScreenToWorldPoint(startPosition);
        }
    }
}
