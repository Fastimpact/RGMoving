using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class Combo : BehaviorNode, ICombo
    {
        private readonly IAttack _attack;
        private readonly CharacterInput _input;
        private readonly CharacterController _controller;
        private readonly Animator _animator;
        private readonly int _isPunch;

        private bool _isStart;

        public Combo(IAttack attack, CharacterInput input, Animator animator, CharacterController controller, string animatorBool)
        {
            _attack = attack ?? throw new ArgumentNullException(nameof(attack));
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _animator = animator;
            _controller = controller;
            _isPunch = Animator.StringToHash(animatorBool);
        }

        public float NormalizedTime { get; private set; }

        public int AttacksCount { get; private set; }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Status == BehaviorNodeStatus.Idle)
            {
                _controller.enabled = false;
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                {
                    _isStart = true;
                    _animator.SetBool(_isPunch, true);
                }
                else
                    _attack.Release();
            }

            if (!_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                if (_isStart)
                    return BehaviorNodeStatus.Running;
                
                _animator.SetBool(_isPunch, false);
                _controller.enabled = true;
                return BehaviorNodeStatus.Success;
            }

            if (_isStart)
            {
                Punch();
            }

            NormalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (NormalizedTime < 0.5)
            {
                if (AttacksCount > 0)
                {
                    Punch();
                }
                _animator.SetBool(_isPunch, false);
            }

            if (_input.Attack.RangeAttack.IsPressed() && NormalizedTime >= 0.8f)
            {
                _animator.SetBool(_isPunch, false);
                _controller.enabled = true;
                return BehaviorNodeStatus.Success;
            }
            
            return BehaviorNodeStatus.Running;
        }

        private void Punch()
        {
            _animator.SetBool(_isPunch, false);
            _attack.Release();
            AttacksCount = Math.Max(0, AttacksCount - 1);

            if (AttacksCount == 0)
                _isStart = false;
        }

        public void IncreaseAttacksCount(int count)
        {
            AttacksCount += count;
            _animator.SetBool(_isPunch, true);
        }
    }
}