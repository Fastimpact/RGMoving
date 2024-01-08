using System.Collections.Generic;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public static class BehaviorTreeExtensions
    {
        public static void Update(this List<BehaviorNode> behaviorTrees, long time)
        {
            for (var i = 0; i < behaviorTrees.Count; i++)
            {
                var behaviorTree = behaviorTrees[i];
              
                if (behaviorTree.Status == BehaviorNodeStatus.Success)
                    behaviorTree.Reset();

                behaviorTree.Execute(time);
            }
        }
    }
}