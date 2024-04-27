using Assets.Code.ECS.Status.Pool;
using Leopotam.Ecs;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Code.ECS.Status.Pyro
{
    internal class InitBurnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BurnableComponent, BurnTriggerComponent>
            .Exclude<BurningComponent, SteamComponent> _filter;

        private readonly EffectConfig _config;
        private readonly PyroParticlePool _burnParticlePool;

        private AttackEffectData _effect => _config.BurningData as AttackEffectData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref BurnableComponent burnable = ref _filter.Get1(i);
                ref BurnTriggerComponent burnObject = ref _filter.Get2(i);

                ref BurningComponent burning = ref entity.Get<BurningComponent>();

                burning.objTransform = burnObject.collider.gameObject.transform;
                burning.damage = _effect.Damage;
                burning.duration = _effect.Duration;

                ParticleSetup(ref burning);

                entity.Del<BurnTriggerComponent>();
            }
        }

        private void ParticleSetup(ref BurningComponent burning)
        {
            ParticleSystem particle = _burnParticlePool.Get();

            particle.transform.parent = burning.objTransform.transform;
            particle.transform.localPosition = Vector3.zero;

            var shape = particle.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = burning.objTransform.GetComponent<MeshFilter>().mesh;

            burning.particle = particle;
        }
    }
}
