using System;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class RangeAimedAttack : IAttack
    {
        private readonly Transform _transform;
        private readonly AttackConfig _attackConfig;

        public RangeAimedAttack(Transform transform, AttackConfig attackConfig)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            _attackConfig = attackConfig ?? throw new ArgumentNullException(nameof(attackConfig));
        }

        public void Release()
        {
            Debug.DrawRay(_transform.position, _transform.forward, Color.red, 10);


            int layer = LayerMask.NameToLayer("Target");

            bool cast = Physics.SphereCast(_transform.position, _attackConfig.MagnetizationRadius, _transform.forward,
                out RaycastHit hit, _attackConfig.DamageDistance, layer);
      
        }
    }
}