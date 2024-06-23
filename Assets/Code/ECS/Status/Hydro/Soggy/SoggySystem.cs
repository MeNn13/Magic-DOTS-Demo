using Code.ECS.Pool;
using Code.ECS.Skill;
using Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status.Hydro.Soggy
{
    internal class SoggySystem : IEcsRunSystem
    {
        private readonly EcsFilter<SoggyComponent>
            .Exclude<SkillComponent> _filter;
        private readonly HydroParticlePool _hydroParticlePool;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SoggyComponent soggy = ref _filter.Get1(i);

                if (soggy.Duration >= 0)
                {
                    soggy.Duration -= Time.deltaTime;
                }
                else
                {
                    _hydroParticlePool.Release(soggy.Particle);
                    entity.Del<SoggyComponent>();
                }
            }
        }
    }
}
