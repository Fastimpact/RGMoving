using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class StopMovementNode : BehaviorNode
    {
        private readonly MovementStop _movementStop;

        public StopMovementNode(MovementStop movementStop)
        {
            _movementStop = movementStop ?? throw new ArgumentNullException(nameof(movementStop));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            Debug.Log("������� � ������");
            _movementStop.Activate();
            return BehaviorNodeStatus.Success;
        }
    }
}