using UnityEngine;
using Variables;

namespace Ship
{
    public class Hull : MonoBehaviour
    {
        [SerializeField] private IntObservable _healthObservable;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (string.Equals(other.gameObject.tag, "Asteroid"))
            {
                if (_healthObservable.Value > 0) {
                    _healthObservable.ApplyChange(-1);    
                }
            }
        }
    }
}
