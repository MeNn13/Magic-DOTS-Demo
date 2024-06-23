using Code.ECS.Skill;
using Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status.Combine.Steam
{
    internal class SteamSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SteamComponent>
            .Exclude<SkillComponent> _filter;

        private readonly SteamParticlePool _steamParticlePool;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SteamComponent steam = ref _filter.Get1(i);

                if (steam.Duration >= 0)
                {
                    steam.Duration -= Time.deltaTime;
                }
                else
                {
                    _steamParticlePool.Release(steam.Particle);
                    entity.Del<SteamComponent>();
                    entity.Del<SteamDamageTriggerComponent>();
                }
            }
        }
    }
}
