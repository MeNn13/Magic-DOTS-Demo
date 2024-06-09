using Code.ECS.Skill;
using Code.ECS.Status.Geo.Defending;
using Code.ECS.Status.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Status.Burning
{
    internal class DefendBurningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DefendingComponent, BurningComponent>
            .Exclude<SkillComponent> _filter;

        private readonly PyroParticlePool _particlePool;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref DefendingComponent health = ref _filter.Get1(i);
                ref BurningComponent burning = ref _filter.Get2(i);

                TryTakeDefensePoint(ref burning, ref health, ref entity);
            }
        }

        private void TryTakeDefensePoint(ref BurningComponent burning, ref DefendingComponent defensePoint,
            ref EcsEntity entity)
        {
            if (burning.Duration <= 0)
            {
                _particlePool.Release(burning.Particle);
                entity.Del<BurningComponent>();

                return;
            }

            burning.Duration -= Time.deltaTime;
            defensePoint.DefensePoints -= burning.Damage * Time.deltaTime;
        }
    }
}
