using Code.ECS.Skill.Mono;
using Code.ECS.Skill.Pyro;
using Code.ECS.Skill.SkillsComponent;
using Leopotam.Ecs;

namespace Code.ECS.Skill.Combine.Vape
{
    public class VapeInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillComponent, HydroComponent, PyroComponent>
            .Exclude<VapeComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);

                entity.Get<VapeComponent>();
                entity.Get<CombineComponent>();
                
                entity.Del<HydroComponent>();
                entity.Del<PyroComponent>();
            }
        }
    }
}
