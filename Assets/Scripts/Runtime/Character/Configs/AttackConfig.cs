using UnityEngine;

namespace RunGun.Gameplay
{
    public abstract class AttackConfig : ScriptableObject
    {
        [field: SerializeField] public float MagnetizationRadius { get; private set; } = 0.3f;
        
        [field: SerializeField] public int Damage { get; private set; } = 1;

        [field: SerializeField] public int DamageToProtection { get; private set; } = 1;

        [field: SerializeField] public float DamageDistance { get; private set; } = 2.5f;

        [field: SerializeField] public float AttackAngle { get; private set; } = 120f;

        [field: SerializeField] public float AttackCooldown { get; private set; } = 1.5f;
        
        [field: SerializeField] public float AttackMoveSpeed { get; private set; } = 1.5f;
        
        [field: SerializeField] public TypeOfAttack TypeOfAttack { get; private set; }
        
        [field: SerializeField] public ChargedConfig ChargedConfig { get; private set; }
        
    }
}