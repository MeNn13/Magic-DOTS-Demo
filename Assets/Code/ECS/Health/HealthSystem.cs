using System;
using Code.ECS.DamageTextUI;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ECS.Health
{
    internal class HealthSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<HealthComponent> _filter;

        private Quaternion _rotationToCamera;

        public void Init()
        {
            _rotationToCamera = Camera.main.transform.rotation;

            foreach (var i in _filter)
            {
                ref HealthComponent component = ref _filter.Get1(i);
                component.health = component.maxHealth;
                component.previousHealth = component.health;
            }
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(i);
                ref HealthComponent health = ref _filter.Get1(i);

                RotateUIToCamera(health);

                if (Math.Abs(health.previousHealth - health.health) > 1)
                {
                    health.ui.UpdateHealth(health.health / health.maxHealth);

                    health.previousHealth = health.health;
                }
            }
        }

        private void RotateUIToCamera(HealthComponent health)
        {
            if (health.ui.transform.rotation != _rotationToCamera)
                health.ui.transform.rotation = _rotationToCamera;
        }
    }
}
