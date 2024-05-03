using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill
{
    internal class SkillSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillComponent>
            .Exclude<SkillContainerComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SkillComponent skill = ref _filter.Get1(i);

                if (skill.Duration >= 0)
                    skill.Duration -= Time.deltaTime; 
                else
                {
                    Object.Destroy(skill.Transform.gameObject);
                    entity.Destroy();
                }
            }
        }
    }
}
