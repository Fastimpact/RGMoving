using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class DebugNode : BehaviorNode
    {
        private readonly string _line;

        public DebugNode(string line)
        {
            _line = line;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            Debug.Log(_line);
            return BehaviorNodeStatus.Success;
        }
    }
}