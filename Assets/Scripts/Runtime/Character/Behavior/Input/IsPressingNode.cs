using System;
using System.Threading.Tasks;
using BananaParty.BehaviorTree;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunGun.Gameplay
{
    public class IsPressingNode : BehaviorNode
    {
        private readonly InputAction _action;
        private readonly float _timeToPress;

        private float _time;
        
        public IsPressingNode(InputAction action, float timeToPress = 0f)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            _timeToPress = timeToPress;
            CheckPressing();
        }

        private async void CheckPressing()
        {
            while (true)
            {
                if (_action.IsPressed())
                {
                    _time += Time.deltaTime;
                }
                else
                {
                    _time = 0;
                }

                await Task.Yield();
            }
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_timeToPress != 0f)
            {
                return _time >= _timeToPress ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
            }

            return _action.IsPressed() ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}