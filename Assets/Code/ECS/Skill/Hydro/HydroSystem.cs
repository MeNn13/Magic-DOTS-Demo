using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Hydro
{
    public class HydroSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<SkillComponent, HydroComponent>
            .Exclude<SkillContainerComponent> _filter;
        
        private readonly SkillsConfig _skillsConfig;
        
        private SkillData _skillData;
        
        public void Init()
        {
            foreach (var skill in _skillsConfig.Skills)
                if (skill.name == "Hydro")
                    _skillData = skill;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref SkillComponent skillComponent = ref _filter.Get1(i);
                ref HydroComponent hydroComponent = ref _filter.Get2(i);

                CheckHasParticle(ref hydroComponent,ref skillComponent);
            }
        }
        private void CheckHasParticle(ref HydroComponent pyroComponent,ref SkillComponent skillComponent)
        {
            if (pyroComponent.HydroSkill == null)
            {
                GameObject instantiate = Object.Instantiate(_skillData.skillPrefab,
                    skillComponent.Transform);
                
                pyroComponent.HydroSkill = instantiate;
                skillComponent.Duration = _skillData.duration;
            }
        }
    }
}
