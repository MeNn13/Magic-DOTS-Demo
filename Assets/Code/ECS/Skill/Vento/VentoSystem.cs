using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Vento
{
    public class VentoSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<SkillComponent, VentoComponent>
            .Exclude<SkillContainerComponent> _filter;
        
        private readonly SkillsConfig _skillsConfig;
        private SkillData _skillData;
        
        public void Init()
        {
            foreach (var skill in _skillsConfig.Skills)
                if (skill.name == "Vento")
                    _skillData = skill;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref SkillComponent skillComponent = ref _filter.Get1(i);
                ref VentoComponent ventoComponent = ref _filter.Get2(i);

                CheckHasParticle(ref ventoComponent,ref skillComponent);
            }
        }
        private void CheckHasParticle(ref VentoComponent ventoComponent,ref SkillComponent skillComponent)
        {
            if (ventoComponent.VentoSkill == null)
            {
                GameObject instantiate = Object.Instantiate(_skillData.skillPrefab,
                    skillComponent.Transform);
                
                ventoComponent.VentoSkill = instantiate;
                skillComponent.Duration = _skillData.duration;
            }
        }
    }
}
