using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public interface ICombo : IBehaviorNode
    {
        float NormalizedTime { get; }
        
        int AttacksCount { get; }
        
        void IncreaseAttacksCount(int count);
    }
}