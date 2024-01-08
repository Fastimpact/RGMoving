using SaveSystem;
using SaveSystem.Paths;

namespace RunGun.Gameplay
{
    public class Skill : ISkill
    {
        private readonly ISaveStorage<bool> _isOpenStorage;
        
        public Skill(SkillType type)
        {
            Type = type;
            _isOpenStorage = new BinaryStorage<bool>(new Path($"skill {type.ToString()} "));
            
            IsOpen = _isOpenStorage.HasSave() ? _isOpenStorage.Load() : false;
        }

        public SkillType Type { get; }
        
        public bool IsOpen { get; private set; }

        public void Open()
        {
            IsOpen = true;
            _isOpenStorage.Save(true);
        }
    }
}