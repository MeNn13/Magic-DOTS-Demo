using Code.ECS.EntityRef.Mono;
using Code.ECS.Status;
using Code.ECS.Status.Burnable;
using Code.ECS.Status.Burning;
using Code.ECS.Status.Combine.Steam;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.Scripts.Mono.Status
{
    public class Pyro : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EntityReference reference))
            {
                EcsEntity entity = reference.Entity;
                if (entity.Has<BurnableComponent>()
                    && !entity.Has<BurnTriggerComponent>()
                    && !entity.Has<SteamComponent>()
                    && !entity.Has<BurningComponent>())
                    entity.Get<BurnTriggerComponent>().Collider = other;
            }
        }
    }
}