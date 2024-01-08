using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class ParryIsNotActiveNode : BehaviorNode
    {
        private readonly IParry _parry;

        public ParryIsNotActiveNode(IParry parry)
        {
            _parry = parry ?? throw new ArgumentNullException(nameof(parry));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _parry.IsActive ? BehaviorNodeStatus.Failure : BehaviorNodeStatus.Success;
        }
    }
}