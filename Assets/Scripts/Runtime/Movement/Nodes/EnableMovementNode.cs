using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class EnableMovementNode : BehaviorNode
    {
        private readonly MovementStop _movementStop;

        public EnableMovementNode(MovementStop movementStop)
        {
            _movementStop = movementStop ?? throw new ArgumentNullException(nameof(movementStop));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_movementStop.IsActive)
                _movementStop.Deactivate();
            
            return BehaviorNodeStatus.Success;
        }
    }
}