using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class ParryNode : BehaviorNode
    {
        private readonly IParry _parry;
        private readonly Animator _animator;
        private readonly int _isBlocking = Animator.StringToHash("Blocking");
        private readonly float _timeEndOfAnimation;

        private float _time;

        public ParryNode(IParry parry, float timeEndOfAnimation, Animator animator)
        {
            _parry = parry ?? throw new ArgumentNullException(nameof(parry));
            _timeEndOfAnimation = timeEndOfAnimation;
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Status == BehaviorNodeStatus.Idle)
            {
                _animator.SetTrigger(_isBlocking);
                _animator.SetBool("isShield", true);
                _time = Time.time;
                Debug.Log(_timeEndOfAnimation);
                _parry.Activate(_timeEndOfAnimation);
                return BehaviorNodeStatus.Running;
            }

            if (Time.time - _time > _timeEndOfAnimation)
            {
                _animator.SetBool("isShield", false);
                return BehaviorNodeStatus.Success;
            }

            return BehaviorNodeStatus.Running;
        }
    }
}