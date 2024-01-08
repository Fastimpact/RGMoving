using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class SpendStyleNode : BehaviorNode
    {
        private readonly IStyle _style;
        private readonly IAbility _ability;
        private readonly int _needStyle;

        public SpendStyleNode(IStyle style, IAbility ability, int needStyle)
        {
            _needStyle = needStyle;
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_ability.IsActive)
            {
                _style.SpendStyle(_ability.NeedStyle);
            }
            else
            {
                _style.SpendStyle(_needStyle);
            }
            return BehaviorNodeStatus.Success;
        }
    }
}