using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class IsShootNode : BehaviorNode
    {
        private readonly Animator _animator;

        public IsShootNode(Animator animator)
        {
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (!_animator.GetCurrentAnimatorStateInfo(1).IsTag("Shoot"))
                return BehaviorNodeStatus.Success;
            return BehaviorNodeStatus.Failure;
        }
    }
}
