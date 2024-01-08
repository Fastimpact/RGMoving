using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class IsAbilityActiveNode : BehaviorNode
    {
        private readonly IAbility _ability;

        public IsAbilityActiveNode(IAbility ability)
        {
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _ability.IsActive ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}