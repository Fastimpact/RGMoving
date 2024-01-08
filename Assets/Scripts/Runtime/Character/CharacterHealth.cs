using System;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class CharacterHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private CharacterConfig _config;
        [SerializeField] private CharacterHealthView _healthView;

        public IHealth Health { get; private set; }

        public int MaxValue => _config.HealthMax;

        public int MaxValueAfterSynchronization { get; private set; }

        public bool IsAlive => Health.IsAlive;

        public int Value => Health.Value;

        public bool IsInvulnerable => Health.IsInvulnerable;

        private void Awake()
        {
            _maxHealth = _config.HealthMax;
            SetMaxHealth(_maxHealth);
        }

        public void SetMaxHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            MaxValueAfterSynchronization = maxHealth;
            Health = new Health(_maxHealth,_maxHealth, _healthView);
            _healthView.SaveMax(_maxHealth, _maxHealth);
        }

        public void SetMaxHealthAfterSynchronization(int maxHealth)
        {
            MaxValueAfterSynchronization = maxHealth;
            
            if (MaxValueAfterSynchronization < 0)
                MaxValueAfterSynchronization = _maxHealth - 2;
            
            Health = new Health(MaxValueAfterSynchronization,MaxValueAfterSynchronization, _healthView);
            _healthView.SaveMax(_maxHealth, MaxValueAfterSynchronization);
        }

        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
        }

        public void Heal(int heal)
        {
            Health.Heal(heal);
        }

        public void SetInvulnerability(bool isInvulnerable)
        {
            Health.SetInvulnerability(isInvulnerable);
        }
    }
}