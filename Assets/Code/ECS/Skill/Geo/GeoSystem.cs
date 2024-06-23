using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Geo
{
    public class GeoSystem: IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<SkillComponent, GeoComponent>
                .Exclude<SkillContainerComponent> _filter;
        
            private readonly SkillsConfig _skillsConfig;
            private SkillData _skillData;
        
            public void Init()
            {
                foreach (var skill in _skillsConfig.Skills)
                    if (skill.name == "Geo")
                        _skillData = skill;
            }
        
            public void Run()
            {
                foreach (var i in _filter)
                {
                    ref EcsEntity entity = ref _filter.GetEntity(i);
                    ref SkillComponent skillComponent = ref _filter.Get1(i);
                    ref GeoComponent geoComponent = ref _filter.Get2(i);

                    CheckHasParticle(ref geoComponent, ref skillComponent);
                    
                    entity.Del<GeoComponent>();
                }
            }
            private void CheckHasParticle(ref GeoComponent ventoComponent, ref SkillComponent skillComponent)
            {
                if (ventoComponent.GeoSkill == null)
                {
                    GameObject instantiate = Object.Instantiate(_skillData.skillPrefab.gameObject,
                        skillComponent.Transform);
                
                    ventoComponent.GeoSkill = instantiate;
                    skillComponent.Duration = _skillData.duration;
                }
            }
    }
}
