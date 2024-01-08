using BananaParty.BehaviorTree;

using UnityEngine;

namespace RunGun.Gameplay
{
    public class IsDashingNode : BehaviorNode
    {
        private readonly CharacterHealth _health;

        public IsDashingNode(CharacterHealth health)
        {
            _health = health;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            BehaviorNodeStatus status = _health.IsInvulnerable ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
            return status;
        }
    }
}
