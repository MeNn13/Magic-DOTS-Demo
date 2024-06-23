using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Combine.Vape
{
    public class VapeSystem: IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<SkillComponent, VapeComponent>
            .Exclude<SkillContainerComponent> _filter;
        
        private readonly SkillsConfig _skillsConfig;
        
        private SkillData _skillData;
        
        public void Init()
        {
            foreach (var skill in _skillsConfig.Skills)
                if (skill.name == "Vape")
                    _skillData = skill;
        }
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref SkillComponent skillComponent = ref _filter.Get1(i);
                ref VapeComponent vapeComponent = ref _filter.Get2(i);

                CheckHasParticle(ref vapeComponent,ref skillComponent);
            }
        }
        private void CheckHasParticle(ref VapeComponent pyroComponent,ref SkillComponent skillComponent)
        {
            if (pyroComponent.VapeSkill == null)
            {
                GameObject instantiate = Object.Instantiate(_skillData.skillPrefab.gameObject,
                    skillComponent.Transform);
                
                pyroComponent.VapeSkill = instantiate;
                skillComponent.Duration = _skillData.duration;
            }
        }
    }
}
