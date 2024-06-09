using System.Collections.Generic;
using Code.ECS.Skill.Pyro;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill
{
    internal class SkillSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillComponent>
            .Exclude<SkillContainerComponent> _skillFilter;
        
        public void Run()
        {
            foreach (var i in _skillFilter)
            {
                ref EcsEntity entity = ref _skillFilter.GetEntity(i);
                ref SkillComponent skill = ref _skillFilter.Get1(i);

                if (skill.Transform == null)
                    return;

                if (skill.Duration >= 0)
                    skill.Duration -= Time.deltaTime;
                else
                    DestroySkill(ref entity, ref skill);
            }
        }

        private void DestroySkill(ref EcsEntity entity, ref  SkillComponent skill)
        {
            Object.Destroy(skill.Transform.gameObject);
            entity.Destroy();
        }
    }
}
