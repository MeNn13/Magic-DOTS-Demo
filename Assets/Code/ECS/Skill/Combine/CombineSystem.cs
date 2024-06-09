using Code.ECS.Skill.Combine.Vape;
using Code.ECS.Skill.Mono;
using Code.ECS.Skill.Pyro;
using Code.ECS.Skill.SkillsComponent;
using Leopotam.Ecs;

namespace Code.ECS.Skill.Combine
{
    public class CombineSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SkillComponent, CombineComponent> _filter;
        private readonly SkillsCircle _skillsCircle;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);

                SkillHasMoreOneCombine<PyroComponent>(ref entity);
                SkillHasMoreOneCombine<HydroComponent>(ref entity);
                SkillHasMoreOneCombine<VentoComponent>(ref entity);
                SkillHasMoreOneCombine<GeoComponent>(ref entity);
            }
        }
        private void SkillHasMoreOneCombine<T>(ref EcsEntity entity)
            where T : struct
        {
            if (entity.Has<T>())
            {
                entity.Del<T>();
                entity.Del<CombineComponent>();
                entity.Del<VapeComponent>();
                
                _skillsCircle.SetAllCircles(false);
            }
        }
    }
}
