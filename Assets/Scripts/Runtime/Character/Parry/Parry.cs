using System;
using System.Threading.Tasks;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class Parry : IParry
    {
        private readonly Transform _transform;
        private readonly ParryConfig _config;
        private readonly IStyle _style;
        private readonly IAbility _ability;
        private readonly Animator _animator;
        private readonly Collider[] _results = new Collider[50];

        public Parry(Transform transform, ParryConfig config, IStyle style, IAbility ability, Animator animator)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
        }

        public bool IsActive { get; private set; }

        public async void Activate(float seconds)
        {
            IsActive = true;
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            IsActive = false;
        }

        public async void Use()
        {
            int resultsCount = Physics.OverlapSphereNonAlloc(_transform.position, _config.Radius, _results);
            Debug.Log("Parry");

            _animator.SetTrigger("Parry");

            _style.IncreaseStyle(_config.AddStyle);
            _ability.MakeFree();

            if (_ability.IsActive == false)
                _ability.Activate();
            
            await Task.Delay(TimeSpan.FromSeconds(_config.AbilityIsFreeSeconds));

            if (_ability.IsActive)
                _ability.Deactivate();
            
            _ability.BackToStartNeedStyle();
        }
    }
}