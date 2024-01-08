using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunGun.Gameplay
{
    public class CharacterMovement : IMovement
    {
        private readonly CharacterController _controller;
        private readonly Animator _animator;

        private readonly int _forward = Animator.StringToHash("Forward");
        private readonly int _moveSpeed = Animator.StringToHash("MoveSpeed");

        public CharacterMovement(CharacterController controller, Animator animator, float startSpeed)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            Speed = StartSpeed = startSpeed;
        }

        public float StartSpeed { get; }

        public float Speed { get; private set; }

        public void Move(Vector3 direction)
        {
            if (Vector3.Angle(Vector3.forward, direction) > 1f || Vector3.Angle(Vector3.forward, direction) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(_controller.transform.forward, direction, 2.5f, 0.0f);
                    _controller.transform.rotation = Quaternion.LookRotation(direct);
            }

            if (Keyboard.current.spaceKey.isPressed)
            {
                Speed = StartSpeed * 2f;
            }
            else
            {
                if (Speed > StartSpeed)
                    Speed = StartSpeed;
            }

            _controller.Move(direction * Time.deltaTime * Speed);
            _animator.SetFloat(_forward, 1);
            _animator.SetFloat(_moveSpeed, 1.2f);
        }

        public void SetSpeed(float speed)
        {
            Speed = speed;
        }
    }
}