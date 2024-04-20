using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Skills.Fire.Burning
{
    public class BurningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BurningComponent> _filter;
        private ParticleSystem _particlePref;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref BurningComponent burning = ref _filter.Get1(i);

                if (TryDestroyObject(ref burning, ref entity))
                    return;

                SetParticleValue(burning);
            }
        }

        private void SetParticleValue(BurningComponent burning)
        {
            ParticleSystem particle = burning.burningObject.GetComponentInChildren<ParticleSystem>();

            if (particle != null)
                return;

            particle = Object.Instantiate(_particlePref, burning.burningObject);
            var shape = particle.shape;
            shape.mesh = burning.mesh;
        }

        private bool TryDestroyObject(ref BurningComponent burning, ref EcsEntity entity)
        {
            if (burning.burningTime <= 0)
            {
                Object.Destroy(burning.burningObject.gameObject);
                entity.Destroy();
                return true;
            }
            else
            {
                burning.burningTime -= Time.deltaTime;
                return false;
            }
        }
    }
}