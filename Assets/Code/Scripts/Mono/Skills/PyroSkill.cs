using Code.ECS.EntityRef.Mono;
using Code.ECS.Status;
using Code.ECS.Status.Burnable;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Scripts.Mono.Skills
{
    internal class PyroSkill : DamageParticleSkill
    {
        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EntityReference reference))
            {
                base.TakeDamage(reference);
                
                EcsEntity entity = reference.Entity;
                if (entity.Has<BurnableComponent>())
                    entity.Get<BurnTriggerComponent>().Collider = other.GetComponent<Collider>();
            }
        }
    }
}
