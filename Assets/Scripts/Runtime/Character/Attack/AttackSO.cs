using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGun.Gameplay
{
    [CreateAssetMenu(menuName = "Create/Attacks/Normal Attack")]
    public class AttackSO : ScriptableObject
    {
        public AnimatorOverrideController AnimatorOverride;
        public float Damage;
    }
}
