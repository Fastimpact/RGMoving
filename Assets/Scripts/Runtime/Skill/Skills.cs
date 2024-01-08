using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RunGun.Gameplay
{
    public class Skills : MonoBehaviour
    {
        [SerializeField] private bool _skillsAreOpen;
        [SerializeField] private bool _debugMode;
        
        private readonly List<ISkill> _skills = new();
        private readonly StringBuilder _stringBuilder = new();
        
        private void Awake()
        {
            var skillTypes = Enum.GetValues(typeof(SkillType)).Cast<SkillType>();

            foreach (SkillType skillType in skillTypes)
            {
                ISkill skill = new Skill(skillType);
                
                _skills.Add(skill);
                
                if(_skillsAreOpen && !skill.IsOpen)
                    skill.Open();
            }
        }

        private void Update()
        {
            if (_debugMode)
            {
                foreach (ISkill skill in _skills)
                {
                    _stringBuilder.Append($"Skill with name {skill.Type.ToString()} is open: {skill.IsOpen} \n");
                }

                Debug.Log(_stringBuilder.ToString());
           
                _stringBuilder.Clear();
                _debugMode = false;
            }
        }

        public void Open(int skillType)
        {
            ISkill skill = GetSkill((SkillType)skillType);
         
            skill.Open();
        }

        public ISkill GetSkill(SkillType skillType)
        {
            return _skills.Find(skill => skill.Type == skillType);
        }
    }
}