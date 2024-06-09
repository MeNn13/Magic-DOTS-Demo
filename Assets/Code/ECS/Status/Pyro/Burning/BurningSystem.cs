using Code.ECS.Health;
using Code.ECS.Skill;
using Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status.Burning
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
            if (burning.Duration <= 0)
            {
                _particlePool.Release(burning.Particle);
                Object.Destroy(burning.ObjTransform.gameObject);
                entity.Destroy();
            }
            else
            {
                burning.Duration -= Time.deltaTime;
            }
        }
    }
}