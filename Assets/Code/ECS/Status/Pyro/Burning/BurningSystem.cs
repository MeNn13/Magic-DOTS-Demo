using Assets.Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Status.Pyro.Burning
{
    public class BurningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BurningComponent>
            .Exclude<HealthComponent, SkillComponent> _filter;
        private readonly PyroParticlePool _particlePool;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref BurningComponent burning = ref _filter.Get1(i);

                TryDestroyObject(ref burning, ref entity);
            }
        }

        private void TryDestroyObject(ref BurningComponent burning, ref EcsEntity entity)
        {
            if (burning.burningTime <= 0)
            {
                _particlePool.Release(burning.burningParticle);
                Object.Destroy(burning.burningObject.gameObject);
                entity.Destroy();
            }
            else
            {
                burning.burningTime -= Time.deltaTime;
            }
        }
    }
}