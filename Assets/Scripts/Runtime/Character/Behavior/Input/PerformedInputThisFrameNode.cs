using System;
using BananaParty.BehaviorTree;
using UnityEngine.InputSystem;

namespace RunGun.Gameplay
{
    public class PerformedInputThisFrameNode : BehaviorNode
    {
        private readonly InputAction _inputAction;

        public PerformedInputThisFrameNode(InputAction inputAction)
        {
            _inputAction = inputAction ?? throw new ArgumentNullException(nameof(inputAction));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _inputAction.WasPerformedThisFrame() ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}