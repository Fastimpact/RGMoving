using System;

namespace RunGun.Gameplay
{
    public class MovementStop
    {
        private readonly IAdjustableMovement _movement;

        public MovementStop(IAdjustableMovement movement)
        {
            _movement = movement ?? throw new ArgumentNullException(nameof(movement));
        }

        public bool IsActive { get; private set; }
        
        public void Activate()
        {
            IsActive = true;
            _movement.Stop();
        }

        public void Deactivate()
        {
            IsActive = false;
            _movement.Start();
        }
    }
}