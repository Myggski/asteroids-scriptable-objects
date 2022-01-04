using System.Collections;
using UnityEngine;
using Variables;

namespace Game {
    /// <summary>
    /// Changes the total time played (play time) every second
    /// </summary>
    public class GameTimer : MonoBehaviour {
        [Header("References:")]
        [SerializeField] private IntObservable _playTime;

        private IEnumerator Start() {
            while (_playTime.Value > 0f) {
                yield return new WaitForSeconds(1f);

                _playTime.ApplyChange(-1);
            }
        }
    }
}
