using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class RangeAttack : IAttack
    {
        private readonly AttackConfig _config;
        private readonly Transform _characterTransform;

        public RangeAttack(AttackConfig config)
        {
 
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Release()
        {
       }

        private void Hit(GameObject gameObject)
        {
        }       
    }
}
