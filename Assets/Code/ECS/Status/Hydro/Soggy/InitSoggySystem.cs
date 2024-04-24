using Assets.Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Status.Hydro.Soggy
{
    internal class InitSoggySystem : IEcsRunSystem
    {
        private readonly EcsFilter<WetComponent, WetTriggerComponent>.Exclude<SoggyComponent> _filter;

        private readonly EffectConfig _config;
        private readonly HydroParticlePool _hydroParticlePool;

        private EffectData _effect => _config.SoggyData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref WetComponent wet = ref _filter.Get1(i);
                ref WetTriggerComponent wetObject = ref _filter.Get2(i);

                ref SoggyComponent soggy = ref entity.Get<SoggyComponent>();

                soggy.objTransform = wetObject.collider.gameObject.transform;
                soggy.multiplyDamage = _effect.MultiplyDamage;
                soggy.duration = _effect.Duration;

                ParticleSetup(ref soggy, ref wetObject);

                entity.Del<WetTriggerComponent>();
            }
        }

        private void ParticleSetup(ref SoggyComponent soggy, ref WetTriggerComponent wetTrigger)
        {
            ParticleSystem particle = _hydroParticlePool.Get();
            particle.transform.parent = wetTrigger.collider.transform;
            particle.transform.localPosition = Vector3.zero;

            var shape = particle.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = soggy.objTransform.GetComponent<MeshFilter>().mesh;

            soggy.particle = particle;
        }
    }
}
