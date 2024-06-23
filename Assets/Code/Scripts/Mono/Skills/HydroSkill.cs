using Code.ECS.EntityRef.Mono;
using Code.ECS.Status.Hydro;
using Code.ECS.Status.Hydro.Wet;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Scripts.Mono.Skills
{
    public class HydroSkill : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EntityReference reference))
            {
                EcsEntity entity = reference.Entity;
                if (entity.Has<WetComponent>())
                    entity.Get<WetTriggerComponent>().Collider = other.GetComponent<Collider>();
            }
        }
    }
}
