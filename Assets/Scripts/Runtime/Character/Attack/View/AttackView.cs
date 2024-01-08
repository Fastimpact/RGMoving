using System;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class AttackView : IAttackView
    {
        private readonly Animator _animator;
        private readonly string _triggerName;

        public AttackView(Animator animator, string triggerName)
        {
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _triggerName = triggerName;
        }

        public void ResetAnimatorSpeed()
        {
            _animator.speed = 1f;
        }

        public void MultiplyAnimatorSpeed(float multiplier)
        {
            _animator.speed *= multiplier;
        }

        public void Release()
        {
            _animator.SetTrigger(_triggerName);
        }
    }
}