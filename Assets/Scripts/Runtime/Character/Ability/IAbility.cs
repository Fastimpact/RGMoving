namespace RunGun.Gameplay
{
    public interface IAbility
    {
        bool IsActive { get; }
        
        int NeedStyle { get; }
        
        void Activate();

        void Deactivate();

        void MakeFree();

        void BackToStartNeedStyle();
    }
}