using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class ChargedCombo : BehaviorNode, ICombo
    {
        private readonly ICombo _combo;
        private readonly IAbility _chargedAbility;
        private readonly IAttackView _attackView;
        private readonly IStyle _style;
        private readonly ChargedConfig _chargedConfig;
        
        private int _wasAttacksCount;

        public ChargedCombo(ICombo combo, IAbility chargedAbility, IAttackView attackView, IStyle style, ChargedConfig chargedConfig)
        {
            _combo = combo ?? throw new ArgumentNullException(nameof(combo));
            _chargedAbility = chargedAbility ?? throw new ArgumentNullException(nameof(chargedAbility));
            _attackView = attackView ?? throw new ArgumentNullException(nameof(attackView));
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _chargedConfig = chargedConfig ?? throw new ArgumentNullException(nameof(chargedConfig));
        }

        public float NormalizedTime => _combo.NormalizedTime;

        public int AttacksCount => _combo.AttacksCount;

        public void IncreaseAttacksCount(int count)
        {
            _combo.IncreaseAttacksCount(count);
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_chargedAbility.IsActive)
            {
                if (_wasAttacksCount == 0)
                {
                    _attackView.MultiplyAnimatorSpeed(1.5f);
                    _style.SpendStyle(_chargedAbility.NeedStyle);

                    _combo.IncreaseAttacksCount(_chargedConfig.AttacksCount);
                    _wasAttacksCount = _combo.AttacksCount;
                }
                else if (_wasAttacksCount - _combo.AttacksCount >= _chargedConfig.AttacksCount)
                {
                    _wasAttacksCount = 0;
                    _chargedAbility.Deactivate();
                    _attackView.ResetAnimatorSpeed();
                }
            }

            return _combo.Execute(time);
        }

        public override void OnReset()
        {
            _combo.Reset();
        }
    }
}