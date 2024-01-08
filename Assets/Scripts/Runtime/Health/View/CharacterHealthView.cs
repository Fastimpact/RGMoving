using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RunGun.Gameplay
{
    [RequireComponent(typeof(Character))]
    public class 
        CharacterHealthView : MonoBehaviour, IHealthView
    {
        [Header("Links")]
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private Image _fill;
        [SerializeField] private Character _character;
        
        [Header("GameOverPanelOptions")]
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private float _showingTime = 1.35f;

        private readonly int _stun = Animator.StringToHash("Injured");
        private int _lastShowedHealth;
        private int _maxHealth;

        public void Initialize()
        {
        }

        public void SaveMax(int maxHealth, int currentHealth)
        {
            _maxHealth = maxHealth;
            _lastShowedHealth = currentHealth;
        }
        
        public void Show(int health)
        {
            _fill.fillAmount = (float)health / (float)_maxHealth;
            if (_lastShowedHealth > health)
            {
                _characterAnimator.SetTrigger(_stun);
            }

            if (health <= 0)
            {
                Die();
            }

            _lastShowedHealth = health;
        }

        private void Die()
        {
            DieAsync();
        }

        private async Task DieAsync()
        {
            _characterAnimator.Play("Death");
            _gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            await Task.Delay(TimeSpan.FromSeconds(_showingTime));

            _gameOverPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        /// <summary>
        /// Character revive in last synchronization
        /// </summary>
        /// <param name="synchronization"></param>
        /// <returns></returns>
        private async Task ReviveOnSynchronizationPoint()
        {

            await Task.Delay(TimeSpan.FromSeconds(0.3f));
           
        }

        /// <summary>
        /// Character die and revive in spawn point. Level restart
        /// </summary>
        /// <param name="synchronization"></param>
        public void ReviveOnSpawnPoint()
        {

        }
    }
}