using System;
using UnityEngine;
using BananaParty.BehaviorTree;

namespace RunGun.Gameplay
{
    public class BackToStartNeedStyleAbilityNode : BehaviorNode
    {
        private readonly IAbility _ability;

        public BackToStartNeedStyleAbilityNode(IAbility ability)
        {
            _ability = ability ?? throw new ArgumentNullException(nameof(ability));
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _ability.BackToStartNeedStyle();
            return BehaviorNodeStatus.Success;
        }
    }
}