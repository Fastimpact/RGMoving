using System;

namespace RunGun.Gameplay
{
    public class Health : IHealth
    {
        private readonly IHealthView _view;

        public Health(int value, int maxValue, IHealthView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            Value = value;
            MaxValue = maxValue;
        }

        public bool IsAlive => Value > 0;

        public bool IsInvulnerable { get; private set; }
        
        public int Value { get; private set; }
        
        public int MaxValue { get; }

        public void TakeDamage(int damage)
        {
            if (!IsAlive)
                throw new Exception($"Health is died! You can't attack it!");
            
            if (IsInvulnerable)
                return;
            
            Value = Math.Max(0, Value - damage);
            _view.Show(Value);
        }

        public void Heal(int heal)
        {
            if (Value + heal > MaxValue)
            {
                Value = MaxValue;
            }
            else
            {
                Value += heal;
            }

            _view.Show(Value);
        }

        public void SetInvulnerability(bool invulnerabilyty)
        {
            IsInvulnerable = invulnerabilyty;
        }
    }
}