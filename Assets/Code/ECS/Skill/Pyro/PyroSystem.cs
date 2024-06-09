using Code.ScriptableObjects;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Pyro
{
    public class PyroSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillComponent, PyroComponent>
            .Exclude<SkillContainerComponent> _filter;
        
        private readonly SkillsConfig _skillsConfig;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref SkillComponent skillComponent = ref _filter.Get1(i);
                ref PyroComponent pyroComponent = ref _filter.Get2(i);

                CheckHasParticle(ref pyroComponent, skillComponent);

                pyroComponent.PyroSkill.Play();
            }
        }
        private void CheckHasParticle(ref PyroComponent pyroComponent, SkillComponent skillComponent)
        {
            if (pyroComponent.PyroSkill == null)
            {
                GameObject particle = Object.Instantiate(_skillsConfig.PyroSkill.gameObject,
                    skillComponent.Transform);
                
                pyroComponent.PyroSkill = particle.GetComponent<ParticleSystem>();
            }
        }
    }
}
