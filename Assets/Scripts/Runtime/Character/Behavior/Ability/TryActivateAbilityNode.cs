using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class TryActivateAbilityNode : BehaviorNode
    {
        private readonly IStyle _style;
        private readonly IAbility _ability;

        public TryActivateAbilityNode(IStyle style, IAbility ability)
        {
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_ability.IsActive)
            {
                _ability.Deactivate();
                return BehaviorNodeStatus.Failure;
            }
            
            if (_style.Value - _ability.NeedStyle >= 0)
            {
                _ability.Activate();
                return BehaviorNodeStatus.Success;
            }

            return BehaviorNodeStatus.Failure;
        }
    }
}