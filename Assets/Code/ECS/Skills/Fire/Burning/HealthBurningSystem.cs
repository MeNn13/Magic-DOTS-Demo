using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Assets.Code.ECS.Skills.Fire.Burning
{
    internal class HealthBurningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent, BurningComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref HealthComponent health = ref _filter.Get1(i);
                ref BurningComponent burning = ref _filter.Get2(i);

                BurningParticleActive(burning,out ParticleSystem _burningParticle);

                if (TryTakeHealth(ref burning, ref health))
                    _burningParticle.Play();
                else
                    _burningParticle.Stop();
            }
        }
        private void BurningParticleActive(BurningComponent burning, out ParticleSystem burnableParticle)
        {
            ParticleSystem particle = burning.burningObject
                .Find("Burning Particle")
                .GetComponent<ParticleSystem>();

            if (particle.isStopped)
                particle.Play();

            burnableParticle = particle;
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
