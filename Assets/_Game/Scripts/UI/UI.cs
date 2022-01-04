using TMPro;
using UnityEngine;
using Variables;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [Header("Health:")]
        [SerializeField] private IntVariable _healthVar;
        [SerializeField] private TextMeshProUGUI _healthText;
        
        [Header("Score:")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        [Header("Timer:")]
        [SerializeField] private TextMeshProUGUI _timerText;
        
        [Header("Laser:")]
        [SerializeField] private TextMeshProUGUI _laserText;
        
        private void Start()
        {
            SetHealthText($"Health: {_healthVar.Value}");
        }

        public void OnHealthChanged(IntReference newHealth)
        {
            SetHealthText($"Health: {newHealth.GetValue()}");
        }
        
        public void OnDestroyedAsteroidsChanged(IntReference newDestroyedAsteroids)
        {
            SetScoreText($"Asteroids Destroyed: {newDestroyedAsteroids.GetValue()}");
        }

        public void OnPlayTimeUpdated(IntReference timeLeft) {
            SetTimerText($"Time: {timeLeft.GetValue()}");
        }

        public void OnLaserFired(IntReference totalLasersFired) {
            SetLaserText($"Lasers Fired: {totalLasersFired.GetValue()}");
        }

        private void SetHealthText(string text)
        {
            _healthText.text = text;
        }
        
        private void SetScoreText(string text)
        {
            _scoreText.text = text;
        }
        
        private void SetTimerText(string text)
        {
            _timerText.text = text;
        }
        
        private void SetLaserText(string text)
        {
            _laserText.text = text;
        }
    }
}
