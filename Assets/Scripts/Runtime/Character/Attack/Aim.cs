namespace RunGun.Gameplay
{
    public class Aim
    {
        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
        }
        
        public void Deactivate()
        {
            IsActive = false;
        }
    }
}