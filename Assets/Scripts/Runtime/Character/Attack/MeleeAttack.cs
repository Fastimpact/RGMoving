using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class MeleeAttack : IAttack
    {
        private readonly Character _character;
        private readonly Transform _characterTransform;
        private readonly MeleeAttackConfig _config;
        private readonly IHealth _characterHealth;
        private readonly IMovement _characterMovement;
        private readonly IAbility _chargeAbility;
        private readonly IStyle _style;
        
        public MeleeAttack(Character character, IMovement characterMovement, IAbility chargeAbility, MeleeAttackConfig config, IHealth characterHealth, IStyle style)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));
            _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));
            _chargeAbility = chargeAbility ?? throw new ArgumentNullException(nameof(chargeAbility));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _characterHealth = characterHealth ?? throw new ArgumentNullException(nameof(characterHealth));
            _characterTransform = _character.transform;
        }

        public async void Release()
        {
            _characterMovement.SetSpeed(_config.AttackMoveSpeed);
            List<GameObject> gameObjects = _characterTransform.FindObjectsNear(_config.Damage);
            List<GameObject> enemiesInAttackField = gameObjects.GetObjectsInAttackField(_characterTransform, _config.AttackAngle / 2f);

            await Task.Delay(TimeSpan.FromSeconds(0.01));
            _characterMovement.SetSpeed(_characterMovement.StartSpeed);
        }

        private void LookOnAndDamageEnemies(GameObject closest, List<GameObject> gameObjects)
        {
            _characterTransform.LookAtTarget(closest);
            DoDamage(gameObjects);
            _characterHealth.Heal(_config.HealingHealth);
        }

        private void DoDamage(List<GameObject> gameObjects)
        {
            foreach (var enemy in gameObjects)
            {
                Vector3 dirToTarget = (enemy.transform.position - _characterTransform.position).normalized;
            }

            if (!_chargeAbility.IsActive)
                _style.IncreaseStyle(_config.StyleUp);
        }
    }
}