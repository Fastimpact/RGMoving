using System;
using UnityEngine;

namespace RunGun.Gameplay
{
    [Serializable]
    public class ChargedConfig
    {
        [field: SerializeField] public int AttacksCount { get; private set; } = 2;

        [field: SerializeField] public int DamageCoefficient { get; private set; } = 2;

        [field: SerializeField] public float Speed { get; private set; } = 0.15f;
    }
}