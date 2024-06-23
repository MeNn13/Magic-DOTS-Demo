using Code.ECS.DamageTextUI;
using Code.ECS.Health;
using Leopotam.Ecs;

namespace Code.ECS.Status.Combine.Steam
{
    public class HealthSteamSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SteamComponent, HealthComponent>
            .Exclude<SteamDamageTriggerComponent> _filter;

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref SteamComponent steamComponent = ref _filter.Get1(i);
                ref HealthComponent healthComponent = ref _filter.Get2(i);

                healthComponent.health -= steamComponent.Damage;
                entity.Get<DamageTextStartTriggerComponent>().Damage = steamComponent.Damage;
                
                entity.Get<SteamDamageTriggerComponent>();
            }
        }
    }
}
