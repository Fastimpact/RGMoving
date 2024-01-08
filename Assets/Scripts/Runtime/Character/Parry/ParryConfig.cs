using UnityEngine;

namespace RunGun.Gameplay
{
    [CreateAssetMenu(menuName = "Create/ParryConfig", fileName = "ParryConfig", order = 0)]
    public class ParryConfig : ScriptableObject
    {
        [field: SerializeField] public float Radius { get; private set; }

        [field: SerializeField] public float AddStyle { get; private set; } = 1;
        
        [field: SerializeField] public float AbilityIsFreeSeconds { get; private set; } = 4f;

    }
}