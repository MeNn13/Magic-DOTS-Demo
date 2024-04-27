using UnityEngine;
using Leopotam.Ecs;
using Assets.Code.ECS.Status.Pool;

namespace Assets.Code.ECS.Status.Hydro.Soggy
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

                if (soggy.duration >= 0)
                {
                    soggy.duration -= Time.deltaTime;
                }
                else
                {
                    _hydroParticlePool.Release(soggy.particle);
                    entity.Del<SoggyComponent>();
                }
            }
        }
    }
}
