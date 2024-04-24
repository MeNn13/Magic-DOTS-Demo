using Assets.Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Status.Pyro.Burning
{
    internal class HealthBurningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, BurningComponent> _filter;
        private readonly PyroParticlePool _particlePool;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref HealthComponent health = ref _filter.Get1(i);
                ref BurningComponent burning = ref _filter.Get2(i);

                TryTakeHealth(ref burning, ref health, ref entity);
            }
        }
        private void TryTakeHealth(ref BurningComponent burning, ref HealthComponent health, 
            ref EcsEntity entity)
        {
            if (burning.burningTime <= 0)
            {
                _particlePool.Release(burning.burningParticle);
                entity.Del<BurningComponent>();
                
                return;
            }

            burning.burningTime -= Time.deltaTime;
            health.health -= burning.multiplyDamage * Time.deltaTime;
        }
    }
}
