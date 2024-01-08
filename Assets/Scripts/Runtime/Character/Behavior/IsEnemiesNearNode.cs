using BananaParty.BehaviorTree;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class IsEnemiesNearNode : BehaviorNode
    {
        private readonly Transform _transform;
        private readonly float _searchDistance;
        public IsEnemiesNearNode(Transform transform, float searchDistance)
        {
            _transform = transform;
            _searchDistance = searchDistance;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            var enemies = _transform.FindObjectsNear(_searchDistance);
            return enemies.Count > 0 ? BehaviorNodeStatus.Failure : BehaviorNodeStatus.Success;
        }
    }
}
