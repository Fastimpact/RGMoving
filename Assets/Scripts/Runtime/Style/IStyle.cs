namespace RunGun.Gameplay
{
    public interface IStyle
    {
        bool HasStyle { get; }

        float Value { get; }

        float MaxValue { get; }

        void IncreaseStyle(float value);
        
        void SpendStyle(float value);
    }
}
