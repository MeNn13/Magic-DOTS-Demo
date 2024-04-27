using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Skill.Cooldown
{
    internal class SkillCooldownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillCooldownComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var skillCooldown = ref _filter.Get1(i);

                if (skillCooldown.cooldown > 0)
                    skillCooldown.cooldown -= Time.deltaTime;
                else
                    entity.Del<SkillCooldownComponent>();
            }
        }
    }
}
