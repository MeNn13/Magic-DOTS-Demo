﻿using Assets.Code.ECS.EntityRef;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Code.Scripts.Mono
{
    internal class Flamethrower : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out EntityReference reference))
            {
                EcsEntity entity = reference.Entity;
                if (entity.Has<BurnableComponent>())
                    entity.Get<BurnTriggerComponent>().collider = other.GetComponent<Collider>();
            }
        }
    }
}
