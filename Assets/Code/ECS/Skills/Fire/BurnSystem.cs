﻿using Assets.Code.ECS.Skills.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Skills.Fire
{
    internal class BurnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BurnableComponent, BurnTriggerComponent>.Exclude<BurningComponent> _filter;
        private readonly EffectConfig _config;
        private readonly BurnParticlePool _burnParticlePool;

        private EffectData _effect => _config.BurningData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref BurnableComponent burnable = ref _filter.Get1(i);
                ref BurnTriggerComponent burnObject = ref _filter.Get2(i);

                ref BurningComponent burning = ref entity.Get<BurningComponent>();

                burning.burningObject = burnObject.collider.gameObject.transform;
                burning.multiplyDamage = _effect.MultiplyDamage;
                burning.burningTime = _effect.Duration;
                burning.burningTime = _effect.Duration;

                ParticleSetup(ref burning, ref burnObject);

                entity.Del<BurnTriggerComponent>();
            }
        }

        private void ParticleSetup(ref BurningComponent burning, ref BurnTriggerComponent burnTrigger)
        {
            ParticleSystem particle = _burnParticlePool.Get();
            particle.transform.parent = burnTrigger.collider.transform;
            particle.transform.localPosition = Vector3.zero;

            var shape = particle.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = burning.burningObject.GetComponent<MeshFilter>().mesh;

            burning.burningParticle = particle;
        }
    }
}
