using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class AimIsNotActiveNode : BehaviorNode
    {
        private readonly Aim _aim;

        public AimIsNotActiveNode(Aim aim)
        {
            _aim = aim ?? throw new ArgumentNullException(nameof(aim));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _aim.IsActive ? BehaviorNodeStatus.Failure : BehaviorNodeStatus.Success;
        }
    }
}