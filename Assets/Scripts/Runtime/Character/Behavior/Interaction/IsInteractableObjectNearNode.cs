using BananaParty.BehaviorTree;
using System;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class IsInteractableObjectNearNode : BehaviorNode
    {
        private readonly InteractSensor _sensor;

        public IsInteractableObjectNearNode(InteractSensor sensor)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _sensor.IsInteract ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }        
    }
}
