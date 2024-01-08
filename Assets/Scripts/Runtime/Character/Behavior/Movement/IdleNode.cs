using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class IdleNode : BehaviorNode
    {
        private readonly Animator _animator;
        private readonly int _forwardId = Animator.StringToHash("Forward");

        public IdleNode(Animator animator)
        {
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _animator.SetFloat(_forwardId, 0);
            return BehaviorNodeStatus.Success;
        }
    }
}