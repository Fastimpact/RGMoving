using UnityEngine;

namespace RunGun.Gameplay
{
    [CreateAssetMenu(menuName = "Create/CharacterConfig", fileName = "CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField] public int HealthMax { get; private set; } = 25;
        [field: SerializeField] public int StyleMax { get; private set; } = 25;

        [field: SerializeField] public int StyleName { get; private set; } = 5;
        [field: SerializeField] public float Speed { get; private set; } = 1.5f;
        
        [field: SerializeField] public float DashForce { get; private set; } = 5f;

        [field: SerializeField] public float ShortDashForce { get; private set; } = 2.5f;

        [field: SerializeField] public float ParryTime { get; private set; } = 1.2f;


    }
}