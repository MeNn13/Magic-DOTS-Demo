using Code.ECS.Status.Burnable;
using Code.ECS.Status.Burning;
using Code.ECS.Status.Combine.Steam;
using Code.ECS.Status.Pool;
using Code.ScriptableObjects.Status_Effect;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status
{
    internal class InitBurnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BurnableComponent, BurnTriggerComponent>
            .Exclude<BurningComponent, SteamComponent> _filter;

        private readonly EffectConfig _config;
        private readonly PyroParticlePool _burnParticlePool;

        private AttackEffectData Effect => _config.BurningData as AttackEffectData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref BurnTriggerComponent burnObject = ref _filter.Get2(i);

                ref BurningComponent burning = ref entity.Get<BurningComponent>();

                burning.ObjTransform = burnObject.Collider.gameObject.transform;
                burning.Damage = Effect.Damage;
                burning.Duration = Effect.Duration;

                ParticleSetup(ref burning);

                entity.Del<BurnTriggerComponent>();
            }
        }

        private void ParticleSetup(ref BurningComponent burning)
        {
            ParticleSystem particle = _burnParticlePool.Get();

            var transform = particle.transform;
            transform.parent = burning.ObjTransform.transform;
            transform.localPosition = Vector3.zero;

            var shape = particle.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = burning.ObjTransform.GetComponent<MeshFilter>().mesh;

            burning.Particle = particle;
        }
    }
}
