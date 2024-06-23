using Code.ECS.Pool;
using Code.ECS.Skill;
using Code.ECS.Status.Burning;
using Code.ECS.Status.Hydro.Soggy;
using Code.ECS.Status.Pool;
using Code.ScriptableObjects.Status_Effect;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status.Combine.Steam
{
    internal class InitSteamSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SoggyComponent, BurningComponent>
            .Exclude<SteamComponent, SkillComponent> _filter;

        private readonly EffectConfig _config;
        private readonly SteamParticlePool _steamParticlePool;
        private readonly PyroParticlePool _pyroParticlePool;
        private readonly HydroParticlePool _hydroParticlePool;

        private AttackEffectData Effect => _config.SteamData as AttackEffectData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SoggyComponent soggy = ref _filter.Get1(i);
                ref BurningComponent burning = ref _filter.Get2(i);
                
                ref SteamComponent steam = ref entity.Get<SteamComponent>();
                steam.ObjTransform = soggy.ObjTransform.gameObject.transform;
                steam.Duration = Effect.Duration;
                steam.Damage = Effect.Damage;

                ParticleSetup(ref steam);

                _hydroParticlePool.Release(soggy.Particle);
                _pyroParticlePool.Release(burning.Particle);

                entity.Del<SoggyComponent>();
                entity.Del<BurningComponent>();
            }
        }
       

        private void ParticleSetup(ref SteamComponent steam)
        {
            ParticleSystem particle = _steamParticlePool.Get();
            particle.transform.parent = steam.ObjTransform;
            particle.transform.localPosition = Vector3.zero;

            var shape = particle.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = steam.ObjTransform.GetComponent<MeshFilter>().mesh;

            steam.Particle = particle;
        }
    }
}
