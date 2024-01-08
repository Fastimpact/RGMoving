using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RunGun.Gameplay
{
    public class EnemyHealthView : IHealthView
    {
        private readonly GameObject _gameObject;
        private readonly bool _destroy;
        private readonly Animator? _animator;

        private float _lastShowedHealth;

        public EnemyHealthView(GameObject gameObject, bool destroy, Animator? animator)
        {
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
            _destroy = destroy;
            _animator = animator;
        }

        public async void Show(int health)
        {
            if (health <= 0)
            {
                _animator?.Play("Die");
                await UniTask.Delay(TimeSpan.FromSeconds(0.4f));
              
                if (_destroy)
                {
                    Object.Destroy(_gameObject);
                }
                else
                {
                    _gameObject.SetActive(false);
                }
            }
        }
    }
}