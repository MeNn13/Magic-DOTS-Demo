using Leopotam.Ecs;

namespace Assets.Code.ECS.EntityRef
{
    internal class InitEntityReferenceSystem : IEcsInitSystem
    {
        private readonly EcsFilter<InitEntityReferenceComponent> _filter;
        public void Init()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref InitEntityReferenceComponent reference = ref _filter.Get1(i);

                reference.reference.Entity = entity;
            }
        }
    }
}
