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
            }
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref HealthComponent health = ref _filter.Get1(i);

                RotateUIToCamera(health);

                health.ui.UpdateHealth(health.health / health.maxHealth);
            }
        }

        private void RotateUIToCamera(HealthComponent health)
        {
            if (health.ui.transform.rotation != _rotationToCamera)
                health.ui.transform.rotation = _rotationToCamera;
        }
    }
}
