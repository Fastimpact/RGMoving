using System;

namespace RunGun.Gameplay
{
    public class AttackWithView : IAttack
    {
        private readonly IAttack _attack;
        private readonly IAttackView _view;

        public AttackWithView(IAttack attack, IAttackView view)
        {
            _attack = attack ?? throw new ArgumentNullException(nameof(attack));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Release()
        {
            _attack.Release();
            _view.Release();
        }
    }
}