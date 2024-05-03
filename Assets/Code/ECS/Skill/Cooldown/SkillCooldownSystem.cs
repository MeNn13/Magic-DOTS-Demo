using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Skill.Cooldown
{
    internal class SkillCooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillCooldownComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SkillCooldownComponent skillCooldown = ref _filter.Get1(i);

                if (skillCooldown.Cooldown > 0)
                    skillCooldown.Cooldown -= Time.deltaTime;
                else
                    entity.Del<SkillCooldownComponent>();
            }
        }
    }
}
