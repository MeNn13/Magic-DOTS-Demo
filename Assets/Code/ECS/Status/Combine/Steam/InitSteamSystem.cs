using Assets.Code.ECS.Status.Pool;
using Leopotam.Ecs;
using System.Linq;
using Code.ECS.Skill;
using UnityEngine;

namespace Assets.Code.ECS.Status.Combine.Steam
{
    internal class InitSteamSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SoggyComponent, BurningComponent>
            .Exclude<SteamComponent, SkillComponent> _filter;

        private readonly EffectConfig _config;
        private readonly SteamParticlePool _steamParticlePool;
        private readonly PyroParticlePool _pyroParticlePool;
        private readonly HydroParticlePool _hydroParticlePool;

        private EffectData _effect => _config.SteamData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SoggyComponent soggy = ref _filter.Get1(i);
                ref BurningComponent burning = ref _filter.Get2(i);
                ref SteamComponent steam = ref entity.Get<SteamComponent>();

                steam.objTransform = soggy.objTransform.gameObject.transform;
                steam.duration = _effect.Duration;

                ParticleSetup(ref steam);

                _hydroParticlePool.Release(soggy.particle);
                _pyroParticlePool.Release(burning.particle);

                entity.Del<SoggyComponent>();
                entity.Del<BurningComponent>();
            }
        }
       

        private void ParticleSetup(ref SteamComponent steam)
        {
            ParticleSystem particle = _steamParticlePool.Get();
            particle.transform.parent = steam.objTransform;
            particle.transform.localPosition = Vector3.zero;

            var shape = particle.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = steam.objTransform.GetComponent<MeshFilter>().mesh;

            steam.particle = particle;
        }
    }
}
