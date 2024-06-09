using Code.ECS.Status.Combine.Steam;
using Code.ECS.Status.Hydro.Wet;
using Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status.Hydro.Soggy
{
    internal class InitSoggySystem : IEcsRunSystem
    {
        private readonly EcsFilter<WetComponent, WetTriggerComponent>
            .Exclude<SoggyComponent, SteamComponent> _filter;

        private readonly EffectConfig _config;
        private readonly HydroParticlePool _hydroParticlePool;

        private EffectData Effect => _config.SoggyData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref WetTriggerComponent wetObject = ref _filter.Get2(i);

                ref SoggyComponent soggy = ref entity.Get<SoggyComponent>();

                soggy.ObjTransform = wetObject.Collider.gameObject.transform;
                soggy.Duration = Effect.Duration;

                ParticleSetup(ref soggy, ref wetObject);

                entity.Del<WetTriggerComponent>();
            }
        }

        private void ParticleSetup(ref SoggyComponent soggy, ref WetTriggerComponent wetTrigger)
        {
            ParticleSystem particle = _hydroParticlePool.Get();
            particle.transform.parent = wetTrigger.Collider.transform;
            particle.transform.localPosition = Vector3.zero;

            var shape = particle.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = soggy.ObjTransform.GetComponent<MeshFilter>().mesh;

            soggy.Particle = particle;
        }
    }
}
