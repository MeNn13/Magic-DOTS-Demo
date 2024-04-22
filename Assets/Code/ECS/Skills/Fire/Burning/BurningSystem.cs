using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Skills.Fire.Burning
{
    public class BurningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BurningComponent>.Exclude<HealthComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref BurningComponent burning = ref _filter.Get1(i);

                BurningParticleActive(burning);

                TryDestroyObject(ref burning, ref entity);
            }
        }

        private void BurningParticleActive(BurningComponent burning)
        {
            ParticleSystem particle = burning.burningObject
                .Find("Burning Particle")
                .GetComponent<ParticleSystem>();

            if (particle.isStopped)
                particle.Play();
        }

        private void TryDestroyObject(ref BurningComponent burning, ref EcsEntity entity)
        {
            if (burning.burningTime <= 0)
            {
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