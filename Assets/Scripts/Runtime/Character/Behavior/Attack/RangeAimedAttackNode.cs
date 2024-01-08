using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class RangeAimedAttackNode : BehaviorNode
    {
        private readonly Character _character;
        private readonly LineRenderer _line;
        private readonly CharacterInput _input;
        private readonly RangeAttackConfig _attackConfig;
        private readonly Camera _camera;
        private readonly IAttack _attack;
        private readonly Animator _animator;
        private readonly Aim _aim;
     
        private float _timeOfAnimation;
        private float _timeEndOfAnimation;
        private float _time = -1f;
        private bool _hasShoot = true;

        public RangeAimedAttackNode(LineRenderer line, CharacterInput input, Aim aim, RangeAttackConfig attackConfig, IAttack attack, Character character, Animator animator)
        {
            _line = line ?? throw new ArgumentNullException(nameof(line));
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _aim = aim ?? throw new ArgumentNullException(nameof(aim));
            _attackConfig = attackConfig ?? throw new ArgumentNullException(nameof(attackConfig));
            _attack = attack ?? throw new ArgumentNullException(nameof(attack));
            _camera = Camera.main;
            _character = character;
            _animator = animator;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Status == BehaviorNodeStatus.Idle)
            {
                if (_time != -1f && time < _time + _attackConfig.AttackCooldown)
                    return BehaviorNodeStatus.Failure;
                _aim.Activate();
                _animator.SetFloat("Forward", 0);
                _animator.SetFloat("Right", 0);
                _animator.SetBool("isShootReady", true);
                _line.gameObject.SetActive(true);
                _hasShoot = false;
                _timeOfAnimation = 0;
            }
            _time = Time.time;

            if (!_hasShoot)
            {
                if (!_input.Attack.RangeAttack.IsPressed())
                {
                    _line.gameObject.SetActive(false);
                    _animator.SetBool("isShootReady", false);
                    _attack.Release();
                    _hasShoot = true;
                }
                Aim();
            }

            return _hasShoot ? ShootTimer() : BehaviorNodeStatus.Running;
        }

        private BehaviorNodeStatus ShootTimer()
        {
            _timeOfAnimation += Time.deltaTime;
            _timeEndOfAnimation = _animator.GetCurrentAnimatorStateInfo(0).length / _animator.speed;
            if (_timeOfAnimation + 0.1f >= _timeEndOfAnimation)
            {
                _aim.Deactivate();
                return BehaviorNodeStatus.Success;
            } 
            return BehaviorNodeStatus.Running;
        }

        private void Aim()
        {
            Vector2 _lock = _input.Movement.Lock.ReadValue<Vector2>();
            if (_lock == Vector2.zero)
                _lock = MouseAim();

            _character.transform.forward = new Vector3(_lock.x, 0, _lock.y);
            DrawAimLine();
        }

        private Vector2 MouseAim()
        {
            var (success, position) = GetMousePosition();

            if (success)
            {
                var direction = position - _line.transform.position;
                return new Vector2(direction.x, direction.z);
            }
            return Vector2.zero;
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            Plane plane = new Plane(Vector3.down, _character.transform.position.y);
            var ray = _camera.ScreenPointToRay(_input.Movement.Mouse.ReadValue<Vector2>());
            
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                return (success: true, position: hitPoint);
            }

            return (success: false, position: Vector3.zero);
        }

        private void DrawAimLine()
        {
            if (_line == null)
                return;

            Vector3 lineEnd;
            if (Physics.SphereCast(_line.transform.position, _attackConfig.MagnetizationRadius, _line.transform.forward, out var hitInfo,
               _attackConfig.DamageDistance, ~9))
            {
                if (hitInfo.transform.gameObject.layer == 8)
                {
                    lineEnd = hitInfo.transform.position;
                }
                else
                {
                    lineEnd = hitInfo.point;
                }
                _line.SetPosition(1, _line.transform.InverseTransformPoint(lineEnd));
            }
            else
            {

                lineEnd = _line.transform.position + _line.transform.forward * _attackConfig.DamageDistance;
                lineEnd.y = _line.transform.position.y;
                _line.SetPosition(1, _line.transform.InverseTransformPoint(lineEnd));
            }
        }
    }
}