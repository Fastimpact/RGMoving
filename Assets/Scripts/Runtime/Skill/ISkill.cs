namespace RunGun.Gameplay
{
    public interface ISkill
    {
        bool IsOpen { get; }
        
        SkillType Type { get; }
        
        void Open();
    }
}