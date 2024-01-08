using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class AddStyleNode : BehaviorNode
    {
        private readonly IStyle _style;
        private readonly int _value;

        public AddStyleNode(IStyle style, int value)
        {
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _value = value;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _style.IncreaseStyle(_value);
            return BehaviorNodeStatus.Success;
        }
    }
}