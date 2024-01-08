using System;
using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class GravitationNode : BehaviorNode
    {
        private readonly CharacterController _controller;
        private readonly Animator _animator;
        private readonly int _isFall = Animator.StringToHash("isFall");
       
        private float _velocity;
        private Vector3 _fallPosition;
        private bool _hasFall;

        public GravitationNode(CharacterController controller, Animator animator, Vector3 fallPosition)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _fallPosition = fallPosition;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _velocity += Physics.gravity.y * 5 * Time.deltaTime;
            if(_controller.enabled)
                _controller.Move(_controller.transform.up * _velocity * Time.deltaTime);

            FallCheck();

            if (_hasFall)
                return CheckStatus();
            
            _velocity = 0;
            return BehaviorNodeStatus.Failure;
        }
        
        private void FallCheck()
        {
            if (!Physics.Raycast(_controller.transform.position, Vector3.down, 2f))
                _animator.SetBool(_isFall, true);
            else if (Physics.Raycast(_controller.transform.position, Vector3.down, 0.8f))
            {
                _animator.SetBool(_isFall, false);
                CheckDamageZone();
            }
            else if (Physics.Raycast(_controller.transform.position, Vector3.down, 0.2f))
                _hasFall = false;
            if (!Physics.Raycast(_controller.transform.position, Vector3.down, 0.3f))
                _hasFall = true;

        }

        private BehaviorNodeStatus CheckStatus()
        {
            if (Physics.Raycast(_controller.transform.position, Vector3.down, 0.2f))
            {
                _hasFall = false;
                _velocity = 0;
                return BehaviorNodeStatus.Success;
            }

            return BehaviorNodeStatus.Running;
        }

        private void CheckDamageZone()
        {
            if (Physics.Raycast(_controller.transform.position, Vector3.down, 0.8f, LayerMask.GetMask("DamageZone")))
            {
                _controller.enabled = false;
                _controller.transform.position = _fallPosition;
                _controller.enabled = true;
            }
            else
            {
                _fallPosition = _controller.transform.position;
            }
        }
    }
}