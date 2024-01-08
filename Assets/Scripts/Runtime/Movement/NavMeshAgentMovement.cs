using System;
using UnityEngine;
using UnityEngine.AI;

namespace RunGun.Gameplay
{
    public class NavMeshAgentMovement : IAdjustableMovement
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _animationSpeed;

        private readonly int _forward = Animator.StringToHash("Forward");
        private readonly int _moveSpeed = Animator.StringToHash("MoveSpeed");

        public NavMeshAgentMovement(NavMeshAgent navMeshAgent, Animator animator, float animationSpeed)
        {
            _navMeshAgent = navMeshAgent ?? throw new ArgumentNullException(nameof(navMeshAgent));
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _animationSpeed = animationSpeed;
            StartSpeed = _navMeshAgent.speed;
        }

        public float StartSpeed { get; }

        public float Speed => _navMeshAgent.speed;
        
        public void Move(Vector3 direction)
        {
            if (_navMeshAgent.enabled == false)
                return;

            _navMeshAgent.SetDestination(_navMeshAgent.transform.position + direction);
            _animator.SetFloat(_forward, 1);
            _animator.SetFloat(_moveSpeed, _animationSpeed);
        }

        public void SetSpeed(float speed)
        {
            _navMeshAgent.speed = speed;
        }

        public void Start()
        {
            _navMeshAgent.enabled = true;
        }

        public void Stop()
        {
            _animator.SetFloat(_forward, 0);
            _navMeshAgent.enabled = false;
        }
    }
}