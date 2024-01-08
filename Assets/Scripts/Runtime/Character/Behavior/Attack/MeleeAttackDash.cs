using System;
using System.Threading.Tasks;
using BananaParty.BehaviorTree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

namespace RunGun.Gameplay
{
    public class MeleeAttackDash : BehaviorNode
    {
        private readonly CharacterController _controller;
        private readonly IAttackView _dashView;
        private readonly Animator _animator;
        private readonly float _distance;
        private readonly float _searchRadius;

        private float _speed;
        private readonly int _dash = Animator.StringToHash("isMeleeDash");
        private readonly float _dashResetTime = 0.1f;

        private float _dashing;
        private Vector3 _moveDirection;

        public MeleeAttackDash(CharacterController controller, Animator animator, float speed, float searchRadius , IAttackView dashView)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _distance = speed;
            _searchRadius = searchRadius;
            _dashView = dashView ?? throw new ArgumentNullException(nameof(dashView));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {            
            if (Status == BehaviorNodeStatus.Idle)
            {
                _dashing = 0f;

                _moveDirection = _animator.transform.forward;

                if (Vector3.Angle(Vector3.forward, _moveDirection) > 1f ||
                Vector3.Angle(Vector3.forward, _moveDirection) == 0)
                {
                    Vector3 direct = Vector3.RotateTowards(_controller.transform.forward, _moveDirection, 2.5f, 0.0f);
                    _controller.transform.rotation = Quaternion.LookRotation(direct);
                }
                _dashView.Release();
                _animator.SetTrigger("Punch");
                _animator.SetBool(_dash, true);

                _speed = _distance / _dashResetTime;
            }

            var haveEnemies = CheckForEnemies();
            var haveGround = CheckForDamageZone();

            //Debug.Log($"{haveEnemies} + {haveGround}");

            if (haveEnemies || !haveGround)
            {
                _animator.SetBool(_dash, false);
                return BehaviorNodeStatus.Success;
            }

            if (_dashing < _distance)
            {
                _controller.Move(_moveDirection * _speed * Time.deltaTime);
                _dashing += _speed * Time.deltaTime;
            }

            if (_dashing >= _distance)
            {
                _animator.SetBool(_dash, false);
                return BehaviorNodeStatus.Success;
            }
            return BehaviorNodeStatus.Running;
        }

        private bool CheckForEnemies()
        {
            var enemies = _controller.transform.FindObjectsNear(_searchRadius);
            return enemies.Count > 0 ? true : false;
        }

        private bool CheckForDamageZone()
        {
            var position = _controller.transform.position + Vector3.forward * 0.3f;
            return Physics.Raycast(position, Vector3.down, 1f, LayerMask.GetMask("Floor"));
        }

    }
}