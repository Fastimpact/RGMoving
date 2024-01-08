using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class AttackNode : BehaviorNode
    {
        private readonly float _attackCooldown;
        private readonly ICombo _combo;

        private long _time = -1;

        public AttackNode(float attackCooldown, ICombo combo)
        {
            _attackCooldown = attackCooldown;
            _combo = combo ?? throw new ArgumentNullException(nameof(combo));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_time != -1 && time < _time + _attackCooldown * 1000)
                return BehaviorNodeStatus.Failure;

            _time = time;
            return _combo.Execute(time);
        }

        public override void OnReset()
        {
            _combo.Reset();
        }
    }
}