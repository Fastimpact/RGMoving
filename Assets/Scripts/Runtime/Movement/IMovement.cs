using UnityEngine;

namespace RunGun.Gameplay
{
    public interface IMovement
    {
        float StartSpeed { get; }
        
        float Speed { get; }
        
        void Move(Vector3 direction);
        
        void SetSpeed(float speed);
    }
}