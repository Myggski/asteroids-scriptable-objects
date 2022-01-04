using UnityEngine;

namespace Asteroids
{
    public class OnBecameInvisibleDestroyer : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private GameObject _toDestroy;
        
        private void OnBecameInvisible()
        {
            Destroy(_toDestroy);
        }
    }
}