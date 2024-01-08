using UnityEngine;

namespace RunGun.Gameplay
{
    [CreateAssetMenu(menuName = "Create/Range Attack Config", fileName = "RangeAttackConfig", order = 0)]
    public class RangeAttackConfig : AttackConfig
    {
        [field: SerializeField] public int StyleSpend { get; private set; } = 1;
    }
}