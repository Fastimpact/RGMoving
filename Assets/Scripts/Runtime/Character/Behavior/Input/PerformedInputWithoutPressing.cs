using System;
using BananaParty.BehaviorTree;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunGun.Gameplay
{
    public class PerformedInputWithoutPressing : BehaviorNode
    {
        private readonly InputAction _input;
        private readonly float _notPressingTime;
       
        private float _time;
        private bool _hasPerformed;

        public PerformedInputWithoutPressing(InputAction input, float notPressingTime = 0.15f)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _notPressingTime = notPressingTime;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            float passedTime = Time.time - _time;
            
            if (_hasPerformed && passedTime >= _notPressingTime)
            {
                _hasPerformed = false;
                return _input.IsPressed() ? BehaviorNodeStatus.Failure : BehaviorNodeStatus.Success;
            }
            
            if (_input.WasPerformedThisFrame())
            {
                _time = Time.time;
                _hasPerformed = true;
            }

            return BehaviorNodeStatus.Failure;
        }
    }
}