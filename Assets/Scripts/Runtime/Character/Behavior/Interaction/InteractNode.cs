using System;
using BananaParty.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace RunGun.Gameplay
{
    public class InteractNode : BehaviorNode
    {
        private readonly InteractSensor _sensor;
        private readonly CharacterController _controller;
        private readonly CharacterInput _input;
        private readonly InteractView _view;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly int _forward = Animator.StringToHash("Forward");
        private readonly int _moveSpeed = Animator.StringToHash("MoveSpeed");

        private Vector3 _walkPoint;
        private bool _isWalkingToPoint = true;

        public InteractNode(InteractSensor sensor, InteractView view, Animator animator, NavMeshAgent navMeshAgent, CharacterController controller, CharacterInput input)
        {
            _sensor = sensor;
            _view = view;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
            _controller = controller;
            _input = input;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if ( _isWalkingToPoint)
            {
                _navMeshAgent.enabled = true;
                _walkPoint = _sensor.IntreactObjectTransform.position;
                _isWalkingToPoint = true;
                _animator.SetFloat(_forward, 1);
                _animator.SetFloat(_moveSpeed, 1.2f);
                _controller.enabled = false;
                _input.Disable();
                _navMeshAgent.SetDestination(_walkPoint);

                if (_isWalkingToPoint && (_walkPoint - _navMeshAgent.transform.position).sqrMagnitude <= 0.1f)
                {
                    _isWalkingToPoint = false;
                    _navMeshAgent.enabled = false;
                    _controller.transform.rotation *= Quaternion.Euler(0,180,0);
                    _controller.enabled = true;                    
                    _input.Enable();
                    _animator.SetFloat(_forward, 0);
                    _animator.SetTrigger("enableSiting");
                    _animator.SetBool("isSitting", true);
                    
                }
                return BehaviorNodeStatus.Running;
            }
            else
            {
                _isWalkingToPoint = true;
                _sensor.InteractableObject.OnInteract?.Invoke();
                _view.Hide();
                return BehaviorNodeStatus.Success;
            }            
        }
    }
}
