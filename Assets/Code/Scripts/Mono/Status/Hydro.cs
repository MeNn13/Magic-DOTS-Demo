using Assets.Code.ECS.EntityRef;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.Scripts.Mono.Status
{
    public class Hydro : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EntityReference reference))
            {
                EcsEntity entity = reference.Entity;
                if (entity.Has<WetComponent>() 
                    && !entity.Has<WetTriggerComponent>()
                    && !entity.Has<SteamComponent>()
                    && !entity.Has<SoggyComponent>())
                    entity.Get<WetTriggerComponent>().collider = other;
            }
        }
    }
}
