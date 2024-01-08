using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class NodeFromConditionSelector : BehaviorNode
    {
        private readonly IBehaviorNode _conditionNode;
        private readonly IBehaviorNode _firstNode;
        private readonly IBehaviorNode _secondNode;

        public NodeFromConditionSelector(IBehaviorNode conditionNode, IBehaviorNode firstNode, IBehaviorNode secondNode)
        {
            _conditionNode = conditionNode;
            _firstNode = firstNode;
            _secondNode = secondNode;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            return _conditionNode.Execute(time) == BehaviorNodeStatus.Success ? _firstNode.Execute(time) : _secondNode.Execute(time);
        }
    }
}