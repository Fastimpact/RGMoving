using System;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class SkillIsOpenNode : BehaviorNode
    {
        private readonly SkillType _skillType;
        private readonly Skills _skills;

        public SkillIsOpenNode(SkillType skillType, Skills skills)
        {
            _skillType = skillType;
            _skills = skills ?? throw new ArgumentNullException(nameof(skills));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            ISkill skill = _skills.GetSkill(_skillType);
            
            return skill.IsOpen ? BehaviorNodeStatus.Success : BehaviorNodeStatus.Failure;
        }
    }
}