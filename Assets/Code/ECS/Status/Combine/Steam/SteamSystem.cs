using Assets.Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Status.Combine.Steam
{
    internal class SteamSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SteamComponent>
            .Exclude<SkillComponent> _filter;

        private readonly SteamParticlePool _steamParticlePool;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SteamComponent steam = ref _filter.Get1(i);

                if (steam.duration >= 0)
                {
                    steam.duration -= Time.deltaTime;
                }
                else
                {
                    _steamParticlePool.Release(steam.particle);
                    entity.Del<SteamComponent>();
                }
            }
        }
    }
}
