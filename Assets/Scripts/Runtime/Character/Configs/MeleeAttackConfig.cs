using System.Collections.Generic;
using UnityEngine;

namespace RunGun.Gameplay
{
    [CreateAssetMenu(menuName = "Create/Melee Attack Config", fileName = "MeleeAttackConfig", order = 0)]
    public class MeleeAttackConfig : AttackConfig
    {
        [field: SerializeField] public int HealingHealth { get; private set; } = 1;

        [field: SerializeField] public int StyleUp { get; private set; } = 1;
        [field: SerializeField] public List<AttackSO> Combo { get; private set; }

    }
}