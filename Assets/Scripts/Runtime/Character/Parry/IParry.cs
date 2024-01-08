namespace RunGun.Gameplay
{
    public interface IParry
    {
        bool IsActive { get; }

        void Activate(float seconds);

        void Use();
    }
}