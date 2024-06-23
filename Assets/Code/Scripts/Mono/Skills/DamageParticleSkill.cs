using Code.ECS.DamageTextUI;
using Code.ECS.EntityRef.Mono;
using Code.ECS.Health;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.Scripts.Mono.Skills
{
    public class DamageParticleSkill : MonoBehaviour
    {
        [SerializeField] private int damage;
        
        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EntityReference reference))
                TakeDamage(reference);
        }

        protected virtual void TakeDamage(EntityReference reference)
        {
            EcsEntity entity = reference.Entity;
            if (entity.Has<HealthComponent>())
            {
                entity.Get<HealthComponent>().health -= damage;
                entity.Get<DamageTextStartTriggerComponent>().Damage = damage;
            }
        }
    }
}
