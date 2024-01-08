using System;

namespace RunGun.Gameplay
{
    public class AttackStyleSpend : IAttack
    {
        private readonly IAttack _attack;
        private readonly IAbility _ability;
        private readonly IStyle _style;

        public AttackStyleSpend(IAttack attack, IAbility ability, IStyle style)
        {
            _attack = attack ?? throw new ArgumentNullException(nameof(attack));
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
            _style = style ?? throw new ArgumentNullException(nameof(style));
        }

        public void Release()
        {
            if (!_ability.IsActive)
                _style.SpendStyle(1);

            _attack.Release();
        }
    }
}