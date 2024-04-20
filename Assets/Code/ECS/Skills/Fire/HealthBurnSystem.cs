using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Skills.Fire
{
    internal class HealthBurnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, BurningComponent> _filter;
        private readonly ParticleSystem _particlePref;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref HealthComponent health = ref _filter.Get1(i);
                ref BurningComponent burning = ref _filter.Get2(i);

                SetParticleValue(burning, TryTakeHealth(ref burning, ref health));
            }
        }

        private void SetParticleValue(BurningComponent burning, bool isBurning)
        {
            ParticleSystem particle = burning.burningObject.GetComponentInChildren<ParticleSystem>();

            if (!isBurning && particle != null)
                Object.Destroy(particle.gameObject);

            if (isBurning && particle == null)
            {
                particle = Object.Instantiate(_particlePref, burning.burningObject);
                var shape = particle.shape;
                shape.mesh = burning.mesh;
            }
        }

        private bool TryTakeHealth(ref BurningComponent burning, ref HealthComponent health)
        {
            if (burning.burningTime <= 0)
                return false;

            burning.burningTime -= Time.deltaTime;
            health.health -= burning.multiplyDamage * Time.deltaTime;
            return true;
        }
    }
}
