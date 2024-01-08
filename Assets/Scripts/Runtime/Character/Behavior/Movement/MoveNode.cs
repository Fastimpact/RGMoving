using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class MoveNode : BehaviorNode
    {
        private readonly IMovement _movement;
        private readonly CharacterInput _input;
        private readonly CharacterController _controller;

        public MoveNode(IMovement movement, CharacterInput input, CharacterController controller)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _controller = controller ?? throw new ArgumentNullException();
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            Vector2 inputDirection = _input.Movement.Move.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
            if (_controller.enabled)
                _movement.Move(moveDirection);
            return BehaviorNodeStatus.Success;
        }
    }
}