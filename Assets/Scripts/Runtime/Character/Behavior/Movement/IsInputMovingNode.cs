using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class IsInputMovingNode : BehaviorNode
    {
        private readonly CharacterInput _input;

        public IsInputMovingNode(CharacterInput input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            BehaviorNodeStatus status = _input.Movement.Move.ReadValue<Vector2>() != Vector2.zero
                ? BehaviorNodeStatus.Success
                : BehaviorNodeStatus.Failure;

            return status;
        }
    }
}