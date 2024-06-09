using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Hydro
{
    public class HydroSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillComponent, HydroComponent>
            .Exclude<SkillContainerComponent> _filter;
        
        private readonly SkillsConfig _skillsConfig;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref SkillComponent skillComponent = ref _filter.Get1(i);
                ref HydroComponent pyroComponent = ref _filter.Get2(i);

                CheckHasParticle(ref pyroComponent, skillComponent);

                pyroComponent.HydroSkill.Play();
            }
        }
        private void CheckHasParticle(ref HydroComponent pyroComponent, SkillComponent skillComponent)
        {
            if (pyroComponent.HydroSkill == null)
            {
                GameObject particle = Object.Instantiate(_skillsConfig.PyroSkill.gameObject,
                    skillComponent.Transform);
                
                pyroComponent.HydroSkill = particle.GetComponent<ParticleSystem>();
            }
        }
    }
}
