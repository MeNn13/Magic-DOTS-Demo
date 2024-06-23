using Leopotam.Ecs;

namespace Code.ECS.EntityRef
{
    internal class InitEntityReferenceSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitEntityReferenceComponent> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref InitEntityReferenceComponent init = ref _filter.Get1(i);

                init.reference.Entity = entity;
                entity.Get<EntityReferenceComponent>().Reference = init.reference;
            }
        }
    }
}
