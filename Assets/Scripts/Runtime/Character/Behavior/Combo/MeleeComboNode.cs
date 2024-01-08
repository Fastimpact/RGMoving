using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class MeleeComboNode : BehaviorNode
    {
        private readonly ICombo _combo;
        private readonly IAbility _chargedAbility;
        private readonly CharacterInput _input;
        
        public MeleeComboNode(ICombo combo, IAbility chargedAbility, CharacterInput input)
        {
            _combo = combo ?? throw new ArgumentNullException(nameof(combo));
            _chargedAbility = chargedAbility ?? throw new ArgumentNullException(nameof(chargedAbility));
            _input = input ?? throw new ArgumentNullException(nameof(input));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_input.Attack.MeleeAtack.WasPerformedThisFrame() && _combo.NormalizedTime > 0.5f && !_chargedAbility.IsActive)
            {
                _combo.IncreaseAttacksCount(1);
            }

            return _combo.Execute(time);
        }

        public override void OnReset()
        {
            _combo.Reset();
        }
    }
}
