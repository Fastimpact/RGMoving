using System;

namespace RunGun.Gameplay
{
    public class Ability : IAbility
    {
        private readonly int _startStyle;

        public Ability(int needStyle)
        {
            if (needStyle <= 0)
                throw new ArgumentOutOfRangeException(nameof(needStyle));
            
            _startStyle = NeedStyle = needStyle;
        }

        public bool IsActive { get; private set; }
        
        public int NeedStyle { get; private set; }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void MakeFree()
        {
            NeedStyle = 0;
        }

        public void BackToStartNeedStyle()
        {
            NeedStyle = _startStyle;
        }
    }
}