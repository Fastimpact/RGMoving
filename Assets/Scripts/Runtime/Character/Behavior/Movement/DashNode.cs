using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class DashNode : BehaviorNode
    {
        private readonly CharacterController _controller;
        private readonly CharacterHealth _characterHealth;
        private readonly CharacterInput _input;
        private readonly IAttackView _dashView;
        private readonly Animator _animator;
        private readonly float _distance;
        private readonly bool _isShort;

        private float _speed;
        private readonly int _dash = Animator.StringToHash("isDash");
        private readonly float _dashResetTime = 0.2f;
     
        private float _dashing;
        private bool _isNextDash;
        private Vector3 _moveDirection;

        public DashNode(CharacterController controller, CharacterInput input, Animator animator, float speed, IAttackView dashView, CharacterHealth characterHealth, bool isShort)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _distance = speed;
            _dashView = dashView ?? throw new ArgumentNullException(nameof(dashView));
            _characterHealth = characterHealth;
            _isShort = isShort;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            
            if (Status == BehaviorNodeStatus.Idle)
            {
                _dashing = 0f;
                _isNextDash = false;

                var inputDirection = _input.Movement.Move.ReadValue<Vector2>();

                _moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

                if (Vector3.Angle(Vector3.forward, _moveDirection) > 1f ||
                Vector3.Angle(Vector3.forward, _moveDirection) == 0)
                {
                    Vector3 direct = Vector3.RotateTowards(_controller.transform.forward, _moveDirection, 2.5f, 0.0f);
                    _controller.transform.rotation = Quaternion.LookRotation(direct);

                    if (_moveDirection == Vector3.zero)
                    {
                        _moveDirection = direct;
                        if (_isShort)
                            _moveDirection *= -1;
                    }
                }
                _dashView.Release();
                _animator.SetBool(_dash, true);

                _speed = _distance / _dashResetTime;
                _characterHealth.SetInvulnerability(true);
            }

            if (_dashing > _distance / 3 && _input.Movement.Dash.WasPerformedThisFrame() && _isShort)
                _isNextDash = true;

            if (_dashing < _distance)
            {           
                _controller.Move(_moveDirection * _speed * Time.deltaTime);
                _dashing += _speed * Time.deltaTime;   
            }

            if (_dashing >= _distance)
            {
                _animator.SetBool(_dash, false);
                _characterHealth.SetInvulnerability(_isNextDash);
                return BehaviorNodeStatus.Success;
            }
            return BehaviorNodeStatus.Running;
        }
    }
}