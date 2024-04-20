using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.ECS.Skills.Fire
{
    internal class BurnSystem : IEcsRunSystem
    {
        private EcsFilter<BurnableComponent, BurnTriggerComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref BurnableComponent burnable = ref _filter.Get1(i);
                ref BurnTriggerComponent burnObject = ref _filter.Get2(i);

                ref BurningComponent burning = ref entity.Get<BurningComponent>();
                burning.burningObject = burnObject.collider.gameObject.transform;
                burning.mesh = burnObject.collider.GetComponent<MeshFilter>().mesh;
                burning.burningTime = 3f;

                entity.Del<BurnTriggerComponent>();
            }
        }
    }
}
