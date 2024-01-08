using System;
using BananaParty.BehaviorTree;
using UnityEngine;
using Random = System.Random;

namespace RunGun.Gameplay
{
    public class RandomNode : BehaviorNode
    {
        private readonly IBehaviorNode[] _behaviorNodes;
        private readonly Random _random;

        private IBehaviorNode _randomSelectedNode;
        
        public RandomNode(params IBehaviorNode[] behaviorNodes)
        {
            _behaviorNodes = behaviorNodes ?? throw new ArgumentNullException(nameof(behaviorNodes));
            _random = new Random();
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (_randomSelectedNode == null)
            {
                int randomIndex = _random.Next(0, _behaviorNodes.Length);
                _randomSelectedNode = _behaviorNodes[randomIndex];
            }

            return _randomSelectedNode.Execute(time);
        }

        public override void OnReset()
        {
            _randomSelectedNode = null;
        }
    }
}