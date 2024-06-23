using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Pyro
{
    public class PyroSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<SkillComponent, PyroComponent>
            .Exclude<SkillContainerComponent> _filter;
        
        private readonly SkillsConfig _skillsConfig;
        private SkillData _skillData;
        
        public void Init()
        {
            foreach (var skill in _skillsConfig.Skills)
                if (skill.name == "Pyro")
                    _skillData = skill;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref SkillComponent skillComponent = ref _filter.Get1(i);
                ref PyroComponent ventoComponent = ref _filter.Get2(i);

                CheckHasParticle(ref ventoComponent, ref skillComponent);
            }
        }
        private void CheckHasParticle(ref PyroComponent ventoComponent, ref SkillComponent skillComponent)
        {
            if (ventoComponent.PyroSkill == null)
            {
                GameObject particle = Object.Instantiate(_skillData.skillPrefab,
                    skillComponent.Transform);
                
                ventoComponent.PyroSkill = particle;
                skillComponent.Duration = _skillData.duration;
            }
        }
    }
}
