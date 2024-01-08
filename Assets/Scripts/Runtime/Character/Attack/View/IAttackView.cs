namespace RunGun.Gameplay
{
    public interface IAttackView
    {
        void ResetAnimatorSpeed();
        
        void MultiplyAnimatorSpeed(float multiplier);
        
        void Release();
    }
}