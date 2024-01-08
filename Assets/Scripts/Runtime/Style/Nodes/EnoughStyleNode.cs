using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class EnoughStyleNode : BehaviorNode
    {
        private readonly IStyle _style;
        private readonly IAbility _ability;
        private readonly int _needStyle;

        public EnoughStyleNode(IStyle style, IAbility ability, int needStyle)
        {
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
            _needStyle = needStyle;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            int style = _ability.IsActive ? _ability.NeedStyle : _needStyle;
            return _style.Value - style >= 0 ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}