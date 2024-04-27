using Assets.Code.ECS.EntityRef;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.Scripts.Mono.Status
{
    internal class Geo : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EntityReference reference))
            {
                EcsEntity entity = reference.Entity;
                if (entity.Has<DefendComponent>()
                    && !entity.Has<DefendTriggerComponent>()
                    && !entity.Has<DefendingComponent>())
                    entity.Get<DefendTriggerComponent>().collider = other;
            }
        }
    }
}
